using System.Windows.Forms;

namespace BIONVideoPlayer
{
    public partial class InfoForm : Form
    {
        public InfoForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        public void SetBorderRegion()
        {
            if (IsHandleCreated)
            {
                var hRgn = NativeMethods.CreateRoundRectRgn(1, 1, Width - 1, Height - 1, 16, 16);
                if (NativeMethods.SetWindowRgn(Handle, hRgn, true) == 0) NativeMethods.DeleteObject(hRgn);
            }
        }

        public void DoShow(Form parent)
        {
            var r = parent.Bounds;
            int x = r.Left + (r.Width - Width) / 2;
            int y = r.Top + (r.Height - Height) / 2;

            NativeMethods.ShowAtNa(this, x, y + 20);
            Visible = true;
        }

        public void DoHide()
        {
            Hide();
            Visible = false;
        }
    }
}
