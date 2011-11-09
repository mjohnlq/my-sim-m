using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Data;
using PersistenceLayer;
using BusinessEntity;

namespace StockInvestmentManagement.BaseData
{
    public partial class GetYjData : Form
    {
        SqlDal db = new SqlDal();

        /// <summary>
        /// 银江接收
        /// </summary>
        public GetYjData()
        {
            InitializeComponent();
            AllocConsole();

            //初始化行情表

            HqTable.Columns.Add("证券代码", typeof(string));
            HqTable.Columns.Add("证券名称", typeof(string));            
            HqTable.Columns.Add("昨收", typeof(float));
            HqTable.Columns.Add("今开", typeof(float));
            HqTable.Columns.Add("最高", typeof(float));
            HqTable.Columns.Add("最低", typeof(float));
            HqTable.Columns.Add("最新", typeof(float));
            HqTable.Columns.Add("成交量", typeof(float));
            HqTable.Columns.Add("成交额", typeof(float));
            HqTable.Columns.Add("买一", typeof(float));
            HqTable.Columns.Add("买一量", typeof(float));
            HqTable.Columns.Add("买二", typeof(float));
            HqTable.Columns.Add("买二量", typeof(float));
            HqTable.Columns.Add("买三", typeof(float));
            HqTable.Columns.Add("买三量", typeof(float));
            HqTable.Columns.Add("买四", typeof(float));
            HqTable.Columns.Add("买四量", typeof(float));
            HqTable.Columns.Add("买五", typeof(float));
            HqTable.Columns.Add("买五量", typeof(float));
            HqTable.Columns.Add("卖一", typeof(float));
            HqTable.Columns.Add("卖一量", typeof(float));
            HqTable.Columns.Add("卖二", typeof(float));
            HqTable.Columns.Add("卖二量", typeof(float));
            HqTable.Columns.Add("卖三", typeof(float));
            HqTable.Columns.Add("卖三量", typeof(float));
            HqTable.Columns.Add("卖四", typeof(float));
            HqTable.Columns.Add("卖四量", typeof(float));
            HqTable.Columns.Add("卖五", typeof(float));
            HqTable.Columns.Add("卖五量", typeof(float));
            HqTable.Columns.Add("成交时间", typeof(string));

            //HqTable.Columns.Add("证券代码", typeof(string));
            //HqTable.Columns.Add("证券名称", typeof(string));
            //HqTable.Columns.Add("成交时间", typeof(string));
            //HqTable.Columns.Add("昨收", typeof(string));
            //HqTable.Columns.Add("今开", typeof(string));
            //HqTable.Columns.Add("最高", typeof(string));
            //HqTable.Columns.Add("最低", typeof(string));
            //HqTable.Columns.Add("最新", typeof(string));
            //HqTable.Columns.Add("成交量", typeof(string));
            //HqTable.Columns.Add("成交额", typeof(string));
            //HqTable.Columns.Add("买一", typeof(string));
            //HqTable.Columns.Add("买二", typeof(string));
            //HqTable.Columns.Add("买三", typeof(string));
            //HqTable.Columns.Add("买四", typeof(string));
            //HqTable.Columns.Add("买五", typeof(string));
            //HqTable.Columns.Add("卖一", typeof(string));
            //HqTable.Columns.Add("卖二", typeof(string));
            //HqTable.Columns.Add("卖三", typeof(string));
            //HqTable.Columns.Add("卖四", typeof(string));
            //HqTable.Columns.Add("卖五", typeof(string));
            //HqTable.Columns.Add("买一量", typeof(string));
            //HqTable.Columns.Add("买二量", typeof(string));
            //HqTable.Columns.Add("买三量", typeof(string));
            //HqTable.Columns.Add("买四量", typeof(string));
            //HqTable.Columns.Add("买五量", typeof(string));
            //HqTable.Columns.Add("卖一量", typeof(string));
            //HqTable.Columns.Add("卖二量", typeof(string));
            //HqTable.Columns.Add("卖三量", typeof(string));
            //HqTable.Columns.Add("卖四量", typeof(string));
            //HqTable.Columns.Add("卖五量", typeof(string));
        }

        /// <summary>
        /// 启动控制台
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
        /// <summary>
        /// 释放控制台
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();



        #region 银江数据定义

        //计算日期和时间的基准点
        DateTime date19700101 = new DateTime(1970, 1, 1);

