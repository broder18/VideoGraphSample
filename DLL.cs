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

        public static void Open(string path, Dictionary<ushort, bool> map_pids, RendererConrainerForm[] _renderers)
        {
            if (_renderers == null) return;
            var settings = new GsSettings();
            settings.Size = (uint)Marshal.SizeOf(settings);
            settings.filePath = path;
            AddItemStruct(_renderers, ref settings);
            if (!NativeMethods.gsOpenRefact(ref settings)) throw new Exception("gsOpenrefact() failed: " + GetLastError());
        }  
        

        private static void AddItemStruct(RendererConrainerForm[] _renderers, ref GsSettings settings)
        {
            foreach(var item in _renderers)
            {
                if (AddPidHWND(ref settings.VideoPid.pid0, ref settings.hContainerWnds.hwnd0, item)) continue;
                if (AddPidHWND(ref settings.VideoPid.pid1, ref settings.hContainerWnds.hwnd1, item)) continue;
                if (AddPidHWND(ref settings.VideoPid.pid2, ref settings.hContainerWnds.hwnd2, item)) continue;
                if (AddPidHWND(ref settings.VideoPid.pid3, ref settings.hContainerWnds.hwnd3, item)) continue;
                if (AddPidHWND(ref settings.VideoPid.pid4, ref settings.hContainerWnds.hwnd4, item)) continue;
            }
        }

        private static bool AddPidHWND(ref ushort pid, ref IntPtr hwnd, RendererConrainerForm item)
        {
            if (pid != 0) return false;
            pid = Convert.ToUInt16(item.Name, 16);
            hwnd = item.Handle;
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

        public static void SetParams(ushort text_alpha, ushort x, ushort y)
        {
            var text_atr = new TextParams { alpha = text_alpha, position_x = x, position_y = y };
            text_atr.size = (ushort)Marshal.SizeOf(text_atr);
            if (!NativeMethods.gsSetPMTParams(ref text_atr)) throw new Exception("gsSetPMTParams() failed: " + GetLastError());
        }

    }
}