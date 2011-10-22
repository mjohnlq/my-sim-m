using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StockInvestmentManagement
{
    public partial class Main : Form
    {
        //定义一个静态股票代码表，便于高速检索股票代码
        public static DataTable staticStockCode = new DataTable();

        public Main()
        {
            InitializeComponent();            
        }

        private void Main_Load(object sender, EventArgs e)
        {
            GetStockCode();
            

        }

        /// <summary>
        /// 读取股票代码表到静态表
        /// </summary>
        public static void GetStockCode()
        {
            //SqlDal db = new SqlDal();
            //staticStockCode = db.GetStockCode();

            Class.DzhData dzhData = new Class.DzhData();

            dzhData.DataPath = @"C:\dzh2\data\";

            //以后改写到界面设置中，不要在程序中写死
            //上证指数、深成指数
            dzhData.SpecificCode = new string[] { "SH000001", "SZ3990001" };

            //沪深A股、深圳创业板
            dzhData.SegularCode = new string[] { "SH60", "SZ00", "SZ30" };

            dzhData.Market = "SH";
            DataTable dtSh = dzhData.GetStockCode();
            dzhData.Market = "SZ";
            DataTable dtSZ = dzhData.GetStockCode();

            staticStockCode = dtSh.Copy();
            staticStockCode.Merge(dtSZ);
        }

        private void 代码表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isExist = false;

            foreach (Form childrenForm in this.MdiChildren)
            {
                if (childrenForm.Name == "StockCode")
                {
                    isExist = true;
                    childrenForm.Visible = true;
                    childrenForm.WindowState = FormWindowState.Normal;
                    childrenForm.Activate();
                    return;
                }
            }
            if (!isExist)
            {
                BaseData.StockCode frmStockCode = new BaseData.StockCode();
                frmStockCode.MdiParent = this;
                frmStockCode.WindowState = FormWindowState.Normal;
                frmStockCode.Show();
            }
        }

        private void 数据管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isExist = false;

            foreach (Form childrenForm in this.MdiChildren)
            {
                if (childrenForm.Name == "ReadStockData")
                {
                    isExist = true;
                    childrenForm.Visible = true;
                    childrenForm.WindowState = FormWindowState.Normal;
                    childrenForm.Activate();
                    return;
                }
            }
            if (!isExist)
            {
                BaseData.ReadStockData frmReadStockData = new BaseData.ReadStockData();
                frmReadStockData.MdiParent = this;
                frmReadStockData.WindowState = FormWindowState.Normal;
                frmReadStockData.Show();
            }
        }

        
    }
}
