#include "LibGraph.h"

#define THR                                                 \
    catch(Exception &e)                                     \
    {                                                       \
        ThrowWithMethodName(e, __FUNCSIG__);                \
    }

#define THRDEL                                              \
    catch(Exception &e)                                     \
    {                                                       \
        DelFilter(pszFilterName);                           \
        ThrowWithMethodName(e, __FUNCSIG__);                \
    }



HRESULT AddGraphToRot(IUnknown *pUnkGraph, DWORD *pRegister)
{
    IRunningObjectTable *pROT = 0;
    *pRegister = 0;

    HRESULT Hr = GetRunningObjectTable(0, &pROT);
    if(SUCCEEDED(Hr))
    {
        WCHAR wsz[MAX_PATH];
        StringCchPrintfW(wsz, MAX_PATH, L"FilterGraph %08x pid %08x", (DWORD_PTR)pUnkGraph, GetCurrentProcessId());
    
        IMoniker *pMoniker = 0;
        Hr = CreateItemMoniker(L"!", wsz, &pMoniker);
        if(SUCCEEDED(Hr)) 
        {
            Hr = pROT->Register(ROTFLAGS_REGISTRATIONKEEPSALIVE, pUnkGraph, pMoniker, pRegister);
            pMoniker->Release();
        }
        pROT->Release();
    }
    return Hr;
}

//////////////////////////////////////////////////////////////////////////
// Removes a filter graph from the Running Object Table
//////////////////////////////////////////////////////////////////////////
void RemoveGraphFromRot(DWORD pRegister)
{
    if(pRegister)
    {
        IRunningObjectTable *pROT = 0;
        if(SUCCEEDED(GetRunningObjectTable(0, &pROT)))
        {
            pROT->Revoke(pRegister);
            pROT->Release();
        }
    }
}


// Append the methodname to the exception to simulate call backtrace
void __declspec(noreturn) ThrowWithMethodName(Exception &e, LPCTSTR pszMethodName)
{
    e.m_what = string("ERROR in ") + pszMethodName + string("() ") + e.m_what;
    throw e;
}


//////////////////////////////////////////////////////////////////////////
// The ctor makes a FilterGraph and gets its MediaControl interface
// Also adds the graph to the ROT and sets its logfile if the bDebug parameter
// is true
//////////////////////////////////////////////////////////////////////////
CGraph::CGraph(bool bDebug)
{
    m_dwRegister = 0;
    m_hr = S_OK;

    try
    {
        m_hr = m_pGraph.CoCreateInstance(CLSID_FilterGraph);
        if(FAILED(m_hr)) throw Exception(m_hr, "Unable to create filter graph");

        m_pMC = CComQIPtr<IMediaControl>(m_pGraph);
        if(!m_pMC) throw Exception(E_NOINTERFACE, "Unable to query IMediaControl");

        m_pME = CComQIPtr<IMediaEventEx>(m_pGraph);
        if(!m_pME) throw Exception(E_NOINTERFACE, "Unable to query IMediaEventEx");

        if(bDebug)
        {
            m_hr = AddGraphToRot(m_pGraph, &m_dwRegister);
            if(FAILED(m_hr)) throw Exception(m_hr, "Unable to add filter graph to ROT");
        }
    }
    THR;
}

//////////////////////////////////////////////////////////////////////////
// dtor just clears the STL containers which will cause the contained
// COM smart ptrs to release themselves automagically
// None of the methods used here throw
//////////////////////////////////////////////////////////////////////////m_pGraph
CGraph::~CGraph()
{
    // We dont care if this fails as we are shutting shop
    if(m_pMC) m_pMC->Stop();

    if(m_pME) m_pME->SetNotifyWindow(0, 0, 0);

    while(m_Filters.size())
    {
        FilterMap::iterator fi = m_Filters.begin();
        DelFilter(fi->first.c_str());
    }

    if(m_dwRegister != 0)
    {
        RemoveGraphFromRot(m_dwRegister);
        m_dwRegister = 0;
    }

    /* free all loaded libraries */
    while(!m_Libraries.empty())
    {
        HINSTANCE hDll = m_Libraries[m_Libraries.size() - 1];
        CoFreeLibrary(hDll);
        m_Libraries.pop_back();
    }
}

