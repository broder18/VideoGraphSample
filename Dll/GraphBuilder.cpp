#include "GraphSamplePvt.h"
#include <mmreg.h>
#include <Shlwapi.h>
#include "GraphBuilder.h"
#include "MediaTypes.h"
#include "Filters.h"

#define GETIF(if, filt)             CComPtr<if> p##if = QI<if>(filt, IID_ ## if)
#define GETOUTPINIF(if, filt, pin)  CComPtr<if> p##if = PinQI<if>(filt, pin, PINDIR_OUTPUT, IID_ ## if)
#define GETCONCATLPCTSTR(str1, str2)  

//------------------------------------------------------------------------
// This stuff is not in strmiids.lib for Windows SDK v7.1
//
EXTERN_C const IID IID_IMPEG2PIDMap = __uuidof(IMPEG2PIDMap);  // NOLINT(clang-diagnostic-language-extension-token)

//------------------------------------------------------------------------
// Throw an exception
//
void __declspec(noreturn) THROW(const char *pErrMsg)
{
    throw Exception(E_FAIL, pErrMsg);
}

//------------------------------------------------------------------------
// Check a HRESULT and throw an exception if necessary
//
void CHECK_HR(HRESULT Hr, const char *pErrMsg)
{
    if(FAILED(Hr)) throw Exception(Hr, pErrMsg);
}


//------------------------------------------------------------------------
// MPEG-2 demultiplexer
//
void GRAPH_CONTROL::AddTSPushSource(LPCOLESTR *pszFileName)
{
    AddFilter(CLSID_TSPUSHFILESOURCE, TsPushSourceName);
    CComPtr<IFileSourceFilter> pTSPushFileSource = QI<IFileSourceFilter>(TsPushSourceName, IID_IFileSourceFilter);
    CMediaType pmt;
    PrepareMediaType(&pmt);
    CHECK_HR(pTSPushFileSource->Load(*pszFileName, &pmt), "TSPushFileSource Load() function failed");
}


//------------------------------------------------------------------------
// MPEG-2 demultiplexer
//


void GRAPH_CONTROL::AddDemuxRefact(PIDS *Pids)
{
    AddFilter(CLSID_MPEG2Demultiplexer, DemuxName);

    /* demux input must (?) be connected before IMPEG2PIDMap interface is available */
    Connect(TsPushSourceName, DemuxName);
    if(CheckPidNull(Pids->pidV0))AddDemuxPinVideoStream(Pids->pidV0, 0);
    if(CheckPidNull(Pids->pidV1))AddDemuxPinVideoStream(Pids->pidV1, 1);
    if(CheckPidNull(Pids->pidV2))AddDemuxPinVideoStream(Pids->pidV2, 2);
    if(CheckPidNull(Pids->pidV3))AddDemuxPinVideoStream(Pids->pidV3, 3);
    if(CheckPidNull(Pids->pidV4))AddDemuxPinVideoStream(Pids->pidV4, 4);
    AddDemuxPMTPin();
}

BOOL GRAPH_CONTROL::CheckPidNull(WORD pid)
{
    if (pid == 0) return FALSE;
    return TRUE;
}


void GRAPH_CONTROL::AddDemuxPinVideoStream(WORD Pid, int Idx)
{
    CMediaType MtV;
    PrepareMediaType(&MtV);
    GETIF(IMpeg2Demultiplexer, DemuxName);

    char PinNameA[32];//Âûםוסעט ג מעהוכüםûי לועמה
    wchar_t PinNameW[32];
    sprintf_s(PinNameA, "V%d", Idx);
    swprintf_s(PinNameW, L"V%d", Idx);

    CComPtr<IPin> pDemuxOutPinV; 
    CHECK_HR(pIMpeg2Demultiplexer->CreateOutputPin(&MtV, PinNameW, &pDemuxOutPinV), "pIMpeg2Demultiplexer->CreateOutputPin('V') failed");

    RefreshPins(DemuxName);

    GETOUTPINIF(IMPEG2PIDMap, DemuxName, PinNameA);
    ULONG PidToMap = Pid;
    CHECK_HR(pIMPEG2PIDMap->MapPID(1, &PidToMap, MEDIA_ELEMENTARY_STREAM), "IMPEG2PIDMap::MapPID() failed");
}

