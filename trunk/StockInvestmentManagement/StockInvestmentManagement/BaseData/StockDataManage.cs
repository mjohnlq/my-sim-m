using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace StockInvestmentManagement.BaseData
{
    public partial class StockDataManage : Form
    {
        public StockDataManage()
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
            DzhData dzhData = new DzhData();
            SqlDal db = new SqlDal();


            if (cbMarket.SelectedItem.ToString() == "上海")
            {
                dzhData.Market = "SH";
            }
            else if (cbMarket.SelectedItem.ToString() == "深圳")
            {
                dzhData.Market = "SZ";
            }

            dzhData.DataPath = Main.staticAppSetup["DzhPath"].ToString();

            //上证指数、深成指数
            dzhData.SpecificCode = Main.staticAppSetup["SpecificCode"].ToString();

            //沪深A股、深圳创业板
            dzhData.SegularCode = Main.staticAppSetup["SegularCode"].ToString();

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
                case "5分钟":
                    if (tbStockCode.Text.Length > 0)
                    {
                        string sc = dzhData.Market + tbStockCode.Text.Trim();
                        dt = dzhData.GetStockDataLine("5MIN", sc);
                    }
                    else
                    {
                        dt = dzhData.GetStockDataLine("5MIN", null);
                    }
                    break;
                case "逐笔成交":
                    string scc = tbStockCode.Text.Trim();
                    dt = dzhData.GetStockZhuBi(scc);
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


        #region 导入逐笔数据
        private void btnImportFenBi_Click(object sender, EventArgs e)
        {
            //用来保存目录名（日期），文件名（证券代码）的表
            DataTable dtFile = new DataTable();
            dtFile.Columns.Add("日期", typeof(string));
            dtFile.Columns.Add("文件路径", typeof(string));
            dtFile.Columns.Add("证券代码", typeof(string));

            //选择目录
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = Main.staticAppSetup["ZbPath"].ToString(); //缺省目录
            fbd.Description = "选取的目录必须是日期形式，如20111101,所有证券的逐笔成交数据在此目录下，此目录下不允许再有子目录。如果要导入多日数据，请选择20111101的上一级目录。";
            if (fbd.ShowDialog() == DialogResult.Cancel)
            {
                //MessageBox.Show(DialogResult.Cancel.ToString());
                return;
            }

            DirectoryInfo L2Folder = new DirectoryInfo(fbd.SelectedPath);
            //遍历文件夹,判断是否存在子目录
            int dateCount = L2Folder.GetDirectories().Length;

            if (dateCount > 0)
            {
                //存在子目录，说明要导入多日数据
                //遍历子目录，目录名代表数据的日期
                foreach (DirectoryInfo nextFolder in L2Folder.GetDirectories())
                {
                    //遍历该目录下的文件
                    foreach (FileInfo nextFile in nextFolder.GetFiles("*.csv"))
                    {
                        //判断该证券是否是需要的
                        //判断证券代码是否存在于内存表中
                        string zqdm = nextFile.Name.Substring(0, 8);
                        DataRow[] arrRows = Main.staticStockCode.Select("证券代码 = '" + zqdm + "'");
                        if (arrRows.GetLength(0) > 0)
                        {
                            DataRow row = dtFile.NewRow();
                            row["日期"] = nextFolder.Name;
                            row["文件路径"] = nextFile.FullName;
                            row["证券代码"] = zqdm;
                            dtFile.Rows.Add(row);
                        }
                    }
                }
            }
            else
            {
                //选取的是单日数据
                //遍历该目录下的文件
                foreach (FileInfo nextFile in L2Folder.GetFiles("*.csv"))
                {
                    //判断该证券是否是需要的
                    string zqdm = nextFile.Name.Substring(0, 8);
                    DataRow[] arrRows = Main.staticStockCode.Select("证券代码 = '" + zqdm + "'");
                    if (arrRows.GetLength(0) > 0)
                    {
                        DataRow row = dtFile.NewRow();
                        row["日期"] = L2Folder.Name;
                        row["文件路径"] = nextFile.FullName;
                        row["证券代码"] = zqdm;
                        dtFile.Rows.Add(row);
                    }
                }
            }

            //遍历文件表，读取逐笔成交记录，保存到数据库
            //新开一个模式窗口，显示数据导入进度

            //this.dataGridView1.DataSource = dtFile;
            ImportFenBi winImportFenbi = new ImportFenBi(dtFile);
            winImportFenbi.ShowDialog();
        }

       
        #endregion


        /// <summary>
        /// 删除逐笔数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleZhuBi_Click(object sender, EventArgs e)
        {
            DeleZhuBi winDeleZhuBi = new DeleZhuBi();
            winDeleZhuBi.ShowDialog();
        }


        /// <summary>
        /// 读取交割单记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetJgd_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "文本文件(*.csv)|*.csv";
            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            //构建交割单表 
            DataTable dtJgd = new DataTable();
            dtJgd.Columns.Add("成交时间", typeof(string));
            dtJgd.Columns.Add("股东代码", typeof(string));
            dtJgd.Columns.Add("证券代码", typeof(string));
            dtJgd.Columns.Add("证券名称", typeof(string));
            dtJgd.Columns.Add("买卖标志", typeof(string));
            dtJgd.Columns.Add("成交价格", typeof(decimal));
            dtJgd.Columns.Add("成交数量", typeof(Int32));
            dtJgd.Columns.Add("成交金额", typeof(decimal));
            dtJgd.Columns.Add("发生金额", typeof(decimal));
            dtJgd.Columns.Add("佣金", typeof(decimal));
            dtJgd.Columns.Add("印花税", typeof(decimal));
            dtJgd.Columns.Add("过户费", typeof(decimal));
            dtJgd.Columns.Add("结算费", typeof(decimal));
            dtJgd.Columns.Add("其他费", typeof(decimal));
            dtJgd.Columns.Add("交易规费", typeof(decimal));
            dtJgd.Columns.Add("增值服务费", typeof(decimal));
            dtJgd.Columns.Add("成交编号", typeof(string));

            
            StreamReader sr = new StreamReader(openFileDialog.FileName,Encoding.Default);

            string nextLine = null;
            //跳过第一行
            sr.ReadLine();
            while ((nextLine = sr.ReadLine()) != null)
            {
                //解析每行记录
                string[] strRec = nextLine.Split(new string[] { "," }, StringSplitOptions.None);

                DataRow row = dtJgd.NewRow();
                row["成交时间"] = Convert.ToDateTime(strRec[0].Substring(0, 4) + "-" + strRec[0].Substring(4, 2) +"-"+strRec[0].Substring(6,2)+ "  " + strRec[1]);
               // row["成交时间"] = strRec[1];
                row["股东代码"] = strRec[2];
                row["证券代码"] = strRec[3];
                row["证券名称"] = strRec[4];
                row["买卖标志"] = strRec[5];
                row["成交价格"] = Convert.ToDecimal(strRec[6]);
                row["成交数量"] = Convert.ToInt32(strRec[7]);
                row["成交金额"] = Convert.ToDecimal(strRec[8]);
                row["发生金额"] = Convert.ToDecimal(strRec[9]);
                row["佣金"] = Convert.ToDecimal(strRec[10]);
                row["印花税"] = Convert.ToDecimal(strRec[11]);
                row["过户费"] = Convert.ToDecimal(strRec[12]);
                row["结算费"] = Convert.ToDecimal(strRec[13]);
                row["其他费"] = Convert.ToDecimal(strRec[14]);
                row["交易规费"] = Convert.ToDecimal(strRec[15]);
                //row["增值服务费"] = Convert.ToDecimal(strRec[16]);
                row["增值服务费"] = "0";
                row["成交编号"] = strRec[17];
                dtJgd.Rows.Add(row);
                
            }
            //this.dataGridView1.DataSource = dtJgd;

            SqlDal db = new SqlDal();
            db.UpdateJgdWhitTVP(dtJgd);
            
        }
    }
}
