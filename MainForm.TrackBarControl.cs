using System;

namespace VideoGraphSample
{
    public partial class MainForm
    {
        private bool _timerEnable = false;

        public void TurnOnTimerUpdate()
        {
            if (_timerEnable) return;
            timer_updateTrackBar.Start();
            _timerEnable = true;

        }

        public void TurnOffTimerUpdate()
        {
            if (!_timerEnable) return;
            timer_updateTrackBar.Stop();
            _timerEnable = false;
        }

        private void UpdateTrackBar()
        {
            ushort percent = 0;
            Dll.GetPositionTrackBar(ref percent);
            if (percent > 100)
            {
                Dll.SetStop();
            }
            else
            {
                foreach (var item in _renderers)
                {
                    item.SetTrackBarPosition(percent);
                }
            }
        }

        private bool CheckEnabled()
        {
            if (_renderers == null) return false;
            foreach (var item in _renderers)
            {
                if (item.TrackBarEnabled)
                {
                    return true;
                }         
            }
            return false;
        }

        private void timer_updateTrackBar_Tick(object sender, EventArgs e)
        {
            if(!CheckEnabled()) UpdateTrackBar();
        }


    }
}