using System;
using System.Runtime.InteropServices;

namespace BIONVideoPlayer
{
    public static class Dll
    {
        private static bool _dllInitialized;
        public static bool DllOpened;

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public unsafe struct AllChannels
        {
            public int NumVideoPids;
            public fixed int Pids[Defines.MaxChannels];
            public fixed int Pmts[Defines.MaxChannels];
            public fixed int hWnds[Defines.MaxChannels];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct BvpSettings
        {
            public uint Size;
            public AllChannels Channels;
        }

        private static class NativeMethods
        {
            [DllImport("BIONVideoPlayerDLL.dll", EntryPoint = "bvpInitialize", CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool bvpInitialize();

            [DllImport("BIONVideoPlayerDLL.dll", EntryPoint = "bvpUninitialize", CallingConvention = CallingConvention.StdCall)]
            public static extern void bvpUninitialize();

            [DllImport("BIONVideoPlayerDLL.dll", EntryPoint = "bvpGetLastError", CallingConvention = CallingConvention.StdCall)]
            public static extern IntPtr bvpGetLastError();

            [DllImport("BIONVideoPlayerDLL.dll", EntryPoint = "bvpClose", CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern void bvpClose();

            [DllImport("BIONVideoPlayerDLL.dll", EntryPoint = "bvpResizeRenderer", CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool bvpResizeRenderer(IntPtr hContainerWnd);

            [DllImport("BIONVideoPlayerDLL.dll", EntryPoint = "bvpOpen", CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool bvpOpen(ref BvpSettings settings, string fileName);

            [DllImport("BIONVideoPlayerDLL.dll", EntryPoint = "bvpGetPositionTrackBar", CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool bvpGetPositionTrackBar(ref ushort percent);

            [DllImport("BIONVideoPlayerDLL.dll", EntryPoint = "bvpSetPositionTrackBar", CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool bvpSetPositionTrackBar(ushort percent);

            [DllImport("BIONVideoPlayerDLL.dll", EntryPoint = "bvpSetStart", CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool bvpSetStart();

            [DllImport("BIONVideoPlayerDLL.dll", EntryPoint = "bvpSetPause", CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool bvpSetPause();

            [DllImport("BIONVideoPlayerDLL.dll", EntryPoint = "bvpSetStop", CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool bvpSetStop();

            [DllImport("BIONVideoPlayerDLL.dll", EntryPoint = "bvpSetTelemetryPosition", CallingConvention = CallingConvention.StdCall)]
            public static extern void bvpSetTelemetryPosition(int x, int y);

            [DllImport("BIONVideoPlayerDLL.dll", EntryPoint = "bvpSetTelemetryAlpha")]
            public static extern void bvpSetTelemetryAlpha(int alpha);

            [DllImport("BIONVideoPlayerDLL.dll", EntryPoint = "bvpMapUnmapChannel", CallingConvention = CallingConvention.StdCall)]
            public static extern void bvpMapUnmapChannel(int ch, bool map);

            [DllImport("BIONVideoPlayerDLL.dll", EntryPoint = "bvpSetTelemetryColors", CallingConvention = CallingConvention.StdCall)]
            public static extern void bvpSetTelemetryColors(uint txtColor, uint bkgColor);

            [DllImport("BIONVideoPlayerDLL.dll", EntryPoint = "bvpEnableTelemetry", CallingConvention = CallingConvention.StdCall)]
            public static extern void bvpEnableTelemetry(int enable);
        }

        private static string GetLastError()
        {
            return Marshal.PtrToStringAnsi(NativeMethods.bvpGetLastError());
        }

        public static void Initialize()
        {
            if(!_dllInitialized)
            {
                if (!NativeMethods.bvpInitialize()) throw new Exception("bvpInitialize() failed: " + GetLastError());
                _dllInitialized = true;
            }
        }

        public static void Uninitialize()
        {
            if(_dllInitialized)
            {
                NativeMethods.bvpUninitialize();
                _dllInitialized = false;
            }
        }

        public static void Open(string path, AllChannels channels)
        {
            if (channels.NumVideoPids < 1) throw new Exception("Not found pids !");
            var settings = new BvpSettings();
            settings.Size = (uint)Marshal.SizeOf(settings);
            settings.Channels = channels;

            if (!NativeMethods.bvpOpen(ref settings, path)) throw new Exception("bvpOpen() failed: " + GetLastError());
            DllOpened = true;

        }

        public static void Close()
        {
            NativeMethods.bvpClose();
            DllOpened = false;
        }

        public static void Resize(IntPtr hwnd)
        {
            if (!DllOpened) return; 
            if (!NativeMethods.bvpResizeRenderer(hwnd)) throw new Exception("bvpResizeRenderer() failed: " + GetLastError());
        }

        public static void UpdateTelemetryPosition()
        {
            if (!DllOpened) return;
            NativeMethods.bvpSetTelemetryPosition(AllSettings.TelemetryPosX, AllSettings.TelemetryPosY);
        }

        public static void UpdateTelemetryAlpha()
        {
            if (!DllOpened) return;
            NativeMethods.bvpSetTelemetryAlpha(AllSettings.TelemetryAlpha);
        }

        public static void UpdateTelemetryColors()
        {
            if (!DllOpened) return;
            NativeMethods.bvpSetTelemetryColors(AllSettings.TelemetryTxtColor, AllSettings.TelemetryBkgColor);
        }

        public static void UpdateTelemetryEnable()
        {
            NativeMethods.bvpEnableTelemetry(AllSettings.EnableTelemetry);
        }

        public static void MapUnmapChannel(int channel, bool map)

        {
            NativeMethods.bvpMapUnmapChannel(channel, map);
        }
        public static void GetPositionTrackBar(ref ushort percent)
        {
            if (!NativeMethods.bvpGetPositionTrackBar(ref percent)) throw new Exception("bvpGetPositionTrackBar() failed: " + GetLastError());
        }

        public static void SetPositionTrackBar(ushort percent)
        {
            if (!NativeMethods.bvpSetPositionTrackBar(percent)) throw new Exception("bvpSetPositionTrackBar() failed: " + GetLastError());
        }

        public static void SetStart()
        {
            if (!NativeMethods.bvpSetStart()) throw new Exception("bvpSetStart() failed: " + GetLastError());
        }

        public static void SetPause()
        {
            if (!NativeMethods.bvpSetPause()) throw new Exception("bvpSetPause() failed: " + GetLastError());
        }

        public static void SetStop()
        {
            if (!NativeMethods.bvpSetStop()) throw new Exception("bvpSetStop() failed: " + GetLastError());
        }

    }
}