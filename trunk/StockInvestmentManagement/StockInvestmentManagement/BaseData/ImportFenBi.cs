using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace StockInvestmentManagement.BaseData
{
    public partial class ImportFenBi : Form
    {
        //待导入的逐笔成交数据的日期和文件路径表
        public DataTable dt = null;

        //用来保存逐笔成交记录的表
        DataTable dtRec = new DataTable();

        SqlDal db = new SqlDal();


        //计数器
        int i = 0;
        int j = 0;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dt">待导入的逐笔成交数据的日期和文件路径表</param>
        public ImportFenBi(DataTable dtImport)
        {
            InitializeComponent();

            //dtRec.Columns.Add("证券代码", typeof(string));
            //dtRec.Columns.Add("成交日期", typeof(string));
            dtRec.Columns.Add("成交时间", typeof(string));
            dtRec.Columns.Add("成交价格", typeof(string));
            dtRec.Columns.Add("买卖", typeof(string));
            dtRec.Columns.Add("成交量", typeof(string));

            //传入的DT
            dt = dtImport;

            this.progressBar1.Maximum = dt.Rows.Count;

            this.timer1.Enabled = true;

            //后台处理进程
            this.bg.RunWorkerAsync();

        }

        private void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            StreamReader sr = null;
            Stopwatch sw = new Stopwatch();
            Stopwatch swT = new Stopwatch(); //总耗时
            swT.Start();
            sw.Start();
            this.bg.ReportProgress(-1, "正在禁用逐笔成交记录表的索引，请稍候……");
            db.DisableOrEnableZhuBiTableIndex("禁用");
            sw.Stop();
            this.bg.ReportProgress(-1, "禁用逐笔成交记录表的索引耗时" + sw.ElapsedMilliseconds.ToString() + "毫秒");

            foreach (DataRow row in this.dt.Rows)
            {
                sw.Restart();
                if (!bg.CancellationPending)
                {
                    this.bg.ReportProgress(i, row);

                    //选用不同的插入方式
                    //TVP表变量方式
                    if (Main.staticAppSetup["ZbInsertMode"].ToString() == "TVP")
                    {
                        //清空用来保存每个文件主笔成交记录的表
                        dtRec.Clear();

                        //读取主笔成交记录文件
                        sr = new StreamReader(row["文件路径"].ToString());
                        string nextLine = null;
                        while ((nextLine = sr.ReadLine()) != null)
                        {
                            //解析每行记录
                            string[] strRec = nextLine.Split(new string[] { "," }, StringSplitOptions.None);

                            DataRow rRec = dtRec.NewRow();
                            //rRec["证券代码"] = row["证券代码"].ToString();
                            //rRec["成交日期"] = row["日期"].ToString();
                            rRec["成交时间"] = strRec[0].ToString();
                            rRec["成交价格"] = strRec[1];
                            rRec["买卖"] = strRec[2].ToString();
                            rRec["成交量"] = strRec[3].ToString();
                            dtRec.Rows.Add(rRec);
                        }

                        //传入数据库
                        db.UpdateZhuBi(row["证券代码"].ToString(), row["日期"].ToString(), dtRec);
                        i++;
                        j += dtRec.Rows.Count;
                    }

                    //BULK INSERT 方式
                    if (Main.staticAppSetup["ZbInsertMode"].ToString() == "BULK")
                    {
                        db.UpdataZhuBi(row["文件路径"].ToString(), row["证券代码"].ToString(), row["日期"].ToString(), Application.StartupPath + "\\逐笔成交.fmt");
                        i++;
                    }
                }
                else
                {
                    break;
                }
                sw.Stop();
                this.bg.ReportProgress(-1, "耗时:" + sw.ElapsedMilliseconds.ToString() + "毫秒"+",总耗时："+swT.ElapsedMilliseconds.ToString()+"毫秒");
            }
            swT.Stop();
        }

        private void bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == -1)
            {
                this.label3.Text = e.UserState.ToString();
            }
            else
            {
                DataRow row = (DataRow)e.UserState;
                this.label1.Text = "正在导入:" + row["日期"].ToString() + "  " + row["证券代码"].ToString() + "的逐笔成交记录";
                this.label2.Text = "文件路径:" + row["文件路径"].ToString();
                this.progressBar1.Value = i;
            }
        }

        private void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //this.label3.Text = "正在重建逐笔成交记录表的索引，请稍候……";
            db.DisableOrEnableZhuBiTableIndex("重建");
            //sw.Stop();
            //this.label3.Text = "重建逐笔成交记录表的索引耗时" + sw.ElapsedMilliseconds.ToString() + "毫秒";
            MessageBox.Show("共导入" + i.ToString() + "条证券记录，" + j.ToString() + "条逐笔成交记录");
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            this.bg.CancelAsync();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //每10分钟释放SQL内存一次
            db.ReclaimMemory();
        }
    }
}