void GRAPH_CONTROL::AddDemuxPMTPin()
{
    CMediaType MtTS;
    PrepareMediaTypeTS(&MtTS);
    GETIF(IMpeg2Demultiplexer, DemuxName);

    char PinNameA[32];
    wchar_t PinNameW[32];
    sprintf_s(PinNameA, "PMT");
    swprintf_s(PinNameW, L"PMT");
    CComPtr<IPin> pDemuxOutPinV;
    CHECK_HR(pIMpeg2Demultiplexer->CreateOutputPin(&MtTS, PinNameW, &pDemuxOutPinV), "pIMpeg2Demultiplexer->CreateOutputPin('PMT') failed");

    RefreshPins(DemuxName);

    GETOUTPINIF(IMPEG2PIDMap, DemuxName, PinNameA);
    ULONG PMTPids[5] = { 0x44, 0x45, 0x46, 0x47, 0x48 };
    CHECK_HR(pIMPEG2PIDMap->MapPID(ARRAYSIZE(PMTPids), PMTPids, MEDIA_TRANSPORT_PACKET), "IMPEG2PIDMap::MapPID() failed");

}


//------------------------------------------------------------------------
// Add video decoder
//

void GRAPH_CONTROL::AddFFDSHOWDecoder(LPCTSTR VideoDecoderName, LPCTSTR outputId)
{
    AddFilter(CLSID_ffdshowVideoDecoder, VideoDecoderName);
    Connect(DemuxName, VideoDecoderName, outputId);
}

void GRAPH_CONTROL::AddLAVDecoder(LPCTSTR VideoDecoderName, LPCTSTR outputId)
{
    AddFilter(CLSID_LAVVideo, VideoDecoderName);
    Connect(DemuxName, VideoDecoderName, outputId);
}

void GRAPH_CONTROL::AddVideoDecoderRefact(PIDS *pids)
{
#if 0
    /* LAV Video Decoder works pretty bad with the latest NVidia updates making video jerky */

    AddLAVDecoder(VideoDecoderName0, "V0");
    AddLAVDecoder(VideoDecoderName1, "V1");
    AddLAVDecoder(VideoDecoderName2, "V2");
    AddLAVDecoder(VideoDecoderName3, "V3");
    AddLAVDecoder(VideoDecoderName4, "V4");

#else

    if(CheckPidNull(pids->pidV0))AddFFDSHOWDecoder(VideoDecoderName0, "V0");
    if(CheckPidNull(pids->pidV1))AddFFDSHOWDecoder(VideoDecoderName1, "V1");
    if(CheckPidNull(pids->pidV2))AddFFDSHOWDecoder(VideoDecoderName2, "V2");
    if(CheckPidNull(pids->pidV3))AddFFDSHOWDecoder(VideoDecoderName3, "V3");
    if(CheckPidNull(pids->pidV4))AddFFDSHOWDecoder(VideoDecoderName4, "V4");

#endif
}

//------------------------------------------------------------------------
// Add UDP Local Source filter
//
/*void GRAPH_CONTROL::AddUDPLocalSource()
{
    AddFilter(CLSID_UDPLOCALSOURCE, UDPLocalSourceName);

    CComPtr<IUDPLocalSourceCtrl> pIUDPLocalSourceCtrl = QI<IUDPLocalSourceCtrl>(UDPLocalSourceName, IID_IUDPLocalSourceCtrl);
    LocalPort = pIUDPLocalSourceCtrl->GetBoundPort();
}*/

