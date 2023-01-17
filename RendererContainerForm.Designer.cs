
using System.Windows.Forms;

namespace BIONVideoPlayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RendererContainerForm));
            this.rendererContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemSize11 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSize12 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSize13 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSize14 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemShowTelemetry = new System.Windows.Forms.ToolStripMenuItem();
            this.rendererContextMenu.SuspendLayout();
            this.SuspendLayout();
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
            this.rendererContextMenu.Size = new System.Drawing.Size(155, 120);
            // 
            // menuItemSize11
            // 
            this.menuItemSize11.Name = "menuItemSize11";
            this.menuItemSize11.Size = new System.Drawing.Size(154, 22);
            this.menuItemSize11.Tag = "1";
            this.menuItemSize11.Text = "1:1";
            this.menuItemSize11.Click += new System.EventHandler(this.menuItemSize1X_Click);
            // 
            // menuItemSize12
            // 
            this.menuItemSize12.Name = "menuItemSize12";
            this.menuItemSize12.Size = new System.Drawing.Size(154, 22);
            this.menuItemSize12.Tag = "2";
            this.menuItemSize12.Text = "1:2";
            this.menuItemSize12.Click += new System.EventHandler(this.menuItemSize1X_Click);
            // 
            // menuItemSize13
            // 
            this.menuItemSize13.Name = "menuItemSize13";
            this.menuItemSize13.Size = new System.Drawing.Size(154, 22);
            this.menuItemSize13.Tag = "3";
            this.menuItemSize13.Text = "1:3";
            this.menuItemSize13.Click += new System.EventHandler(this.menuItemSize1X_Click);
            // 
            // menuItemSize14
            // 
            this.menuItemSize14.Name = "menuItemSize14";
            this.menuItemSize14.Size = new System.Drawing.Size(154, 22);
            this.menuItemSize14.Tag = "4";
            this.menuItemSize14.Text = "1:4";
            this.menuItemSize14.Click += new System.EventHandler(this.menuItemSize1X_Click);
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
            this.menuItemShowTelemetry.Size = new System.Drawing.Size(154, 22);
            this.menuItemShowTelemetry.Text = "ShowTelemetry";
            this.menuItemShowTelemetry.Click += new System.EventHandler(this.menuItemShowTelemetry_Click);
            // 
            // RendererContainerForm
            // 
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.ContextMenuStrip = this.rendererContextMenu;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(240, 240);
            this.Name = "RendererContainerForm";
            this.Activated += new System.EventHandler(this.RendererContainerForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RendererContainerForm_FormClosing);
            this.ResizeBegin += new System.EventHandler(this.RendererContainerForm_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.RendererContainerForm_ResizeEnd);
            this.Resize += new System.EventHandler(this.RendererContainerForm_Resize);
            this.rendererContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private ContextMenuStrip rendererContextMenu;
        private ToolStripMenuItem menuItemSize11;
        private ToolStripMenuItem menuItemSize12;
        private ToolStripMenuItem menuItemSize13;
        private ToolStripMenuItem menuItemSize14;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem menuItemShowTelemetry;
    }
}