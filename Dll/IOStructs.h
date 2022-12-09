#ifndef _IOSTRUCTS_H
#define _IOSTRUCTS_H

#include <PshPack4.h>

static constexpr int MAX_CHANNELS = 8;

typedef struct tagChannels
{
    int NumVideoPids;
    int NumPMTs;
    int pids[MAX_CHANNELS];
    int pmts[MAX_CHANNELS];
    HWND hwnds[MAX_CHANNELS];
} CHANNELS;

typedef struct tagBVP_Settings
{
    DWORD Size;
    char* fileName;
    CHANNELS AllChannels;
} BVP_SETTINGS;

#include <PopPack.h>

#endif