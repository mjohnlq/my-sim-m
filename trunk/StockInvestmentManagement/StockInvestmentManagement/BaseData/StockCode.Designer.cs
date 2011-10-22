namespace StockInvestmentManagement.BaseData
{
    partial class StockCode
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
            this.lbYczdm = new System.Windows.Forms.Label();
            this.lvYczdmb = new System.Windows.Forms.ListView();
            this.lv1id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lv1code = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lv1name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lv1sjm = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvXhqdmb = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lbXhqdm = new System.Windows.Forms.Label();
            this.btnGetStockCode = new System.Windows.Forms.Button();
            this.btnUpdateStockCode = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbYczdm);
            this.groupBox1.Controls.Add(this.lvYczdmb);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 438);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "已存在代码表";
            // 
            // lbYczdm
            // 
            this.lbYczdm.AutoSize = true;
            this.lbYczdm.Location = new System.Drawing.Point(9, 413);
            this.lbYczdm.Name = "lbYczdm";
            this.lbYczdm.Size = new System.Drawing.Size(41, 12);
            this.lbYczdm.TabIndex = 1;
            this.lbYczdm.Text = "label1";
            // 
            // lvYczdmb
            // 
            this.lvYczdmb.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lv1id,
            this.lv1code,
            this.lv1name,
            this.lv1sjm});
            this.lvYczdmb.Location = new System.Drawing.Point(6, 20);
            this.lvYczdmb.Name = "lvYczdmb";
            this.lvYczdmb.Size = new System.Drawing.Size(268, 382);
            this.lvYczdmb.TabIndex = 0;
            this.lvYczdmb.UseCompatibleStateImageBehavior = false;
            this.lvYczdmb.View = System.Windows.Forms.View.Details;
            // 
            // lv1id
            // 
            this.lv1id.Text = "ID";
            this.lv1id.Width = 30;
            // 
            // lv1code
            // 
            this.lv1code.Text = "证券代码";
            this.lv1code.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lv1code.Width = 80;
            // 
            // lv1name
            // 
            this.lv1name.Text = "证券名称";
            this.lv1name.Width = 80;
            // 
            // lv1sjm
            // 
            this.lv1sjm.Text = "速记码";
            this.lv1sjm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lvXhqdmb);
            this.groupBox2.Controls.Add(this.lbXhqdm);
            this.groupBox2.Location = new System.Drawing.Point(310, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(278, 438);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "新获取代码表";
            // 
            // lvXhqdmb
            // 
            this.lvXhqdmb.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvXhqdmb.Location = new System.Drawing.Point(5, 20);
            this.lvXhqdmb.Name = "lvXhqdmb";
            this.lvXhqdmb.Size = new System.Drawing.Size(267, 382);
            this.lvXhqdmb.TabIndex = 2;
            this.lvXhqdmb.UseCompatibleStateImageBehavior = false;
            this.lvXhqdmb.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 30;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "证券代码";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "证券名称";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "速记码";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbXhqdm
            // 
            this.lbXhqdm.AutoSize = true;
            this.lbXhqdm.Location = new System.Drawing.Point(7, 413);
            this.lbXhqdm.Name = "lbXhqdm";
            this.lbXhqdm.Size = new System.Drawing.Size(41, 12);
            this.lbXhqdm.TabIndex = 1;
            this.lbXhqdm.Text = "label1";
            // 
            // btnGetStockCode
            // 
            this.btnGetStockCode.Location = new System.Drawing.Point(611, 114);
            this.btnGetStockCode.Name = "btnGetStockCode";
            this.btnGetStockCode.Size = new System.Drawing.Size(90, 23);
            this.btnGetStockCode.TabIndex = 2;
            this.btnGetStockCode.Text = "读取新代码表";
            this.btnGetStockCode.UseVisualStyleBackColor = true;
            this.btnGetStockCode.Click += new System.EventHandler(this.btnGetStockCode_Click);
            // 
            // btnUpdateStockCode
            // 
            this.btnUpdateStockCode.Location = new System.Drawing.Point(611, 175);
            this.btnUpdateStockCode.Name = "btnUpdateStockCode";
            this.btnUpdateStockCode.Size = new System.Drawing.Size(90, 23);
            this.btnUpdateStockCode.TabIndex = 3;
            this.btnUpdateStockCode.Text = "更新代码表";
            this.btnUpdateStockCode.UseVisualStyleBackColor = true;
            this.btnUpdateStockCode.Click += new System.EventHandler(this.btnUpdateStockCode_Click);
            // 
            // StockCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 462);
            this.Controls.Add(this.btnUpdateStockCode);
            this.Controls.Add(this.btnGetStockCode);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "StockCode";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "代码表";
            this.Activated += new System.EventHandler(this.StockCode_Activated);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbYczdm;
        private System.Windows.Forms.ListView lvYczdmb;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbXhqdm;
        private System.Windows.Forms.Button btnGetStockCode;
        private System.Windows.Forms.Button btnUpdateStockCode;
        private System.Windows.Forms.ColumnHeader lv1id;
        private System.Windows.Forms.ColumnHeader lv1code;
        private System.Windows.Forms.ColumnHeader lv1name;
        private System.Windows.Forms.ListView lvXhqdmb;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader lv1sjm;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}