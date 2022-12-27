
namespace VideoGraphSample
{
    partial class VideoPlayControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VideoPlayControl));
            this.trackBar_Player = new System.Windows.Forms.TrackBar();
            this.btn_Play = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Player)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar_Player
            // 
            this.trackBar_Player.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar_Player.Location = new System.Drawing.Point(54, 12);
            this.trackBar_Player.Maximum = 100;
            this.trackBar_Player.Name = "trackBar_Player";
            this.trackBar_Player.Size = new System.Drawing.Size(592, 45);
            this.trackBar_Player.TabIndex = 1;
            this.trackBar_Player.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackBar_Player_MouseDown);
            this.trackBar_Player.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBar_Player_MouseUp);
            // 
            // btn_Play
            // 
            this.btn_Play.Image = global::VideoGraphSample.Properties.Resources.PlayPause3;
            this.btn_Play.Location = new System.Drawing.Point(3, 3);
            this.btn_Play.Name = "btn_Play";
            this.btn_Play.Size = new System.Drawing.Size(45, 45);
            this.btn_Play.TabIndex = 0;
            this.btn_Play.UseVisualStyleBackColor = true;
            this.btn_Play.Click += new System.EventHandler(this.VideoPlayControl_Paused);
            // 
            // VideoPlayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 50);
            this.Controls.Add(this.trackBar_Player);
            this.Controls.Add(this.btn_Play);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VideoPlayControl";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.VideoPlayControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.VideoPlayControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.VideoPlayControl_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Player)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Play;
        private System.Windows.Forms.TrackBar trackBar_Player;
    }
}