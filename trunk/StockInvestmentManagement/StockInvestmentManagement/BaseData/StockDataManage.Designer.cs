namespace StockInvestmentManagement.BaseData
{
    partial class StockDataManage
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnImportFenBi = new System.Windows.Forms.Button();
            this.lbZqCount = new System.Windows.Forms.Label();
            this.btnGetData = new System.Windows.Forms.Button();
            this.tbStockCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbDataType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbMarket = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbDataSource = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnDeleZhuBi = new System.Windows.Forms.Button();
            this.btnGetJgd = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGetJgd);
            this.groupBox1.Controls.Add(this.btnDeleZhuBi);
            this.groupBox1.Controls.Add(this.btnImportFenBi);
            this.groupBox1.Controls.Add(this.lbZqCount);
            this.groupBox1.Controls.Add(this.btnGetData);
            this.groupBox1.Controls.Add(this.tbStockCode);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbDataType);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbMarket);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbDataSource);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(859, 92);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选项";
            // 
            // btnImportFenBi
            // 
            this.btnImportFenBi.AutoSize = true;
            this.btnImportFenBi.Location = new System.Drawing.Point(389, 51);
            this.btnImportFenBi.Name = "btnImportFenBi";
            this.btnImportFenBi.Size = new System.Drawing.Size(87, 23);
            this.btnImportFenBi.TabIndex = 10;
            this.btnImportFenBi.Text = "导入逐笔数据";
            this.btnImportFenBi.UseVisualStyleBackColor = true;
            this.btnImportFenBi.Click += new System.EventHandler(this.btnImportFenBi_Click);
            // 
            // lbZqCount
            // 
            this.lbZqCount.AutoSize = true;
            this.lbZqCount.Location = new System.Drawing.Point(614, 18);
            this.lbZqCount.Name = "lbZqCount";
            this.lbZqCount.Size = new System.Drawing.Size(41, 12);
            this.lbZqCount.TabIndex = 9;
            this.lbZqCount.Text = "label5";
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(230, 51);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(75, 23);
            this.btnGetData.TabIndex = 8;
            this.btnGetData.Text = "读取数据";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // tbStockCode
            // 
            this.tbStockCode.Location = new System.Drawing.Point(80, 48);
            this.tbStockCode.Name = "tbStockCode";
            this.tbStockCode.Size = new System.Drawing.Size(100, 21);
            this.tbStockCode.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "证券代码：";
            // 
            // cbDataType
            // 
            this.cbDataType.FormattingEnabled = true;
            this.cbDataType.Items.AddRange(new object[] {
            "日线",
            "分笔成交",
            "代码表",
            "5分钟",
            "逐笔成交"});
            this.cbDataType.Location = new System.Drawing.Point(482, 18);
            this.cbDataType.Name = "cbDataType";
            this.cbDataType.Size = new System.Drawing.Size(89, 20);
            this.cbDataType.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(411, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "数据类型：";
            // 
            // cbMarket
            // 
            this.cbMarket.FormattingEnabled = true;
            this.cbMarket.Items.AddRange(new object[] {
            "上海",
            "深圳"});
            this.cbMarket.Location = new System.Drawing.Point(299, 18);
            this.cbMarket.Name = "cbMarket";
            this.cbMarket.Size = new System.Drawing.Size(87, 20);
            this.cbMarket.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(228, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "证券市场：";
            // 
            // cbDataSource
            // 
            this.cbDataSource.FormattingEnabled = true;
            this.cbDataSource.Items.AddRange(new object[] {
            "大智慧",
            "通达信",
            "银江接口"});
            this.cbDataSource.Location = new System.Drawing.Point(78, 18);
            this.cbDataSource.Name = "cbDataSource";
            this.cbDataSource.Size = new System.Drawing.Size(121, 20);
            this.cbDataSource.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据来源：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(14, 111);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(858, 427);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "显示结果";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(852, 407);
            this.dataGridView1.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(17, 541);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(852, 16);
            this.progressBar1.TabIndex = 2;
            // 
            // btnDeleZhuBi
            // 
            this.btnDeleZhuBi.AutoSize = true;
            this.btnDeleZhuBi.Location = new System.Drawing.Point(496, 51);
            this.btnDeleZhuBi.Name = "btnDeleZhuBi";
            this.btnDeleZhuBi.Size = new System.Drawing.Size(87, 23);
            this.btnDeleZhuBi.TabIndex = 11;
            this.btnDeleZhuBi.Text = "删除逐笔数据";
            this.btnDeleZhuBi.UseVisualStyleBackColor = true;
            this.btnDeleZhuBi.Click += new System.EventHandler(this.btnDeleZhuBi_Click);
            // 
            // btnGetJgd
            // 
            this.btnGetJgd.AutoSize = true;
            this.btnGetJgd.Location = new System.Drawing.Point(616, 51);
            this.btnGetJgd.Name = "btnGetJgd";
            this.btnGetJgd.Size = new System.Drawing.Size(99, 23);
            this.btnGetJgd.TabIndex = 12;
            this.btnGetJgd.Text = "读取交割单记录";
            this.btnGetJgd.UseVisualStyleBackColor = true;
            this.btnGetJgd.Click += new System.EventHandler(this.btnGetJgd_Click);
            // 
            // StockDataManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 562);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "StockDataManage";
            this.Text = "股票数据管理";
            this.Activated += new System.EventHandler(this.ReadStockData_Activated);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbDataSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.TextBox tbStockCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbDataType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbMarket;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lbZqCount;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnImportFenBi;
        private System.Windows.Forms.Button btnDeleZhuBi;
        private System.Windows.Forms.Button btnGetJgd;

    }
}