//////////////////////////////////////////////////////////////////////////
//  Tells if a given filter exists in the graph
//////////////////////////////////////////////////////////////////////////
bool CGraph::FilterExists(LPCTSTR pszFilterName)
{
    return m_Filters.find(pszFilterName) != m_Filters.end();
}

//////////////////////////////////////////////////////////////////////////
// Adds a filter to the graph and make an entry for it in the m_Filters map
// Also enumerate the input and output pins and store them 
//////////////////////////////////////////////////////////////////////////
FILTERPTR CGraph::AddFilter(REFGUID guid, LPCTSTR pszFilterName)
{
    try
    {
        m_hr = m_Filters[pszFilterName].CoCreateInstance(guid);
        if(FAILED(m_hr)) throw Exception(m_hr, "Unable to create filter %s", pszFilterName);

        RefreshPins(pszFilterName);

        m_hr = m_pGraph->AddFilter(m_Filters[pszFilterName], CA2W(pszFilterName));
        if(FAILED(m_hr)) throw Exception(m_hr, "Unable to add filter %s to graph", pszFilterName);

        return m_Filters[pszFilterName];
    }
    THRDEL;
}

void CGraph::AddExisitingFilter(FILTERPTR pFilter, LPCTSTR pszFilterName)
{
    try
    {
        m_Filters[pszFilterName] = pFilter;
        RefreshPins(pszFilterName);
        m_hr = m_pGraph->AddFilter(m_Filters[pszFilterName], CA2W(pszFilterName));
        if(!SUCCEEDED(m_hr)) 
            throw Exception(m_hr, "Unable to add filter %s to graph", pszFilterName);
    }
    THRDEL;
}

typedef HRESULT (WINAPI* PFNDllGetClassObject)(REFCLSID rclsid, REFIID riid, LPVOID* ppv); 

FILTERPTR CGraph::AddFilterFromDLL(REFGUID guid, LPCTSTR pszFilterName, LPCTSTR pszModuleName)
{
    HINSTANCE hDll = 0;
    try
    {
        hDll = CoLoadLibrary(CA2W(pszModuleName), FALSE);
        if(!hDll) throw Exception(GetLastError(), "CoLoadLibrary failed for %s", pszModuleName);
    
        PFNDllGetClassObject pfnDllGetClassObject = (PFNDllGetClassObject)GetProcAddress(hDll, "DllGetClassObject"); 
        if(!pfnDllGetClassObject) throw Exception(0, "Can't get address of DllGetClassObject()");

        CComPtr<IClassFactory> pFactory; 
        m_hr = pfnDllGetClassObject(guid, IID_IClassFactory, (LPVOID*)&pFactory); 
        if(FAILED(m_hr)) throw Exception(m_hr, "DllGetClassObject() failed");

        FILTERPTR pFilter;
        if(FAILED(pFactory->CreateInstance(NULL, IID_IBaseFilter, (void**)&pFilter)) || pFilter == 0) throw Exception(m_hr, "IClassFactory:CreateInstance() failed");

        /* remember just loaded library in the list of all loader libraries */
        m_Libraries.push_back(hDll);

        AddExisitingFilter(pFilter, pszFilterName);
        return pFilter;
    }
    catch(Exception &e)
    {
        if(hDll) CoFreeLibrary(hDll);
        ThrowWithMethodName(e, "CGraph::AddFilterFromDLL");
    }
    return 0;
}



