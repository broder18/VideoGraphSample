#pragma once

extern LPCTSTR TsPushSourceName;
extern LPCTSTR UDPLocalSourceName;
extern LPCTSTR RTPSourceName;
extern LPCTSTR DemuxName;

extern LPCTSTR VideoDecoderName0;
extern LPCTSTR VideoDecoderName1;
extern LPCTSTR VideoDecoderName2;
extern LPCTSTR VideoDecoderName3;
extern LPCTSTR VideoDecoderName4;

extern LPCTSTR FFDShowVideoDecoderName;
extern LPCTSTR FFDShowRawVideoFilterName;

extern LPCTSTR VideoRendererName0;
extern LPCTSTR VideoRendererName1;
extern LPCTSTR VideoRendererName2;
extern LPCTSTR VideoRendererName3;
extern LPCTSTR VideoRendererName4;

extern LPCTSTR PMTPvtDataName;


//------------------------------------------------------------------------
// FFDShow/Extra Filters guids
//
// {04FE9017-F873-410E-871E-AB91661A4EF7}
DEFINE_GUID(CLSID_ffdshowVideoDecoder, 0x04FE9017, 0xF873, 0x410E, 0x87, 0x1E, 0xAB, 0x91, 0x66, 0x1A, 0x4E, 0xF7);

// {0B390488-D80F-4A68-8408-48DC199F0E97}
DEFINE_GUID(CLSID_ffdshowRawVideoFilter, 0x0B390488, 0xD80F, 0x4A68, 0x84, 0x08, 0x48, 0xDC, 0x19, 0x9F, 0x0E, 0x97);

// EE30215D-164F-4A92-A4EB-9D4C13390F9F
DEFINE_GUID(CLSID_LAVVideo, 0xEE30215D, 0x164F, 0x4A92, 0xA4, 0xEB, 0x9D, 0x4C, 0x13, 0x39, 0x0F, 0x9F);
