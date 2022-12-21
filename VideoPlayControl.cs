using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoGraphSample
{
    public partial class VideoPlayControl : Form
    {
        public bool Paused { get; private set; }
        
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
        }

        private void Set_trackBar_Position()
        {
            Dll.SetPositionTrackBar((ushort)trackBar_Player.Value);
        }

        private void trackBar_Player_MouseDown(object sender, MouseEventArgs e)
        {
            Dll.SetPause();
        }

        private void trackBar_Player_MouseUp(object sender, MouseEventArgs e)
        {
            Set_trackBar_Position();
            if (trackBar_Player.Value != 100) Dll.SetStart();
        }

        #endregion


        #region Form Control

        private void SetStart()
        {
            if (!Dll._dllOpened) return;
            if (Paused) Dll.SetStart();
            else
                Dll.SetPause();
        }

        #endregion Form Control

        #region Form handler

        public void VideoPlayControl_Paused(object sender, EventArgs e)
        {
            Paused = !Paused;
            SetStart();
        }

        #endregion
    }
}
