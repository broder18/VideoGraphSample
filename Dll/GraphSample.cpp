#include <Shlwapi.h>
#include <dshow.h>
#include "GraphSamplePvt.h"
#include "GraphBuilder.h"
#include "Filters.h"

#define CATCHALL                                            \
    catch(Exception &E) { SetErrorMessage(E.m_what); }      \
    catch(...) { SetErrorMessage("Exception"); }            \

//------------------------------------------------------------------------
// Globals
//
static HINSTANCE hInst = nullptr;
static std::string LastError;

static GRAPH_CONTROL *pGraphControl = nullptr;

/* just to make Win10 SDK strmbase.lib happy */
class CFactoryTemplate * g_Templates = nullptr;
int g_cTemplates = 0;

//------------------------------------------------------------------------
// DLL Entry point
//
extern "C" void EXPORT gsClose();

BOOL WINAPI DllMain(HINSTANCE hInstDll, DWORD fdwReason, LPVOID lpvReserved)
{
    switch(fdwReason)
    {
        case DLL_PROCESS_ATTACH:
            hInst = hInstDll;
            break;

        case DLL_PROCESS_DETACH:
            gsClose();
            break;

        default:
            break;
    }
    return TRUE;
}

//------------------------------------------------------------------------
// Remember error message
//
bool SetErrorMessage(const char *pErrMsg)
{
    LastError = pErrMsg;
    return false;
}

bool SetErrorMessage(const std::string &Err)
{
    LastError = Err;
    return false;
}

void ClearErrorMessage()
{
    LastError.clear();
}

//------------------------------------------------------------------------
// Exported functions
// The caller is responsible for calling Open() and Close() only once and in given order
//
extern "C" BOOL EXPORT gsInitialize()
{
    SetErrorMessage("CoInitialize() failed");
    return SUCCEEDED(CoInitialize(nullptr));
}

extern "C" void EXPORT gsUninitialize()
{
    CoUninitialize();
}

extern "C" void EXPORT gsClose()
{
    QDELETE(pGraphControl)
}


extern "C" BOOL EXPORT gsOpenRefact(GS_SETTINGSRefact *pSettings)
{
    SetErrorMessage(std::to_string(sizeof(GS_SETTINGSRefact)).c_str());
    if (pSettings->Size != sizeof(GS_SETTINGSRefact)) return FALSE;

    gsClose();

    ClearErrorMessage();
    try
    {
        SetErrorMessage("new CGraph() failed");
        pGraphControl = new GRAPH_CONTROL();
        pGraphControl->BuildGraphRefact(pSettings);
        return TRUE;
    }
    CATCHALL

    gsClose();
    return FALSE;
}

extern "C" LPCTSTR EXPORT gsGetLastError()
{
    return LastError.c_str();
}

extern "C" BOOL EXPORT gsSetInputNetwork(INPUT_NETWORK *pInNet)
{
    try
    {
        pGraphControl->SetRTPSource(pInNet);
        return TRUE;
    }
    CATCHALL
    return FALSE;
}

extern "C" BOOL EXPORT gsResizeRenderer(HWND hContainerWnd)
{
    try
    {
        pGraphControl->PlaceRendererRefact(hContainerWnd);
        return TRUE;
    }
    CATCHALL
    return FALSE;
}

extern "C" BOOL EXPORT gsGetStatistics(PIDSTATISTICS *pStats) //
{
    try
    {
        pGraphControl->GetStatistics(pStats);
        return TRUE;
    }
    CATCHALL
    return FALSE;
}

extern "C" void EXPORT gsResetStatistics() //
{
    try
    {
        pGraphControl->ResetStatistics();
    }
    CATCHALL

}

extern "C" BOOL EXPORT gsSetPMTParams(TEXT_PARAMS *pPMT) //
{
    if (pPMT->size != sizeof(TEXT_PARAMS)) return FALSE;
    try
    {
        pGraphControl->SetPMTParams(pPMT);
        return TRUE;
    }
    CATCHALL
    return FALSE;
    
}