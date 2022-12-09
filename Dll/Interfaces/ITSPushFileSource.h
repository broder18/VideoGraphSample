#pragma once

#include <d3d9.h>
#include <vmr9.h>
#include <strmif.h>

//{93B72F4C-4C2C-40c1-820B-BD82B2A491BC}
DEFINE_GUID(IID_ITSPushFileSource, 0x93b72f4c, 0x4c2c, 0x40c1, 0x82, 0x0b, 0xbd, 0x82, 0xb2, 0xa4, 0x91, 0xbc);
DEFINE_GUID(CLSID_TSPUSHFILESOURCE,  0x4dc17023, 0x2d29, 0x4069, 0xa8, 0x1b, 0xe3, 0xd1, 0xf2, 0xa3, 0x1c, 0xba);

DECLARE_INTERFACE_(ITSPushFileSource, IUnknown)
{
    STDMETHOD(GetFilePosition) (THIS_ DWORD* percent) PURE;
    STDMETHOD(SetFilePosition) (THIS_ DWORD percent) PURE;
};