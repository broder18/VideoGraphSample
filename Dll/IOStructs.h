#ifndef _IOSTRUCTS_H
#define _IOSTRUCTS_H

#include <PshPack4.h>

typedef struct tagTEXT_PARAMS
{
    WORD size;
    WORD alpha;
    WORD x;
    WORD y;
} TEXT_PARAMS;

typedef struct tagINPUT_NETWORK
{
    DWORD MulticastIP;
    WORD MulticastPort;
} INPUT_NETWORK;

typedef struct tagHCONTAINER_WND
{
    HWND hContainerWnd0;
    HWND hContainerWnd1;
    HWND hContainerWnd2;
    HWND hContainerWnd3;
    HWND hContainerWnd4;
} HCONTAINER_WND;

typedef struct tagPIDS
{
    WORD pidV0;
    WORD pidV1;
    WORD pidV2;
    WORD pidV3;
    WORD pidV4;
} PIDS;

typedef struct tagGS_SettingsRefact
{
    DWORD Size;
    INPUT_NETWORK InNet;
    HCONTAINER_WND hWnd;
    PIDS V_Pids;
} GS_SETTINGSRefact;

#include <PopPack.h>

#endif