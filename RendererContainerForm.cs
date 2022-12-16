using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace VideoGraphSample
{
    public sealed partial class RendererContainerForm : Form
    {
        private const int VideoW = Defines.VideoW;
        private const int VideoH = Defines.VideoH;
        private const double AspectRatio = (double) VideoW / VideoH;

        /* we want borders around our renderer (symmetric; actually, the DLL manages them internally) */
        private const int ImageBorderWidth = 1;
        private const int ImageBorderHeight = 1;
        private const int BordersWidth = ImageBorderWidth * 2;
        private const int BordersHeight = ImageBorderHeight * 2;

        //private const int TrackBarHeight = 45;

        /* WM_SIZING message operates with the coordinates of the whole window but we need to work with its client part */
        /* In order to abstract from the current window style, we remember the differences between the windows and client sizes */
        private int _clientDeltaX;
        private int _clientDeltaY;

        /* the way to inform the main form about our activation */
        public EventHandler OnActivation { get; set; }

        /* inform about hiding so that an interested party can disable our stream */
        public EventHandler OnHidden { get; set; }
        public EventHandler OnMoveBegin { get; set; }
        public EventHandler OnMoveEnd { get; set; }

        /* inform about telemetry enabling/disabling */
        public EventHandler OnTelemetryEnableChange { get; set; }

        private readonly string _baseTitle;


        //private int _oldLen;
        //private const double Prop = 300 / 400;
        public bool TrackBarEnabled { get; private set; }

        public RendererContainerForm(string basetitle, bool enableTelemetry)
        {
            InitializeComponent();
            menuItemShowTelemetry.Checked = enableTelemetry;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, false);

            _baseTitle = basetitle;
            DoCreateHandle();
            SetVideoSize(VideoW / 3, VideoH / 3);
        }

        public void SetVideoSize(int w, int h)
        {
            ClientSize = new Size(w + BordersWidth, h + BordersHeight);
        }

        public void SetVideoWidth(int w)
        {
            int h = Convert.ToInt32(w / AspectRatio);
            SetVideoSize(w, h);
        }

        public void SetVideoHeight(int h)
        {
            int w = Convert.ToInt32(h / AspectRatio);
            SetVideoSize(w, h);
        }

        public int W2H(int w)
        {
            return Convert.ToInt32(w / AspectRatio);
        }

        public Size GetWindowVideoDifference()
        {
            int deltaX = Width - VideoWidth;
            int deltaY = Height - VideoHeight;
            return new Size(deltaX, deltaY);
        }

        public int VideoWidth => ClientSize.Width - BordersWidth;
        public int VideoHeight => ClientSize.Height - BordersHeight;

        [Localizable(false)]
        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        private void click_MouseDown(object sender, EventArgs e)
        {
            TrackBarEnabled = true;
            Dll.SetStop();
        }

        private void click_MouseUp(object sender, EventArgs e)
        {
            if (TrackBarEnabled)
            {
                Dll.SetStart();
                //Dll.SetPositionTrackBar((ushort) trackBar_Player.Value);
                TrackBarEnabled = false;
            }
        }

        public IntPtr GetPictureBoxHandle()
        {
            return this.Handle;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case NativeMethods.WM_SIZING:
                    WmSizing(m.WParam.ToInt32(), m.LParam);
                    m.Result = (IntPtr) 1;
                    return;
            }

            base.WndProc(ref m);
        }

        private void WmSizing(int fwSide, IntPtr lParam)
        {
            var rc = (NativeMethods.RECT) Marshal.PtrToStructure(lParam, typeof(NativeMethods.RECT));

            if (rc.Width < Defines.MinWindowW) rc.Width = Defines.MinWindowW;
            if (rc.Height < Defines.MinWindowH) rc.Height = Defines.MinWindowH;

            switch (fwSide)
            {
                case NativeMethods.WMSZ_LEFT:
                case NativeMethods.WMSZ_RIGHT:
                    rc.Bottom = rc.Top + CalcNewHeight(rc);
                    break;

                case NativeMethods.WMSZ_TOP:
                case NativeMethods.WMSZ_BOTTOM:
                    rc.Right = rc.Left + CalcNewWidth(rc);
                    break;

                case NativeMethods.WMSZ_TOPLEFT:
                case NativeMethods.WMSZ_TOPRIGHT:
                    rc.Top = rc.Bottom - CalcNewHeight(rc);
                    break;

                case NativeMethods.WMSZ_BOTTOMLEFT:
                case NativeMethods.WMSZ_BOTTOMRIGHT:
                    rc.Bottom = rc.Top + CalcNewHeight(rc);
                    break;
            }

            Marshal.StructureToPtr(rc, lParam, true);
        }

        private int CalcNewHeight(NativeMethods.RECT rc)
        {
            int imageW = rc.Width - _clientDeltaX - BordersWidth;
            int imageH = Convert.ToInt32(imageW / AspectRatio);
            return imageH + _clientDeltaY + BordersHeight;
        }

        private int CalcNewWidth(NativeMethods.RECT rc)
        {
            int imageH = rc.Height - _clientDeltaY - BordersHeight;
            int imageW = Convert.ToInt32(imageH * AspectRatio);
            return imageW + _clientDeltaX + BordersWidth;
        }

        public void SetTrackBarPosition(ushort percent)
        {
            //this.trackBar_Player.Value = percent;
        }

        public ushort GetTrackBarPosition()
        {
            //return (ushort) this.trackBar_Player.Value;
            return 0;
        }

        private void DoCreateHandle()
        {
            if(!IsHandleCreated) CreateHandle();
        }

        private void RendererContainerForm_ResizeBegin(object sender, EventArgs e)
        {
            OnMoveBegin?.Invoke(this, EventArgs.Empty);

            _clientDeltaX = Width - ClientSize.Width;
            _clientDeltaY = Height - ClientSize.Height;
        }

        private void RendererContainerForm_ResizeEnd(object sender, EventArgs e)
        {
            OnMoveEnd?.Invoke(this, EventArgs.Empty);
        }

        private void RendererContainerForm_Activated(object sender, EventArgs e)
        {
            OnActivation?.Invoke(this, EventArgs.Empty);
        }

        private void RendererContainerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Hide();
                OnHidden?.Invoke(this, EventArgs.Empty);
                e.Cancel = true;
            }
        }

        private void DoClose()
        {
            FormClosing -= RendererContainerForm_FormClosing;
            Close();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            using (var pen = new Pen(Color.FromArgb(255, 166, 202, 240), 1))
            {
                var r = new Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
                e.Graphics.DrawRectangle(pen, r);
            }
        }

        private void menuItemSize1X_Click(object sender, EventArgs e)
        {
            int scale = MainForm.GetMenuItemScale(sender);
            if (scale <= 0) return;

            int w = Defines.VideoW / scale;
            int h = Defines.VideoH / scale;
            SetVideoSize(w, h);
        }

        private void menuItemShowTelemetry_Click(object sender, EventArgs e)
        {
            OnTelemetryEnableChange?.Invoke(this, EventArgs.Empty);
        }

        public bool IsTelemetryEnabled()
        {
            return menuItemShowTelemetry.Checked;
        }

        public void EnableTelemetry(bool enable)
        {
            menuItemShowTelemetry.Checked = enable;
        }

        private void RendererContainerForm_Resize(object sender, EventArgs e)
        {
            Invalidate();
            Dll.Resize(this.Handle);
            Text = _baseTitle + $@"({VideoWidth} x {VideoHeight})";
        }

    }
}
