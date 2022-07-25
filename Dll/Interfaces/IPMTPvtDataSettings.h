#pragma once

#include <d3d9.h>
#include <vmr9.h>

// {0CD68CB5-6163-4D80-8AAF-E698F95CE2B9}
DEFINE_GUID(CLSID_PMTPvtData, 0xcd68cb5, 0x6163, 0x4d80, 0x8a, 0xaf, 0xe6, 0x98, 0xf9, 0x5c, 0xe2, 0xb9);

// {A3552889-8BF2-49D2-B23B-A538E1FB4F39}
DEFINE_GUID(IID_IPMTPvtDataSettings, 0xa3552889, 0x8bf2, 0x49d2, 0xb2, 0x3b, 0xa5, 0x38, 0xe1, 0xfb, 0x4f, 0x39);

DECLARE_INTERFACE_(IPMTPvtDataSettings, IUnknown)
{
    STDMETHOD(SetRenderer)  (THIS_ int Idx, IVMRMixerBitmap9 *pIVMRMixerBitmap9) PURE;
    STDMETHOD(SetAlpha)     (THIS_ int Alpha100) PURE;
    STDMETHOD(SetPosition)  (THIS_ int X, int Y) PURE;
};
