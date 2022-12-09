
using System.Windows.Forms;

namespace VideoGraphSample
{
    sealed partial class RendererContainerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code



        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.trackBar_Player = new System.Windows.Forms.TrackBar();
            this.pictureBox_Player = new System.Windows.Forms.PictureBox();
            this.rendererContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemSize11 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSize12 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSize13 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSize14 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemShowTelemetry = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Player)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Player)).BeginInit();
            this.rendererContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBar_Player
            // 
            this.trackBar_Player.Cursor = System.Windows.Forms.Cursors.Hand;
            this.trackBar_Player.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.trackBar_Player.LargeChange = 0;
            this.trackBar_Player.Location = new System.Drawing.Point(0, 316);
            this.trackBar_Player.Maximum = 100;
            this.trackBar_Player.Name = "trackBar_Player";
            this.trackBar_Player.Size = new System.Drawing.Size(384, 45);
            this.trackBar_Player.TabIndex = 0;
            // 
            // pictureBox_Player
            // 
            this.pictureBox_Player.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_Player.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_Player.Name = "pictureBox_Player";
            this.pictureBox_Player.Size = new System.Drawing.Size(384, 316);
            this.pictureBox_Player.TabIndex = 1;
            this.pictureBox_Player.TabStop = false;
            // 
            // rendererContextMenu
            // 
            this.rendererContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemSize11,
            this.menuItemSize12,
            this.menuItemSize13,
            this.menuItemSize14,
            this.toolStripSeparator1,
            this.menuItemShowTelemetry});
            this.rendererContextMenu.Name = "rendererContextMenu";
            this.rendererContextMenu.Size = new System.Drawing.Size(181, 142);
            // 
            // menuItemSize11
            // 
            this.menuItemSize11.Name = "menuItemSize11";
            this.menuItemSize11.Size = new System.Drawing.Size(154, 22);
            this.menuItemSize11.Text = "1:1";
            // 
            // menuItemSize12
            // 
            this.menuItemSize12.Name = "menuItemSize12";
            this.menuItemSize12.Size = new System.Drawing.Size(154, 22);
            this.menuItemSize12.Text = "1:2";
            // 
            // menuItemSize13
            // 
            this.menuItemSize13.Name = "menuItemSize13";
            this.menuItemSize13.Size = new System.Drawing.Size(154, 22);
            this.menuItemSize13.Text = "1:3";
            // 
            // menuItemSize14
            // 
            this.menuItemSize14.Name = "menuItemSize14";
            this.menuItemSize14.Size = new System.Drawing.Size(154, 22);
            this.menuItemSize14.Text = "1:4";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(151, 6);
            // 
            // menuItemShowTelemetry
            // 
            this.menuItemShowTelemetry.Checked = true;
            this.menuItemShowTelemetry.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItemShowTelemetry.Name = "menuItemShowTelemetry";
            this.menuItemShowTelemetry.Size = new System.Drawing.Size(180, 22);
            this.menuItemShowTelemetry.Text = "ShowTelemetry";
            this.menuItemShowTelemetry.Click += new System.EventHandler(this.menuItemShowTelemetry_Click);
            // 
            // RendererContainerForm
            // 
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.pictureBox_Player);
            this.Controls.Add(this.trackBar_Player);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "RendererContainerForm";
            this.Activated += new System.EventHandler(this.RendererContainerForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RendererContainerForm_FormClosing);
            this.ResizeBegin += new System.EventHandler(this.RendererContainerForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.RendererContainerForm_ResizeEnd);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Player)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Player)).EndInit();
            this.rendererContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TrackBar trackBar_Player;
        private PictureBox pictureBox_Player;
        private ContextMenuStrip rendererContextMenu;
        private ToolStripMenuItem menuItemSize11;
        private ToolStripMenuItem menuItemSize12;
        private ToolStripMenuItem menuItemSize13;
        private ToolStripMenuItem menuItemSize14;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem menuItemShowTelemetry;
    }
}