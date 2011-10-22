using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StockInvestmentManagement.BaseData
{
    public partial class ReadStockData : Form
    {
        public ReadStockData()
        {
            InitializeComponent();
        }

        private void ReadStockData_Activated(object sender, EventArgs e)
        {
            this.cbDataSource.SelectedIndex = 0;
            this.cbDataType.SelectedIndex = 0;
            this.cbMarket.SelectedIndex = 0;

            this.dataGridView1.DataSource = Main.staticStockCode;
        }


        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetData_Click(object sender, EventArgs e)
        {
            Class.DzhData dzhData = new Class.DzhData();


            if (cbMarket.SelectedItem.ToString() == "上海")
            {
                dzhData.Market = "SH";
            }
            else if (cbMarket.SelectedItem.ToString() == "深圳")
            {
                dzhData.Market = "SZ";
            }

            dzhData.DataPath = @"C:\dzh2\data\";

            //以后改写到界面设置中，不要在程序中写死
            //上证指数、深成指数
            dzhData.SpecificCode = new string[] { "SH000001", "SZ3990001" };

            //沪深A股、深圳创业板
            dzhData.SegularCode = new string[] { "SH60", "SZ00", "SZ30" };

            DataTable dt = new DataTable();

            switch (cbDataType.SelectedItem.ToString())
            {
                case "日线":
                    if (tbStockCode.Text.Length > 0)
                    {
                        string sc = dzhData.Market + tbStockCode.Text.Trim();
                        dt = dzhData.GetStockDataLine("DAY", sc);
                    }
                    else
                    {
                        dt = dzhData.GetStockDataLine("DAY", null);
                    }
                    break;
                case "代码表":
                    dt = dzhData.GetStockCode();
                    break;
                case "1分钟":
                    //dt = dzhData.GetStockDataLine("1MIN");
                    break;
                case "5分钟":
                    //dt = dzhData.GetStockDataLine("5MIN");
                    break;
                default:
                    break;
            }
            this.dataGridView1.DataSource = dt;

            try
            {
                this.lbZqCount.Text = "证券总数为" + dt.Rows.Count.ToString() + "条";
            }
            catch
            {
                this.lbZqCount.Text = dzhData.Message;
            }

            GC.Collect();

        }
    }
}
