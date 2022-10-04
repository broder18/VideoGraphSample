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
using System.IO;

namespace VideoGraphSample
{
    public partial class MainForm : Form
    {
        private const int MBYTE = 1024 * 1024;
        private const byte SYNC_BYTE = 0x47;
        private const byte TP_SIZE = 188;
        private const byte PID_MASK = 0x1F;
        private Dictionary<ushort, bool> map_pids;
        private bool graphCreated = false;
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
                //CreateWndRender();
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
            if (graphCreated) CloseWndRender();
            if (path_FileDialog.ShowDialog() != DialogResult.OK) return;
            if (map_pids != null) map_pids.Clear();
            CreateMap();
            path_textBox.Text = path_FileDialog.FileName;
            SearchSyncByte(path_FileDialog.FileName);
            CreateWndRender();
            Dll.Open(path_FileDialog.FileName, map_pids, _renderers);
            graphCreated = true;
        }

        private void CreateFileDialog()
        {
            path_FileDialog = new OpenFileDialog();
            path_FileDialog.Title = "Выберите файл";
            path_FileDialog.Filter = "Video files(*.ts)|*.ts";
        }

        private bool SearchSyncByte(string path)
        {
            using (FileStream fsSource = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                fsSource.Seek(0, SeekOrigin.Begin);
                byte[] bytes = new byte[MBYTE];
                int counter = 0;
                int bytesRead = fsSource.Read(bytes, 0, MBYTE);

                while (bytesRead != 0)
                {

                    if (ReadByte(ref counter, bytesRead, ref bytes, fsSource))
                    {
                        CheckPids(ref bytes);
                        return true;
                    }
                    fsSource.Seek(counter, SeekOrigin.Begin);
                    bytesRead = fsSource.Read(bytes, 0, MBYTE);
                }
            }
            return false;
        }

        private bool ReadByte(ref int counter, int bytesRead, ref byte[] bytes, FileStream fsSource)
        {
            for (int idx = 0; idx < bytesRead; idx++)
            {
                if (CheckSyncByte(idx, ref bytes))
                {
                    fsSource.Seek(counter, SeekOrigin.Begin);
                    bytesRead = fsSource.Read(bytes, 0, MBYTE);
                    if (IsValidPointer(ref bytes, 0))
                    {
                        return true; 
                    }

                    idx = 0;
                }
                counter++;
            }

            return false;
        }

        private bool CheckSyncByte(int idx, ref byte[] bytes)
        {
            if (bytes[idx] == SYNC_BYTE) return true;
            return false;
        }

        private static bool IsValidPointer(ref byte[] bytes, int idX)
        {
            int idX2 = idX + TP_SIZE;
            int idX3 = idX + TP_SIZE;
            return bytes[idX] == SYNC_BYTE && bytes[idX2] == SYNC_BYTE && bytes[idX3] == SYNC_BYTE;
        }

        private void CheckPids(ref byte[] bytes)
        {
            byte cropped_byte;
            ushort res;
            for (int idx = 0; idx < bytes.Length; idx += 188)
            {
                cropped_byte = (byte)(bytes[idx + 1] & PID_MASK);
                res = (ushort)(cropped_byte * 256 + bytes[idx + 2]);
                if (map_pids.ContainsKey(res)) MarkPid(res);
            }
        }

        private void MarkPid(ushort id)
        {
            if (map_pids[id] == true) return;
            map_pids[id] = true;
        }

        /*private IntPtr[] ParseIntPtr()
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
        }*/


    }
}
