using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;


namespace VideoGraphSample
{
    public static class Dll
    {
        private static bool _dllInitialized;

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public unsafe struct AllChannels
        {
            public int NumVideoPids;
            public fixed int Pids[Defines.MaxChannels];
            public fixed int Pmts[Defines.MaxChannels];
            public fixed int hWnds[Defines.MaxChannels];
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct BvpSettings
        {
            public uint Size;
            public AllChannels Channels;
        }

        private static class NativeMethods
        {
            [DllImport("GraphSampleDLL.dll", EntryPoint = "gsInitialize", CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool gsInitialize();

            [DllImport("GraphSampleDLL.dll", EntryPoint = "gsUninitialize", CallingConvention = CallingConvention.StdCall)]
            public static extern void gsUninitialize();

            [DllImport("GraphSampleDLL.dll", EntryPoint = "gsGetLastError", CallingConvention = CallingConvention.StdCall)]
            public static extern IntPtr gsGetLastError();

            [DllImport("GraphSampleDLL.dll", EntryPoint = "gsClose", CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern void gsClose();

            [DllImport("GraphSampleDLL.dll", EntryPoint = "gsResizeRenderer", CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool gsResizeRenderer(IntPtr hContainerWnd);

            [DllImport("GraphSampleDLL.dll", EntryPoint = "gsOpenRefact", CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool gsOpenRefact(ref GsSettings settings);

            [DllImport("GraphSampleDLL.dll", EntryPoint = "gsSetPMTParams", CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool gsSetPMTParams(ref TextParams textParams);

            [DllImport("GraphSampleDLL.dll", EntryPoint = "gsGetPositionTrackBar", CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool gsGetPositionTrackBar(ref ushort percent);

            [DllImport("GraphSampleDLL.dll", EntryPoint = "gsSetPositionTrackBar", CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool gsSetPositionTrackBar(ushort percent);

            [DllImport("GraphSampleDLL.dll", EntryPoint = "gsSetStart", CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool gsSetStart();

            [DllImport("GraphSampleDLL.dll", EntryPoint = "gsSetPause", CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool gsSetPause();

            [DllImport("GraphSampleDLL.dll", EntryPoint = "gsSetStop", CallingConvention = CallingConvention.StdCall)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool gsSetStop();

        }

        private static string GetLastError()
        {
            return Marshal.PtrToStringAnsi(NativeMethods.gsGetLastError());
        }

        public static void Initialize()
        {
            if(!_dllInitialized)
            {
                if (!NativeMethods.gsInitialize()) throw new Exception("gsInitialize() failed: " + GetLastError());
                _dllInitialized = true;
            }
        }

        public static void Uninitialize()
        {
            if(_dllInitialized)
            {
                NativeMethods.gsUninitialize();
                _dllInitialized = false;
            }
        }

        public static void Open(string path, Dictionary<ushort, bool> mapPids, RendererContainerForm[] renderers)
        {
            if (renderers == null) return;
            var settings = new GsSettings();
            settings.Size = (uint)Marshal.SizeOf(settings);
            settings.filePath = path;
            AddItemStruct(renderers, ref settings);
            if (!NativeMethods.gsOpenRefact(ref settings)) throw new Exception("gsOpenrefact() failed: " + GetLastError());
            
        }  
        

        private static void AddItemStruct(RendererContainerForm[] renderers, ref GsSettings settings)
        {
            foreach(var item in renderers)
            {
                if (AddPidHwnd(ref settings.VideoPid.pid0, ref settings.hContainerWnds.hwnd0, item)) continue;
                if (AddPidHwnd(ref settings.VideoPid.pid1, ref settings.hContainerWnds.hwnd1, item)) continue;
                if (AddPidHwnd(ref settings.VideoPid.pid2, ref settings.hContainerWnds.hwnd2, item)) continue;
                if (AddPidHwnd(ref settings.VideoPid.pid3, ref settings.hContainerWnds.hwnd3, item)) continue;
                if (AddPidHwnd(ref settings.VideoPid.pid4, ref settings.hContainerWnds.hwnd4, item)) continue;
            }
        }

        private static bool AddPidHwnd(ref ushort pid, ref IntPtr hwnd, RendererContainerForm item)
        {
            if (pid != 0) return false;
            pid = Convert.ToUInt16(item.Name, 16);
            hwnd = item.GetPictureBoxHandle();
            //hwnd = item.Handle;
            return true;
        }



        public static void Close()
        {
            NativeMethods.gsClose();
        }

        public static void Resize(IntPtr hwnd)
        {
            if (!NativeMethods.gsResizeRenderer(hwnd)) throw new Exception("gsResizeRenderer() failed: " + GetLastError());
        }

        public static void SetParams(ushort textAlpha, ushort x, ushort y)
        {
            var textAtr = new TextParams { alpha = textAlpha, position_x = x, position_y = y };
            textAtr.size = (ushort)Marshal.SizeOf(textAtr);
            if (!NativeMethods.gsSetPMTParams(ref textAtr)) throw new Exception("gsSetPMTParams() failed: " + GetLastError());
        }

        public static void GetPositionTrackBar(ref ushort percent)
        {
            if (!NativeMethods.gsGetPositionTrackBar(ref percent)) throw new Exception("gsGetPositionTrackBar() failed: " + GetLastError());
        }

        public static void SetPositionTrackBar(ushort percent)
        {
            if (!NativeMethods.gsSetPositionTrackBar(percent)) throw new Exception("gsSetPositionTrackBar() failed: " + GetLastError());
        }

        public static void SetStart()
        {
            if (!NativeMethods.gsSetStart()) throw new Exception("gsSetStart() failed: " + GetLastError());
        }

        public static void SetPause()
        {
            if (!NativeMethods.gsSetPause()) throw new Exception("gsSetPause() failed: " + GetLastError());
        }

        public static void SetStop()
        {
            if (!NativeMethods.gsSetStop()) throw new Exception("gsSetStop() failed: " + GetLastError());
        }

    }
}