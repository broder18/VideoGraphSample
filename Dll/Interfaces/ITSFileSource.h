/**
*  ITSFileSource.h
*  Copyright (C) 2003      bisswanger
*  Copyright (C) 2004-2006 bear
*  Copyright (C) 2005      nate
*
*  This file is part of TSFileSource, a directshow push source filter that
*  provides an MPEG transport stream output.
*
*  TSFileSource is free software; you can redistribute it and/or modify
*  it under the terms of the GNU General Public License as published by
*  the Free Software Foundation; either version 2 of the License, or
*  (at your option) any later version.
*
*  TSFileSource is distributed in the hope that it will be useful,
*  but WITHOUT ANY WARRANTY; without even the implied warranty of
*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*  GNU General Public License for more details.
*
*  You should have received a copy of the GNU General Public License
*  along with TSFileSource; if not, write to the Free Software
*  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
*
*  bisswanger can be reached at WinSTB@hotmail.com
*    Homepage: http://www.winstb.de
*
*  bear and nate can be reached on the forums at
*    http://forums.dvbowners.com/
*/
#ifndef ITSFILESOURCE_H
#define ITSFILESOURCE_H

//DEFINE_GUID(CLSID_TSFileSource,
//0x4f8bf30c, 0x3beb, 0x43a3, 0x8b, 0xf2, 0x10, 0x9, 0x6f, 0xd2, 0x8c, 0xf2);
// {559E6E81-FAC4-4EBC-9530-662DAA27EDC2}
DEFINE_GUID(IID_ITSFileSource,
0x559e6e81, 0xfac4, 0x4ebc, 0x95, 0x30, 0x66, 0x2d, 0xaa, 0x27, 0xed, 0xc2);

DECLARE_INTERFACE_(ITSFileSource, IUnknown) //compatable to 2.0.1.7 official release
{
	
	STDMETHOD(Load) (THIS_ LPCOLESTR pszFileName,const AM_MEDIA_TYPE *pmt) PURE;
	//New method for TS Decoder
	STDMETHOD(GetPosition) (THIS_ LONGLONG* start, LONGLONG* stop) PURE;
	STDMETHOD(SetPosition) (THIS_ LONGLONG* current, LONGLONG* stop) PURE;
};
#endif
