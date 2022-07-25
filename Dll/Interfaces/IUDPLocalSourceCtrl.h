#pragma once

// {D55F98AA-B1E9-44C5-9C96-BA40F6090B7B}
DEFINE_GUID(IID_IUDPLocalSourceCtrl, 0xd55f98aa, 0xb1e9, 0x44c5, 0x9c, 0x96, 0xba, 0x40, 0xf6, 0x9, 0xb, 0x7b);

DECLARE_INTERFACE_(IUDPLocalSourceCtrl, IUnknown)
{
    STDMETHOD_(WORD, GetBoundPort) (THIS_) PURE;
    STDMETHOD(FlushOutput) (THIS_) PURE;
};