//////////////////////////////////////////////////////////////////////////
// Similar to the above function, except that we let DShow to choose which 
// filter to insert based on the file media format, if the clsid is not specified
// For compatibility with pre-WMV9 systems, a forceASFReader parameter is
// given to insert the ASF reader instead of the legacy filter  
//////////////////////////////////////////////////////////////////////////
FILTERPTR CGraph::AddSourceFilter(LPCTSTR pszFileName, LPCTSTR pszFilterName, bool bForceASFReader, CLSID clsid)
{
    try
    {
        IBaseFilter *pTempFilter;
        m_hr = S_OK;

        size_t nameLen = strlen(pszFilterName);
        bool isWMV = bForceASFReader && 
            (_stricmp(pszFileName + nameLen - 3, "WMV") == 0 || 
            _stricmp(pszFileName + nameLen - 3, "ASF") == 0);

        if(clsid == GUID_NULL)
        {
            if(isWMV)
            {
                AddFilter(CLSID_WMAsfReader, pszFilterName);
                pTempFilter = m_Filters[pszFilterName];
            }
            else
            {
                m_hr = m_pGraph->AddSourceFilter(CA2W(pszFileName), CA2W(pszFilterName), &pTempFilter);
            }
        }
        else
        {
            AddFilter(clsid, pszFilterName);
            pTempFilter = m_Filters[pszFilterName];
        }

        if(!SUCCEEDED(m_hr))
            throw Exception(m_hr, "Unable to add a source filter to graph for %s", pszFileName);

        CComQIPtr<IFileSourceFilter> pSrc(pTempFilter);
        m_hr = pSrc->Load(CA2W(pszFileName), 0);
   
//        returns E_UNEXPECTED sometimes why???
//        if(!SUCCEEDED(m_hr))
//          throw Exception(m_hr, "Unable to load the file %s", fileName);

        if(clsid == GUID_NULL)
        {
            if(!isWMV)
            {
                m_Filters[pszFilterName] = pTempFilter;
                RefreshPins(pszFilterName);
               // pTempFilter->Release();
            }
            else
                RefreshPins(pszFilterName);

            return m_Filters[pszFilterName];
        }
        return 0;
    }
    THRDEL;
}

//////////////////////////////////////////////////////////////////////////
// Similar to AddFilter, but we add filters which support IFileSinkFilter
// and set the output file name. Some examples are 'File Writer', 'ASF Writer'
//////////////////////////////////////////////////////////////////////////
FILTERPTR CGraph::AddSinkFilter(REFGUID guid, LPCTSTR pszFileName, LPCTSTR pszFilterName)
{
    try
    {
        AddFilter(guid, pszFilterName);

        CComQIPtr<IFileSinkFilter2> pSink(m_Filters[pszFilterName]);
        m_hr = pSink->SetFileName(CA2W(pszFileName), 0);
        pSink->SetMode(AM_FILE_OVERWRITE);
        
        if(!SUCCEEDED(m_hr))
            throw Exception(m_hr, "Unable to set the filename %s", pszFileName);
        // New Input pins might get created after setting the output file, so refresh our list
        RefreshPins(pszFilterName);
        return m_Filters[pszFilterName];
    }
    THRDEL;
}

//////////////////////////////////////////////////////////////////////////
// Removes a filter from the graph and also any entries that we have for it
// This method doesnt throw anything as it is called from the dtor
//////////////////////////////////////////////////////////////////////////
void CGraph::DelFilter(LPCTSTR pszFilterName)
{
    CheckFilter(pszFilterName);
    size_t i;
    for(i = 0; i < GetPinCount(pszFilterName, PINDIR_INPUT); ++i)
        Disconnect(pszFilterName, i, PINDIR_INPUT);

    for(i = 0; i < GetPinCount(pszFilterName, PINDIR_OUTPUT); ++i)
        Disconnect(pszFilterName, i, PINDIR_OUTPUT);

    m_InPins.erase(pszFilterName);
    m_OutPins.erase(pszFilterName);
    
    m_pGraph->RemoveFilter(m_Filters[pszFilterName]); // we dont care for the hresult
    m_Filters.erase(pszFilterName);
}

