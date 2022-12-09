#pragma once

class CAWString
{
	static constexpr int MaxLen = 1024;

	char ABuffer[MaxLen]{};
	wchar_t WBuffer[MaxLen]{};

public:
    CAWString(const char* BaseName, int Idx)
    {
        sprintf_s(ABuffer, "%s%d", BaseName, Idx);
        swprintf_s(WBuffer, L"%S%d", BaseName, Idx);
    }

    CAWString(const char* BaseName)
    {
        sprintf_s(ABuffer, "%s", BaseName);
        swprintf_s(WBuffer, L"%S", BaseName);
    }

    operator char * () { return ABuffer; }
    operator wchar_t * () { return WBuffer; }
};