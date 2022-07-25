#include "GraphSamplePvt.h"
#include "MediaTypes.h"
#include "Defaults.h"

//------------------------------------------------------------------------
// Allocate format buffer
//
void AllocateFormatBuffer(CMediaType *pMt, ULONG Size)
{
    BYTE *pFmt = pMt->AllocFormatBuffer(Size);
    if(pFmt == nullptr) THROW("AllocFormatBuffer() failed");
    memset(pFmt, 0, Size);
}

//------------------------------------------------------------------------
// Set types
//
void SetTypes(CMediaType *pMt, const GUID *pType, const GUID *pSubType, const GUID *pFmtType)
{
    pMt->SetType(pType);
    pMt->SetSubtype(pSubType);
    pMt->SetFormatType(pFmtType);
}

//------------------------------------------------------------------------
// Prepare MPEG-2 media type
//
void PrepareMediaTypeMPEG2(CMediaType *pMt)
{
    SetTypes(pMt, &MEDIATYPE_Video, &MEDIASUBTYPE_MPEG2_VIDEO, &FORMAT_MPEG2Video);
    AllocateFormatBuffer(pMt, sizeof(MPEG2VIDEOINFO));

    MPEG2VIDEOINFO *pMVIH = (MPEG2VIDEOINFO *)pMt->pbFormat;

    pMVIH->hdr.AvgTimePerFrame = 333333;  // 30 fps.
    pMVIH->hdr.dwPictAspectRatioX = 1;
    pMVIH->hdr.dwPictAspectRatioY = 1;

    pMVIH->hdr.bmiHeader.biSize = sizeof(BITMAPINFOHEADER);
    pMVIH->hdr.bmiHeader.biWidth = OUTPUT_WIDTH;
    pMVIH->hdr.bmiHeader.biHeight = OUTPUT_HEIGHT;

    pMVIH->dwLevel = AM_MPEG2Profile_Main;
    pMVIH->dwProfile = AM_MPEG2Level_Main;
}

void PrepareMediaTypeTS(CMediaType *pMt)
{
    SetTypes(pMt, &MEDIATYPE_Stream, &MEDIASUBTYPE_MPEG2_TRANSPORT, &FORMAT_None);
}

//------------------------------------------------------------------------
// Prepare media type for given stream
//
void PrepareMediaType(CMediaType *pMt)
{
    PrepareMediaTypeMPEG2(pMt);
}
