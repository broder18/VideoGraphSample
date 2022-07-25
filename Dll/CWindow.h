#pragma once

class CWindow
{
private:
    static int RegCount;

protected:
    HWND hWnd;
    DWORD Style;
    DWORD StyleEx;
    int X, Y, W, H;
    HWND hParent;

    void RegisterClass();
    void UnregisterClass();
    void DestroyWindow();
    void MessageLoop();
    void PostStopMessage();

public:
    CWindow(DWORD AStyle, DWORD AStyleEx);
    ~CWindow();

    virtual LRESULT WndProc(UINT Msg, WPARAM wParam, LPARAM lParam);
    virtual void OnCreate(HWND hNewWnd);

    void Process();
    void SetPos(int AX, int AY);
    void SetSize(int AW, int AH);
    void DoCreateWindow();
    void SetAbove(HWND hAboveWnd);
    void SetParent(HWND hAParent);
};