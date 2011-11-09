namespace StockInvestmentManagement.BaseData
{
    partial class GetYjData
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
            this.btnYjStart = new System.Windows.Forms.Button();
            this.btnStopYjData = new System.Windows.Forms.Button();
            this.btnActiveYjData = new System.Windows.Forms.Button();
            this.bgReport = new System.ComponentModel.BackgroundWorker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnYjStart
            // 
            this.btnYjStart.Location = new System.Drawing.Point(36, 24);
            this.btnYjStart.Name = "btnYjStart";
            this.btnYjStart.Size = new System.Drawing.Size(94, 23);
            this.btnYjStart.TabIndex = 0;
            this.btnYjStart.Text = "启动银江接收";
            this.btnYjStart.UseVisualStyleBackColor = true;
            this.btnYjStart.Click += new System.EventHandler(this.btnYjStart_Click);
            // 
            // btnStopYjData
            // 
            this.btnStopYjData.Location = new System.Drawing.Point(235, 24);
            this.btnStopYjData.Name = "btnStopYjData";
            this.btnStopYjData.Size = new System.Drawing.Size(92, 23);
            this.btnStopYjData.TabIndex = 1;
            this.btnStopYjData.Text = "停止银江接收";
            this.btnStopYjData.UseVisualStyleBackColor = true;
            this.btnStopYjData.Click += new System.EventHandler(this.btnStopYjData_Click);
            // 
            // btnActiveYjData
            // 
            this.btnActiveYjData.Location = new System.Drawing.Point(136, 24);
            this.btnActiveYjData.Name = "btnActiveYjData";
            this.btnActiveYjData.Size = new System.Drawing.Size(93, 23);
            this.btnActiveYjData.TabIndex = 2;
            this.btnActiveYjData.Text = "激活银江接收";
            this.btnActiveYjData.UseVisualStyleBackColor = true;
            this.btnActiveYjData.Click += new System.EventHandler(this.btnActiveYjData_Click);
            // 
            // bgReport
            // 
            this.bgReport.WorkerReportsProgress = true;
            this.bgReport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgReport_DoWork);
            this.bgReport.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgReport_ProgressChanged);
            this.bgReport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgReport_RunWorkerCompleted);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 63);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(755, 350);
            this.dataGridView1.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GetYjData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 425);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnActiveYjData);
            this.Controls.Add(this.btnStopYjData);
            this.Controls.Add(this.btnYjStart);
            this.Name = "GetYjData";
            this.Text = "GetYjData";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnYjStart;
        private System.Windows.Forms.Button btnStopYjData;
        private System.Windows.Forms.Button btnActiveYjData;
        private System.ComponentModel.BackgroundWorker bgReport;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Timer timer1;
    }
}