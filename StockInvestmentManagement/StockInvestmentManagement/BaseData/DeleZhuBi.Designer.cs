namespace StockInvestmentManagement.BaseData
{
    partial class DeleZhuBi
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
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.btnDele = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbDeleZhuBiByDate = new System.Windows.Forms.CheckBox();
            this.cbTruncateZhuBi = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(44, 65);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(107, 21);
            this.dtpStart.TabIndex = 2;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(195, 65);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(104, 21);
            this.dtpEnd.TabIndex = 3;
            // 
            // btnDele
            // 
            this.btnDele.Location = new System.Drawing.Point(129, 121);
            this.btnDele.Name = "btnDele";
            this.btnDele.Size = new System.Drawing.Size(75, 23);
            this.btnDele.TabIndex = 4;
            this.btnDele.Text = "删  除";
            this.btnDele.UseVisualStyleBackColor = true;
            this.btnDele.Click += new System.EventHandler(this.btnDele_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "从";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "到";
            // 
            // cbDeleZhuBiByDate
            // 
            this.cbDeleZhuBiByDate.AutoSize = true;
            this.cbDeleZhuBiByDate.Checked = true;
            this.cbDeleZhuBiByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbDeleZhuBiByDate.Location = new System.Drawing.Point(128, 20);
            this.cbDeleZhuBiByDate.Name = "cbDeleZhuBiByDate";
            this.cbDeleZhuBiByDate.Size = new System.Drawing.Size(180, 16);
            this.cbDeleZhuBiByDate.TabIndex = 1;
            this.cbDeleZhuBiByDate.Text = "删除所选日期范围的逐笔数据";
            this.cbDeleZhuBiByDate.UseVisualStyleBackColor = true;
            this.cbDeleZhuBiByDate.CheckedChanged += new System.EventHandler(this.cbDeleZhuBiByDate_CheckedChanged);
            // 
            // cbTruncateZhuBi
            // 
            this.cbTruncateZhuBi.AutoSize = true;
            this.cbTruncateZhuBi.Location = new System.Drawing.Point(9, 20);
            this.cbTruncateZhuBi.Name = "cbTruncateZhuBi";
            this.cbTruncateZhuBi.Size = new System.Drawing.Size(96, 16);
            this.cbTruncateZhuBi.TabIndex = 0;
            this.cbTruncateZhuBi.Text = "清空逐笔数据";
            this.cbTruncateZhuBi.UseVisualStyleBackColor = true;
            this.cbTruncateZhuBi.CheckedChanged += new System.EventHandler(this.cbTruncateZhuBi_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbTruncateZhuBi);
            this.groupBox1.Controls.Add(this.cbDeleZhuBiByDate);
            this.groupBox1.Location = new System.Drawing.Point(13, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 111);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // DeleZhuBi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 150);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDele);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeleZhuBi";
            this.Text = "删除逐笔数据";
            this.Load += new System.EventHandler(this.DeleZhuBi_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Button btnDele;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbDeleZhuBiByDate;
        private System.Windows.Forms.CheckBox cbTruncateZhuBi;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}