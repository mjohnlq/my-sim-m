using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PersistenceLayer;
using BusinessEntity;

namespace StockInvestmentManagement
{
    public partial class Main : Form
    {
        //定义一个静态股票代码表，便于高速检索股票代码
        public static DataTable staticStockCode = new DataTable();

        //定义一个静态行情数据表，用来保存每个股票最新的行情数据
        public static DataTable staticStockHQ = new DataTable();

        SqlDal db = new SqlDal();

        public Main()
        {
            InitializeComponent();            
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //读取证券代码到内存表
            GetStockCode();       

            //保存证券代码到数据库
            db.UpdateStockCodeTVP(staticStockCode);
    
            //定义行情数据表，并初始化
            staticStockHQ.Columns.Add("证券代码", typeof(string));
            staticStockHQ.Columns.Add("证券名称", typeof(string));
            staticStockHQ.Columns.Add("成交时间", typeof(string));
            staticStockHQ.Columns.Add("昨收", typeof(string));
            staticStockHQ.Columns.Add("今开", typeof(string));
            staticStockHQ.Columns.Add("最高", typeof(string));
            staticStockHQ.Columns.Add("最低", typeof(string));
            staticStockHQ.Columns.Add("最新", typeof(string));
            staticStockHQ.Columns.Add("成交量", typeof(string));
            staticStockHQ.Columns.Add("成交额", typeof(string));
            staticStockHQ.Columns.Add("买一", typeof(string));
            staticStockHQ.Columns.Add("买二", typeof(string));
            staticStockHQ.Columns.Add("买三", typeof(string));
            staticStockHQ.Columns.Add("买四", typeof(string));
            staticStockHQ.Columns.Add("买五", typeof(string));
            staticStockHQ.Columns.Add("卖一", typeof(string));
            staticStockHQ.Columns.Add("卖二", typeof(string));
            staticStockHQ.Columns.Add("卖三", typeof(string));
            staticStockHQ.Columns.Add("卖四", typeof(string));
            staticStockHQ.Columns.Add("卖五", typeof(string));
            staticStockHQ.Columns.Add("买一量", typeof(string));
            staticStockHQ.Columns.Add("买二量", typeof(string));
            staticStockHQ.Columns.Add("买三量", typeof(string));
            staticStockHQ.Columns.Add("买四量", typeof(string));
            staticStockHQ.Columns.Add("买五量", typeof(string));
            staticStockHQ.Columns.Add("卖一量", typeof(string));
            staticStockHQ.Columns.Add("卖二量", typeof(string));
            staticStockHQ.Columns.Add("卖三量", typeof(string));
            staticStockHQ.Columns.Add("卖四量", typeof(string));
            staticStockHQ.Columns.Add("卖五量", typeof(string));

            //根据静态股票代码表初始化行情表
            foreach (DataRow row in staticStockCode.Rows)
            {
                DataRow r = staticStockHQ.NewRow();
                r["证券代码"] = row["证券代码"];
                r["证券名称"] = row["证券名称"];

                staticStockHQ.Rows.Add(r);
            }
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

        private void 银江接收ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isExist = false;

            foreach (Form childrenForm in this.MdiChildren)
            {
                if (childrenForm.Name == "GetYjData")
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
                BaseData.GetYjData frmGetYjData = new BaseData.GetYjData();
                frmGetYjData.MdiParent = this;
                frmGetYjData.WindowState = FormWindowState.Normal;
                frmGetYjData.Show();
            }
        }

        
    }
}
