#pragma once

#include <exception>

//////////////////////////////////////////////////////////////////////////
//  Wrapper exception class, constructor uses printf style arguments
//////////////////////////////////////////////////////////////////////////
class Exception : public std::exception
{
public:
    std::string m_what;
    HRESULT m_hr;

    Exception(HRESULT hResult, const char* fmt, ...)
    {
        m_hr = hResult;

        try
        {
            char pBuffer[1024] = {0};

            va_list args;
            va_start(args, fmt);
            _vsnprintf_s(pBuffer, sizeof(pBuffer), sizeof(pBuffer) - 1, fmt, args);
            va_end(args);

            m_what = pBuffer;
            
            // Convert windows error code to a Message
            if(hResult) 
            {
                LPSTR lpMsgBuf;
                sprintf_s(pBuffer, sizeof(pBuffer), " Error code : %X ", hResult);
                m_what += pBuffer;
                FormatMessage(FORMAT_MESSAGE_ALLOCATE_BUFFER | FORMAT_MESSAGE_FROM_SYSTEM | FORMAT_MESSAGE_IGNORE_INSERTS, NULL, hResult, MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT), (LPSTR)&lpMsgBuf, 0, NULL);
                if(lpMsgBuf)
                {
                    m_what += lpMsgBuf;
                    LocalFree(lpMsgBuf);
                }
            }
        }
        catch(...)
        {
            m_what = "Failed in Exception::Exception()";
        }

        if(m_hr == 0) m_hr = E_UNEXPECTED; // Unknown error means unexpected
    }
};
