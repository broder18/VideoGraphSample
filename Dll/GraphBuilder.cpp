#include "BIONVideoPlayerPvt.h"
#include <mmreg.h>
//#include <Shlwapi.h>
#include <bdaiface.h>
#include "GraphBuilder.h"
#include "MediaTypes.h"
#include "Filters.h"
#include "CAWString.h"

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
void __declspec(noreturn) THROW(const char *p_err_msg)
{
    throw Exception(E_FAIL, p_err_msg);
}

//------------------------------------------------------------------------
// Check a HRESULT and throw an exception if necessary
//
void CHECK_HR(const HRESULT hr, const char *p_err_msg)
{
    if(FAILED(hr)) throw Exception(hr, p_err_msg);
}


//------------------------------------------------------------------------
// MPEG-2 demultiplexer
//

void GRAPH_CONTROL::AddTSFileSource(LPCOLESTR *psz_file_name)
{
    AddFilter(CLSID_TSFileSource, TSFileSourceName);
    pTSPushFileSource = QI<IFileSourceFilter>(TSFileSourceName, IID_IFileSourceFilter);
    CMediaType pmt;
    PrepareMediaType(&pmt);
    CHECK_HR(pTSPushFileSource->Load(*psz_file_name, &pmt), "TSFileSource Load() function failed");
}

//------------------------------------------------------------------------
// MPEG-2 demultiplexer
//


void GRAPH_CONTROL::AddDemux(CHANNELS *p_Channels)
{
    AddFilter(CLSID_MPEG2Demultiplexer, DemuxName);

    /* demux input must (?) be connected before IMPEG2PIDMap interface is available */
    Connect(TSFileSourceName, DemuxName);
    for(int i = 0; i < p_Channels->NumVideoPids; i++)
    {
        AddDemuxPinVideoStream(static_cast<WORD>(p_Channels->pids[i]), i);
    }
    AddDemuxPMTPin(p_Channels);
}


void GRAPH_CONTROL::AddDemuxPinVideoStream(const WORD pid, const int idx)
{
    CMediaType MtV;
    PrepareMediaType(&MtV);
    GETIF(IMpeg2Demultiplexer, DemuxName);

    CAWString pinName("V", idx);

    CComPtr<IPin> pDemuxOutPinV; 
    CHECK_HR(pIMpeg2Demultiplexer->CreateOutputPin(&MtV, pinName, &pDemuxOutPinV), "pIMpeg2Demultiplexer->CreateOutputPin('V') failed");

    RefreshPins(DemuxName);

    GETOUTPINIF(IMPEG2PIDMap, DemuxName, pinName);
    ULONG PidToMap = pid;
    CHECK_HR(pIMPEG2PIDMap->MapPID(1, &PidToMap, MEDIA_ELEMENTARY_STREAM), "IMPEG2PIDMap::MapPID() failed");
}

void GRAPH_CONTROL::AddDemuxPMTPin(CHANNELS* pChannels)
{
    CMediaType MtTS;
    PrepareMediaTypeTS(&MtTS);
    GETIF(IMpeg2Demultiplexer, DemuxName);

    CAWString PinName("PMT");
    
    CComPtr<IPin> pDemuxOutPinV;
    CHECK_HR(pIMpeg2Demultiplexer->CreateOutputPin(&MtTS, PinName, &pDemuxOutPinV), "pIMpeg2Demultiplexer->CreateOutputPin('PMT') failed");

    RefreshPins(DemuxName);
	
    ULONG PMTPids[MAX_CHANNELS];
    int actualPMTs = 0;
	for(int i = 0; i < pChannels->NumPMTs; i++)
	{
        const ULONG CurPMT = pChannels->pmts[i];
        if (CurPMT < 0x2000) PMTPids[actualPMTs++] = pChannels->pmts[i];
	}

    if (actualPMTs == 0) throw Exception(E_FAIL, "No PMT PIDs");

    GETOUTPINIF(IMPEG2PIDMap, DemuxName, PinName);
    CHECK_HR(pIMPEG2PIDMap->MapPID(actualPMTs, PMTPids, MEDIA_TRANSPORT_PACKET), "IMPEG2PIDMap::MapPID() failed");
}


