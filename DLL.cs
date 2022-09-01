using System;
using System.Runtime.InteropServices;


namespace VideoGraphSample
{
    public static class Dll
    {
        private static bool _dllInitialized;

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct TextParams
        {
            public ushort size;
            public ushort alpha;
            public ushort position_x;
            public ushort position_y;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct Pids
        {
            public ushort pid0;
            public ushort pid1;
            public ushort pid2;
            public ushort pid3;
            public ushort pid4;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct Hwnds
        {
            public IntPtr hwnd0;
            public IntPtr hwnd1;
            public IntPtr hwnd2;
            public IntPtr hwnd3;
            public IntPtr hwnd4;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct GsSettings
        {
            public uint Size;
            public string filePath;
            //System.Text.StringBuilder filePath;
            public Hwnds hContainerWnds;
            public Pids VideoPid;
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
            public static extern bool gsSetPMTParams(ref TextParams text_params);
        }

        private static string GetLastError()
        {
            return Marshal.PtrToStringAnsi(NativeMethods.gsGetLastError());
        }

        //Подумать ещё раз над инициализацией
        public static void Initialize()
        {
            if(!_dllInitialized)
            {
                if (!NativeMethods.gsInitialize()) throw new Exception("gsInitialize() failed: " + GetLastError());
                _dllInitialized = true;
            }
        }

        //Подумать надо деинсталляцией
        public static void Uninitialize()
        {
            if(_dllInitialized)
            {
                NativeMethods.gsUninitialize();
                _dllInitialized = false;
            }
        }

        public static void Open(string path, ushort[] pids, IntPtr[] hWnds)
        {
            var settings = new GsSettings();
            settings.Size = (uint)Marshal.SizeOf(settings);
            settings.filePath = path;
            Console.WriteLine(path);
            Add_Pids(ref settings, pids);
            Add_HWNDS(ref settings, hWnds);
            if (!NativeMethods.gsOpenRefact(ref settings)) throw new Exception("gsOpenrefact() failed: " + GetLastError());
        }

        private static void Add_Pids(ref GsSettings settings, ushort[] pids)
        {
            settings.VideoPid.pid0 = pids[0];
            settings.VideoPid.pid1 = pids[1];
            settings.VideoPid.pid2 = pids[2];
            settings.VideoPid.pid3 = pids[3];
            settings.VideoPid.pid4 = pids[4];
        }

        private static void Add_HWNDS(ref GsSettings settings, IntPtr[] hWnds)
        {
            settings.hContainerWnds.hwnd0 = hWnds[0];
            settings.hContainerWnds.hwnd1 = hWnds[1];
            settings.hContainerWnds.hwnd2 = hWnds[2];
            settings.hContainerWnds.hwnd3 = hWnds[3]; 
            settings.hContainerWnds.hwnd4 = hWnds[4];
        }

        public static void Close()
        {
            NativeMethods.gsClose();
        }

        public static void Resize(IntPtr hwnd)
        {
            if (!NativeMethods.gsResizeRenderer(hwnd)) throw new Exception("gsResizeRenderer() failed: " + GetLastError());
        }

        public static void SetParams(ushort text_alpha, ushort x, ushort y)
        {
            var text_atr = new TextParams { alpha = text_alpha, position_x = x, position_y = y };
            text_atr.size = (ushort)Marshal.SizeOf(text_atr);
            if (!NativeMethods.gsSetPMTParams(ref text_atr)) throw new Exception("gsSetPMTParams() failed: " + GetLastError());
        }

    }
}