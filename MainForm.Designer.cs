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
            this.StatisticsList = new System.Windows.Forms.ListView();
            this.columnPid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timer_updateTrackBar = new System.Windows.Forms.Timer(this.components);
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemEditPids = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.popupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sizeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.telemetryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHideDblClickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableDisableSpaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableThisOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leaveThisOnlyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button_PathFile = new System.Windows.Forms.Button();
            this.path_textBox = new System.Windows.Forms.TextBox();
            this.mainMenu.SuspendLayout();
            this.popupMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatisticsList
            // 
            this.StatisticsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPid});
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
            this.StatisticsList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.StatisticsList_ItemChecked);
            // 
            // columnPid
            // 
            this.columnPid.Text = "PID";
            this.columnPid.Width = 83;
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
            this.menuItemEditPids,
            this.toolStripSeparator1,
            this.menuItemExit});
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(37, 20);
            this.menuItemFile.Text = "File";
            // 
            // menuItemOptions
            // 
            this.menuItemOptions.Name = "menuItemOptions";
            this.menuItemOptions.Size = new System.Drawing.Size(120, 22);
            this.menuItemOptions.Text = "Options";
            // 
            // menuItemEditPids
            // 
            this.menuItemEditPids.Name = "menuItemEditPids";
            this.menuItemEditPids.Size = new System.Drawing.Size(120, 22);
            this.menuItemEditPids.Text = "Edit PIDs";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(117, 6);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(120, 22);
            this.menuItemExit.Text = "Exit";
            // 
            // menuItemVideo
            // 
            this.menuItemVideo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSize,
            this.tileToolStripMenuItem,
            this.cascadeToolStripMenuItem});
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
            this.menuItemSize.Size = new System.Drawing.Size(118, 22);
            this.menuItemSize.Text = "Size";
            // 
            // menuItem11
            // 
            this.menuItem11.Name = "menuItem11";
            this.menuItem11.Size = new System.Drawing.Size(133, 22);
            this.menuItem11.Text = "1:1";
            // 
            // menuItem12
            // 
            this.menuItem12.Name = "menuItem12";
            this.menuItem12.Size = new System.Drawing.Size(133, 22);
            this.menuItem12.Text = "1:2";
            // 
            // menuItem13
            // 
            this.menuItem13.Name = "menuItem13";
            this.menuItem13.Size = new System.Drawing.Size(133, 22);
            this.menuItem13.Text = "1:3";
            // 
            // menuItem14
            // 
            this.menuItem14.Name = "menuItem14";
            this.menuItem14.Size = new System.Drawing.Size(133, 22);
            this.menuItem14.Text = "1:4";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(130, 6);
            // 
            // menuItemToLargest
            // 
            this.menuItemToLargest.Name = "menuItemToLargest";
            this.menuItemToLargest.Size = new System.Drawing.Size(133, 22);
            this.menuItemToLargest.Text = "To Largest";
            // 
            // menuItemToSmallest
            // 
            this.menuItemToSmallest.Name = "menuItemToSmallest";
            this.menuItemToSmallest.Size = new System.Drawing.Size(133, 22);
            this.menuItemToSmallest.Text = "To Smallest";
            // 
            // tileToolStripMenuItem
            // 
            this.tileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fitToDesktopToolStripMenuItem,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9});
            this.tileToolStripMenuItem.Name = "tileToolStripMenuItem";
            this.tileToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.tileToolStripMenuItem.Text = "Tile";
            // 
            // fitToDesktopToolStripMenuItem
            // 
            this.fitToDesktopToolStripMenuItem.Name = "fitToDesktopToolStripMenuItem";
            this.fitToDesktopToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.fitToDesktopToolStripMenuItem.Text = "Fit to Desktop";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(147, 22);
            this.toolStripMenuItem6.Text = "1:1";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(147, 22);
            this.toolStripMenuItem7.Text = "1:2";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(147, 22);
            this.toolStripMenuItem8.Text = "1:3";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(147, 22);
            this.toolStripMenuItem9.Text = "1:4";
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.cascadeToolStripMenuItem.Text = "Cascade";
            // 
            // popupMenu
            // 
            this.popupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sizeToolStripMenuItem1,
            this.telemetryToolStripMenuItem,
            this.allChannelsToolStripMenuItem,
            this.showHideDblClickToolStripMenuItem,
            this.enableDisableSpaceToolStripMenuItem,
            this.enableThisOnlyToolStripMenuItem,
            this.leaveThisOnlyToolStripMenuItem});
            this.popupMenu.Name = "popupMenu";
            this.popupMenu.Size = new System.Drawing.Size(187, 158);
            // 
            // sizeToolStripMenuItem1
            // 
            this.sizeToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem10,
            this.toolStripMenuItem11,
            this.toolStripMenuItem12,
            this.toolStripMenuItem13});
            this.sizeToolStripMenuItem1.Name = "sizeToolStripMenuItem1";
            this.sizeToolStripMenuItem1.Size = new System.Drawing.Size(186, 22);
            this.sizeToolStripMenuItem1.Text = "Size";
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(89, 22);
            this.toolStripMenuItem10.Text = "1:1";
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(89, 22);
            this.toolStripMenuItem11.Text = "1:2";
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(89, 22);
            this.toolStripMenuItem12.Text = "1:3";
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(89, 22);
            this.toolStripMenuItem13.Text = "1:4";
            // 
            // telemetryToolStripMenuItem
            // 
            this.telemetryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableAllToolStripMenuItem,
            this.disableAllToolStripMenuItem});
            this.telemetryToolStripMenuItem.Name = "telemetryToolStripMenuItem";
            this.telemetryToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.telemetryToolStripMenuItem.Text = "Telemetry";
            // 
            // enableAllToolStripMenuItem
            // 
            this.enableAllToolStripMenuItem.Name = "enableAllToolStripMenuItem";
            this.enableAllToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.enableAllToolStripMenuItem.Text = "Enable All";
            // 
            // disableAllToolStripMenuItem
            // 
            this.disableAllToolStripMenuItem.Name = "disableAllToolStripMenuItem";
            this.disableAllToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.disableAllToolStripMenuItem.Text = "Disable All";
            // 
            // allChannelsToolStripMenuItem
            // 
            this.allChannelsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableToolStripMenuItem,
            this.disableToolStripMenuItem,
            this.showToolStripMenuItem});
            this.allChannelsToolStripMenuItem.Name = "allChannelsToolStripMenuItem";
            this.allChannelsToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.allChannelsToolStripMenuItem.Text = "All Channels";
            // 
            // enableToolStripMenuItem
            // 
            this.enableToolStripMenuItem.Name = "enableToolStripMenuItem";
            this.enableToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.enableToolStripMenuItem.Text = "Enable";
            // 
            // disableToolStripMenuItem
            // 
            this.disableToolStripMenuItem.Name = "disableToolStripMenuItem";
            this.disableToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.disableToolStripMenuItem.Text = "Disable";
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.showToolStripMenuItem.Text = "Show";
            // 
            // showHideDblClickToolStripMenuItem
            // 
            this.showHideDblClickToolStripMenuItem.Name = "showHideDblClickToolStripMenuItem";
            this.showHideDblClickToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.showHideDblClickToolStripMenuItem.Text = "Show/Hide DblClick";
            // 
            // enableDisableSpaceToolStripMenuItem
            // 
            this.enableDisableSpaceToolStripMenuItem.Name = "enableDisableSpaceToolStripMenuItem";
            this.enableDisableSpaceToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.enableDisableSpaceToolStripMenuItem.Text = "Enable/Disable Space";
            // 
            // enableThisOnlyToolStripMenuItem
            // 
            this.enableThisOnlyToolStripMenuItem.Name = "enableThisOnlyToolStripMenuItem";
            this.enableThisOnlyToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.enableThisOnlyToolStripMenuItem.Text = "Enable This Only";
            // 
            // leaveThisOnlyToolStripMenuItem
            // 
            this.leaveThisOnlyToolStripMenuItem.Name = "leaveThisOnlyToolStripMenuItem";
            this.leaveThisOnlyToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.leaveThisOnlyToolStripMenuItem.Text = "Leave This Only";
            // 
            // button_PathFile
            // 
            this.button_PathFile.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_PathFile.Location = new System.Drawing.Point(212, 231);
            this.button_PathFile.Name = "button_PathFile";
            this.button_PathFile.Size = new System.Drawing.Size(122, 27);
            this.button_PathFile.TabIndex = 19;
            this.button_PathFile.Text = "Open file...";
            this.button_PathFile.UseVisualStyleBackColor = true;
            this.button_PathFile.Click += new System.EventHandler(this.button_PathFile_Click);
            // 
            // path_textBox
            // 
            this.path_textBox.Location = new System.Drawing.Point(12, 205);
            this.path_textBox.Name = "path_textBox";
            this.path_textBox.Size = new System.Drawing.Size(322, 20);
            this.path_textBox.TabIndex = 20;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 270);
            this.Controls.Add(this.path_textBox);
            this.Controls.Add(this.button_PathFile);
            this.Controls.Add(this.StatisticsList);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.popupMenu.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem menuItemEditPids;
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
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip popupMenu;
        private System.Windows.Forms.ToolStripMenuItem sizeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem telemetryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showHideDblClickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableDisableSpaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableThisOnlyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leaveThisOnlyToolStripMenuItem;
        private System.Windows.Forms.Button button_PathFile;
        private System.Windows.Forms.TextBox path_textBox;
    }
}

