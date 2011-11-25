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
            
            //控制台显示
            //AllocConsole();

            //初始化行情表
            HqTable.Columns.Add("证券代码", typeof(string));
            HqTable.Columns.Add("证券名称", typeof(string));
            HqTable.Columns.Add("昨收", typeof(Single));
            HqTable.Columns.Add("今开", typeof(Single));
            HqTable.Columns.Add("最高", typeof(Single));
            HqTable.Columns.Add("最低", typeof(Single));
            HqTable.Columns.Add("最新", typeof(Single));
            HqTable.Columns.Add("成交量", typeof(Single));
            HqTable.Columns.Add("成交额", typeof(Single));
            HqTable.Columns.Add("买一", typeof(Single));
            HqTable.Columns.Add("买一量", typeof(Single));
            HqTable.Columns.Add("买二", typeof(Single));
            HqTable.Columns.Add("买二量", typeof(Single));
            HqTable.Columns.Add("买三", typeof(Single));
            HqTable.Columns.Add("买三量", typeof(Single));
            HqTable.Columns.Add("买四", typeof(Single));
            HqTable.Columns.Add("买四量", typeof(Single));
            HqTable.Columns.Add("买五", typeof(Single));
            HqTable.Columns.Add("买五量", typeof(Single));
            HqTable.Columns.Add("卖一", typeof(Single));
            HqTable.Columns.Add("卖一量", typeof(Single));
            HqTable.Columns.Add("卖二", typeof(Single));
            HqTable.Columns.Add("卖二量", typeof(Single));
            HqTable.Columns.Add("卖三", typeof(Single));
            HqTable.Columns.Add("卖三量", typeof(Single));
            HqTable.Columns.Add("卖四", typeof(Single));
            HqTable.Columns.Add("卖四量", typeof(Single));
            HqTable.Columns.Add("卖五", typeof(Single));
            HqTable.Columns.Add("卖五量", typeof(Single));
            HqTable.Columns.Add("成交时间", typeof(string));

            //初始化分时线数据表
            MinTable.Columns.Add("证券代码", typeof(string));
            MinTable.Columns.Add("证券名称", typeof(string));
            MinTable.Columns.Add("成交时间", typeof(string));
            MinTable.Columns.Add("最新", typeof(Single));
            MinTable.Columns.Add("成交量", typeof(Single));
            MinTable.Columns.Add("成交额", typeof(Single));

            //启动银江
            Stock_Init(this.Handle, My_Msg_StkData, RCV_WORK_SENDMSG);
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
        private const int RCV_REPORT = 0x3f001234;       // 接收到行情数据
        private const int RCV_FILEDATA = 0x3f001235;     // 接收到文件型数据
        private const int RCV_FENBIDATA = 0x3f001301;    // 接收到分笔数据

        private const int FILE_HISTORY_EX = 2;           // 接收到日线数据
        private const int FILE_MINUTE_EX = 4;            // 接收到分钟线数据
        private const int FILE_POWER_EX = 6;             // 接收到除权数据
        private const int FILE_5MINUTE_EX = 81;          // 接收到5分钟数据
        private const UInt32 EKE_HEAD_TAG = 0xffffffff;  // 文件类型数据分隔桢,每个品种的开头以RCV_EKE_HEADEx标识帧开头


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

        //分时线数据结构
        [StructLayout(LayoutKind.Explicit)]
        public struct tag_RCV_MINUTE_STRUCTEx
        {
            [FieldOffset(0)]
            public UInt32 m_time; // UCT
            [FieldOffset(4)]
            public Single m_fPrice;
            [FieldOffset(8)]
            public Single m_fVolume;
            [FieldOffset(12)]
            public Single m_fAmount;
        }

        //定义一个分时数据表，用来存放解析出来的分时线数据
        DataTable MinTable = new DataTable();

        //对于补充到的日线、分时数据、5分钟数据，第一祯为标识帧（或称数据头），后面跟数据帧，直到下一个标识帧或包尾，标识帧的时间字段为全FF，里面含市场和代码信息，用于指示后续数据帧是哪个市场哪个品种的。如果将标识帧按照数据帧解释会得出错误的结果。标识帧与数据帧大小总是一致。
        //补充数据头
        //数据头 m_dwHeadTag == EKE_HEAD_TAG 
        [StructLayout(LayoutKind.Sequential)]
        public struct tag_RCV_EKE_HEADEx
        {
            //[FieldOffset(0)]
            public UInt32 m_dwHeadTag; 		  // = EKE_HEAD_TAG 数据帧头部标识 时间字段全是FF
            //[FieldOffset(4)]
            public UInt16 m_wMarket;			  // 市场类型
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = STKLABEL_LEN)]
            //[FieldOffset(6)]
            public string m_szLabel;                  // 股票代码,以'\0'结尾
        }

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
            tag_RCV_REPORT_STRUCTExV3 rReport;
            tag_RCV_MINUTE_STRUCTEx rMin;
            string zqdm = "";

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

                        #region 接收行情数据
                        HqTable.Clear();
                        for (int i = 0; i < rData.m_nPacketNum; i++)
                        {
                            //将指针所指的内存数据转换成行情数据结构，每个行情数据158字节
                            rReport = (tag_RCV_REPORT_STRUCTExV3)
                                Marshal.PtrToStructure(
                                new IntPtr((int)rData.ptr + 158 * i),
                                typeof(tag_RCV_REPORT_STRUCTExV3));
                            //提取证券代码
                            if (rReport.m_wMarket == SH_MARKET_EX)
                            {
                                zqdm = "SH" + rReport.m_szLabelName.Substring(0, 6);
                            }
                            if (rReport.m_wMarket == SZ_MARKET_EX)
                            {
                                zqdm = "SZ" + rReport.m_szLabelName.Substring(0, 6);
                            }
                            //判断证券代码是否存在于内存表中
                            DataRow[] arrRows = Main.staticStockCode.Select("证券代码 = '" + zqdm + "'");
                            if (arrRows.GetLength(0) > 0)
                            {
                                DataRow row = HqTable.NewRow();
                                row["证券代码"] = zqdm;
                                row["证券名称"] = "";  //没用到，数据库中已有
                                row["成交时间"] = date19700101.AddSeconds(rReport.m_time).AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");

                                row["昨收"] = rReport.m_fLastClose;
                                row["今开"] = rReport.m_fOpen;
                                row["最高"] = rReport.m_fHigh;
                                row["最低"] = rReport.m_fLow;
                                row["最新"] = rReport.m_fNewPrice;
                                row["成交量"] = rReport.m_fVolume;
                                row["成交额"] = rReport.m_fAmount;

                                row["买一"] = rReport.m_fBuyPrice1;
                                row["买二"] = rReport.m_fBuyPrice2;
                                row["买三"] = rReport.m_fBuyPrice3;
                                row["买四"] = rReport.m_fBuyPrice4;
                                row["买五"] = rReport.m_fBuyPrice5;

                                row["卖一"] = rReport.m_fSellPrice1;
                                row["卖二"] = rReport.m_fSellPrice2;
                                row["卖三"] = rReport.m_fSellPrice3;
                                row["卖四"] = rReport.m_fSellPrice4;
                                row["卖五"] = rReport.m_fSellPrice5;

                                row["卖一量"] = rReport.m_fSellVolume1;
                                row["卖二量"] = rReport.m_fSellVolume2;
                                row["卖三量"] = rReport.m_fSellVolume3;
                                row["卖四量"] = rReport.m_fSellVolume4;
                                row["卖五量"] = rReport.m_fSellVolume5;

                                row["买一量"] = rReport.m_fBuyVolume1;
                                row["买二量"] = rReport.m_fBuyVolume2;
                                row["买三量"] = rReport.m_fBuyVolume3;
                                row["买四量"] = rReport.m_fBuyVolume4;
                                row["买五量"] = rReport.m_fBuyVolume5;

                                HqTable.Rows.Add(row);
                            }
                            // Console.WriteLine("新数据" + zqdm + "  " + date19700101.AddSeconds(rReport.m_time).AddHours(8).ToString("yyyy-MM-dd HH:mm:ss") + "  " + rReport.m_fNewPrice.ToString("F2") + "  " + i.ToString());
                        }

                        //保存到数据库
                        try
                        {
                            db.UpdateHqTVP(HqTable);
                        }
                        catch (Exception exp)
                        {
                            //Console.WriteLine(exp.ToString());
                        }

                        break;

                        #endregion

                    case RCV_FILEDATA:
                        switch (rData.m_wDataType)
                        {
                            case FILE_MINUTE_EX:
                                #region 接收分时线数据
                                MinTable.Clear();
                                // Console.WriteLine("收到分时线数据");
                                for (int i = 0; i < rData.m_nPacketNum; i++)
                                {
                                    //将指针所指的内存数据转换成分时结构，每个分时线数据16字节
                                    rMin = (tag_RCV_MINUTE_STRUCTEx)
                                        Marshal.PtrToStructure(
                                        new IntPtr((int)rData.ptr + 16 * i),
                                        typeof(tag_RCV_MINUTE_STRUCTEx));

                                    //判断是不是一个新的股票标识,如果是，转换成数据头结构，更新当前的股票代码
                                    if (rMin.m_time == EKE_HEAD_TAG)
                                    {
                                        //在更新证券代码代码前，将上一次证券代码的分时线数据保存到数据库中，这样每次传送的Table只含有一个证券的数据，简化操作
                                        if (zqdm != "")
                                        {
                                            try
                                            {
                                                db.UpdateMinTVP(zqdm, MinTable);
                                            }
                                            catch (Exception exp)
                                            {
                                                //Console.WriteLine(exp.ToString());
                                            }
                                        }

                                        //清空临时表
                                        MinTable.Clear();

                                        tag_RCV_EKE_HEADEx ekeHead = (tag_RCV_EKE_HEADEx)Marshal.PtrToStructure(new IntPtr((int)rData.ptr + 16 * i), typeof(tag_RCV_EKE_HEADEx));
                                        //提取股票代码
                                        if (ekeHead.m_wMarket == SH_MARKET_EX)
                                        {
                                            zqdm = "SH" + ekeHead.m_szLabel.Substring(0, 6);
                                        }
                                        if (ekeHead.m_wMarket == SZ_MARKET_EX)
                                        {
                                            zqdm = "SZ" + ekeHead.m_szLabel.Substring(0, 6);
                                        }
                                        //Console.WriteLine("分时线数据证券代码更新为" + zqdm);
                                    }
                                    else //不是数据头，则提取分时线数据
                                    {
                                        //判断证券代码是否存在于内存表中
                                        DataRow[] arrRows = Main.staticStockCode.Select("证券代码 = '" + zqdm + "'");
                                        if (arrRows.GetLength(0) > 0)
                                        {
                                            DataRow row = MinTable.NewRow();
                                            row["证券代码"] = zqdm;
                                            row["证券名称"] = "";  //没用到，数据库中已有
                                            row["成交时间"] = date19700101.AddSeconds(rMin.m_time).AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");
                                            row["最新"] = rMin.m_fPrice;
                                            row["成交量"] = rMin.m_fVolume;
                                            row["成交额"] = rMin.m_fAmount;

                                            MinTable.Rows.Add(row);
                                            //Console.WriteLine("新分时数据" + row["证券代码"] + "  " + row["成交时间"] + "  " + row["最新"].ToString() + "  " + row["成交量"].ToString() + "  " + row["成交额"].ToString() + "  " + i.ToString());
                                        }
                                    }
                                }
                                break;
                                #endregion
                            case FILE_HISTORY_EX:
                                //Console.WriteLine("收到日线数据");
                                break;
                            case FILE_POWER_EX:
                                //Console.WriteLine("收到除权数据");
                                break;
                            case FILE_5MINUTE_EX:
                                //Console.WriteLine("收到5分钟数据");
                                break;
                            default:
                                break;
                        }
                        break;
                    case RCV_FENBIDATA:
                        //Console.WriteLine("收到分笔数据");
                        break;
                    default:
                        break;
                }
            }

            base.WndProc(ref m); //处理窗口基类其他的消息队列
        }     
    }
}
