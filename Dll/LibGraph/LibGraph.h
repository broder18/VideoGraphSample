#pragma once

#include "..\Defines.h"
#include <stdio.h>
#include <tchar.h>
#include <atlbase.h>
#include <atlconv.h>
#include <comutil.h>
#include <streams.h>

// STL headers
#include <string>
#include <map>
#include <vector>

using std::map;
using std::string;
using std::vector;

#include "Exception.h"
#include <DShow.h>
    
typedef CComPtr<IBaseFilter> FILTERPTR;
typedef CComPtr<IPin> PINPTR;

typedef vector<PINPTR> Pins;
typedef map<string, FILTERPTR> FilterMap;
typedef map<string, Pins> PinMap;

typedef vector<HINSTANCE> Libraries;

class CGraph
{
public:
    CGraph(bool bDebug = true);
    ~CGraph();

    FILTERPTR AddFilter(REFGUID guid, LPCTSTR pszFilterName);
    FILTERPTR AddFilterFromDLL(REFGUID guid, LPCTSTR pszFilterName, LPCTSTR moduleName);
    void AddExisitingFilter(FILTERPTR pFilter, LPCTSTR pszFilterName);

    FILTERPTR AddSourceFilter(LPCTSTR pszFileName, LPCTSTR pszFilterName, bool bForceASFReader = false, CLSID clsid = GUID_NULL);
    FILTERPTR AddSinkFilter(REFGUID guid, LPCTSTR pszFileName, LPCTSTR pszFilterName);
    FILTERPTR AddFilterByCategory(size_t index, LPCTSTR pszFilterName, REFGUID guid);
    void EnumFiltersByCategory(vector<string>& sFilterNames, REFGUID guid);
    void AddFilterByCategoryEx(LPCTSTR pPartialVFWName, LPCTSTR pszFilterName, REFGUID guid = CLSID_VideoCompressorCategory);

    bool FilterExists(LPCTSTR pszFilterName);
    void DelFilter(LPCTSTR pszFilterName);
    void Connect(LPCTSTR pszFilterNameOut, LPCTSTR pszFilterNameIn, const AM_MEDIA_TYPE *pmt = 0, size_t iPinOut = 0, size_t iPinIn = 0, bool Direct = true);
    void Connect(LPCTSTR pszFilterNameOut, LPCTSTR pszFilterNameIn, LPCTSTR pszPinNameOut, LPCTSTR pszPinNameIn);
    void Connect(LPCTSTR pszFilterNameOut, LPCTSTR pszFilterNameIn, LPCTSTR pszPinNameOut);
    
    FILTERPTR GetFilter(LPCTSTR pszFilterName);
    CComPtr<IGraphBuilder> GetFilterGraph();
    CComPtr<IMediaControl> GetMediaControl();
    CComPtr<IMediaEventEx> GetMediaEventEx();
   
    PINPTR ConnectedTo(LPCTSTR pszFilterName, size_t iPin, PIN_DIRECTION pinDir);
    PINPTR GetPin(LPCTSTR pszFilterName, size_t iPin, PIN_DIRECTION pinDir);
    GUID GetPinMajorType(LPCTSTR pszFilterName, size_t iPin, PIN_DIRECTION pinDir);
    PINPTR Disconnect(LPCTSTR pszFilterName, size_t iPin, PIN_DIRECTION pinDir);
    void RefreshPins(LPCTSTR pszFilterName);
    FilterMap RefreshFilters();

    size_t GetPinCount(LPCTSTR pszFilterName, PIN_DIRECTION pinDir);
    void ShowPropertyDialog(LPCTSTR pszFilterName, HWND hwnd);
    string GUIDToString(GUID *pGUID);

    void Start();
    void Stop(bool NoException = true);
    void Pause();
    void WaitForEndOfStateTransition(LONG msTimeout = 1000);

    void SetMediaEventSinkWindow(HWND hNotifyWnd, UINT NotifyMsg);

    void QI(LPCTSTR pszFilterName, REFIID riid, void **pInterface);
    template<typename T> CComPtr<T> QI(LPCTSTR pszFilterName, REFIID riid)
    {
        if(!FilterExists(pszFilterName)) throw Exception(E_FAIL, "QI(): No such filter: %s", pszFilterName);

        CComPtr<T> If;
        m_hr = m_Filters[pszFilterName]->QueryInterface(riid, (void **)&If);
        if(FAILED(m_hr) || If == 0) throw Exception(E_NOINTERFACE, "%s->QueryInterface(%s) failed", pszFilterName, GUIDToString((GUID *)&riid).c_str());
        return If;
    }

    template<typename T> CComPtr<T> PinQI(LPCTSTR pszFilterName, int PinIdx, PIN_DIRECTION pinDir, REFIID riid)
    {
        IPin *pPin = GetPin(pszFilterName, PinIdx, pinDir);

        CComPtr<T> If;
        m_hr = pPin->QueryInterface(riid, (void **)&If);
        if(FAILED(m_hr) || If == 0) throw Exception(E_NOINTERFACE, "%s->%sPin[%d]->QueryInterface(%s) failed", pszFilterName, pinDir == PINDIR_INPUT ? "In" : "Out", PinIdx, GUIDToString((GUID *)&riid).c_str());
        return If;
    }

    template<typename T> CComPtr<T> PinQI(LPCTSTR pszFilteName, LPCTSTR pPinName, PIN_DIRECTION pinDir, REFIID riid)
    {
        return PinQI<T>(pszFilteName, GetPinIdx(pszFilteName, pPinName, pinDir), pinDir, riid);
    }


    size_t GetPinIdx(LPCTSTR pszFilterName, LPCTSTR pPinName, PIN_DIRECTION pinDir);

private:
    CComPtr<IGraphBuilder> m_pGraph;
    CComQIPtr<IMediaControl> m_pMC;
    CComQIPtr<IMediaEventEx> m_pME;
    DWORD m_dwRegister;
    HRESULT m_hr;

    FilterMap m_Filters;
    PinMap m_InPins, m_OutPins;
    Libraries m_Libraries;

    IPin *CheckPin(LPCTSTR pszFilterName, size_t iPin, PIN_DIRECTION pinDir);
    void CheckFilter(LPCTSTR pszFilterName);
};

