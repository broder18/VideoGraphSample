using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoGraphSample.Properties;

namespace VideoGraphSample
{
    public partial class MainForm : Form
    {
        private enum Pids
        {
            Pid0 = 0x85,
            Pid1 = 0x86,
            Pid2 = 0x87,
            Pid3 = 0x88,
            Pid4 = 0x89
        }

        private RendererConrainerForm[] _renderers;
        private OpenFileDialog path_FileDialog;
        

        public MainForm()
        {
            InitializeComponent();
            
            try
            {
                CreateListItems();
                CreateWndRender();
                CreateFileDialog();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateListItems()
        {
            foreach(int pid in Enum .GetValues(typeof(Pids)))
            {
                var item = StatisticsList.Items.Add($"0x{(ushort)pid:X4}");
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dll.Close();
            CloseWndRender();
            SavePosition();
        }

        private void alpha_Numeric_ValueChanged(object sender, EventArgs e)
        {
            SaveTextAlpha((ushort)alpha_Numeric.Value);
            change_TextParams();
        }

        private void positionX_Numeric_ValueChanged(object sender, EventArgs e)
        {
            SaveTextPositionX((ushort)positionX_Numeric.Value);
            change_TextParams();
        }

        private void positionY_Numeric_ValueChanged(object sender, EventArgs e)
        {
            SaveTextPositionY((ushort)positionY_Numeric.Value);
            change_TextParams();
        }

        private void change_TextParams()
        {
            Dll.SetParams(Settings.Default.alpha_Text, Settings.Default.positionX_Text, Settings.Default.positionY_Text);
        }

        private void button_PathFile_Click(object sender, EventArgs e)
        {
            if (path_FileDialog.ShowDialog() != DialogResult.OK) return;
            path_textBox.Text = path_FileDialog.FileName;
            Dll.Open(path_FileDialog.FileName, ParseUshort(), ParseIntPtr());
        }

        private void CreateFileDialog()
        {
            path_FileDialog = new OpenFileDialog();
            path_FileDialog.Title = "Выберите файл";
            path_FileDialog.Filter = "Video files(*.ts)|*.ts";
        }

        private IntPtr[] ParseIntPtr()
        {
            IntPtr[] hWnd = new IntPtr[_renderers.Length];
            for(byte ind = 0; ind < _renderers.Length; ind++)
            {
                hWnd[ind] = _renderers[ind].Handle;
            }
            return hWnd;
        }

        private ushort[] ParseUshort()
        {
            ushort[] pids = new ushort[_renderers.Length];
            int ind = 0;
            foreach (int pid in Enum.GetValues(typeof(Pids)))
            {
                pids[ind++] = (ushort)pid;
            }
            return pids;
        }


    }
}
