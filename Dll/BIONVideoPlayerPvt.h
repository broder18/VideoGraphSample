#pragma once

#include "LibGraph\LibGraph.h"
#include <dshow.h>
#include <atlbase.h>
//#include <bdaiface.h>
#include <initguid.h>
#include <dvdmedia.h>
#include <bdaiface.h>

#include "Interfaces\IPMTPvtDataSettings.h"
#include "Interfaces\ITSFileSource.h"


#define BUFANDSIZE(x)       x, (sizeof(x) / sizeof((x)[0]))
#define EXPORT              __declspec(dllexport) __stdcall

#include "IOStructs.h"

class GRAPH_CONTROL : private CGraph
{

    /* link between the actual renderer window and its parent window */
    map<HWND, CComPtr<IVideoWindow>> RendererMap;
    CComPtr<IPMTPvtDataSettings2> pIPMTPvtDataSettings2;
    CComPtr<IFileSourceFilter> pTSFileSource;
    CComPtr<IMediaSeeking> pIMediaSeeking;

    ULONG UsedPids[MAX_CHANNELS]{};

    
    void AddDemux(CHANNELS *pChannels);
    void AddDemuxPinVideoStream(WORD pid, int idx);
    void AddDemuxPMTPin(CHANNELS* pChannels);
    void AddVideoDecoder(CHANNELS* pChannels);
    void AddVideoRenderer(CHANNELS* pChannels);
    static void SetupRenderer(IVideoWindow* pVideoWindow, HWND hContainerWnd);
    void AddPMTPvtData(CHANNELS* pChannels);   

public:
    GRAPH_CONTROL()
    {
        for (int i = 0; i < MAX_CHANNELS; i++) UsedPids[i] = 0xffff;
    }

    ~GRAPH_CONTROL();

    void AddTSFileSource(char* psz_file_name);
    void BuildGraph(BVP_SETTINGS* pSettings, char* psz_file_name);
    void PlaceRenderer(HWND h_container_wnd) const;
    void GetPositions(DWORD* percent);
    void SetPosition(DWORD percent);
    void SetTelemetryAlpha(int alpha) const;
    void SetTelemetryPosition(int x, int y) const;
    void SetTelemetryColors(COLORREF txt_color, COLORREF bkg_color) const;
    void EnableTelemetry(int enable) const;
    void SetPause();
    void SetStart();
    void SetStop();
    void MapUnmap(int ch, bool map);
};

typedef GRAPH_CONTROL *PGRAPH_CONTROL;

void __declspec(noreturn) THROW(const char *p_err_msg);
