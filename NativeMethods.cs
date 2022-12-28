using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VideoGraphSample
{
    internal static class NativeMethods
    {
        #region SDK constants

        private const int WM_CHANGEUISTATE = 0x0127;
        private const int UIS_CLEAR = 2;
        private const int UISF_HIDEFOCUS = 0x1;
        private const int UISF_HIDEACCEL = 0x2;

        private const int HWND_TOP = 0;
        private const int HWND_BOTTOM = 1;
        private const int HWND_TOPMOST = -1;
        private const int HWND_NOTOPMOST = -2;

        private const int SWP_NOSIZE = 0x0001;
        private const int SWP_NOMOVE = 0x0002;
        private const int SWP_NOZORDER = 0x0004;
        private const int SWP_NOREDRAW = 0x0008;
        private const int SWP_NOACTIVATE = 0x0010;
        private const int SWP_DRAWFRAME = 0x0020;
        private const int SWP_FRAMECHANGED = 0x0020;
        private const int SWP_SHOWWINDOW = 0x0040;
        private const int SWP_HIDEWINDOW = 0x0080;
        private const int SWP_NOCOPYBITS = 0x0100;
        private const int SWP_NOOWNERZORDER = 0x0200;
        private const int SWP_NOREPOSITION = 0x0200;
        private const int SWP_NOSENDCHANGING = 0x0400;
        private const int SWP_DEFERERASE = 0x2000;
        private const int SWP_ASYNCWINDOWPOS = 0x4000;

        public const int WmSizing = 0x214;
        public const int WmMoving = 0x216;
        public const int WmErasebkgnd = 0x14;

        public const int WmszLeft = 1;
        public const int WmszRight = 2;
        public const int WmszTop = 3;
        public const int WmszTopleft = 4;
        public const int WmszTopright = 5;
        public const int WmszBottom = 6;
        public const int WmszBottomleft = 7;
        public const int WmszBottomright = 8;

        public const int WmLbuttondblclk = 0x203;

        /* class styles */
        public const int CsVredraw = 0x0001;
        public const int CsHredraw = 0x0002;
        public const int CsDblclks = 0x0008;
        public const int CsOwndc = 0x0020;
        public const int CsClassdc = 0x0040;
        public const int CsParentdc = 0x0080;
        public const int CsNoclose = 0x0200;
        public const int CsSavebits = 0x0800;
        public const int CsBytealignclient = 0x1000;
        public const int CsBytealignwindow = 0x2000;
        public const int CsGlobalclass = 0x4000;
        public const int CsIme = 0x00010000;
        public const int CsDropshadow = 0x00020000;

        /* window styles */
        public const int WsOverlapped = 0x00000000;
        public const int WsPopup = unchecked((int)0x80000000);
        public const int WsChild = 0x40000000;
        public const int WsMinimize = 0x20000000;
        public const int WsVisible = 0x10000000;
        public const int WsDisabled = 0x08000000;
        public const int WsClipsiblings = 0x04000000;
        public const int WsClipchildren = 0x02000000;
        public const int WsMaximize = 0x01000000;
        public const int WsCaption = 0x00C00000;
        public const int WsBorder = 0x00800000;
        public const int WsDlgframe = 0x00400000;
        public const int WsVscroll = 0x00200000;
        public const int WsHscroll = 0x00100000;
        public const int WsSysmenu = 0x00080000;
        public const int WsThickframe = 0x00040000;
        public const int WsGroup = 0x00020000;
        public const int WsMinimizebox = 0x00020000;
        public const int WsTabstop = 0x00010000;
        public const int WsMaximizebox = 0x00010000;

        /* extended window styles */
        public const int WsExLeft = 0x00000000;
        public const int WsExLtrreading = 0x00000000;
        public const int WsExRightscrollbar = 0x00000000;
        public const int WsExDlgmodalframe = 0x00000001;
        public const int WsExNoparentnotify = 0x00000004;
        public const int WsExTopmost = 0x00000008;
        public const int WsExAcceptfiles = 0x00000010;
        public const int WsExTransparent = 0x00000020;
        public const int WsExMdichild = 0x00000040;
        public const int WsExToolwindow = 0x00000080;
        public const int WsExWindowedge = 0x00000100;
        public const int WsExClientedge = 0x00000200;
        public const int WsExContexthelp = 0x00000400;
        public const int WsExRight = 0x00001000;
        public const int WsExRtlreading = 0x00002000;
        public const int WsExLeftscrollbar = 0x00004000;
        public const int WsExControlparent = 0x00010000;
        public const int WsExStaticedge = 0x00020000;
        public const int WsExAppwindow = 0x00040000;
        public const int WsExLayered = 0x00080000;
        public const int WsExNoinheritlayout = 0x00100000;
        public const int WsExLayoutrtl = 0x00400000;
        public const int WsExComposited = 0x02000000;
        public const int WsExNoactivate = 0x08000000;

        public const int RgnAnd = 1;
        public const int RgnOr = 2;
        public const int RgnXor = 3;
        public const int RgnDiff = 4;
        public const int RgnCopy = 5;

        private const int VK_SHIFT = 0x10;
        private const int VK_CONTROL = 0x11;
        private const int VK_MENU = 0x12;
        private const int VK_LWIN = 0x5b;
        private const int VK_RWIN = 0x5c;

        public const uint GwHwndnext = 2;      // get the handle of the window below the given window.
        public const uint GwHwndprev = 3;      //

        #endregion

        #region Structures

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public readonly int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public int Width
            {
                get { return Right - Left; }
                set { Right = Left + value; }
            }

            public int Height
            {
                get { return Bottom - Top; }
                set { Bottom = Top + value; }
            }
        }

        #endregion

        #region Native functions

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern uint GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        internal static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString,
            string lpFileName);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wp, int lp);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy,
            uint uFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, [MarshalAs(UnmanagedType.Bool)] bool bRedraw);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DeleteObject([In] IntPtr hObject);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern int CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1, IntPtr hrgnSrc2, int fnCombineMode);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern short GetKeyState(int nVirtKey);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        #endregion

        /* override Windows settings and show the focus rectangle around the control */
        public static void ShowFocusRectangle(Control ctrl)
        {
            SendMessage(ctrl.Handle, WM_CHANGEUISTATE, UIS_CLEAR | ((UISF_HIDEACCEL | UISF_HIDEFOCUS) << 16), 0);
        }

        /* bring the form/window to the top of the Z-order without activatin it */
        public static void BringFormToFront(Form frm)
        {
            SetWindowPos(frm.Handle, (IntPtr)HWND_TOP, 0, 0, 0, 0, SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSIZE);
        }

        public static void BringFormToFront(IntPtr hwnd)
        {
            SetWindowPos(hwnd, (IntPtr)HWND_TOP, 0, 0, 0, 0, SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSIZE);
        }

        /* show a form/window without activating it */
        public static void ShowNa(IntPtr hwnd)
        {
            SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, SWP_SHOWWINDOW | SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSIZE);
        }

        public static void ShowNa(Form frm)
        {
            ShowNa(frm.Handle);
        }

        public static void ShowAtNa(Form frm, int x, int y)
        {
            SetWindowPos(frm.Handle, (IntPtr)HWND_TOP, x, y, 0, 0, SWP_SHOWWINDOW | SWP_NOACTIVATE | SWP_NOSIZE);
        }

        public static void HideNa(IntPtr hwnd)
        {
            SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, SWP_HIDEWINDOW | SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSIZE);
        }

        public static void HideNa(Form frm)
        {
            HideNa(frm.Handle);
        }

        public static bool IsShiftPressed()
        {
            return GetKeyState(VK_SHIFT) < 0;
        }
    }
}
