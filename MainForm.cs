using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace VideoGraphSample
{
    public partial class MainForm : Form
    {
        private Dictionary<ushort, bool> _mapPids;
        private int[] _pids;
        private int[] _pmts;

        private RendererContainerForm[] _renderers;
        private readonly VideoPlayControl _controlVideoPanel = new VideoPlayControl();

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
                MessageBox.Show(e.Message, @"Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private Dll.AllChannels GetChannels()
        {
            unsafe
            {
                var channels = new Dll.AllChannels
                    {NumVideoPids = _renderers.Length};

                
                for (var i = 0; i < _renderers.Length; i++)
                {
                    channels.Pids[i] = _pids[i];
                    channels.hWnds[i] = (int) _renderers[i].Handle;
                    channels.Pmts[i] = _pmts[i];
                }

                for (var i = _renderers.Length; i < Defines.MaxChannels; i++)
                {
                    channels.Pids[i] = 0;
                    channels.hWnds[i] = 0;
                    channels.Pmts[i] = 0;
                }

                return channels;
            }
        }

        #region Setup func

        private void UpdateTelemetry()
        {
            Dll.UpdateTelemetryPosition();
            Dll.UpdateTelemetryAlpha();
            Dll.UpdateTelemetryColors();
        }

        private void CreateListItems()
        {
            StatisticsList.Items.Clear();

            foreach (var item in _mapPids.Where(item => item.Value))
            {
                StatisticsList.Items.Add($"0x{item.Key:X4}");
            }
        }

        private void ClearAuxiliary()
        {
            TurnOff_TimerUpdate();
            Hide_VideoControlPanel();
            _mapPids?.Clear();
            RemoveRendererEvent();
            CloseWndRender();
            Dll.Close();
        }

        private void Start(string filePath)
        {
            if (Dll.DllOpened) ClearAuxiliary();

            CreateMap();
            ScanBytes.SearchSyncByte(filePath, ref _mapPids);
            CreateListItems();
            CreateWndRender();
            Dll.Open(filePath, GetChannels());
            UpdateTelemetry();
            Dll.SetStart();
            TurnOn_TimerUpdate();
            Setup_VideoControlStart();
        }

        #endregion

        #region VideoControl panel

        private void Setup_VideoControlStart()
        {
            _controlVideoPanel.SetDefault();
            Show_VideoControlPanel();
            controlPanelToolStripMenuItem.Visible = true;
            Setup_ControlPanelSettings(false);
        }

        private void Show_VideoControlPanel()
        {
            NativeMethods.ShowNa(_controlVideoPanel);
            var newLocation = new Point(Location.X + 8, Location.Y + Height);
            _controlVideoPanel.Location = newLocation;
            _controlVideoPanel.Visible = true;
        }

        private void Hide_VideoControlPanel()
        {
            _controlVideoPanel.Hide();
            NativeMethods.HideNa(_controlVideoPanel);
        }

        private void showCtrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setup_ControlPanelSettings(false);
            Show_VideoControlPanel();
        }

        private void hideCtrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Setup_ControlPanelSettings(true);
            Hide_VideoControlPanel();
        }

        private void Setup_ControlPanelSettings(bool value)
        {
            showCtrlToolStripMenuItem.Enabled = value;
            hideCtrlToolStripMenuItem.Enabled = !value;
        }

        #endregion

        #region Timer function

        public void TurnOn_TimerUpdate()
        {
            if (timer_updateTrackBar.Enabled) return;
            timer_updateTrackBar.Start();
        }

        public void TurnOff_TimerUpdate()
        {
            if (!timer_updateTrackBar.Enabled) return;
            timer_updateTrackBar.Stop();
        }

        private void timer_updateTrackBar_Tick(object sender, EventArgs e)
        {
            if(!_controlVideoPanel.Paused) _controlVideoPanel.Get_trackBar_Position();
        }

        #endregion

        #region Setup Form

        private void SetTitle()
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
            var quantity = CalculatePids();
            if (quantity == 0) return;
            CreateWndRenderArray(quantity);
            AddRendererEvent();
        }

        private byte CalculatePids()
        {
            byte count = 0;
            foreach (var item in _mapPids.Where(item => item.Value)) count++;
            return count;
        }

        private void CreateWndRenderArray(byte count)
        {
            _renderers = new RendererContainerForm[count];
            _pids = new int[count];
            _pmts = new int[count];
            ushort id = 0;
            ushort counter = 0;
            foreach (var item in _mapPids)
            {
                if (item.Value)
                {
                    
                    _pids[id] = item.Key;
                    _pmts[id] = item.Key - 0x41;
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

            for (var i = 0; i < _renderers.Length; i++)
            {
                var rend = _renderers[i];
                AllSettings.Renderers[i] = new Rectangle(rend.Left, rend.Top, rend.VideoWidth, rend.VideoHeight);
                rend.DoClose();
            }
            
            _renderers = null;
            _pids = null;
            _pmts = null;
        }
        #endregion

        #region Menu item handlers

        public static int GetMenuItemScale(object sender)
        {
            if (!(sender is ToolStripMenuItem menuItem)) return -1;
            if (int.TryParse(menuItem.Tag.ToString(), out var scale)) return scale;

            return -1;
        }

        public void menuItemSetScale_Click(object sender, EventArgs e)
        {
            var scale = GetMenuItemScale(sender);
            if (scale <= 0) return;

            var w = Defines.VideoW / scale;
            var h = Defines.VideoH / scale;

            foreach(var rend in _renderers) rend.SetVideoSize(w, h);
        }

        private void menuItemWindow1X_Click(object sender, EventArgs e)
        {
            var scale = GetMenuItemScale(sender);
            if (scale <= 0) return;

            var rend = GetSelectedRenderer();
            if (rend == null) return;

            var w = Defines.VideoW / scale;
            var h = Defines.VideoH / scale;
            rend.SetVideoSize(w, h);
        }

        private void menuItemCascade_Click(object sender, EventArgs e)
        {
            var visrenderers = GetVisibleRenderers();
            if (visrenderers == null || visrenderers.Length < 2) return;

            for (var i = 0; i < visrenderers.Length; i++)
            {
                var rend = visrenderers[i];
                rend.Left = Defines.CascadeOffsetX * i;
                rend.Top = Defines.CascadeOffsetY * i;
                NativeMethods.BringFormToFront(rend);
            }

        }

        private void menuItemToLargest_Click(object sender, EventArgs e)
        {
            var maxW = int.MinValue;
            foreach (var rend in _renderers)
            {
                if (rend.VideoWidth > maxW) maxW = rend.VideoWidth;
            }

            foreach(var rend in _renderers) rend.SetVideoWidth(maxW);
        }

        private void menuItemToSmallest_Click(object sender, EventArgs e)
        {
            var minW = int.MaxValue;
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
            var numrows = (visrenderers.Length + Defines.GridSize - 1) / Defines.GridSize;

            var rend = visrenderers[0];

            var diff = rend.GetWindowVideoDifference();

            var maxWindowW = desktop.Width / Defines.GridSize;
            var maxWindowH = desktop.Height / numrows;
            var maxVideoW = maxWindowW - diff.Width;
            var maxVideoH = maxWindowH - diff.Height;

            var useH = visrenderers[0].W2H(maxWindowW) > maxVideoH;

            foreach (var r in visrenderers)
            {
                if (useH) r.SetVideoHeight(maxVideoH);
                else r.SetVideoWidth(maxVideoW);
            }

            var i = 0;
            var rendW = rend.Width;
            var rendH = rend.Height;
            for (var row = 0; row < Defines.GridSize; row++)
            {
                var y = row * rendH;
                for (var col = 0; col < Defines.GridSize; col++)
                {
                    if (i >= visrenderers.Length) break;

                    var x = col * rendW;
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

            var scale = GetMenuItemScale(sender);
            if (scale <= 0) return;

            var w = Defines.VideoW / scale;
            var h = Defines.VideoH / scale;

            foreach(var rend in visrenderers) rend.SetVideoSize(w, h);

            var i = 0;
            var rendW = visrenderers[0].Width;
            var rendH = visrenderers[0].Height;
            for (var row = 0; row < Defines.GridSize; row++)
            {
                var y = row * rendH;
                for (var col = 0; col < Defines.GridSize; col++)
                {
                    if (i >= visrenderers.Length) break;

                    var x = col * rendW;
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
                if ((frm.ShowDialog() != DialogResult.OK) || frm.FilePath == null) return;
                Start(frm.FilePath);
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
            foreach(var rend in _renderers) NativeMethods.ShowNa(rend);
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

            foreach (var rend in _renderers)
            {
                if (rend == selrend) continue;
                var item = FindRendererItem(rend);
                if (item != null) item.Checked = false;
                NativeMethods.HideNa(rend);
            }
        }

        #endregion

        #region Renderer stuff

        private void RemoveRendererEvent()
        {
            StatisticsList.ItemChecked -= StatisticsList_ItemChecked;

            GetSelectedRenderer();

            foreach (var rend in _renderers)
            {
                rend.OnActivation -= RendererActivated;
                rend.OnHidden -= RendererHidden;
                rend.OnMoveBegin -= OnRendererMoveBegin;
                rend.OnMoveEnd -= OnRendererMoveEnd;
                rend.OnTelemetryEnableChange -= OnRendererTelemetryEnableChange;
            }

            Activated -= MainForm_Activated;
        }

        private void AddRendererEvent()
        {
            StatisticsList.ItemChecked += StatisticsList_ItemChecked;

            GetSelectedRenderer();

            foreach (var rend in _renderers)
            {
                NativeMethods.ShowNa(rend); 
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
            var idx = e.Item.Index;
            var map = e.Item.Checked;
            Dll.MapUnmapChannel(idx, map);
        }

        private void StatisticsList_DoubleClick(object sender, EventArgs e)
        {
            var rend = GetSelectedRenderer();
            if (rend == null) return;

            var item = FindRendererItem(rend);

            if (NativeMethods.IsWindowVisible(rend.Handle))
            {
                if (item != null) item.Checked = false;
                NativeMethods.HideNa(rend);
            }
            else
            {
                if (item != null) item.Checked = true;
                NativeMethods.ShowNa(rend);
            }
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
            var idx = FindRendererIdx(o);
            return idx == -1 ? null : StatisticsList.Items[idx];
        }

        private void OnRendererMoveBegin(object sender, EventArgs e)
        {
            if (NativeMethods.IsShiftPressed())
            {
                _rendererLocations = new Point[_renderers.Length];
                for (var i = 0; i < _renderers.Length; i++)
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
            var originatoridx = FindRendererIdx(sender);
            if (originatoridx < 0) return;
            var originator = _renderers[originatoridx];
            var origpos = _rendererLocations[originatoridx];
            var deltaX = originator.Left - origpos.X;
            var deltaY = originator.Top - origpos.Y;

            for (var i = 0; i < _renderers.Length; i++)
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
            var originatoridx = FindRendererIdx(sender);
            if (originatoridx < 0) return;
            var originator = _renderers[originatoridx];

            for (var i = 0; i < _renderers.Length; i++)
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
            var rendidx = FindRendererIdx(sender);
            if (rendidx < 0) return;

            var bit = 1 << rendidx;
            var rend = _renderers[rendidx];
            if (rend.IsTelemetryEnabled()) AllSettings.EnableTelemetry |= bit;
            else AllSettings.EnableTelemetry &= ~bit;

        }

        private IntPtr[] GetRendererHandles()
        {
            var hwnds = new IntPtr[_renderers.Length];
            for (var i = 0; i < _renderers.Length; i++) hwnds[i] = _renderers[i].Handle;
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
                for (var i = 0; i < _renderers.Length; i++) NativeMethods.BringFormToFront(hwnds[i]);
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
                            NativeMethods.HideNa(hwnd);
                        }
                    }
                    break;

                case FormWindowState.Normal:
                    if (_visiblerenderers != null)
                    {
                        foreach (var hwnd in _visiblerenderers) NativeMethods.ShowNa(hwnd);
                        _visiblerenderers = null;
                    }
                    break;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Dll.DllOpened)
            {
                TurnOff_TimerUpdate();
                foreach (var rend in _renderers) NativeMethods.HideNa(rend);
            }
            
            Activated -= MainForm_Activated;
            
            Hide();
            Dll.Close();

            CloseWndRender();
            _infoForm.Close();
            _infoForm = null;
            _controlVideoPanel.Close();

            AllSettings.MainForm = new Rectangle(Left, Top, Width, Height);
            AllSettings.Save();
            
        }

        #endregion

        
    }
}
