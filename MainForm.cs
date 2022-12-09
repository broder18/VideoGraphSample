using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using VideoGraphSample.Properties;
using System.IO;
using System.Threading;
using System.Linq;

namespace VideoGraphSample
{
    public partial class MainForm : Form
    {
        private Dictionary<ushort, bool> _mapPids;
        private int[] Pids;
        private int[] Pmts;

        private bool _graphCreated;
        private RendererContainerForm[] _renderers;
        private OpenFileDialog _pathFileDialog;


        private InfoForm _infoForm = new InfoForm();
        private Point[] _rendererLocations;

        private int _focusedItemIdx = -1;
        private List<IntPtr> _visiblerenderers;

        public MainForm()
        {
            InitializeComponent();
            
            try
            {
                var r = AllSettings.MainForm;
                Location = new Point(r.X, r.Y);
                SetTitle();
                CreateFileDialog();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateListItems()
        {
            foreach(var pid in _mapPids)
            {
                var item = StatisticsList.Items.Add($"0x{pid.Key:X4}");
            }
        }

        private Dll.AllChannels GetChannels()
        {
            unsafe
            {
                var channels = new Dll.AllChannels
                    {NumVideoPids = _renderers.Length};

                
                for (int i = 0; i < _renderers.Length; i++)
                {
                    channels.Pids[i] = Pids[i];
                    channels.hWnds[i] = (int) _renderers[i].GetPictureBoxHandle();
                    channels.Pmts[i] = Pmts[i];
                }

                return channels;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dll.Close();
            TurnOffTimerUpdate();
            CloseWndRender();
        }

        private void button_PathFile_Click(object sender, EventArgs e)
        {
            if (_graphCreated) CloseWndRender();
            if (_pathFileDialog.ShowDialog() != DialogResult.OK) return;
            _mapPids?.Clear();
            CreateMap();
            CreateListItems();
            ScanBytes.SearchSyncByte(_pathFileDialog.FileName, ref _mapPids);
            ShowPath();
            CreateWndRender();
            //Dll.Open(_pathFileDialog.FileName, _mapPids, _renderers);
            //TurnOnTimerUpdate();
            //_graphCreated = true;
            //Dll.SetStart();
        }

        #region Setup Form

        private void CreateFileDialog()
        {
            _pathFileDialog = new OpenFileDialog {Title = @"Выберите файл", Filter = @"Video files(*.ts)|*.ts"};
        }

        private void SetTitle(bool ok = true)
        {
            Text = @"BION Video Player ";
        }

        private void ShowPath()
        {
            path_textBox.Text = _pathFileDialog.FileName;
        }

        #endregion

        private RendererContainerForm GetSelectedRenderer()
        {
            var focus = StatisticsList.FocusedItem;
            if (focus == null)
            {
                _focusedItemIdx = -1;
                return null;
            }

            _focusedItemIdx = focus.Index;
            return _renderers[_focusedItemIdx];
        }

        #region RendererSettings

        private void CreateMap()
        {
            _mapPids = new Dictionary<ushort, bool>()
            {
                {(ushort)Defines.Pids[0], false },
                {(ushort)Defines.Pids[1], false },
                {(ushort)Defines.Pids[2], false },
                {(ushort)Defines.Pids[3], false },
                {(ushort)Defines.Pids[4], false }
            };
        }

        private void CreateWndRender()
        {
            byte quantity = CalculatePids();
            if (quantity == 0) return;
            CreateWndRenderArray(quantity);
            AddRendererEvent();
        }

        private byte CalculatePids()
        {
            byte count = 0;
            foreach (var item in _mapPids.Where(item => item.Value == true))
            {
                count++;
            }

            return count;
        }

        private void CreateWndRenderArray(byte count)
        {
            _renderers = new RendererContainerForm[count];
            Pids = new int[count];
            Pmts = new int[count];
            ushort id = 0;
            ushort counter = 0;
            foreach (var item in _mapPids)
            {
                if (item.Value)
                {
                    
                    Pids[id] = item.Key;
                    Pmts[id] = item.Key - 0x41;
                    var rend = new RendererContainerForm($"0x{item.Key:X4}", (AllSettings.EnableTelemetry & (1 << counter)) != 0);
                    var rect = AllSettings.Renderers[counter];
                    rend.Location = new Point(rect.X, rect.Y);
                    rend.SetVideoSize(rect.Width, rect.Height);
                    _renderers[id] = rend;
                    id++;
                }
                counter++;
            }
        }

        private void CloseWndRender()
        {
            if (_renderers == null) return;
            foreach (var render in _renderers)
            {
                render.Close();
            }
            _renderers = null;
            Pids = null;
            Pmts = null;
        }
        #endregion

        #region Menu item handlers

        public static int GetMenuItemScale(object sender)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                int scale;
                if (int.TryParse(menuItem.Tag.ToString(), out scale)) return scale;
            }

            return -1;
        }

        #endregion

        #region Renderer stuff

        private void AddRendererEvent()
        {
            StatisticsList.ItemChecked += StatisticsList_ItemChecked;

            GetSelectedRenderer();

            for(int i = 0; i < _renderers.Length; i++)
            {
                var rend = _renderers[i];
                NativeMethods.ShowNA(rend); 
                rend.OnActivation += RendererActivated;
                rend.OnHidden += RendererHidden;
                rend.OnMoveBegin += OnRendererMoveBegin;
                rend.OnMoveEnd += OnRendererMoveEnd;
                rend.OnTelemetryEnableChange += OnRendererTelemetryEnableChange;
            }

            Activated += MainForm_Activated;
        }

        private void RendererActivated(object sender, EventArgs e)
        {
            var currend = GetSelectedRenderer();
            if (currend == null || currend == sender) return;

            var newitem = FindRendererItem(sender);
            if (newitem == null) return;

        }

        private void RendererHidden(object sender, EventArgs e)
        {
            var newitem = FindRendererItem(sender);
            if (newitem != null) newitem.Checked = false;
        }
        #endregion

        #region StatisticList handlers

        private void StatisticsList_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int idx = e.Item.Index;
            int mask = 1 << idx;
            bool map = e.Item.Checked;
            //Dll.MapUnmapChannel(idx, map);
        }