//////////////////////////////////////////////////////////////////////////
// This method enumerates the input and output pins of a filter and stores 
// them into corresponding maps
//////////////////////////////////////////////////////////////////////////
void CGraph::RefreshPins(LPCTSTR pszFilterName)
{
    try
    {
        CheckFilter(pszFilterName);
        FILTERPTR pFilter = m_Filters[pszFilterName];
        CComPtr<IEnumPins> pEnum;
        if (!pFilter) 
            throw Exception(0, "Filter '%s' not found!", pszFilterName);

        m_hr = pFilter->EnumPins(&pEnum);
        if(!SUCCEEDED(m_hr))
            throw Exception(0, "Cannot enumerate pins for filter '%s'!", pszFilterName);

        ULONG ulFound;

        m_InPins[pszFilterName].clear();
        m_OutPins[pszFilterName].clear();
        IPin *pPin;

        while(true)
        {
            PIN_DIRECTION pindir;
            if(S_OK != pEnum->Next(1, &pPin, &ulFound)) break;

            m_hr = pPin->QueryDirection(&pindir);
            if(!SUCCEEDED(m_hr))
                throw Exception(m_hr, "IPin::QueryDirection failed for filter '%s'\n Error code %X", pszFilterName);

            if(pindir == PINDIR_INPUT) m_InPins[pszFilterName].push_back(pPin);
            else m_OutPins[pszFilterName].push_back(pPin);
            pPin->Release();
        } 
    }
    THR;
}

//////////////////////////////////////////////////////////////////////////
// This method enumerates any new filters who got added to a graph 
// by render() or renderfile()
//////////////////////////////////////////////////////////////////////////
FilterMap CGraph::RefreshFilters()
{
    try
    {
        FilterMap mTemp;

        // Using a set here breaks VC6 compilation
        map<FILTERPTR, int> sExisting;

        FilterMap::iterator fmi = m_Filters.begin();
        for(;fmi != m_Filters.end(); ++fmi)
            sExisting[fmi->second] = 0;

        // Following code embraced and extended from the MSDN
        // Enumerate filters 
        CComPtr<IEnumFilters> pEnum;
        
        ULONG cFetched;

        m_hr = m_pGraph->EnumFilters(&pEnum);
        if (FAILED(m_hr)) 
            throw Exception(0, "Cannot enumerate filters in the graph");

        IBaseFilter *pFilter;
        while(pEnum->Next(1, &pFilter, &cFetched) == S_OK)
        {
            FILTERPTR pTheFilter = pFilter;
            FILTER_INFO FilterInfo;
            m_hr = pTheFilter->QueryFilterInfo(&FilterInfo);
            if (FAILED(m_hr))
                continue;  // Maybe the next one will work.

            // The FILTER_INFO structure holds a pointer to the Filter Graph
            // Manager, with a reference count that must be released.
            if (FilterInfo.pGraph != NULL)
                FilterInfo.pGraph->Release();

            if(sExisting.find(pTheFilter) == sExisting.end())
            {
                string sName(CW2A(FilterInfo.achName));
                mTemp[sName] = pTheFilter;
            }
        }

        FilterMap::iterator mi;
        for(mi = mTemp.begin(); mi != mTemp.end(); ++mi)
            m_Filters.insert(*mi);
        for(mi = mTemp.begin(); mi != mTemp.end(); ++mi)
            RefreshPins(mi->first.c_str());

            return mTemp;
    }
    THR;
}

//////////////////////////////////////////////////////////////////////////
// This method connects two filters, optionally taking a mediatype
// and pin indices. For most cases the first two parameters suffice and
// it connects output pin 0 of one filter to input pin 0 of the other
// Since the graph does intelligent connect, some filters might get added inbetween
// for which we have no entry in our maps, but we ignore them as they will get 
// released when the the graph is released
//////////////////////////////////////////////////////////////////////////
void CGraph::Connect(LPCTSTR pszFilterNameOut, LPCTSTR pszFilterNameIn, const AM_MEDIA_TYPE *pmt, size_t iPinOut, size_t iPinIn, bool Direct)
{
    try
    {
        CheckFilter(pszFilterNameOut);
        CheckFilter(pszFilterNameIn);

        Pins &inPins = m_InPins[pszFilterNameIn]; 
        Pins &outPins = m_OutPins[pszFilterNameOut]; 
        size_t numInPins = inPins.size();
        size_t numOutPins = outPins.size(); 

        IPin *pPinOut = CheckPin(pszFilterNameOut, iPinOut, PINDIR_OUTPUT);
        IPin *pPinIn = CheckPin(pszFilterNameIn, iPinIn, PINDIR_INPUT);

        if(Direct || pmt) m_hr = m_pGraph->ConnectDirect(pPinOut, pPinIn, pmt);
        else m_hr = m_pGraph->Connect(pPinOut, pPinIn);
        
        if(!SUCCEEDED(m_hr))
        {
            char s[1024];
            AMGetErrorText(m_hr, s, 1024);
            throw Exception(m_hr, "Connect output pin %d of '%s' to input pin %d of '%s' failed: %s", iPinOut, pszFilterNameOut, iPinIn, pszFilterNameIn, s);
        }
    }
    THR;
}

