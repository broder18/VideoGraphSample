using System;
using System.Drawing;
using System.Windows.Forms;

namespace VideoGraphSample
{
    public partial class VideoPlayControl : Form
    {
        public bool Paused { get; private set; }
        private bool Moving = false;
        private Point Offset;
        
        public VideoPlayControl()
        {
            InitializeComponent();
        }

        #region TrackBar Handle

        public void Get_trackBar_Position()
        {
            ushort percent = 0;
            Dll.GetPositionTrackBar(ref percent);
            
            trackBar_Player.Value = percent;
            Console.WriteLine(percent);
            if (percent == 100) btn_Play.Enabled = false;
            else
            {
                btn_Play.Enabled = true;
            }
        }

        private void Set_trackBar_Position()
        {
            Dll.SetPositionTrackBar((ushort)trackBar_Player.Value);
            
        }

        private void trackBar_Player_MouseDown(object sender, MouseEventArgs e)
        {
            Paused = true;
            Dll.SetPause();
        }

        private void trackBar_Player_MouseUp(object sender, MouseEventArgs e)
        {
            Set_trackBar_Position();
            if (trackBar_Player.Value != 100) Dll.SetStart();
            Paused = false;
        }

        #endregion
        #region Form Control

        private void SetStart()
        {
            if (!Dll._dllOpened) return;
            if (!Paused) Dll.SetStart();
            else
                Dll.SetPause();
        }

        public void VideoPlayControl_Paused(object sender, EventArgs e)
        {
            Paused = !Paused;
            SetStart();
        }

        #endregion Form Control
        #region Form handler

        private void VideoPlayControl_MouseDown(object sender, MouseEventArgs e)
        {
            Moving = true;
            Offset = new Point(e.X, e.Y);
        }

        private void VideoPlayControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (Moving)
            {
                Point newLocation = this.Location;
                newLocation.X += e.X - Offset.X;
                newLocation.Y += e.Y - Offset.Y;
                this.Location = newLocation;
            }
        }

        private void VideoPlayControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (Moving) Moving = false;
        }

        #endregion

    }
}