        //自定义消息
        private const int My_Msg_StkData = 0x8000 + 1;

        // 版本 2 建议使用的方式,对于六位代码的深圳市场必须使用这种模式	
        private const int RCV_WORK_SENDMSG = 4;

        //证券市场
        private const ushort SH_MARKET_EX = 18515;		//	'HS' 上海
        private const ushort SZ_MARKET_EX = 23123;		//  'ZS' 深圳

        private const int STKLABEL_LEN = 10;			// 股号数据长度,国内市场股号编码兼容钱龙
        private const int STKNAME_LEN = 32; 			// 股名长度


        //数据通知消息
        //wParam = RCV_QPARAM
        //lParam 指向RCV_DATA结构

        //数据通知类型
        private const int RCV_REPORT = 0x3f001234;       // 接收到行情

        [StructLayout(LayoutKind.Sequential)]
        struct tag_RCV_DATA
        {
            public UInt32 m_wDataType; // 文件类型
            public UInt32 m_nPacketNum; // 记录数,参见注一
            public tag_RCV_FILE_HEADEx m_File; // 文件接口
            public bool m_bDISK; // 文件是否已存盘的文件
            public IntPtr ptr;  //数据缓冲区指针
        }

        //文件型数据头，没用到，主要是tag_RCV_DATA中定义了这个结构，如果程序稳定可删除，并修改tag_RCV_DATA
        [StructLayout(LayoutKind.Sequential)]
        struct tag_RCV_FILE_HEADEx
        {
            public UInt32 m_dwAttrib; // 文件子类型
            public UInt32 m_dwLen; // 文件长度
            public UInt32 m_dwSerialNo; // 序列号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string m_szFileName; // 文件名 or URL
        }