//------------------------------------------------------------------------
// Add 3DDCT RTP Source filter, obtain its interfaces, and make the necessary adjustments
// Note: UDPLocalSource must be already added to the graph
//
/*void GRAPH_CONTROL::SetRTPSource(INPUT_NETWORK* pInNet)
{
    RTPSOURCE_SETTINGS Settings;
    Settings.IP = pInNet->MulticastIP;
    Settings.Port = pInNet->MulticastPort;
    Settings.LocalPort = LocalPort;

    GETIF(IRTPSourceSettings, RTPSourceName);
    CHECK_HR(pIRTPSourceSettings->SetSettings(&Settings), "pRTPSourceSettings->SetSettings() failed");
}

void GRAPH_CONTROL::AddRTPSource(INPUT_NETWORK *pInNet)
{
    AddFilter(CLSID_RTPSOURCE, RTPSourceName);
    SetRTPSource(pInNet);
}*/


//---------------------------------------------------------------------------
//Add Renderer 
void GRAPH_CONTROL::AddVideoRendererRefact(HCONTAINER_WND* hWindows)
{
    if(CheckHWNDNull(hWindows->hContainerWnd0))ConnectRenderer(VideoDecoderName0, VideoRendererName0, hWindows->hContainerWnd0);
    if(CheckHWNDNull(hWindows->hContainerWnd1))ConnectRenderer(VideoDecoderName1, VideoRendererName1, hWindows->hContainerWnd1);
    if(CheckHWNDNull(hWindows->hContainerWnd2))ConnectRenderer(VideoDecoderName2, VideoRendererName2, hWindows->hContainerWnd2);
    if(CheckHWNDNull(hWindows->hContainerWnd3))ConnectRenderer(VideoDecoderName3, VideoRendererName3, hWindows->hContainerWnd3);
    if(CheckHWNDNull(hWindows->hContainerWnd4))ConnectRenderer(VideoDecoderName4, VideoRendererName4, hWindows->hContainerWnd4);
}

BOOL GRAPH_CONTROL::CheckHWNDNull(HWND hwnd)
{
    if (hwnd == 0) return FALSE;
    return TRUE;
}

void GRAPH_CONTROL::ConnectRenderer(LPCTSTR VideoDecoderName, LPCTSTR VideoRendererName, HWND hContainerWnd)
{
    AddFilter(CLSID_VideoMixingRenderer9, VideoRendererName);
    Connect(VideoDecoderName, VideoRendererName);
    RendererMap[hContainerWnd] = QI<IVideoWindow>(VideoRendererName, IID_IVideoWindow);
    SetupRendererRefact(hContainerWnd);
    PlaceRendererRefact(hContainerWnd);
}

void GRAPH_CONTROL::SetupRendererRefact(HWND hContainerWnd) const
{
    
    CHECK_HR(RendererMap.find(hContainerWnd)->second->put_Owner((OAHWND)hContainerWnd), "IVideoWindow::put_Owner() failed");

    long Style, ExStyle;
    CHECK_HR(RendererMap.find(hContainerWnd)->second->get_WindowStyle(&Style), "IVideoWindow::get_WindowStyle() failed");
    CHECK_HR(RendererMap.find(hContainerWnd)->second->get_WindowStyleEx(&ExStyle), "IVideoWindow::get_WindowStyleEx() failed");

    Style &= ~(WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX);
    ExStyle &= ~(WS_EX_WINDOWEDGE);
    ExStyle |= WS_EX_TRANSPARENT;
    CHECK_HR(RendererMap.find(hContainerWnd)->second->put_WindowStyle(Style), "IVideoWindow::put_WindowStyle() failed");
    CHECK_HR(RendererMap.find(hContainerWnd)->second->put_WindowStyleEx(ExStyle), "IVideoWindow::put_WindowStyleEx() failed");
}

void GRAPH_CONTROL::PlaceRendererRefact(HWND hContainerWnd) const
{
    if (RendererMap.find(hContainerWnd)->first != hContainerWnd) THROW("PlaceRenderer(): invalid window handle");

    RECT Rect;
    if (!GetClientRect(hContainerWnd, &Rect)) THROW("GetClientWindow() failed");

    CHECK_HR(RendererMap.find(hContainerWnd)->second->put_Left(1), "IVideoWindow::put_Left() failed");
    CHECK_HR(RendererMap.find(hContainerWnd)->second->put_Top(1), "IVideoWindow::put_Top() failed");
    CHECK_HR(RendererMap.find(hContainerWnd)->second->put_Width(Rect.right - Rect.left - 2), "IVideoWindow::put_Width() failed");
    CHECK_HR(RendererMap.find(hContainerWnd)->second->put_Height(Rect.bottom - Rect.top - 2), "IVideoWindow::put_Height() failed");
}
//------------------------------------------------------------------------
// PMT
//


