#ifndef _GRAPHSAMPLE_H
#define _GRAPHSAMPLE_H

#include "IOStructs.h"

extern "C" BOOL     __stdcall gsInitialize();
extern "C" void     __stdcall gsUninitialize();
extern "C" void     __stdcall gsClose();
extern "C" BOOL		__stdcall gsOpenRefact(GS_SETTINGSRefact *pSettings);
extern "C" LPCTSTR  __stdcall gsGetLastError();
extern "C" BOOL     __stdcall gsSetInputNetwork(INPUT_NETWORK *pInNet);
extern "C" BOOL		__stdcall gsSetPMTParams(TEXT_PARAMS * pPMT);
extern "C" BOOL     __stdcall gsResizeRenderer(HWND hContainerWnd);


#endif