//------------------------------------------------------------------------
// Directly connect two pins using its names
//
void CGraph::Connect(LPCTSTR pszFilterNameOut, LPCTSTR pszFilterNameIn, LPCTSTR pszPinNameOut, LPCTSTR pszPinNameIn)
{
    int IdxOut = GetPinIdx(pszFilterNameOut, pszPinNameOut, PINDIR_OUTPUT);
    int IdxIn = GetPinIdx(pszFilterNameIn, pszPinNameIn, PINDIR_INPUT);
    Connect(pszFilterNameOut, pszFilterNameIn, 0, IdxOut, IdxIn, true);
}


//------------------------------------------------------------------------
// Directly connect given output pin to the first input pin
//
void CGraph::Connect(LPCTSTR pszFilterNameOut, LPCTSTR pszFilterNameIn, LPCTSTR pszPinNameOut)
{
    int IdxOut = GetPinIdx(pszFilterNameOut, pszPinNameOut, PINDIR_OUTPUT);
    Connect(pszFilterNameOut, pszFilterNameIn, 0, IdxOut, 0, true);
}


//////////////////////////////////////////////////////////////////////////
// This retrieves the IBaseFilter for a filter in the graph
//////////////////////////////////////////////////////////////////////////
FILTERPTR CGraph::GetFilter(LPCTSTR pszFilterName)
{
    return m_Filters[pszFilterName];
}

//////////////////////////////////////////////////////////////////////////
// This retrieves the IGraphBuilder for the Filter graph
//////////////////////////////////////////////////////////////////////////
CComPtr<IGraphBuilder> CGraph::GetFilterGraph()
{
    return m_pGraph;
}

//////////////////////////////////////////////////////////////////////////
// This retrieves the pin to which the specified filters pin is connected 
//////////////////////////////////////////////////////////////////////////
PINPTR CGraph::ConnectedTo(LPCTSTR pszFilterName, size_t iPin, PIN_DIRECTION pinDir)
{
    try
    {
        PINPTR pPinConnected = 0; 
        IPin *pPin = CheckPin(pszFilterName, iPin, pinDir);
        pPin->ConnectedTo(&pPinConnected);
        return pPinConnected;
    }
    THR;
}

//////////////////////////////////////////////////////////////////////////
// Retrieves the nth pin of a given filter
//////////////////////////////////////////////////////////////////////////
PINPTR CGraph::GetPin(LPCTSTR pszFilterName, size_t iPin, PIN_DIRECTION pinDir)
{
    try
    {
        return CheckPin(pszFilterName, iPin, pinDir);
    }
    THR;
}

//////////////////////////////////////////////////////////////////////////
// Returns the number of input or output pins on the filter
//////////////////////////////////////////////////////////////////////////
size_t CGraph::GetPinCount(LPCTSTR pszFilterName, PIN_DIRECTION pinDir)
{
    try
    {
        CheckFilter(pszFilterName);
        if(pinDir == PINDIR_INPUT) return m_InPins[pszFilterName].size();
        else return m_OutPins[pszFilterName].size();
    }
    THR;
}