        #endregion

        #region Utilities

        private int FindRendererIdx(object o)
        {
            return Array.FindIndex(_renderers, w => w == o);
        }

        private ListViewItem FindRendererItem(object o)
        {
            int idx = FindRendererIdx(o);
            return idx == -1 ? null : StatisticsList.Items[idx];
        }

        private void OnRendererMoveBegin(object sender, EventArgs e)
        {
            if (NativeMethods.IsShiftPressed())
            {
                _rendererLocations = new Point[_renderers.Length];
                for (int i = 0; i < _renderers.Length; i++)
                {
                    var rend = _renderers[i];
                    _rendererLocations[i] = new Point(rend.Left, rend.Top);
                }

                var currend = (Form) sender;
                currend.Move += OnRendererMove;
                currend.Resize += OnRendererResize;
                return;
            }

            _infoForm.DoShow(this);
        }

        private void OnRendererMove(object sender, EventArgs e)
        {
            int originatoridx = FindRendererIdx(sender);
            if (originatoridx < 0) return;
            var originator = _renderers[originatoridx];
            var origpos = _rendererLocations[originatoridx];
            int deltaX = originator.Left - origpos.X;
            int deltaY = originator.Top - origpos.Y;

            for (int i = 0; i < _renderers.Length; i++)
            {
                if (i == originatoridx) continue;

                var currend = _renderers[i];
                var startpos = _rendererLocations[i];
                currend.Left = startpos.X + deltaX;
                currend.Top = startpos.Y + deltaY;
            }
        }

        private void OnRendererResize(object sender, EventArgs e)
        {
            int originatoridx = FindRendererIdx(sender);
            if (originatoridx < 0) return;
            var originator = _renderers[originatoridx];

            for (int i = 0; i < _renderers.Length; i++)
            {
                if (i == originatoridx) continue;
                _renderers[i].SetVideoSize(originator.VideoWidth, originator.VideoHeight);
            }
        }

        private void OnRendererMoveEnd(object sender, EventArgs e)
        {
            var currend = (Form) sender;
            currend.Move -= OnRendererMove;
            currend.Resize -= OnRendererResize;

            _infoForm.DoHide();
            _rendererLocations = null;
        }

        private void OnRendererTelemetryEnableChange(object sender, EventArgs e)
        {
            int rendidx = FindRendererIdx(sender);
            if (rendidx < 0) return;

            int bit = 1 << rendidx;
            var rend = _renderers[rendidx];
            if (rend.IsTelemetryEnabled()) AllSettings.EnableTelemetry |= bit;
            else AllSettings.EnableTelemetry &= ~bit;

            //Dll.UpdateTelemetryEnable();
        }

        private IntPtr[] GetRendererHandles()
        {
            var hwnds = new IntPtr[_renderers.Length];
            for (int i = 0; i < _renderers.Length; i++) hwnds[i] = _renderers[i].Handle;
            return hwnds;

        }

        #endregion

        #region Form handlers

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (NativeMethods.IsShiftPressed() || _renderers == null) return;

            var hwnds = GetRendererHandles();
            if (Utils.GetZOrderedForms(ref hwnds))
            {
                for (int i = 0; i < _renderers.Length; i++) NativeMethods.BringFormToFront(hwnds[i]);
            }

            NativeMethods.BringFormToFront(this);
        }

        #endregion


    }
}
