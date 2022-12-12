#ifndef _GRAPHSAMPLE_H
#define _GRAPHSAMPLE_H

#include <Windows.h>
#include "IOStructs.h"

extern "C" BOOL     __stdcall bvpInitialize();
extern "C" void     __stdcall bvpUninitialize();
extern "C" void     __stdcall bvpClose();
extern "C" BOOL		__stdcall bvpOpen(BVP_SETTINGS *pSettings);
extern "C" LPCTSTR  __stdcall bvpGetLastError();
extern "C" BOOL     __stdcall bvpResizeRenderer(HWND hContainerWnd);
extern "C" void		__stdcall bvpSetTelemetryPosition(int x, int y); //
extern "C" BOOL		__stdcall bvpGetPositionTrackBar(DWORD* percent); 
extern "C" BOOL		__stdcall bvpSetPositionTrackBar(DWORD percent);
extern "C" void		__stdcall bvpMapUnmapChannel(int ch, BOOL map); //
extern "C" void		__stdcall bvpSetTelemetryColors(COLORREF TxtColor, COLORREF BkgColor); //
extern "C" void		__stdcall bvpSetTelemetryAlpha(int alpha);
extern "C" void		__stdcall bvpEnableTelemetry(int Enable); //
extern "C" BOOL		__stdcall bvpSetStart();
extern "C" BOOL		__stdcall bvpSetPause();
extern "C" BOOL		__stdcall bvpSetStop();


#endif
