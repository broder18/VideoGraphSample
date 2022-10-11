#include "CMediaSeekingProxy.h"

CMediaSeekingProxy::CMediaSeekingProxy(IMediaSeeking* delegate)
	: m_delegate(delegate)
{}

CMediaSeekingProxy::~CMediaSeekingProxy()
{}

void CMediaSeekingProxy::SetSeeker(IMediaSeeking* seeker)
{
	m_delegate = seeker;
}

HRESULT STDMETHODCALLTYPE CMediaSeekingProxy::QueryInterface(REFIID riid, LPVOID* ppvObj)
{
	if (IID_IMediaSeeking == riid)
		return GetInterface(static_cast<IMediaSeeking*>(this), ppvObj);
	return E_NOINTERFACE;
}

ULONG STDMETHODCALLTYPE CMediaSeekingProxy::AddRef()
{
	return MediaSeeking()->AddRef();
}

STDMETHODIMP CMediaSeekingProxy::GetCapabilities(DWORD* pCapabilities)
{
	return MediaSeeking()->GetCapabilities(pCapabilities);
}

STDMETHODIMP CMediaSeekingProxy::CheckCapabilities(DWORD* pCapabilities)
{
	return MediaSeeking()->CheckCapabilities(pCapabilities);
}

STDMETHODIMP CMediaSeekingProxy::IsFormatSupported(const GUID* pFormat)
{
	return MediaSeeking()->IsFormatSupported(pFormat);
}

STDMETHODIMP CMediaSeekingProxy::QueryPreferredFormat(GUID* pFormat)
{
	return MediaSeeking()->QueryPreferredFormat(pFormat);
}

STDMETHODIMP CMediaSeekingProxy::SetTimeFormat(const GUID* pFormat)
{
#ifdef ENABLE_SET
	return MediaSeeking()->SetTimeFormat(pFormat);
#else
	return S_OK;
#endif
}

STDMETHODIMP CMediaSeekingProxy::GetTimeFormat(GUID* pFormat)
{
	return MediaSeeking()->GetTimeFormat(pFormat);
}

STDMETHODIMP CMediaSeekingProxy::GetDuration(LONGLONG* pDuration)
{
	return MediaSeeking()->GetDuration(pDuration);
}

STDMETHODIMP CMediaSeekingProxy::GetStopPosition(LONGLONG* pStop)
{
	return MediaSeeking()->GetStopPosition(pStop);
}

STDMETHODIMP CMediaSeekingProxy::GetCurrentPosition(LONGLONG* pCurrent)
{
	return MediaSeeking()->GetCurrentPosition(pCurrent);
}

STDMETHODIMP CMediaSeekingProxy::ConvertTimeFormat(LONGLONG* pTarget, const GUID* pTargetFormat, LONGLONG Source, const GUID* pSourceFormat)
{
	return MediaSeeking()->ConvertTimeFormat(pTarget, pTargetFormat, Source, pSourceFormat);
}

STDMETHODIMP CMediaSeekingProxy::SetPositions(LONGLONG* pCurrent, DWORD dwCurrentFlags, LONGLONG* pStop, DWORD dwStopFlags)
{
#ifdef ENABLE_SET
	return MediaSeeking()->SetPositions(pCurrent, dwCurrentFlags, pStop, dwStopFlags);
#else
	return S_OK;
#endif
}

STDMETHODIMP CMediaSeekingProxy::GetPositions(LONGLONG* pCurrent, LONGLONG* pStop)
{
	return MediaSeeking()->GetPositions(pCurrent, pStop);
}

STDMETHODIMP CMediaSeekingProxy::GetAvailable(LONGLONG* pEarliest, LONGLONG* pLatest)
{
	return MediaSeeking()->GetAvailable(pEarliest, pLatest);
}

STDMETHODIMP CMediaSeekingProxy::SetRate(double dRate)
{
#ifdef ENABLE_SET
	return MediaSeeking()->SetRate(dRate);
#else
	return S_OK;
#endif
}

STDMETHODIMP CMediaSeekingProxy::GetRate(double* dRate)
{
	return MediaSeeking()->GetRate(dRate);
}

STDMETHODIMP CMediaSeekingProxy::GetPreroll(LONGLONG* pllPreroll)
{
	return MediaSeeking()->GetPreroll(pllPreroll);
}

STDMETHODIMP CMediaSeekingProxy::IsUsingTimeFormat(const GUID* pFormat)
{
	return MediaSeeking()->IsUsingTimeFormat(pFormat);
}