//------------------------------------------------------------------------
// Add video decoder
//

#if 0
#define DECCLSID        CLSID_LAVVideo
#else
#define DECCLSID        CLSID_ffdshowVideoDecoder
#endif

void GRAPH_CONTROL::AddVideoDecoder(CHANNELS* p_channels)
{
	for(int i = 0; i < p_channels->NumVideoPids; i++)
	{
        CAWString Decoder(VideoDecoderName, i);
        CAWString Pin("V", i);

        AddFilter(DECCLSID, Decoder);
        Connect(DemuxName, Decoder, Pin);
	}
}

//---------------------------------------------------------------------------
//Add Renderer 
void GRAPH_CONTROL::AddVideoRenderer(CHANNELS* p_Channels)
{
    for(int i = 0; i < p_Channels->NumVideoPids; i++)
    {
        CAWString Decoder(VideoDecoderName, i);
        CAWString Renderer(VideoRendererName, i);

        AddFilter(CLSID_VideoMixingRenderer9, Renderer);
        Connect(Decoder, Renderer);

        HWND hContainerWnd = p_Channels->hwnds[i];

        const CComPtr<IVideoWindow> pIVW = QI<IVideoWindow>(Renderer, IID_IVideoWindow);

        RendererMap[hContainerWnd] = pIVW;
        SetupRenderer(pIVW, hContainerWnd);
        PlaceRenderer(hContainerWnd);
    }
}

void GRAPH_CONTROL::SetupRenderer(IVideoWindow* pIVideoWindow, HWND hContainerWnd) 
{
    CHECK_HR(pIVideoWindow->put_Owner(reinterpret_cast<OAHWND>(hContainerWnd)), "IVideoWindow::put_Owner() failed");

    long style, exStyle;
    CHECK_HR(pIVideoWindow->get_WindowStyle(&style), "IVideoWindow::get_WindowStyle() failed");
    CHECK_HR(pIVideoWindow->get_WindowStyleEx(&exStyle), "IVideoWindow::get_WindowStyleEx() failed");
	
    style &= ~(WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX);
    exStyle &= ~WS_EX_WINDOWEDGE;
    CHECK_HR(pIVideoWindow->put_WindowStyle(style), "IVideoWindow::put_WindowStyle() failed");
    CHECK_HR(pIVideoWindow->put_WindowStyleEx(exStyle), "IVideoWindow::put_WindowStyleEx() failed");
}

void GRAPH_CONTROL::PlaceRenderer(const HWND h_container_wnd) const
{
    const auto it = RendererMap.find(h_container_wnd);
    if (it == RendererMap.end()) THROW("PlaceRenderer(): invalid window handle");

    RECT rect;
    if (!GetClientRect(h_container_wnd, &rect)) THROW("GetClientWindow() failed");

    IVideoWindow* pIVideoWindow = it->second;

    CHECK_HR(pIVideoWindow->put_Left(1), "IVideoWindow::put_Left() failed");
    CHECK_HR(pIVideoWindow->put_Top(1), "IVideoWindow::put_Top() failed");
    CHECK_HR(pIVideoWindow->put_Width(rect.right - rect.left - 2), "IVideoWindow::put_Width() failed");
    CHECK_HR(pIVideoWindow->put_Height(rect.bottom - rect.top - 2), "IVideoWindow::put_Height() failed");
}

//------------------------------------------------------------------------
// PMT
//

void GRAPH_CONTROL::AddPMTPvtData(CHANNELS* pChannels)
 {
    AddFilter(CLSID_PMTPvtData, PMTPvtDataName);
    Connect(DemuxName, PMTPvtDataName, "PMT");
    pIPMTPvtDataSettings2 = QI<IPMTPvtDataSettings2>(PMTPvtDataName, IID_IPMTPvtDataSettings2);
    
    for(int i = 0; i < pChannels->NumPMTs; i++)
    {
        const int curPMT = pChannels->pmts[i];
        if (curPMT < 0 || curPMT > 0x1fff) continue;

        GETIF(IVMRMixerBitmap9, CAWString(VideoRendererName, i));
        CHECK_HR(pIPMTPvtDataSettings2->SetRendererEx(i, curPMT, pIVMRMixerBitmap9), "IPMTPvtDataSettings2::SetRendererEx() failed");
    }
}

