using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PersistenceLayer;

namespace StockInvestmentManagement.BaseData
{
    public partial class StockCode : Form
    {
        DataTable dtNew; //保存新获取的代码表

        public StockCode()
        {
            InitializeComponent();
            lbXhqdm.Text = "共有0条记录";
        }

        /// <summary>
        /// 从网络上读取最新代码表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetStockCode_Click(object sender, EventArgs e)
        {
            HtmlRegex hl = new HtmlRegex();
            string content = hl.GetHtmlCode(@"http://quote.eastmoney.com/stocklist.html");


            //解析网页，并返回新表，新表中ID不为0表示更新数据，ID为0表示新增数据
            dtNew = hl.RegexStockCode(content);
            lvXhqdmb.BeginUpdate();
            lvXhqdmb.Items.Clear();
            foreach (DataRow row in dtNew.Rows)
            {
                ListViewItem lvi = new ListViewItem(row["ID"].ToString());
                lvi.SubItems.Add(row["证券代码"].ToString());
                lvi.SubItems.Add(row["证券名称"].ToString());
                lvi.SubItems.Add(row["速记码"].ToString());
                lvXhqdmb.Items.Add(lvi);
            }
            lvXhqdmb.EndUpdate();
            this.lbXhqdm.Text = "读取到" + dtNew.Rows.Count.ToString() + "条记录";

        }

        /// <summary>
        /// 更新代码表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateStockCode_Click(object sender, EventArgs e)
        {
            if (dtNew == null)
            {
                return;
            }

            //将dtNew作为表值参数，传递给存储过程，由存储过程在数据库内进行比较、更新处理
            SqlDal db = new SqlDal();
            db.UpdateStockCodeTVP(dtNew);;

            //重新加载代码表到内存
            Main.GetStockCode();

            StockCode_Activated(null, null);

            MessageBox.Show("更新完毕!");
        }

        /// <summary>
        /// 激活窗口时重新加载代码表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StockCode_Activated(object sender, EventArgs e)
        {
            lvYczdmb.BeginUpdate();
            lvYczdmb.Items.Clear();
            foreach (DataRow row in Main.staticStockCode.Rows)
            {
                ListViewItem lvi = new ListViewItem(row["ID"].ToString());
                lvi.SubItems.Add(row["证券代码"].ToString());
                lvi.SubItems.Add(row["证券名称"].ToString());
                lvi.SubItems.Add(row["速记码"].ToString());
                lvYczdmb.Items.Add(lvi);
            }
            lvYczdmb.EndUpdate();
            lbYczdm.Text = "共有" + Main.staticStockCode.Rows.Count.ToString() + "条记录";            
        }
    }
}
