#ifndef IOSTRUCTS_H
#define IOSTRUCTS_H

#pragma pack(push, 4)

static constexpr int MAX_CHANNELS = 8;

typedef struct tagChannels
{
    int NumVideoPids;
    int pids[MAX_CHANNELS];
    int pmts[MAX_CHANNELS];
    HWND hwnds[MAX_CHANNELS];
} CHANNELS;

typedef struct tagBVP_Settings
{
    DWORD Size;
    CHANNELS AllChannels;
} BVP_SETTINGS;

#pragma pack(pop)

#endif