void GRAPH_CONTROL::SetTelemetryAlpha(const int alpha) const
{
    CHECK_HR(pIPMTPvtDataSettings2->SetAlpha(alpha), "IPMTPvtDataSettings::SetAlpha() failed");
}

void GRAPH_CONTROL::SetTelemetryPosition(const int x, const int y) const
{
    CHECK_HR(pIPMTPvtDataSettings2->SetPosition(x, y), "IPMTPvtDataSettings::SetPosition() failed");
}

void GRAPH_CONTROL::SetTelemetryColors(COLORREF txt_color, COLORREF bkg_color) const
{
    pIPMTPvtDataSettings2->SetBkgColor(bkg_color & 0x00ffffff);
    pIPMTPvtDataSettings2->SetTxtColor(txt_color & 0x00ffffff);
}

void GRAPH_CONTROL::EnableTelemetry(int enable) const
{
    pIPMTPvtDataSettings2->EnableTelemetry(enable);
}

//------------------------------------------------------------------------
// Statistics
//

void GRAPH_CONTROL::MapUnmap(const int ch, const bool map)
{
    if (0 <= ch && ch < MAX_CHANNELS)
    {
        GETOUTPINIF(IMPEG2PIDMap, DemuxName, CAWString("V", ch));

        ULONG pid = UsedPids[ch];
        if (pid < 0x2000)
        {
            /* we don't check for success here just because it's totally unclear what we should do when an error occurs */
            if (map) pIMPEG2PIDMap->MapPID(1, &pid, MEDIA_ELEMENTARY_STREAM);
            else pIMPEG2PIDMap->UnmapPID(1, &pid);
        }
    }
}


void GRAPH_CONTROL::GetPositions(DWORD* percent)
{
    CComPtr<ITSFileSource> pTSPushFileSourceSeeking = QI<ITSFileSource>(TSFileSourceName, IID_ITSFileSource);
    LONGLONG start_position = 0, stop_position = 0;
    CHECK_HR(pTSPushFileSourceSeeking->GetPosition(&start_position, &stop_position), "ITSFileSource::GetPosition() failed");
    *percent = static_cast<DWORD>(start_position * 100 / stop_position);
  
}

void GRAPH_CONTROL::SetPosition(const DWORD percent)
{
    CComPtr<ITSFileSource> pTSPushFileSourceSeeking = QI<ITSFileSource>(TSFileSourceName, IID_ITSFileSource);
    LONGLONG current_position = 0, stop_position = 0;
    CHECK_HR(pTSPushFileSourceSeeking->GetPosition(&current_position, &stop_position), "ITSFileSource::GetPosition() failed");
    current_position = stop_position / 100 * percent;
    CHECK_HR(pTSPushFileSourceSeeking->SetPosition(&current_position, &stop_position), "ITSFileSource::SetPosition() failed");
}

void GRAPH_CONTROL::SetStart()
{
    Start();
}

void GRAPH_CONTROL::SetPause()
{
    Pause();
}

void GRAPH_CONTROL::SetStop()
{
    Stop();
}

LPCOLESTR GRAPH_CONTROL::ToCOLEFileName(char* fileName) const
{
    USES_CONVERSION;
    return A2COLE(fileName);
}

void GRAPH_CONTROL::BuildGraph(BVP_SETTINGS *p_settings)
{
    LPCOLESTR pFileCOLE = ToCOLEFileName(p_settings->fileName);
    AddTSFileSource(&pFileCOLE);
	AddDemux(&p_settings->AllChannels);
    AddVideoDecoder(&p_settings->AllChannels);
    AddVideoRenderer(&p_settings->AllChannels);
    AddPMTPvtData(&p_settings->AllChannels);

    for (int i = 0; i < p_settings->AllChannels.NumVideoPids; i++) UsedPids[i] = p_settings->AllChannels.pids[i];
}

GRAPH_CONTROL::~GRAPH_CONTROL()
{
	for(const auto &rend: RendererMap)
	{
        IVideoWindow* pIVW = rend.second;
        pIVW->put_Visible(OAFALSE);
        pIVW->put_Owner(reinterpret_cast<OAHWND>(nullptr));
	}
}


