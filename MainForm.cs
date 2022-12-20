using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using VideoGraphSample.Properties;
using System.IO;
using System.Threading;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace VideoGraphSample
{
    public partial class MainForm : Form
    {
        private Dictionary<ushort, bool> _mapPids;
        private int[] Pids;
        private int[] Pmts;

        private RendererContainerForm[] _renderers;

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

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateListItems()
        {
            StatisticsList.Items.Clear();
            foreach (var item in _mapPids.Where(item => item.Value == true))
            {
                StatisticsList.Items.Add($"0x{item.Key:X4}");
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
                    channels.hWnds[i] = (int) _renderers[i].Handle;
                    Console.WriteLine(channels.hWnds[i]);
                    channels.Pmts[i] = Pmts[i];
                }

                for (int i = _renderers.Length; i < Defines.MaxChannels; i++)
                {
                    channels.Pids[i] = 0;
                    channels.hWnds[i] = 0;
                    channels.Pmts[i] = 0;
                }

                return channels;
            }
        }


        private void Start(string filePath)
        {
            if (Dll._dllOpened)
            {
                _mapPids?.Clear();
                CloseWndRender();
                Dll.Close();
            }
            
            CreateMap();
            ScanBytes.SearchSyncByte(filePath, ref _mapPids);
            CreateListItems();
            CreateWndRender();
            Dll.Open(filePath, GetChannels());
            Dll.SetStart();
            //TurnOnTimerUpdate();
        }

        #region Setup Form

        private void SetTitle(bool ok = true)
        {
            Text = @"BION Video Player ";
        }

        #endregion

        #region RendererSettings

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
            foreach (var item in _mapPids.Where(item => item.Value == true)) count++;
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

            for (int i = 0; i < _renderers.Length; i++)
            {
                var rend = _renderers[i];
                AllSettings.Renderers[i] = new Rectangle(rend.Left, rend.Top, rend.VideoWidth, rend.VideoHeight);
                rend.DoClose();
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

        public void menuItemSetScale_Click(object sender, EventArgs e)
        {
            int scale = GetMenuItemScale(sender);
            if (scale <= 0) return;

            int w = Defines.VideoW / scale;
            int h = Defines.VideoH / scale;

            foreach(var rend in _renderers) rend.SetVideoSize(w, h);
        }

        private void menuItemWindow1X_Click(object sender, EventArgs e)
        {
            int scale = GetMenuItemScale(sender);
            if (scale <= 0) return;

            var rend = GetSelectedRenderer();
            if (rend == null) return;

            int w = Defines.VideoW / scale;
            int h = Defines.VideoH / scale;
            rend.SetVideoSize(w, h);
        }

        private void menuItemCascade_Click(object sender, EventArgs e)
        {
            var visrenderers = GetVisibleRenderers();
            if (visrenderers == null || visrenderers.Length < 2) return;

            for (int i = 0; i < visrenderers.Length; i++)
            {
                var rend = visrenderers[i];
                rend.Left = Defines.CascadeOffsetX * i;
                rend.Top = Defines.CascadeOffsetY * i;
                NativeMethods.BringFormToFront(rend);
            }

        }

        private void menuItemToLargest_Click(object sender, EventArgs e)
        {
            int maxW = int.MinValue;
            foreach (var rend in _renderers)
            {
                if (rend.VideoWidth > maxW) maxW = rend.VideoWidth;
            }

            foreach(var rend in _renderers) rend.SetVideoWidth(maxW);
        }

        private void menuItemToSmallest_Click(object sender, EventArgs e)
        {
            int minW = int.MaxValue;
            foreach (var rend in _renderers)
            {
                if (rend.VideoWidth < minW) minW = rend.VideoWidth;
            }

            foreach(var rend in _renderers) rend.SetVideoWidth(minW);
        }

        private void menuItemTileFitToDesktop_Click(object sender, EventArgs e)
        {
            var visrenderers = GetVisibleRenderers();
            if (visrenderers == null || visrenderers.Length < 2) return;

            var desktop = Screen.PrimaryScreen.WorkingArea;
            int numrows = (visrenderers.Length + Defines.GridSize - 1) / Defines.GridSize;

            var rend = visrenderers[0];

            var diff = rend.GetWindowVideoDifference();

            int maxWindowW = desktop.Width / Defines.GridSize;
            int maxWindowH = desktop.Height / numrows;
            int maxVideoW = maxWindowW - diff.Width;
            int maxVideoH = maxWindowH - diff.Height;

            bool useH = visrenderers[0].W2H(maxWindowW) > maxVideoH;

            foreach (var r in visrenderers)
            {
                if (useH) r.SetVideoHeight(maxVideoH);
                else r.SetVideoWidth(maxVideoW);
            }

            int i = 0;
            int rendW = rend.Width;
            int rendH = rend.Height;
            for (int row = 0; row < Defines.GridSize; row++)
            {
                int y = row * rendH;
                for (int col = 0; col < Defines.GridSize; col++)
                {
                    if (i >= visrenderers.Length) break;

                    int x = col * rendW;
                    visrenderers[i].Left = x;
                    visrenderers[i].Top = y;
                    i++;
                }

                if (i >= visrenderers.Length) break;
            }
        }

        private void menuItemTileFit1X_click(object sender, EventArgs e)
        {
            var visrenderers = GetVisibleRenderers();
            if (visrenderers == null || visrenderers.Length < 2) return;

            int scale = GetMenuItemScale(sender);
            if (scale <= 0) return;

            int w = Defines.VideoW / scale;
            int h = Defines.VideoH / scale;

            foreach(var rend in visrenderers) rend.SetVideoSize(w, h);

            int i = 0;
            int rendW = visrenderers[0].Width;
            int rendH = visrenderers[0].Height;
            for (int row = 0; row < Defines.GridSize; row++)
            {
                int y = row * rendH;
                for (int col = 0; col < Defines.GridSize; col++)
                {
                    if (i >= visrenderers.Length) break;

                    int x = col * rendW;
                    visrenderers[i].Left = x;
                    visrenderers[i].Top = y;
                    i++;
                }

                if (i >= visrenderers.Length) break;
            }
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void menuItemOptions_Click(object sender, EventArgs e)
        {
            using (var frm = new SetupForm())
            {
                if ((frm.ShowDialog() == DialogResult.OK) && frm.FilePath != null)
                {
                    int sas = 4;
                    Start(frm.FilePath);
                }
            }
        }

        private void menuItemEnableAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in StatisticsList.Items) item.Checked = true;
        }

        private void menuItemDisableAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in StatisticsList.Items) item.Checked = false;
        }

        private void menuItemShowAll_Click(object sender, EventArgs e)
        {
            foreach(var rend in _renderers) NativeMethods.ShowNA(rend);
        }

        private void menuItemShowHide_Click(object sender, EventArgs e)
        {
            StatisticsList_DoubleClick(StatisticsList, EventArgs.Empty);
        }

        private void menuItemEnableDisable_Click(object sender, EventArgs e)
        {
            var item = StatisticsList.FocusedItem;
            if (item != null) item.Checked = !item.Checked;
        }

        private void menuItemDisableAllButThis_Click(object sender, EventArgs e)
        {
            var selitem = StatisticsList.FocusedItem;
            if (selitem == null) return;
            foreach (ListViewItem item in StatisticsList.Items)
            {
                item.Checked = item == selitem;
            }
        }

        private void menuItemHideAllButThis_Click(object sender, EventArgs e)
        {
            var selrend = GetSelectedRenderer();
            if (selrend == null) return;

            for (int i = 0; i < _renderers.Length; i++)
            {
                var rend = _renderers[i];
                if (rend == selrend) continue;
                var item = FindRendererItem(rend);
                if (item != null) item.Checked = false;
                NativeMethods.HideNA(rend);
            }
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
            Dll.MapUnmapChannel(idx, map);
        }

        private void StatisticsList_DoubleClick(object sender, EventArgs e)
        {

        }

        private void StatisticsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var rend = GetSelectedRenderer();
            if (rend != null && NativeMethods.IsWindowVisible(rend.Handle)) NativeMethods.BringFormToFront(rend);
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

        private RendererContainerForm[] GetVisibleRenderers()
        {
            var lst = new List<RendererContainerForm>();

            foreach (var rend in _renderers)
            {
                if(NativeMethods.IsWindowVisible(rend.Handle)) lst.Add(rend);
            }

            return lst.ToArray();
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

        private void MainForm_Resize(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Minimized:
                    var hwnds = GetRendererHandles();
                    Utils.GetZOrderedForms(ref hwnds);
                    _visiblerenderers = new List<IntPtr>();
                    foreach (var hwnd in hwnds)
                    {
                        if (NativeMethods.IsWindowVisible(hwnd))
                        {
                            _visiblerenderers.Add(hwnd);
                            NativeMethods.HideNA(hwnd);
                        }
                    }
                    break;

                case FormWindowState.Normal:
                    if (_visiblerenderers != null)
                    {
                        foreach (var hwnd in _visiblerenderers) NativeMethods.ShowNA(hwnd);
                        _visiblerenderers = null;
                    }
                    break;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TurnOffTimerUpdate();
            Activated -= MainForm_Activated;
            foreach(var rend in _renderers) NativeMethods.HideNA(rend);
            Hide();
            Dll.Close();

            CloseWndRender();
            _infoForm.Close();
            _infoForm = null;

            AllSettings.MainForm = new Rectangle(Left, Top, Width, Height);
            AllSettings.Save();
            
        }

        #endregion


    }
}
