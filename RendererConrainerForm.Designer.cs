
namespace VideoGraphSample
{
    partial class RendererConrainerForm
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

        protected void SavePosition()
        {
            switch (this.Name)
            {
                case "x0085":
                    Properties.Settings.Default.WinSize_x0085 = this.Size;
                    Properties.Settings.Default.WinLocation_x0085 = this.Location;
                    break;
                case "x0086":
                    Properties.Settings.Default.WinSize_x0086 = this.Size;
                    Properties.Settings.Default.WinLocation_x0086 = this.Location;
                    break;
                case "x0087":
                    Properties.Settings.Default.WinSize_x0087 = this.Size;
                    Properties.Settings.Default.WinLocation_x0087 = this.Location;
                    break;
                case "x0088":
                    Properties.Settings.Default.WinSize_x0088 = this.Size;
                    Properties.Settings.Default.WinLocation_x0088 = this.Location;
                    break;
                case "x0089":
                    Properties.Settings.Default.WinSize_x0089 = this.Size;
                    Properties.Settings.Default.WinLocation_x0089 = this.Location;
                    break;
            }

            Properties.Settings.Default.Save();
        }

        protected void Set_Params()
        {
            switch (this.Name)
            {
                case "x0085":
                    this.Location = Properties.Settings.Default.WinLocation_x0085;
                    this.Size = Properties.Settings.Default.WinSize_x0085;
                    break;
                case "x0086":
                    this.Location = Properties.Settings.Default.WinLocation_x0086;
                    this.Size = Properties.Settings.Default.WinSize_x0086;
                    break;
                case "x0087":
                    this.Location = Properties.Settings.Default.WinLocation_x0087;
                    this.Size = Properties.Settings.Default.WinSize_x0087;
                    break;
                case "x0088":
                    this.Location = Properties.Settings.Default.WinLocation_x0088;
                    this.Size = Properties.Settings.Default.WinSize_x0088;
                    break;
                case "x0089":
                    this.Location = Properties.Settings.Default.WinLocation_x0089;
                    this.Size = Properties.Settings.Default.WinSize_x0089;
                    break;
            }
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent(string name)
        {
            this.SuspendLayout();
            // 
            // RendererConrainerForm
            // 

            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.ClientSize = new System.Drawing.Size(960, 960);
            this.MinimumSize = new System.Drawing.Size(200, 200);
            this.Text = name;
            this.Name = name;

            this.ResumeLayout(false);

        }

        #endregion
    }
}