//////////////////////////////////////////////////////////////////////////
// Gets the first preffered media type of a filters pin
//////////////////////////////////////////////////////////////////////////
GUID CGraph::GetPinMajorType(LPCTSTR pszFilterName, size_t iPin, PIN_DIRECTION pinDir)
{
    try
    {
        GUID result = GUID_NULL;
        AM_MEDIA_TYPE **mt = new (AM_MEDIA_TYPE*);
        IPin *pin = CheckPin(pszFilterName, iPin, pinDir);

        IEnumMediaTypes *pEnum;
        pin->EnumMediaTypes(&pEnum);
        if(S_OK == pEnum->Next(1, mt, 0))
        {
            result = (*mt)->majortype;
            if((*mt)->pUnk) (*mt)->pUnk->Release();
            CoTaskMemFree((*mt)->pbFormat);
            CoTaskMemFree(*mt);
        }
        delete mt;
        return result;
    }
    THR;
}

//////////////////////////////////////////////////////////////////////////
// Disconnects a filters pin and return the pin it was connected to if any
//////////////////////////////////////////////////////////////////////////
PINPTR CGraph::Disconnect(LPCTSTR pszFilterName, size_t pinNo, PIN_DIRECTION pinDir)
{
    try
    {
        PINPTR pPinConnected;
        IPin *pPin = CheckPin(pszFilterName, pinNo, pinDir);
        pPin->ConnectedTo(&pPinConnected);
        if(pPinConnected)
        {
            m_pGraph->Disconnect(pPinConnected);
            m_pGraph->Disconnect(pPin);
        }
        return pPinConnected;
    }
    THR;
}

//////////////////////////////////////////////////////////////////////////
// Returns the IMediaControl/IMediaEventEx of the graph
//////////////////////////////////////////////////////////////////////////
CComPtr<IMediaControl> CGraph::GetMediaControl()
{
    return m_pMC;
}

CComPtr<IMediaEventEx> CGraph::GetMediaEventEx()
{
    return m_pME;
}

//////////////////////////////////////////////////////////////////////////
//  Helper method to return a pin and throw if non existent 
//////////////////////////////////////////////////////////////////////////
IPin *CGraph::CheckPin(LPCTSTR pszFilterName, size_t pinNo, PIN_DIRECTION pinDir)
{
    CheckFilter(pszFilterName);
    if(pinNo >= ( (pinDir == PINDIR_INPUT) ? m_InPins[pszFilterName].size() : m_OutPins[pszFilterName].size()))
        throw Exception(E_FAIL, " failed for Filter '%s'- %s Pin index '%d' out of range", pszFilterName, pinDir == PINDIR_INPUT ? "Input" : "Output", pinNo);
    
    IPin *pin = (pinDir == PINDIR_INPUT) ? m_InPins[pszFilterName][pinNo] : m_OutPins[pszFilterName][pinNo];
    if(!pin)
        throw Exception(E_FAIL, " failed for Filter '%s'- %s Pin at index '%d' seems to be NULL", pszFilterName, pinDir == PINDIR_INPUT ? "Input" : "Output", pinNo);

    return pin;
}

//------------------------------------------------------------------------
// Get index of a pin by its name
//
size_t CGraph::GetPinIdx(LPCTSTR pszFilterName, LPCTSTR pPinName, PIN_DIRECTION pinDir)
{
    int Idx = 0;
    while(true)
    {
        IPin *pPin = CheckPin(pszFilterName, Idx, pinDir);

        PIN_INFO PinInfo;
        m_hr = pPin->QueryPinInfo(&PinInfo);
        if(FAILED(m_hr)) throw Exception(m_hr, "IPin::QueryPinInfo() failed");

        /* we must release returned PinInfo.pFilter */
        if(PinInfo.pFilter) PinInfo.pFilter->Release();

        /* is this the pin we're looking for? */
        if(_wcsicmp(PinInfo.achName, CA2W(pPinName)) == 0) return Idx;

        Idx++;
    }
}


//////////////////////////////////////////////////////////////////////////
//  Helper method to return a filter and throw if non existent 
//////////////////////////////////////////////////////////////////////////
void CGraph::CheckFilter(LPCTSTR pszFilterName)
{
    if(!m_Filters.count(pszFilterName))
        throw Exception(E_FAIL, " failed - Filter '%s' not found", pszFilterName);
}

