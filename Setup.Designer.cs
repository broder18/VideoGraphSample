
namespace VideoGraphSample
{
    partial class SetupForm
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
            this.gbTelemetry = new System.Windows.Forms.GroupBox();
            this.lblTxt = new System.Windows.Forms.Label();
            this.lblBkg = new System.Windows.Forms.Label();
            this.panelTxt = new System.Windows.Forms.Panel();
            this.panelBkg = new System.Windows.Forms.Panel();
            this.lblTelemetryAlpha = new System.Windows.Forms.Label();
            this.lblTelemetryPosY = new System.Windows.Forms.Label();
            this.lblTelemetryPosX = new System.Windows.Forms.Label();
            this.tbTelemetryAlpha = new System.Windows.Forms.TrackBar();
            this.tbTelemetryPosY = new System.Windows.Forms.TrackBar();
            this.tbTelemetryPosX = new System.Windows.Forms.TrackBar();
            this.btnSetDefaults = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.gbFilePath = new System.Windows.Forms.GroupBox();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.btnPath = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.gbTelemetry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTelemetryAlpha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTelemetryPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTelemetryPosX)).BeginInit();
            this.gbFilePath.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbTelemetry
            // 
            this.gbTelemetry.Controls.Add(this.lblTxt);
            this.gbTelemetry.Controls.Add(this.lblBkg);
            this.gbTelemetry.Controls.Add(this.panelTxt);
            this.gbTelemetry.Controls.Add(this.panelBkg);
            this.gbTelemetry.Controls.Add(this.lblTelemetryAlpha);
            this.gbTelemetry.Controls.Add(this.lblTelemetryPosY);
            this.gbTelemetry.Controls.Add(this.lblTelemetryPosX);
            this.gbTelemetry.Controls.Add(this.tbTelemetryAlpha);
            this.gbTelemetry.Controls.Add(this.tbTelemetryPosY);
            this.gbTelemetry.Controls.Add(this.tbTelemetryPosX);
            this.gbTelemetry.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbTelemetry.Location = new System.Drawing.Point(12, 12);
            this.gbTelemetry.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbTelemetry.Name = "gbTelemetry";
            this.gbTelemetry.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbTelemetry.Size = new System.Drawing.Size(331, 302);
            this.gbTelemetry.TabIndex = 0;
            this.gbTelemetry.TabStop = false;
            this.gbTelemetry.Text = "Telemetry Position/Opacity/Colors";
            // 
            // lblTxt
            // 
            this.lblTxt.Location = new System.Drawing.Point(173, 219);
            this.lblTxt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTxt.Name = "lblTxt";
            this.lblTxt.Size = new System.Drawing.Size(138, 17);
            this.lblTxt.TabIndex = 10;
            this.lblTxt.Text = "Text";
            this.lblTxt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBkg
            // 
            this.lblBkg.Location = new System.Drawing.Point(15, 219);
            this.lblBkg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBkg.Name = "lblBkg";
            this.lblBkg.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblBkg.Size = new System.Drawing.Size(138, 17);
            this.lblBkg.TabIndex = 9;
            this.lblBkg.Text = "Background";
            this.lblBkg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelTxt
            // 
            this.panelTxt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTxt.Location = new System.Drawing.Point(173, 170);
            this.panelTxt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelTxt.Name = "panelTxt";
            this.panelTxt.Size = new System.Drawing.Size(143, 45);
            this.panelTxt.TabIndex = 8;
            this.panelTxt.Click += new System.EventHandler(this.panelTxt_Click);
            // 
            // panelBkg
            // 
            this.panelBkg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBkg.Location = new System.Drawing.Point(15, 170);
            this.panelBkg.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelBkg.Name = "panelBkg";
            this.panelBkg.Size = new System.Drawing.Size(143, 45);
            this.panelBkg.TabIndex = 7;
            this.panelBkg.Click += new System.EventHandler(this.panelBkg_Click);
            // 
            // lblTelemetryAlpha
            // 
            this.lblTelemetryAlpha.AutoSize = true;
            this.lblTelemetryAlpha.Location = new System.Drawing.Point(231, 120);
            this.lblTelemetryAlpha.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTelemetryAlpha.Name = "lblTelemetryAlpha";
            this.lblTelemetryAlpha.Size = new System.Drawing.Size(64, 16);
            this.lblTelemetryAlpha.TabIndex = 6;
            this.lblTelemetryAlpha.Text = "A = 100";
            // 
            // lblTelemetryPosY
            // 
            this.lblTelemetryPosY.AutoSize = true;
            this.lblTelemetryPosY.Location = new System.Drawing.Point(231, 72);
            this.lblTelemetryPosY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTelemetryPosY.Name = "lblTelemetryPosY";
            this.lblTelemetryPosY.Size = new System.Drawing.Size(48, 16);
            this.lblTelemetryPosY.TabIndex = 5;
            this.lblTelemetryPosY.Text = "Y = 0";
            // 
            // lblTelemetryPosX
            // 
            this.lblTelemetryPosX.AutoSize = true;
            this.lblTelemetryPosX.Location = new System.Drawing.Point(231, 25);
            this.lblTelemetryPosX.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTelemetryPosX.Name = "lblTelemetryPosX";
            this.lblTelemetryPosX.Size = new System.Drawing.Size(48, 16);
            this.lblTelemetryPosX.TabIndex = 4;
            this.lblTelemetryPosX.Text = "X = 0";
            // 
            // tbTelemetryAlpha
            // 
            this.tbTelemetryAlpha.Location = new System.Drawing.Point(7, 118);
            this.tbTelemetryAlpha.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbTelemetryAlpha.Maximum = 100;
            this.tbTelemetryAlpha.Name = "tbTelemetryAlpha";
            this.tbTelemetryAlpha.Size = new System.Drawing.Size(216, 45);
            this.tbTelemetryAlpha.TabIndex = 2;
            this.tbTelemetryAlpha.TickFrequency = 10;
            this.tbTelemetryAlpha.Value = 100;
            this.tbTelemetryAlpha.ValueChanged += new System.EventHandler(this.tbTelemetryAlpha_ValueChanged);
            // 
            // tbTelemetryPosY
            // 
            this.tbTelemetryPosY.Location = new System.Drawing.Point(7, 70);
            this.tbTelemetryPosY.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbTelemetryPosY.Maximum = 1000;
            this.tbTelemetryPosY.Minimum = -1000;
            this.tbTelemetryPosY.Name = "tbTelemetryPosY";
            this.tbTelemetryPosY.Size = new System.Drawing.Size(216, 45);
            this.tbTelemetryPosY.TabIndex = 1;
            this.tbTelemetryPosY.TickFrequency = 100;
            this.tbTelemetryPosY.ValueChanged += new System.EventHandler(this.tbTelemetryPosY_ValueChanged);
            // 
            // tbTelemetryPosX
            // 
            this.tbTelemetryPosX.LargeChange = 50;
            this.tbTelemetryPosX.Location = new System.Drawing.Point(7, 22);
            this.tbTelemetryPosX.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbTelemetryPosX.Maximum = 1000;
            this.tbTelemetryPosX.Minimum = -1000;
            this.tbTelemetryPosX.Name = "tbTelemetryPosX";
            this.tbTelemetryPosX.Size = new System.Drawing.Size(216, 45);
            this.tbTelemetryPosX.TabIndex = 0;
            this.tbTelemetryPosX.TickFrequency = 100;
            this.tbTelemetryPosX.ValueChanged += new System.EventHandler(this.tbTelemetryPosX_ValueChanged);
            // 
            // btnSetDefaults
            // 
            this.btnSetDefaults.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSetDefaults.Location = new System.Drawing.Point(253, 356);
            this.btnSetDefaults.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSetDefaults.Name = "btnSetDefaults";
            this.btnSetDefaults.Size = new System.Drawing.Size(83, 31);
            this.btnSetDefaults.TabIndex = 3;
            this.btnSetDefaults.Text = "Defaults";
            this.btnSetDefaults.UseVisualStyleBackColor = true;
            this.btnSetDefaults.Click += new System.EventHandler(this.btnSetDefaults_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancel.Location = new System.Drawing.Point(162, 356);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 31);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // gbFilePath
            // 
            this.gbFilePath.Controls.Add(this.btnPath);
            this.gbFilePath.Controls.Add(this.pathTextBox);
            this.gbFilePath.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbFilePath.Location = new System.Drawing.Point(12, 260);
            this.gbFilePath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbFilePath.Name = "gbFilePath";
            this.gbFilePath.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbFilePath.Size = new System.Drawing.Size(332, 93);
            this.gbFilePath.TabIndex = 5;
            this.gbFilePath.TabStop = false;
            this.gbFilePath.Text = "File Path";
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(9, 27);
            this.pathTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(315, 22);
            this.pathTextBox.TabIndex = 0;
            // 
            // btnPath
            // 
            this.btnPath.Location = new System.Drawing.Point(204, 57);
            this.btnPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(120, 31);
            this.btnPath.TabIndex = 1;
            this.btnPath.Text = "Open File ...";
            this.btnPath.UseVisualStyleBackColor = true;
            this.btnPath.Click += new System.EventHandler(this.btnPath_Click);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnOk.Location = new System.Drawing.Point(71, 356);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(83, 31);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // SetupForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(355, 401);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.gbFilePath);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSetDefaults);
            this.Controls.Add(this.gbTelemetry);
            this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BION Video Player Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetupForm_FormClosing);
            this.Shown += new System.EventHandler(this.SetupForm_Shown);
            this.gbTelemetry.ResumeLayout(false);
            this.gbTelemetry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbTelemetryAlpha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTelemetryPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbTelemetryPosX)).EndInit();
            this.gbFilePath.ResumeLayout(false);
            this.gbFilePath.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbTelemetry;
        private System.Windows.Forms.Label lblTelemetryAlpha;
        private System.Windows.Forms.Label lblTelemetryPosY;
        private System.Windows.Forms.Label lblTelemetryPosX;
        private System.Windows.Forms.TrackBar tbTelemetryAlpha;
        private System.Windows.Forms.TrackBar tbTelemetryPosY;
        private System.Windows.Forms.TrackBar tbTelemetryPosX;
        private System.Windows.Forms.Label lblTxt;
        private System.Windows.Forms.Label lblBkg;
        private System.Windows.Forms.Panel panelTxt;
        private System.Windows.Forms.Panel panelBkg;
        private System.Windows.Forms.Button btnSetDefaults;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.GroupBox gbFilePath;
        private System.Windows.Forms.Button btnPath;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Button btnOk;
    }
}