        //行情数据结构V3，一共158字节
        [StructLayout(LayoutKind.Explicit)]
        public struct tag_RCV_REPORT_STRUCTExV3
        {
            [FieldOffset(0)]
            public UInt16 m_cbSize;          // 结构大小
            [FieldOffset(2)]
            public Int32 m_time;            // 成交时间
            [FieldOffset(6)]
            public UInt16 m_wMarket;         // 股票市场类型

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = STKLABEL_LEN)]
            [FieldOffset(8)]
            public string m_szLabelName;            // 股票代码,以'\0'结尾 

            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = STKNAME_LEN)]
            //[FieldOffset(18)]
            //public string m_szName;               // 股票名称,以'\0'结尾  (18的位置被占用，原因待查)

            [FieldOffset(50)]
            public Single m_fLastClose;     // 昨收
            [FieldOffset(54)]
            public Single m_fOpen;           // 今开
            [FieldOffset(58)]
            public Single m_fHigh;           // 最高
            [FieldOffset(62)]
            public Single m_fLow;            // 最低
            [FieldOffset(66)]
            public Single m_fNewPrice;       // 最新
            [FieldOffset(70)]
            public Single m_fVolume;         // 成交量
            [FieldOffset(74)]
            public Single m_fAmount;         // 成交额
            [FieldOffset(78)]
            public Single m_fBuyPrice1;      // 申买价1
            [FieldOffset(82)]
            public Single m_fBuyPrice2;      // 申买价2
            [FieldOffset(86)]
            public Single m_fBuyPrice3;      // 申买价3
            [FieldOffset(90)]
            public Single m_fBuyVolume1;     // 申买量1
            [FieldOffset(94)]
            public Single m_fBuyVolume2;     // 申买量2
            [FieldOffset(98)]
            public Single m_fBuyVolume3;     // 申买量3
            [FieldOffset(102)]
            public Single m_fSellPrice1;     // 申卖价1
            [FieldOffset(106)]
            public Single m_fSellPrice2;     // 申卖价2
            [FieldOffset(110)]
            public Single m_fSellPrice3;     // 申卖价3
            [FieldOffset(114)]
            public Single m_fSellVolume1;    // 申卖量1
            [FieldOffset(118)]
            public Single m_fSellVolume2;    // 申卖量2
            [FieldOffset(122)]
            public Single m_fSellVolume3;    // 申卖量3
            [FieldOffset(126)]
            public Single m_fBuyPrice4;      // 申买价4
            [FieldOffset(130)]
            public Single m_fBuyVolume4;     // 申买量4
            [FieldOffset(134)]
            public Single m_fSellPrice4;     // 申卖价4
            [FieldOffset(138)]
            public Single m_fSellVolume4;    // 申卖量4
            [FieldOffset(142)]
            public Single m_fBuyPrice5;      // 申买价5
            [FieldOffset(146)]
            public Single m_fBuyVolume5;     // 申买量5
            [FieldOffset(150)]
            public Single m_fSellPrice5;     // 申卖价5
            [FieldOffset(154)]
            public Single m_fSellVolume5;    //申卖量5
        }

        //定义一个行情表，用来存放解析出来的行情
        DataTable HqTable = new DataTable();

        #endregion


        #region 启动激活停止银江接口
        //DLL引入采用绝对路径，有空改成注册表查询

        /// <summary>
        /// 启动银江接口
        /// </summary>
        /// <param name="hWnd">处理消息的窗口句柄</param>
        /// <param name="Msg">Msg 用户自定义消息ID</param>
        /// <param name="nWorkMode">nWorkMode 接口工作方式, 应等于 RCV_WORK_SENDMSG</param>
        /// <returns>1 成功-1 失败</returns>
        [DllImport(@"C:\YjStock\Stock.dll")]
        private static extern int Stock_Init(IntPtr hWnd, uint Msg, int nWorkMode);


        /// <summary>
        /// 退出银江接口,停止发送消息
        /// </summary>
        /// <param name="hWnd">处理消息的窗口句柄同 Stock_Init 的调用入口参数</param>
        /// <returns>1 成功-1 失败</returns>
        [DllImport(@"C:\YjStock\Stock.dll")]
        private static extern int Stock_Quit(IntPtr hWnd);


        /// <summary>
        /// 激活（显示）接收程序,进行设置
        /// </summary>
        /// <param name="bSetup">TRUE 显示窗口,进行设置,FALSE 隐含窗口</param>
        /// <returns>1 成功-1 失败</returns>
        [DllImport(@"C:\YjStock\Stock.dll")]
        private static extern int SetupReceiver(bool bSetup);

        // 取得股票驱动信息
        // 入口参数:
        // nInfo 索引
        // pBuf 缓冲区
        // 出口参数:
        // nInfo == RI_IDSTRING, 返回特征字符串长度, pBuf 为特征字符串
        // 如: "TongShi_StockDrv_1.00"
        // nInfo == RI_IDCODE, 返回信息卡 ID 号, pBuf 为字符串形式的 ID 号
        // 如: 0x78001234 "78001234"
        // nInfo == RI_VERSION, 返回信息卡版本号, pBuf 为字符串版本
        // 如: 1.00 "1.00" 
        // nInfo == RI_ERRRATE, 取信道误码,
        // nInfo == RI_STKNUM, 取上市股票总家数
        [DllImport(@"C:\YjStock\Stock.dll")]
        private static extern int GetStockDrvInfo(int nInfo, [MarshalAs(UnmanagedType.AsAny)] object pBuf);


        /// <summary>
        /// 启动银江接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnYjStart_Click(object sender, EventArgs e)
        {
            Stock_Init(this.Handle, My_Msg_StkData, RCV_WORK_SENDMSG);
        }
        /// <summary>
        /// 停止银江接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStopYjData_Click(object sender, EventArgs e)
        {
            Stock_Quit(this.Handle);
        }
        /// <summary>
        /// 激活银江接收
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnActiveYjData_Click(object sender, EventArgs e)
        {

        }

        #endregion

        /// <summary>
        /// 重写消息机制处理过程
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            tag_RCV_DATA rData;
            tag_RCV_REPORT_STRUCTExV3 r;

            //处理自定义的银江消息
            if (m.Msg == My_Msg_StkData)
            {
                //直接数据引用通知消息
                // wParam = RCV_WPARAM;
                // lParam 指向 RCV_DATA结构;
                rData = (tag_RCV_DATA)m.GetLParam(typeof(tag_RCV_DATA));

                switch (m.WParam.ToInt32())  //判断数据类型
                {
                    case RCV_REPORT: //股票行情1056969268

                        HqTable.Clear();

                        Console.WriteLine("收到行情数据");

                        for (int i = 0; i < rData.m_nPacketNum; i++)
                        {
                            r = (tag_RCV_REPORT_STRUCTExV3)
                                Marshal.PtrToStructure(
                                new IntPtr((int)rData.ptr + 158 * i),
                                typeof(tag_RCV_REPORT_STRUCTExV3));

                            //新建一个backgroundwork，将rData传递到后台处理
                            //BackgroundWorker bg = new BackgroundWorker();
                            //bg.WorkerReportsProgress = true;
                            //bg.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgReport_DoWork);
                            //bg.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgReport_ProgressChanged);
                            //bg.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgReport_RunWorkerCompleted);
                            //bg.RunWorkerAsync(r);

                            string gpsc = "";
                            if (r.m_wMarket == SH_MARKET_EX)
                            {
                                gpsc = "SH";
                            }
                            if (r.m_wMarket == SZ_MARKET_EX)
                            {
                                gpsc = "SZ";
                            }

                            string zqdm = gpsc + r.m_szLabelName.Substring(0, 6);
                            //DataRow[] arrRows = Main.staticStockHQ.Select("证券代码 = '" + zqdm + "'");

                            DataRow row = HqTable.NewRow();
                            row["证券代码"] = zqdm;  
                            row["证券名称"] = "";  //没用到，数据库中已有
                            row["成交时间"] = date19700101.AddSeconds(r.m_time).AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");

                            row["昨收"] = r.m_fLastClose;
                            row["今开"] = r.m_fOpen;
                            row["最高"] = r.m_fHigh;
                            row["最低"] = r.m_fLow;
                            row["最新"] = r.m_fNewPrice;
                            row["成交量"] = r.m_fVolume;
                            row["成交额"] = r.m_fAmount;

                            row["买一"] = r.m_fBuyPrice1;
                            row["买二"] = r.m_fBuyPrice2;
                            row["买三"] = r.m_fBuyPrice3;
                            row["买四"] = r.m_fBuyPrice4;
                            row["买五"] = r.m_fBuyPrice5;

                            row["卖一"] = r.m_fSellPrice1;
                            row["卖二"] = r.m_fSellPrice2;
                            row["卖三"] = r.m_fSellPrice3;
                            row["卖四"] = r.m_fSellPrice4;
                            row["卖五"] = r.m_fSellPrice5;

                            row["卖一量"] = r.m_fSellVolume1;
                            row["卖二量"] = r.m_fSellVolume2;
                            row["卖三量"] = r.m_fSellVolume3;
                            row["卖四量"] = r.m_fSellVolume4;
                            row["卖五量"] = r.m_fSellVolume5;

                            row["买一量"] = r.m_fBuyVolume1;
                            row["买二量"] = r.m_fBuyVolume2;
                            row["买三量"] = r.m_fBuyVolume3;
                            row["买四量"] = r.m_fBuyVolume4;
                            row["买五量"] = r.m_fBuyVolume5;
                            //row["昨收"] = r.m_fLastClose.ToString("F2");
                            //row["今开"] = r.m_fOpen.ToString("F2");
                            //row["最高"] = r.m_fHigh.ToString("F2");
                            //row["最低"] = r.m_fLow.ToString("F2");
                            //row["最新"] = r.m_fNewPrice.ToString("F2");
                            //row["成交量"] = r.m_fVolume.ToString("F0");
                            //row["成交额"] = r.m_fAmount.ToString("F0");

                            //row["买一"] = r.m_fBuyPrice1.ToString("F2");
                            //row["买二"] = r.m_fBuyPrice2.ToString("F2");
                            //row["买三"] = r.m_fBuyPrice3.ToString("F2");
                            //row["买四"] = r.m_fBuyPrice4.ToString("F2");
                            //row["买五"] = r.m_fBuyPrice5.ToString("F2");

                            //row["卖一"] = r.m_fSellPrice1.ToString("F2");
                            //row["卖二"] = r.m_fSellPrice2.ToString("F2");
                            //row["卖三"] = r.m_fSellPrice3.ToString("F2");
                            //row["卖四"] = r.m_fSellPrice4.ToString("F2");
                            //row["卖五"] = r.m_fSellPrice5.ToString("F2");

                            //row["卖一量"] = r.m_fSellVolume1.ToString("F0");
                            //row["卖二量"] = r.m_fSellVolume2.ToString("F0");
                            //row["卖三量"] = r.m_fSellVolume3.ToString("F0");
                            //row["卖四量"] = r.m_fSellVolume4.ToString("F0");
                            //row["卖五量"] = r.m_fSellVolume5.ToString("F0");

                            //row["买一量"] = r.m_fBuyVolume1.ToString("F0");
                            //row["买二量"] = r.m_fBuyVolume2.ToString("F0");
                            //row["买三量"] = r.m_fBuyVolume3.ToString("F0");
                            //row["买四量"] = r.m_fBuyVolume4.ToString("F0");
                            //row["买五量"] = r.m_fBuyVolume5.ToString("F0");

                            HqTable.Rows.Add(row);

                            Console.WriteLine("新数据"+zqdm + "  " + date19700101.AddSeconds(r.m_time).AddHours(8).ToString("yyyy-MM-dd HH:mm:ss") + "  " + r.m_fNewPrice.ToString("F2") + "  " + i.ToString());                            
                        }

                        //保存到数据库
                        Console.WriteLine("准备写入数据库");
                        try
                        {
                            db.UpdateHqTVP(HqTable);
                        }
                        catch (Exception exp)
                        {
                            Console.WriteLine(exp.ToString());
                        }

                        break;
                    default:
                        break;
                }
            }

            base.WndProc(ref m); //处理窗口基类其他的消息队列
        }

        #region 后台处理接收到的行情数据
        private void bgReport_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker bg = (BackgroundWorker)sender;
            tag_RCV_REPORT_STRUCTExV3 r = (tag_RCV_REPORT_STRUCTExV3)e.Argument;
            string gpsc = "";
            if (r.m_wMarket == SH_MARKET_EX)
            {
                gpsc = "SH";
            }
            if (r.m_wMarket == SZ_MARKET_EX)
            {
                gpsc = "SZ";
            }

            string zqdm = gpsc + r.m_szLabelName.Substring(0, 6);

            DataRow[] arrRows = Main.staticStockHQ.Select("证券代码 = '" + zqdm + "'");
            lock (arrRows)
            {
                foreach (DataRow row in arrRows)
                {
                    row["成交时间"] = date19700101.AddSeconds(r.m_time).ToString("yyyy-MM-dd HH:mm:ss");
                    row["昨收"] = r.m_fLastClose.ToString("F2");
                    row["今开"] = r.m_fOpen.ToString("F2");
                    row["最高"] = r.m_fHigh.ToString("F2");
                    row["最低"] = r.m_fLow.ToString("F2");
                    row["最新"] = r.m_fNewPrice.ToString("F2");
                    row["成交量"] = r.m_fVolume.ToString("F0");
                    row["成交额"] = r.m_fAmount.ToString("F0");

                    row["买一"] = r.m_fBuyPrice1.ToString("F2");
                    row["买二"] = r.m_fBuyPrice2.ToString("F2");
                    row["买三"] = r.m_fBuyPrice3.ToString("F2");
                    row["买四"] = r.m_fBuyPrice4.ToString("F2");
                    row["买五"] = r.m_fBuyPrice5.ToString("F2");

                    row["卖一"] = r.m_fSellPrice1.ToString("F2");
                    row["卖二"] = r.m_fSellPrice2.ToString("F2");
                    row["卖三"] = r.m_fSellPrice3.ToString("F2");
                    row["卖四"] = r.m_fSellPrice4.ToString("F2");
                    row["卖五"] = r.m_fSellPrice5.ToString("F2");

                    row["卖一量"] = r.m_fSellVolume1.ToString("F0");
                    row["卖二量"] = r.m_fSellVolume2.ToString("F0");
                    row["卖三量"] = r.m_fSellVolume3.ToString("F0");
                    row["卖四量"] = r.m_fSellVolume4.ToString("F0");
                    row["卖五量"] = r.m_fSellVolume5.ToString("F0");

                    row["买一量"] = r.m_fBuyVolume1.ToString("F0");
                    row["买二量"] = r.m_fBuyVolume2.ToString("F0");
                    row["买三量"] = r.m_fBuyVolume3.ToString("F0");
                    row["买四量"] = r.m_fBuyVolume4.ToString("F0");
                    row["买五量"] = r.m_fBuyVolume5.ToString("F0");

                    this.dataGridView1.DataSource = Main.staticStockHQ;
                }
            }

        }

        private void bgReport_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
        }

        private void bgReport_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {

        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            //从数据库获取行情
            this.dataGridView1.DataSource = db.GetStockHq();
            
        }
    }
}