//////////////////////////////////////////////////////////////////////////
// Enumerates the filters of a given category ( like compressors )
//////////////////////////////////////////////////////////////////////////
void CGraph::EnumFiltersByCategory(std::vector<std::string>& filterNames, REFGUID guid)
{
    filterNames.clear();

    CComPtr<ICreateDevEnum> pDevEnum;
    pDevEnum.CoCreateInstance(CLSID_SystemDeviceEnum);
    if(pDevEnum == 0) throw Exception(m_hr, "Unable to create device enumerator");

    CComPtr<IEnumMoniker> pFilterEnum;
    m_hr = pDevEnum->CreateClassEnumerator(guid, &pFilterEnum, 0);
    if(FAILED(m_hr)) throw Exception(m_hr, "ICreateDevEnum::CreateClassEnumerator() failed");

    // If there are no enumerators for the requested type, then 
    // CreateClassEnumerator will succeed, but pFilterEnum will be NULL.
    while(pFilterEnum)
    {
        CComPtr<IMoniker> pMoniker;
        if(pFilterEnum->Next(1, &pMoniker, NULL) != S_OK) break;

        CComPtr<IPropertyBag> pPropBag;
        if(SUCCEEDED(pMoniker->BindToStorage(0, 0, IID_IPropertyBag, (void **)&pPropBag)))
        {
            VARIANT var;
            VariantInit(&var);
            m_hr = pPropBag->Read(L"FriendlyName", &var, 0);
            
            if(SUCCEEDED(m_hr))
            {
                _bstr_t str(var.bstrVal, FALSE); //necessary to avoid a memory leak
                filterNames.push_back(std::string(str));
            }
            SysFreeString(var.bstrVal);
            
            if(FAILED(m_hr)) throw Exception(m_hr, "Unable to read property bag");
        }
    }
}

FILTERPTR CGraph::AddFilterByCategory(size_t index, LPCTSTR pszFilterName, REFGUID guid)
{
    CComPtr<ICreateDevEnum> pDevEnum;
    CComPtr<IEnumMoniker> pFilterEnum = 0;

    size_t i = 0;
    FILTERPTR pFilter;

    pDevEnum.CoCreateInstance(CLSID_SystemDeviceEnum);
    m_hr = pDevEnum->CreateClassEnumerator(guid, &pFilterEnum, 0);
    if(!pDevEnum)
        throw Exception(m_hr, "Unable to create device enumerator");


    // If there are no enumerators for the requested type, then 
    // CreateClassEnumerator will succeed, but pCompressorEnum will be NULL.
    bool found = false;
    while (!found && pFilterEnum)
    {
        CComPtr<IMoniker> pMoniker;
        if(pFilterEnum->Next(1, &pMoniker, NULL) != S_OK) break;
        if(i == index)
        {
            m_hr = pMoniker->BindToObject(NULL, NULL, IID_IBaseFilter,(void**)&pFilter);
            if(!SUCCEEDED(m_hr))
                throw Exception(m_hr, "Unable to bind filter");
            found = true;
        }   
        i++;
    }

    if(found)
    {
        m_Filters[pszFilterName] = pFilter;
        RefreshPins(pszFilterName);

        m_hr = m_pGraph->AddFilter(pFilter,  CA2W(pszFilterName));
        if(!SUCCEEDED(m_hr))
            throw Exception(m_hr, "Unable to add filter to graph");
        
        return pFilter;

    }
    return 0;
}

void CGraph::AddFilterByCategoryEx(LPCTSTR pPartialVFWName, LPCTSTR pszFilterName, REFGUID guid)
{
    vector<string> Vc;
    EnumFiltersByCategory(Vc, guid);

    for(size_t i = 0; i < Vc.size(); i++)
    {
        if(Vc[i].find(pPartialVFWName) != string::npos)
        {
            if(AddFilterByCategory(i, pszFilterName, guid)) return;
            throw Exception(E_FAIL, "AddFilterByCategoryEx() failed");
        }
    }
    throw Exception(E_FAIL, "No %s filter in given category", pPartialVFWName);
}



