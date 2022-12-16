using System;
using System.Drawing;
using System.Windows.Forms;

namespace VideoGraphSample
{
    public partial class SetupForm : Form
    {
        private readonly int _origTelemetryPosX = AllSettings.TelemetryPosX;
        private readonly int _origTelemetryPosY = AllSettings.TelemetryPosY;
        private readonly int _origTelemetryAlpha = AllSettings.TelemetryAlpha;
        private readonly uint _origTelemetryTxtColor = AllSettings.TelemetryTxtColor;
        private readonly uint _origTelemetryBkgColor = AllSettings.TelemetryBkgColor;

        private OpenFileDialog _pathFileDialog;

        public string FilePath { get; private set;}

        public SetupForm()
        {
            InitializeComponent();

            tbTelemetryPosX.Value = _origTelemetryPosX;
            tbTelemetryPosY.Value = _origTelemetryPosY;
            tbTelemetryAlpha.Value = _origTelemetryAlpha;
            panelBkg.BackColor = ColorRefToColor(AllSettings.TelemetryBkgColor);
            panelTxt.BackColor = ColorRefToColor(AllSettings.TelemetryTxtColor);

            ActiveControl = tbTelemetryPosX;
        }

        private static Color ColorRefToColor(uint colorref)
        {
            byte b = (byte) (colorref >> 16);
            byte g = (byte) (colorref >> 8);
            byte r = (byte) (colorref);
            return Color.FromArgb(255, r, g, b);
        }

        private static uint ColorToColorRef(Color clr)
        {
            return (uint) ((clr.B << 16) | (clr.G << 8) | clr.R);
        }

        private static void UpdateTelemetry()
        {
            Dll.UpdateTelemetryPosition();
            Dll.UpdateTelemetryAlpha();
            Dll.UpdateTelemetryColors();
        }

        private void SetupForm_Shown(object sender, EventArgs e)
        {
            NativeMethods.ShowFocusRectangle(tbTelemetryPosX);
            NativeMethods.ShowFocusRectangle(tbTelemetryPosY);
            NativeMethods.ShowFocusRectangle(tbTelemetryAlpha);
        }

        private void btnSetDefaults_Click(object sender, EventArgs e)
        {
            /* setting trackbar value automatically updates AllSettings */
            tbTelemetryPosX.Value = Defines.TelemetryPosX;
            tbTelemetryPosY.Value = Defines.TelemetryPosY;
            tbTelemetryAlpha.Value = Defines.TelemetryAlpha;

            /* there is no OnColorChange handlers so colors must be written into AllSettings manually */
            panelBkg.BackColor = ColorRefToColor(Defines.TelemetryBkgColor);
            panelTxt.BackColor = ColorRefToColor(Defines.TelemetryTxtColor);
            AllSettings.TelemetryBkgColor = Defines.TelemetryBkgColor;
            AllSettings.TelemetryTxtColor = Defines.TelemetryTxtColor;


            UpdateTelemetry();
        }

        private void SetupForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
            {
                AllSettings.TelemetryPosX = _origTelemetryPosX;
                AllSettings.TelemetryPosY = _origTelemetryPosY;
                AllSettings.TelemetryAlpha = _origTelemetryAlpha;
                AllSettings.TelemetryTxtColor = _origTelemetryTxtColor;
                AllSettings.TelemetryBkgColor = _origTelemetryBkgColor;

                UpdateTelemetry();
            }
        }

        private void tbTelemetryPosX_ValueChanged(object sender, EventArgs e)
        {
            AllSettings.TelemetryPosX = tbTelemetryPosX.Value;
            lblTelemetryPosX.Text = $@"X = {AllSettings.TelemetryPosX}";
            Dll.UpdateTelemetryPosition();
        }

        private void tbTelemetryPosY_ValueChanged(object sender, EventArgs e)
        {
            AllSettings.TelemetryPosY = tbTelemetryPosY.Value;
            lblTelemetryPosY.Text = $@"Y = {AllSettings.TelemetryPosY}";
            Dll.UpdateTelemetryPosition();
        }

        private void tbTelemetryAlpha_ValueChanged(object sender, EventArgs e)
        {
            AllSettings.TelemetryAlpha = tbTelemetryAlpha.Value;
            lblTelemetryAlpha.Text = $@"A = {AllSettings.TelemetryAlpha}";
            Dll.UpdateTelemetryAlpha();
        }

        private void panelBkg_Click(object sender, EventArgs e)
        {
            colorDialog.Color = colorDialog.Color;
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                panelBkg.BackColor = colorDialog.Color;
                AllSettings.TelemetryBkgColor = ColorToColorRef(colorDialog.Color);
                Dll.UpdateTelemetryColors();
            }
        }

        private void panelTxt_Click(object sender, EventArgs e)
        {
            colorDialog.Color = ColorRefToColor(AllSettings.TelemetryTxtColor);
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                panelTxt.BackColor = colorDialog.Color;
                AllSettings.TelemetryTxtColor = ColorToColorRef(colorDialog.Color);
                Dll.UpdateTelemetryColors();
            }
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            CreateFileDialog();
            if (_pathFileDialog.ShowDialog() != DialogResult.OK) return;
            FilePath = _pathFileDialog.FileName;
            ShowPath();
        }

        private void CreateFileDialog()
        {
            _pathFileDialog = new OpenFileDialog { Title = @"Выберите файл", Filter = @"Video files(*.ts)|*.ts" };
        }

        private void ShowPath()
        {
            pathTextBox.Text = _pathFileDialog.FileName;
        }
    }
}
