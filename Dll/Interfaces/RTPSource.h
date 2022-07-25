#pragma once

// {61C19683-2517-49e4-B2B9-5983967BFE72}
DEFINE_GUID(CLSID_RTPSOURCE, 0x61c19683, 0x2517, 0x49e4, 0xb2, 0xb9, 0x59, 0x83, 0x96, 0x7b, 0xfe, 0x72);

// {8241F4E8-51D6-4f40-B8C6-9E2207D82536}
DEFINE_GUID(IID_IRTPSourceSettings, 0x8241f4e8, 0x51d6, 0x4f40, 0xb8, 0xc6, 0x9e, 0x22, 0x7, 0xd8, 0x25, 0x36);

// {D3EC3964-B137-4d47-8F10-993A7F78C7AD}
DEFINE_GUID(IID_IRTPSourceStatistics, 0xd3ec3964, 0xb137, 0x4d47, 0x8f, 0x10, 0x99, 0x3a, 0x7f, 0x78, 0xc7, 0xad);

typedef struct _RTPSOURCE_SETTINGS
{
    DWORD IP;
    WORD Port;
    WORD LocalPort;
} RTPSOURCE_SETTINGS, *PRTPSOURCE_SETTINGS;

DECLARE_INTERFACE_(IRTPSourceSettings, IUnknown)
{
    STDMETHOD(GetSettings)          (THIS_ PRTPSOURCE_SETTINGS pSettings) PURE;
    STDMETHOD(SetSettings)          (THIS_ PRTPSOURCE_SETTINGS pSettings) PURE;
};

typedef struct _PACKET_STATISTICS
{
    DWORD TotalUDPPackets;
    DWORD TotalRTPPackets;
    DWORD TotalFECHPackets;
    DWORD TotalFECVPackets;
    DWORD Sorts;
    DWORD Duplicates;
    DWORD LostPackets;
    DWORD Corrections;
} PACKET_STATISTICS, *PPACKET_STATISTICS;

/* we obtain RTP Source statistics through this interface */
DECLARE_INTERFACE_(IRTPSourceStatistics, IUnknown)
{
    STDMETHOD(GetStatistics) (THIS_ PPACKET_STATISTICS pPacketStatistics) PURE;
    STDMETHOD(ResetStatistics)  (THIS_) PURE;
};
