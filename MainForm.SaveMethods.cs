namespace VideoGraphSample
{
    public partial class MainForm
    {
        private void SavePosition()
        {
            Properties.Settings.Default.WindowSize = this.Size;
            Properties.Settings.Default.WindowLocation = this.Location;
            SaveSettings();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }

        private void SaveTextAlpha(ushort alpha_Numeric)
        {
            Properties.Settings.Default.alpha_Text = alpha_Numeric;
            SaveSettings();
        }

        private void SaveTextPositionX(ushort positionX_Numeric)
        {
            Properties.Settings.Default.positionX_Text = positionX_Numeric;
            SaveSettings();
        }

        private void SaveTextPositionY(ushort positionY_Numeric)
        {
            Properties.Settings.Default.positionY_Text = positionY_Numeric;
            SaveSettings();
        }

    }
}