using System.Drawing;

namespace VideoGraphSample
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.StatisticsList = new System.Windows.Forms.ListView();
            this.columnPid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.popupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemSize1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemWindow11 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemWindow12 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemWindow13 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemWindow14 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTelemetry = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTelemetryEnableAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTelemetryDisableAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemAllChannels = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEnableAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDisableAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemShowAll = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemShowHide = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEnableDisable = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemDisableAllButThis = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHideAllButThis = new System.Windows.Forms.ToolStripMenuItem();
            this.timer_updateTrackBar = new System.Windows.Forms.Timer(this.components);
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemVideo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSize = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemToLargest = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemToSmallest = new System.Windows.Forms.ToolStripMenuItem();
            this.tileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fitToDesktopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTileFit11 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTileFit12 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTileFit13 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTileFit14 = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showCtrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideCtrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.popupMenu.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatisticsList
            // 
            this.StatisticsList.CheckBoxes = true;
            this.StatisticsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPid});
            this.StatisticsList.ContextMenuStrip = this.popupMenu;
            this.StatisticsList.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StatisticsList.FullRowSelect = true;
            this.StatisticsList.GridLines = true;
            this.StatisticsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.StatisticsList.HideSelection = false;
            this.StatisticsList.Location = new System.Drawing.Point(12, 27);
            this.StatisticsList.MultiSelect = false;
            this.StatisticsList.Name = "StatisticsList";
            this.StatisticsList.ShowGroups = false;
            this.StatisticsList.Size = new System.Drawing.Size(322, 154);
            this.StatisticsList.TabIndex = 18;
            this.StatisticsList.UseCompatibleStateImageBehavior = false;
            this.StatisticsList.View = System.Windows.Forms.View.Details;
            this.StatisticsList.SelectedIndexChanged += new System.EventHandler(this.StatisticsList_SelectedIndexChanged);
            this.StatisticsList.DoubleClick += new System.EventHandler(this.StatisticsList_DoubleClick);
            // 
            // columnPid
            // 
            this.columnPid.Text = "PID";
            this.columnPid.Width = 83;
            // 
            // popupMenu
            // 
            this.popupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSize1,
            this.menuItemTelemetry,
            this.menuItemAllChannels,
            this.menuItemShowHide,
            this.menuItemEnableDisable,
            this.menuItemDisableAllButThis,
            this.menuItemHideAllButThis});
            this.popupMenu.Name = "popupMenu";
            this.popupMenu.Size = new System.Drawing.Size(187, 158);
            // 
            // menuItemSize1
            // 
            this.menuItemSize1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemWindow11,
            this.menuItemWindow12,
            this.menuItemWindow13,
            this.menuItemWindow14});
            this.menuItemSize1.Name = "menuItemSize1";
            this.menuItemSize1.Size = new System.Drawing.Size(186, 22);
            this.menuItemSize1.Text = "Size";
            // 
            // menuItemWindow11
            // 
            this.menuItemWindow11.Name = "menuItemWindow11";
            this.menuItemWindow11.Size = new System.Drawing.Size(89, 22);
            this.menuItemWindow11.Tag = "1";
            this.menuItemWindow11.Text = "1:1";
            this.menuItemWindow11.Click += new System.EventHandler(this.menuItemWindow1X_Click);
            // 
            // menuItemWindow12
            // 
            this.menuItemWindow12.Name = "menuItemWindow12";
            this.menuItemWindow12.Size = new System.Drawing.Size(89, 22);
            this.menuItemWindow12.Tag = "2";
            this.menuItemWindow12.Text = "1:2";
            this.menuItemWindow12.Click += new System.EventHandler(this.menuItemWindow1X_Click);
            // 
            // menuItemWindow13
            // 
            this.menuItemWindow13.Name = "menuItemWindow13";
            this.menuItemWindow13.Size = new System.Drawing.Size(89, 22);
            this.menuItemWindow13.Tag = "3";
            this.menuItemWindow13.Text = "1:3";
            this.menuItemWindow13.Click += new System.EventHandler(this.menuItemWindow1X_Click);
            // 
            // menuItemWindow14
            // 
            this.menuItemWindow14.Name = "menuItemWindow14";
            this.menuItemWindow14.Size = new System.Drawing.Size(89, 22);
            this.menuItemWindow14.Tag = "4";
            this.menuItemWindow14.Text = "1:4";
            this.menuItemWindow14.Click += new System.EventHandler(this.menuItemWindow1X_Click);
            // 
            // menuItemTelemetry
            // 
            this.menuItemTelemetry.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemTelemetryEnableAll,
            this.menuItemTelemetryDisableAll});
            this.menuItemTelemetry.Name = "menuItemTelemetry";
            this.menuItemTelemetry.Size = new System.Drawing.Size(186, 22);
            this.menuItemTelemetry.Text = "Telemetry";
            // 
            // menuItemTelemetryEnableAll
            // 
            this.menuItemTelemetryEnableAll.Name = "menuItemTelemetryEnableAll";
            this.menuItemTelemetryEnableAll.Size = new System.Drawing.Size(129, 22);
            this.menuItemTelemetryEnableAll.Text = "Enable All";
            // 
            // menuItemTelemetryDisableAll
            // 
            this.menuItemTelemetryDisableAll.Name = "menuItemTelemetryDisableAll";
            this.menuItemTelemetryDisableAll.Size = new System.Drawing.Size(129, 22);
            this.menuItemTelemetryDisableAll.Text = "Disable All";
            // 
            // menuItemAllChannels
            // 
            this.menuItemAllChannels.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemEnableAll,
            this.menuItemDisableAll,
            this.menuItemShowAll});
            this.menuItemAllChannels.Name = "menuItemAllChannels";
            this.menuItemAllChannels.Size = new System.Drawing.Size(186, 22);
            this.menuItemAllChannels.Text = "All Channels";
            // 
            // menuItemEnableAll
            // 
            this.menuItemEnableAll.Name = "menuItemEnableAll";
            this.menuItemEnableAll.Size = new System.Drawing.Size(112, 22);
            this.menuItemEnableAll.Text = "Enable";
            this.menuItemEnableAll.Click += new System.EventHandler(this.menuItemEnableAll_Click);
            // 
            // menuItemDisableAll
            // 
            this.menuItemDisableAll.Name = "menuItemDisableAll";
            this.menuItemDisableAll.Size = new System.Drawing.Size(112, 22);
            this.menuItemDisableAll.Text = "Disable";
            this.menuItemDisableAll.Click += new System.EventHandler(this.menuItemDisableAll_Click);
            // 
            // menuItemShowAll
            // 
            this.menuItemShowAll.Name = "menuItemShowAll";
            this.menuItemShowAll.Size = new System.Drawing.Size(112, 22);
            this.menuItemShowAll.Text = "Show";
            this.menuItemShowAll.Click += new System.EventHandler(this.menuItemShowAll_Click);
            // 
            // menuItemShowHide
            // 
            this.menuItemShowHide.Name = "menuItemShowHide";
            this.menuItemShowHide.Size = new System.Drawing.Size(186, 22);
            this.menuItemShowHide.Text = "Show/Hide DblClick";
            this.menuItemShowHide.Click += new System.EventHandler(this.menuItemShowHide_Click);
            // 
            // menuItemEnableDisable
            // 
            this.menuItemEnableDisable.Name = "menuItemEnableDisable";
            this.menuItemEnableDisable.Size = new System.Drawing.Size(186, 22);
            this.menuItemEnableDisable.Text = "Enable/Disable Space";
            this.menuItemEnableDisable.Click += new System.EventHandler(this.menuItemEnableDisable_Click);
            // 
            // menuItemDisableAllButThis
            // 
            this.menuItemDisableAllButThis.Name = "menuItemDisableAllButThis";
            this.menuItemDisableAllButThis.Size = new System.Drawing.Size(186, 22);
            this.menuItemDisableAllButThis.Text = "Enable This Only";
            this.menuItemDisableAllButThis.Click += new System.EventHandler(this.menuItemDisableAllButThis_Click);
            // 
            // menuItemHideAllButThis
            // 
            this.menuItemHideAllButThis.Name = "menuItemHideAllButThis";
            this.menuItemHideAllButThis.Size = new System.Drawing.Size(186, 22);
            this.menuItemHideAllButThis.Text = "Leave This Only";
            this.menuItemHideAllButThis.Click += new System.EventHandler(this.menuItemHideAllButThis_Click);
            // 
            // timer_updateTrackBar
            // 
            this.timer_updateTrackBar.Tick += new System.EventHandler(this.timer_updateTrackBar_Tick);
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemVideo});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(344, 24);
            this.mainMenu.TabIndex = 21;
            this.mainMenu.Text = "menuStrip1";
            // 
            // menuItemFile
            // 
            this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemOptions,
            this.toolStripSeparator1,
            this.menuItemExit});
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(37, 20);
            this.menuItemFile.Text = "File";
            // 
            // menuItemOptions
            // 
            this.menuItemOptions.Name = "menuItemOptions";
            this.menuItemOptions.Size = new System.Drawing.Size(180, 22);
            this.menuItemOptions.Text = "Options";
            this.menuItemOptions.Click += new System.EventHandler(this.menuItemOptions_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(180, 22);
            this.menuItemExit.Text = "Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItemVideo
            // 
            this.menuItemVideo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSize,
            this.tileToolStripMenuItem,
            this.cascadeToolStripMenuItem,
            this.controlPanelToolStripMenuItem});
            this.menuItemVideo.Name = "menuItemVideo";
            this.menuItemVideo.Size = new System.Drawing.Size(49, 20);
            this.menuItemVideo.Text = "Video";
            // 
            // menuItemSize
            // 
            this.menuItemSize.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem11,
            this.menuItem12,
            this.menuItem13,
            this.menuItem14,
            this.toolStripSeparator2,
            this.menuItemToLargest,
            this.menuItemToSmallest});
            this.menuItemSize.Name = "menuItemSize";
            this.menuItemSize.Size = new System.Drawing.Size(180, 22);
            this.menuItemSize.Text = "Size";
            // 
            // menuItem11
            // 
            this.menuItem11.Name = "menuItem11";
            this.menuItem11.Size = new System.Drawing.Size(180, 22);
            this.menuItem11.Tag = "1";
            this.menuItem11.Text = "1:1";
            this.menuItem11.Click += new System.EventHandler(this.menuItemSetScale_Click);
            // 
            // menuItem12
            // 
            this.menuItem12.Name = "menuItem12";
            this.menuItem12.Size = new System.Drawing.Size(180, 22);
            this.menuItem12.Tag = "2";
            this.menuItem12.Text = "1:2";
            this.menuItem12.Click += new System.EventHandler(this.menuItemSetScale_Click);
            // 
            // menuItem13
            // 
            this.menuItem13.Name = "menuItem13";
            this.menuItem13.Size = new System.Drawing.Size(180, 22);
            this.menuItem13.Tag = "3";
            this.menuItem13.Text = "1:3";
            this.menuItem13.Click += new System.EventHandler(this.menuItemSetScale_Click);
            // 
            // menuItem14
            // 
            this.menuItem14.Name = "menuItem14";
            this.menuItem14.Size = new System.Drawing.Size(180, 22);
            this.menuItem14.Tag = "4";
            this.menuItem14.Text = "1:4";
            this.menuItem14.Click += new System.EventHandler(this.menuItemSetScale_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // menuItemToLargest
            // 
            this.menuItemToLargest.Name = "menuItemToLargest";
            this.menuItemToLargest.Size = new System.Drawing.Size(180, 22);
            this.menuItemToLargest.Text = "To Largest";
            this.menuItemToLargest.Click += new System.EventHandler(this.menuItemToLargest_Click);
            // 
            // menuItemToSmallest
            // 
            this.menuItemToSmallest.Name = "menuItemToSmallest";
            this.menuItemToSmallest.Size = new System.Drawing.Size(180, 22);
            this.menuItemToSmallest.Text = "To Smallest";
            this.menuItemToSmallest.Click += new System.EventHandler(this.menuItemToSmallest_Click);
            // 
            // tileToolStripMenuItem
            // 
            this.tileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fitToDesktopToolStripMenuItem,
            this.menuItemTileFit11,
            this.menuItemTileFit12,
            this.menuItemTileFit13,
            this.menuItemTileFit14});
            this.tileToolStripMenuItem.Name = "tileToolStripMenuItem";
            this.tileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.tileToolStripMenuItem.Text = "Tile";
            // 
            // fitToDesktopToolStripMenuItem
            // 
            this.fitToDesktopToolStripMenuItem.Name = "fitToDesktopToolStripMenuItem";
            this.fitToDesktopToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fitToDesktopToolStripMenuItem.Text = "Fit to Desktop";
            this.fitToDesktopToolStripMenuItem.Click += new System.EventHandler(this.menuItemTileFitToDesktop_Click);
            // 
            // menuItemTileFit11
            // 
            this.menuItemTileFit11.Name = "menuItemTileFit11";
            this.menuItemTileFit11.Size = new System.Drawing.Size(180, 22);
            this.menuItemTileFit11.Tag = "1";
            this.menuItemTileFit11.Text = "1:1";
            this.menuItemTileFit11.Click += new System.EventHandler(this.menuItemTileFit1X_click);
            // 
            // menuItemTileFit12
            // 
            this.menuItemTileFit12.Name = "menuItemTileFit12";
            this.menuItemTileFit12.Size = new System.Drawing.Size(180, 22);
            this.menuItemTileFit12.Tag = "2";
            this.menuItemTileFit12.Text = "1:2";
            this.menuItemTileFit12.Click += new System.EventHandler(this.menuItemTileFit1X_click);
            // 
            // menuItemTileFit13
            // 
            this.menuItemTileFit13.Name = "menuItemTileFit13";
            this.menuItemTileFit13.Size = new System.Drawing.Size(180, 22);
            this.menuItemTileFit13.Tag = "3";
            this.menuItemTileFit13.Text = "1:3";
            this.menuItemTileFit13.Click += new System.EventHandler(this.menuItemTileFit1X_click);
            // 
            // menuItemTileFit14
            // 
            this.menuItemTileFit14.Name = "menuItemTileFit14";
            this.menuItemTileFit14.Size = new System.Drawing.Size(180, 22);
            this.menuItemTileFit14.Tag = "4";
            this.menuItemTileFit14.Text = "1:4";
            this.menuItemTileFit14.Click += new System.EventHandler(this.menuItemTileFit1X_click);
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cascadeToolStripMenuItem.Text = "Cascade";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.menuItemCascade_Click);
            // 
            // controlPanelToolStripMenuItem
            // 
            this.controlPanelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showCtrlToolStripMenuItem,
            this.hideCtrlToolStripMenuItem});
            this.controlPanelToolStripMenuItem.Name = "controlPanelToolStripMenuItem";
            this.controlPanelToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.controlPanelToolStripMenuItem.Text = "ControlPanel";
            this.controlPanelToolStripMenuItem.Visible = false;
            // 
            // showCtrlToolStripMenuItem
            // 
            this.showCtrlToolStripMenuItem.Name = "showCtrlToolStripMenuItem";
            this.showCtrlToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showCtrlToolStripMenuItem.Text = "Show";
            this.showCtrlToolStripMenuItem.Click += new System.EventHandler(this.showCtrlToolStripMenuItem_Click);
            // 
            // hideCtrlToolStripMenuItem
            // 
            this.hideCtrlToolStripMenuItem.Name = "hideCtrlToolStripMenuItem";
            this.hideCtrlToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.hideCtrlToolStripMenuItem.Text = "Hide";
            this.hideCtrlToolStripMenuItem.Click += new System.EventHandler(this.hideCtrlToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 193);
            this.Controls.Add(this.StatisticsList);
            this.Controls.Add(this.mainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.popupMenu.ResumeLayout(false);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView StatisticsList;
        private System.Windows.Forms.ColumnHeader columnPid;
        private System.Windows.Forms.Timer timer_updateTrackBar;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItemFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemOptions;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.ToolStripMenuItem menuItemVideo;
        private System.Windows.Forms.ToolStripMenuItem menuItemSize;
        private System.Windows.Forms.ToolStripMenuItem menuItem11;
        private System.Windows.Forms.ToolStripMenuItem menuItem12;
        private System.Windows.Forms.ToolStripMenuItem menuItem13;
        private System.Windows.Forms.ToolStripMenuItem menuItem14;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuItemToLargest;
        private System.Windows.Forms.ToolStripMenuItem menuItemToSmallest;
        private System.Windows.Forms.ToolStripMenuItem tileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fitToDesktopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItemTileFit11;
        private System.Windows.Forms.ToolStripMenuItem menuItemTileFit12;
        private System.Windows.Forms.ToolStripMenuItem menuItemTileFit13;
        private System.Windows.Forms.ToolStripMenuItem menuItemTileFit14;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip popupMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItemSize1;
        private System.Windows.Forms.ToolStripMenuItem menuItemWindow11;
        private System.Windows.Forms.ToolStripMenuItem menuItemWindow12;
        private System.Windows.Forms.ToolStripMenuItem menuItemWindow13;
        private System.Windows.Forms.ToolStripMenuItem menuItemWindow14;
        private System.Windows.Forms.ToolStripMenuItem menuItemTelemetry;
        private System.Windows.Forms.ToolStripMenuItem menuItemTelemetryEnableAll;
        private System.Windows.Forms.ToolStripMenuItem menuItemTelemetryDisableAll;
        private System.Windows.Forms.ToolStripMenuItem menuItemAllChannels;
        private System.Windows.Forms.ToolStripMenuItem menuItemEnableAll;
        private System.Windows.Forms.ToolStripMenuItem menuItemDisableAll;
        private System.Windows.Forms.ToolStripMenuItem menuItemShowAll;
        private System.Windows.Forms.ToolStripMenuItem menuItemShowHide;
        private System.Windows.Forms.ToolStripMenuItem menuItemEnableDisable;
        private System.Windows.Forms.ToolStripMenuItem menuItemDisableAllButThis;
        private System.Windows.Forms.ToolStripMenuItem menuItemHideAllButThis;
        private System.Windows.Forms.ToolStripMenuItem controlPanelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showCtrlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideCtrlToolStripMenuItem;
    }
}

