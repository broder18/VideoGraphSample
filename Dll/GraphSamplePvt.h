#pragma once

#include "LibGraph\LibGraph.h"

#include "Defines.h"
#include <tchar.h>
#include <dshow.h>
#include <atlbase.h>
#include <initguid.h>
#include <dvdmedia.h>
#include <bdaiface.h>

#include "Interfaces\UDPLocalSource.h"
#include "Interfaces\IUDPLocalSourceCtrl.h"
#include "Interfaces\IUDPStatistics.h"
#include "Interfaces\RTPSource.h"
#include "Interfaces\IPMTPvtDataSettings.h"
#include "Interfaces\TSPushFileSource.h"

#define BUFANDSIZE(x)       x, (sizeof(x) / sizeof((x)[0]))
#define EXPORT              __declspec(dllexport) __stdcall

#include "IOStructs.h"

class GRAPH_CONTROL : private CGraph
{
    /* local port used to connect RTPSource and UDPLocalSource */
    WORD LocalPort;

    /* link between the actual renderer window and its parent window */
    map<HWND, CComPtr<IVideoWindow>> RendererMap;
    CComPtr<IPMTPvtDataSettings> pIPMTPvtDataSettings;
    //CComPtr<TSPUSHFILESOURCE> pTSPushFileSource;
    


    void AddTSPushSource(LPCOLESTR* pszFileName);
    //void AddUDPLocalSource();
    //void AddRTPSource(INPUT_NETWORK *pInNet);
    void AddDemuxRefact(PIDS *Pids);
    void AddDemuxPinVideoStream(WORD Pid, int Idx);
    void AddVideoDecoderRefact();
    void AddFFDSHOWDecoder(LPCTSTR VideoDecoderName, LPCTSTR outputId);
    void AddLAVDecoder(LPCTSTR VideoDecoderName, LPCTSTR outputId);
    void AddVideoRendererRefact(HCONTAINER_WND *hWindows);
    void ConnectRenderer(LPCTSTR VideoDecoderName, LPCTSTR VideoRendererName, HWND hContainerWnd);
    void SetupRendererRefact(HWND hContainerWnd) const;
    void AddDemuxPMTPin();
    void AddPMTPvtData();
    void ConnectPMTPvtData(LPCTSTR VideoRendererName, int PMTRendererID);
    void SetAlphaPMT(int alpha);
    void SetPositionPMT(int x, int y);

public:
    GRAPH_CONTROL() : LocalPort{ 0 } 
    {
    }

    //void SetRTPSource(INPUT_NETWORK* pInNet);
    void BuildGraphRefact(GS_SETTINGSRefact *pSettings);
    void PlaceRendererRefact(HWND hContainerWnd) const;
    void GetStatistics(PIDSTATISTICS *pStat);
    void ResetStatistics();
    void SetPMTParams(TEXT_PARAMS* pPMT);
};

typedef GRAPH_CONTROL *PGRAPH_CONTROL;

void __declspec(noreturn) THROW(const char *pErrMsg);
