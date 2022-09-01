
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
            this.positionY_Numeric = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.alpha_Numeric = new System.Windows.Forms.NumericUpDown();
            this.positionX_Numeric = new System.Windows.Forms.NumericUpDown();
            this.StatisticsList = new System.Windows.Forms.ListView();
            this.columnPid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_PathFile = new System.Windows.Forms.Button();
            this.path_textBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.positionY_Numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.alpha_Numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.positionX_Numeric)).BeginInit();
            this.SuspendLayout();
            // 
            // positionY_Numeric
            // 
            this.positionY_Numeric.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.positionY_Numeric.Location = new System.Drawing.Point(102, 59);
            this.positionY_Numeric.Maximum = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.positionY_Numeric.Name = "positionY_Numeric";
            this.positionY_Numeric.Size = new System.Drawing.Size(40, 20);
            this.positionY_Numeric.TabIndex = 17;
            this.positionY_Numeric.ValueChanged += new System.EventHandler(this.positionY_Numeric_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(12, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "Position y: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(12, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 20);
            this.label4.TabIndex = 15;
            this.label4.Text = "Position x: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "Alpha: ";
            // 
            // alpha_Numeric
            // 
            this.alpha_Numeric.Location = new System.Drawing.Point(102, 9);
            this.alpha_Numeric.Name = "alpha_Numeric";
            this.alpha_Numeric.Size = new System.Drawing.Size(40, 20);
            this.alpha_Numeric.TabIndex = 13;
            this.alpha_Numeric.ValueChanged += new System.EventHandler(this.alpha_Numeric_ValueChanged);
            // 
            // positionX_Numeric
            // 
            this.positionX_Numeric.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.positionX_Numeric.Location = new System.Drawing.Point(102, 33);
            this.positionX_Numeric.Maximum = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.positionX_Numeric.Name = "positionX_Numeric";
            this.positionX_Numeric.Size = new System.Drawing.Size(40, 20);
            this.positionX_Numeric.TabIndex = 12;
            this.positionX_Numeric.ValueChanged += new System.EventHandler(this.positionX_Numeric_ValueChanged);
            // 
            // StatisticsList
            // 
            this.StatisticsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPid,
            this.columnSize});
            this.StatisticsList.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.StatisticsList.FullRowSelect = true;
            this.StatisticsList.GridLines = true;
            this.StatisticsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.StatisticsList.HideSelection = false;
            this.StatisticsList.Location = new System.Drawing.Point(269, 9);
            this.StatisticsList.MultiSelect = false;
            this.StatisticsList.Name = "StatisticsList";
            this.StatisticsList.ShowGroups = false;
            this.StatisticsList.Size = new System.Drawing.Size(224, 154);
            this.StatisticsList.TabIndex = 18;
            this.StatisticsList.UseCompatibleStateImageBehavior = false;
            this.StatisticsList.View = System.Windows.Forms.View.Details;
            // 
            // columnPid
            // 
            this.columnPid.Text = "PID";
            this.columnPid.Width = 70;
            // 
            // columnSize
            // 
            this.columnSize.Text = "Renderer Size";
            this.columnSize.Width = 150;
            // 
            // button_PathFile
            // 
            this.button_PathFile.Location = new System.Drawing.Point(16, 136);
            this.button_PathFile.Name = "button_PathFile";
            this.button_PathFile.Size = new System.Drawing.Size(247, 27);
            this.button_PathFile.TabIndex = 19;
            this.button_PathFile.Text = "Video Path";
            this.button_PathFile.UseVisualStyleBackColor = true;
            this.button_PathFile.Click += new System.EventHandler(this.button_PathFile_Click);
            // 
            // path_textBox
            // 
            this.path_textBox.Location = new System.Drawing.Point(16, 110);
            this.path_textBox.Name = "path_textBox";
            this.path_textBox.Size = new System.Drawing.Size(247, 20);
            this.path_textBox.TabIndex = 20;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 183);
            this.Controls.Add(this.path_textBox);
            this.Controls.Add(this.button_PathFile);
            this.Controls.Add(this.StatisticsList);
            this.Controls.Add(this.positionY_Numeric);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.alpha_Numeric);
            this.Controls.Add(this.positionX_Numeric);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.positionY_Numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.alpha_Numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.positionX_Numeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown positionY_Numeric;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown alpha_Numeric;
        private System.Windows.Forms.NumericUpDown positionX_Numeric;
        private System.Windows.Forms.ListView StatisticsList;
        private System.Windows.Forms.ColumnHeader columnPid;
        private System.Windows.Forms.ColumnHeader columnSize;
        private System.Windows.Forms.Button button_PathFile;
        private System.Windows.Forms.TextBox path_textBox;
    }
}

