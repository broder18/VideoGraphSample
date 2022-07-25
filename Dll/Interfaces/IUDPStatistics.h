#pragma once

// {12ED78F5-E82E-4c24-82AE-E1EBB0875FEA}
DEFINE_GUID(IID_IUDPStatistics, 0x12ed78f5, 0xe82e, 0x4c24, 0x82, 0xae, 0xe1, 0xeb, 0xb0, 0x87, 0x5f, 0xea);

#define TOTAL_PIDS  0x2000

typedef struct _PIDSTATISTICS
{
    UINT64 Pids[TOTAL_PIDS];
    UINT64 BadPackets;
} PIDSTATISTICS;

/* we obtain TS statistics through this interface */
DECLARE_INTERFACE_(IUDPStatistics, IUnknown)
{
    STDMETHOD(GetStatistics) (THIS_ PIDSTATISTICS *pPidStatistics) PURE;
    STDMETHOD(ResetStatistics)  (THIS_) PURE;
};
