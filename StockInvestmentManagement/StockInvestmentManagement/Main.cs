using System;
using System.Collections;
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

        //定义一个静态哈希表，用来保存系统设置选项
        public static Hashtable staticAppSetup = new Hashtable();

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            SqlDal db = new SqlDal();
            //读取系统设置到哈希表
            db.GetAppSetup();

            GetStockData gst = new GetStockData();
            //读取证券代码填充到内存表
            gst.GetStockCode();

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

            //激活银江数据接收窗口
            string yj = db.GetAppSetupValue("StratYj");
            if (yj == "1")
            {
                BaseData.GetYjData frmGetYjData = new BaseData.GetYjData();
                frmGetYjData.MdiParent = this;
                frmGetYjData.WindowState = FormWindowState.Minimized;
                frmGetYjData.Show();
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
                BaseData.StockDataManage frmReadStockData = new BaseData.StockDataManage();
                frmReadStockData.MdiParent = this;
                frmReadStockData.WindowState = FormWindowState.Normal;
                frmReadStockData.Show();
            }
        }     
    }
}