//////////////////////////////////////////////////////////////////////////
// Displays a property dialog for a filter
//////////////////////////////////////////////////////////////////////////
void CGraph::ShowPropertyDialog(LPCTSTR pszFilterName, HWND hwnd)
{
    CheckFilter(pszFilterName);
    CComQIPtr<ISpecifyPropertyPages> pProp(m_Filters[pszFilterName]);

    if(pProp) 
    {
        IUnknown *pFilterUnk = GetFilter(pszFilterName);    //->QueryInterface(IID_IUnknown, (void **)&pFilterUnk);
        // Show the page. 
        CAUUID caGUID;
        pProp->GetPages(&caGUID);
        OleCreatePropertyFrame(
            hwnd,                   // Parent window
            0, 0,                   // (Reserved)
            L"Properties",          // Caption for the dialog box
            1,                      // Number of objects (just the filter)
            &pFilterUnk,            // Array of object pointers. 
            caGUID.cElems,          // Number of property pages
            caGUID.pElems,          // Array of property page CLSIDs
            0,                      // Locale identifier
            0, NULL                 // Reserved
            );
    }
}

string CGraph::GUIDToString(GUID *pGUID)
{
    char cTemp[128];
    sprintf_s(cTemp, sizeof(cTemp), "{%08X-%04X-%04X-%02X%02X-%02X%02X%02X%02X%02X%02X}", 
        pGUID->Data1, pGUID->Data2, pGUID->Data3, 
        pGUID->Data4[0], pGUID->Data4[1], pGUID->Data4[2], pGUID->Data4[3], 
        pGUID->Data4[4], pGUID->Data4[5], pGUID->Data4[6], pGUID->Data4[7]);

    return cTemp;
}

//------------------------------------------------------------------------
// Run/Stop/Pause graph
//
#define IFSUCC(x)   m_hr = x; if(SUCCEEDED(m_hr))
#define IFFAIL(x)   m_hr = x; if(FAILED(m_hr))
#define IFS_OK(x)   m_hr = x; if(m_hr == S_OK)

void CGraph::WaitForEndOfStateTransition(LONG msTimeout)
{
    OAFilterState State;
    IFFAIL(m_pMC->GetState(msTimeout, &State)) throw Exception(m_hr, "IMediaControl::GetState() failed");
}

void CGraph::Start()
{
    IFSUCC(m_pMC->Run())
    {
        if(m_hr == S_OK) return;
        
        /* seems like it returned S_FALSE, which means that some filters have not completed the transition to a running state */
        WaitForEndOfStateTransition();

        IFS_OK(m_pMC->Run()) return;
    }
    throw Exception(m_hr, "IMediaControl::Run() failed");
}

void CGraph::Stop(bool NoException)
{
    m_hr = m_pMC->Stop();
    if(NoException == false && FAILED(m_hr)) throw Exception(m_hr, "IMediaControl::Stop() failed");
}

void CGraph::Pause()
{
    IFFAIL(m_pMC->Pause()) throw Exception(m_hr, "IMediaControl::Run() failed");
    if(m_hr != S_OK)
    {
        /* seems like it returned S_FALSE, which means that some filters have not completed the transition to a running state */
        WaitForEndOfStateTransition();
    }
}

//------------------------------------------------------------------------
// QuetyInterface
//
void CGraph::QI(LPCTSTR pszFilterName, REFIID riid, void **pInterface)
{
    m_hr = m_Filters[pszFilterName]->QueryInterface(riid, pInterface);
    if(FAILED(m_hr) || *pInterface == 0) throw Exception(E_NOINTERFACE, "%s->QueryInterface(%s) failed", pszFilterName, GUIDToString((GUID *)&riid).c_str());
}

//------------------------------------------------------------------------
// Set media event sink window
//
void CGraph::SetMediaEventSinkWindow(HWND hNotifyWnd, UINT NotifyMsg)
{
    if(m_pME == 0) throw Exception(E_NOINTERFACE, "No IMediaEventSinkEx interface");
    m_hr = m_pME->SetNotifyWindow((OAHWND)hNotifyWnd, NotifyMsg, 0);
    if(FAILED(m_hr)) throw Exception(m_hr, "IMediaEventSinkEx::SetNotifyWnd() failed");
}

