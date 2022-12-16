#include <Shlwapi.h>
#include <dshow.h>
#include "BIONVideoPlayerPvt.h"
#include "GraphBuilder.h"

#define CATCHALL                                            \
	}                                                       \
    catch(Exception &E) { SetErrorMessage(E.m_what); }      \
    catch(...) { SetErrorMessage("Exception"); }            \

#define CATCHALLFALSE                                       \
	CATCHALL                                                \
	return FALSE;                                           

#define TRY                                                 \
	try                                                     \
    {                                                       
                                                

//------------------------------------------------------------------------
// Globals
//
static HINSTANCE h_inst = nullptr;
static std::string last_error;

static GRAPH_CONTROL *pGraphControl = nullptr;

/* just to make Win10 SDK strmbase.lib happy */
class CFactoryTemplate * g_Templates = nullptr;
int g_cTemplates = 0;

//------------------------------------------------------------------------
// DLL Entry point
//
extern "C" void EXPORT bvpClose();

BOOL WINAPI DllMain(HINSTANCE hInstDll, DWORD fdwReason, LPVOID lpvReserved)
{
    switch(fdwReason)
    {
        case DLL_PROCESS_ATTACH:
            h_inst = hInstDll;
            break;

        case DLL_PROCESS_DETACH:
            bvpClose();
            break;

        default:
            break;
    }
    return TRUE;
}

//------------------------------------------------------------------------
// Remember error message
//
bool SetErrorMessage(const char *p_err_msg)
{
    last_error = p_err_msg;
    return false;
}

bool SetErrorMessage(const std::string &err)
{
    last_error = err;
    return false;
}

void ClearErrorMessage()
{
    last_error.clear();
}

//------------------------------------------------------------------------
// Exported functions
// The caller is responsible for calling Open() and Close() only once and in given order
//
extern "C" BOOL EXPORT bvpInitialize()
{
    SetErrorMessage("CoInitialize() failed");
    return SUCCEEDED(CoInitialize(nullptr));
}

extern "C" void EXPORT bvpUninitialize()
{
    CoUninitialize();
}

extern "C" void EXPORT bvpClose()
{
    QDELETE(pGraphControl)
}


extern "C" BOOL EXPORT bvpOpen(BVP_SETTINGS *p_settings, char* psz_file_name)
{
    USES_CONVERSION;
    if (p_settings->Size != sizeof(BVP_SETTINGS)) return FALSE;

    bvpClose();

    ClearErrorMessage();
    
    TRY
        SetErrorMessage("new CGraph() failed");
        pGraphControl = new GRAPH_CONTROL();
        pGraphControl->BuildGraph(p_settings, psz_file_name);
        return TRUE;
    CATCHALL
    
    bvpClose();
    return FALSE;
}

extern "C" LPCTSTR EXPORT bvpGetLastError()
{
    return last_error.c_str();
}

extern "C" BOOL EXPORT bvpResizeRenderer(const HWND h_container_wnd)
{
    if (pGraphControl == nullptr) return TRUE;
    TRY
        pGraphControl->PlaceRenderer(h_container_wnd);
        return TRUE;
    CATCHALLFALSE;
}

extern "C" void EXPORT bvpSetTelemetryPosition(const int x, const int y)
{
    TRY
        pGraphControl->SetTelemetryPosition(x, y);
	CATCHALL
}

extern "C" void EXPORT bvpSetTelemetryAlpha(const int alpha)
{
    TRY
        pGraphControl->SetTelemetryAlpha(alpha);
	CATCHALL
}

extern "C" void EXPORT bvpMapUnmapChannel(const int ch, const BOOL map)
{
    TRY
        pGraphControl->MapUnmap(ch, map ? true : false);
	CATCHALL
}

extern "C" void EXPORT bvpSetTelemetryColors(const COLORREF txtColor, const COLORREF bkgColor)
{
    TRY
        pGraphControl->SetTelemetryColors(txtColor, bkgColor);
	CATCHALL
}

extern "C" void EXPORT bvpEnableTelemetry(const int enable)
{
    TRY
        pGraphControl->EnableTelemetry(enable);
	CATCHALL
}

extern "C" BOOL EXPORT bvpGetPositionTrackBar(DWORD* percent)
{
    TRY
        pGraphControl->GetPositions(percent);
        return TRUE;
    CATCHALLFALSE
}

extern "C" BOOL EXPORT bvpSetPositionTrackBar(const DWORD percent)
{
	TRY
        pGraphControl->SetPosition(percent);
        return TRUE;
	CATCHALLFALSE
}

extern "C" BOOL EXPORT bvpSetStart()
{
	TRY
        pGraphControl->SetStart();
        return TRUE;
    CATCHALLFALSE
}

extern "C" BOOL EXPORT bvpSetPause()
{
	TRY
        pGraphControl->SetPause();
        return TRUE;
    CATCHALLFALSE
}

extern "C" BOOL EXPORT bvpSetStop()
{
    TRY
        pGraphControl->SetStop();
        return TRUE;
    CATCHALLFALSE
}