void GRAPH_CONTROL::AddPMTPvtData(PIDS *pids)
 {
    AddFilter(CLSID_PMTPvtData, PMTPvtDataName);
    pIPMTPvtDataSettings = QI<IPMTPvtDataSettings>(PMTPvtDataName, IID_IPMTPvtDataSettings);
    Connect(DemuxName, PMTPvtDataName, "PMT");
    if(CheckPidNull(pids->pidV0))ConnectPMTPvtData(VideoRendererName0, 0);
    if(CheckPidNull(pids->pidV1))ConnectPMTPvtData(VideoRendererName1, 1);
    if(CheckPidNull(pids->pidV2))ConnectPMTPvtData(VideoRendererName2, 2);
    if(CheckPidNull(pids->pidV3))ConnectPMTPvtData(VideoRendererName3, 3);
    if(CheckPidNull(pids->pidV4))ConnectPMTPvtData(VideoRendererName4, 4);
}

void GRAPH_CONTROL::ConnectPMTPvtData(LPCTSTR VideoRendererName, int PMT_ID)
{
    GETIF(IVMRMixerBitmap9, VideoRendererName);
    CHECK_HR(pIPMTPvtDataSettings->SetRenderer(PMT_ID, pIVMRMixerBitmap9), "IPMTPvtDataSettings::SetRenderer() failed");
}

void GRAPH_CONTROL::SetAlphaPMT(int alpha)
{
    CHECK_HR(pIPMTPvtDataSettings->SetAlpha(alpha), "IPMTPvtDataSettings::SetAlpha() failed");
}

void GRAPH_CONTROL::SetPositionPMT(int x, int y)
{
    CHECK_HR(pIPMTPvtDataSettings->SetPosition(x, y), "IPMTPvtDataSettings::SetPosition() failed");
}

void GRAPH_CONTROL::SetPMTParams(TEXT_PARAMS *pPMT)
{
    SetAlphaPMT(pPMT->alpha);
    SetPositionPMT(pPMT->x, pPMT->y);
}


//------------------------------------------------------------------------
// Statistics
//
void GRAPH_CONTROL::GetStatistics(PIDSTATISTICS *pStat)
{
    GETIF(IUDPStatistics, UDPLocalSourceName);
    CHECK_HR(pIUDPStatistics->GetStatistics(pStat), "IUDPStatistics::GetStatistics() failed");
}

void GRAPH_CONTROL::ResetStatistics()
{
    GETIF(IUDPStatistics, UDPLocalSourceName);
    CHECK_HR(pIUDPStatistics->ResetStatistics(), "IUDPStatistics::ResetStatistics() failed");
}

/*void GRAPH_CONTROL::InitSlider(HWND hwnd)
{
    hScroll = GetDlgItem(hwnd, IDC_SLIDER1);
    EnableWindow(hScroll, FALSE);
    SendMessage(hScroll, TBM_SETRANGE, TRUE, MAKELONG(0, 100));
}*/

void GRAPH_CONTROL::BuildGraphRefact(GS_SETTINGSRefact *pSettings)
{
    USES_CONVERSION;
    char* pFileChar = pSettings->fileName;
    LPCOLESTR pFileCOLE = A2COLE(pFileChar);
    AddTSPushSource(&pFileCOLE);
    AddDemuxRefact(&pSettings->V_Pids);
    AddVideoDecoderRefact(&pSettings->V_Pids);
    AddVideoRendererRefact(&pSettings->hWnd);
    AddPMTPvtData(&pSettings->V_Pids);
    Start();
}


