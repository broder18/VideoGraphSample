#ifndef __MEDIA_SEEKING_PROXY_H__
#define __MEDIA_SEEKING_PROXY_H__

#include <streams.h>
#include <assert.h>
#include <atlcomcli.h>

class CMediaSeekingProxy : public IMediaSeeking
{
private:
	IMediaSeeking* m_delegate;
	IMediaSeeking* MediaSeeking()
	{
		assert(m_delegate);
		return m_delegate;
	}

	CMediaSeekingProxy();
	CMediaSeekingProxy(const CMediaSeekingProxy& copy);
public:
	CMediaSeekingProxy(IMediaSeeking* delegate);
	virtual ~CMediaSeekingProxy(void);
	void SetSeeker(IMediaSeeking* seeker);

	//Methods of IUnknown
	HRESULT STDMETHODCALLTYPE QueryInterface(REFIID riid, LPVOID* ppvObj);
	ULONG STDMETHODCALLTYPE AddRef();
	ULONG STDMETHODCALLTYPE Release();

	//IMediaSeeking Interface
	virtual STDMETHODIMP GetCapabilities(DWORD* pCapabilities);
	virtual STDMETHODIMP CheckCapabilities(DWORD* pCapabilities);
	virtual STDMETHODIMP IsFormatSupported(const GUID* pFormat);
	virtual STDMETHODIMP QueryPreferredFormat(GUID* pFormat);
	virtual STDMETHODIMP SetTimeFormat(const GUID* pFormat);
	virtual STDMETHODIMP GetTimeFormat(GUID* pFormat);
	virtual STDMETHODIMP GetDuration(LONGLONG* pDuration);
	virtual STDMETHODIMP GetStopPosition(LONGLONG* pStop);
	virtual STDMETHODIMP GetCurrentPosition(LONGLONG* pCurrent);
	virtual STDMETHODIMP ConvertTimeFormat(LONGLONG* pTarget, const GUID* pTargetFormat, LONGLONG Source, const GUID* pSourceFormat);
	virtual STDMETHODIMP SetPositions(LONGLONG* pCurrent, DWORD dwCurrentFlags, LONGLONG* pStop, DWORD dwStopFlags);
	virtual STDMETHODIMP GetPositions(LONGLONG* pCurrent, LONGLONG* pStop);
	virtual STDMETHODIMP GetAvailable(LONGLONG* pEarliest, LONGLONG* pLatest);
	virtual STDMETHODIMP SetRate(double dRate);
	virtual STDMETHODIMP GetRate(double* dRate);
	virtual STDMETHODIMP GetPreroll(LONGLONG* pllPreroll);
	virtual STDMETHODIMP IsUsingTimeFormat(const GUID* pFormat);
};
#endif
