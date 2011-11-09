using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
//using System.Windows.Forms;

namespace StockInvestmentManagement.Class
{
    /// <summary>
    /// 读取大智慧软件的各种数据
    /// </summary>
    public class DzhData
    {
        /// <summary>
        /// 当程序返回结果不正确时，可以查看此信息
        /// </summary>
        private string message = "";
        public string Message { get { return this.message; } }

        /// <summary>
        /// 证券市场，SH或SZ
        /// </summary>
        public string Market { get; set; }

        /// <summary>
        /// 大智慧数据文件的路径，一般为“C:\dzh2\data\”
        /// </summary>
        public string DataPath { get; set; }

        /// <summary>
        /// 需要获取的特定的证券代码数组，例如指数SH000001，SZ399001
        /// </summary>
        public string[] SpecificCode { get; set; }

        /// <summary>
        /// 需要获取的有确定编码开头的证券代码数组，
        /// 例如上证A个SH60开头，深圳A股SZ00开头，创业板SZ30开头
        /// </summary>
        public string[] SegularCode { get; set; }

        //计算日期和时间的基准点
        DateTime date19700101 = new DateTime(1970, 1, 1);


        /// <summary>
        /// 证券代码结构
        /// </summary>
        struct StockCode
        {
            /// <summary>
            /// 证券代码
            /// </summary>
            public string code;

            /// <summary>
            /// 证券名称
            /// </summary>
            public string name;

            /// <summary>
            /// 速记码（拼音简称）
            /// </summary>
            public string sjm;
        }

        /// <summary>
        /// 检测将要打开的文件是否存在,
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>将检测结果写入message便于判断</returns>
        private void CheckFile(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                this.message = filePath + "文件存在";
            }
            else
            {
                this.message = filePath + "文件不存在";
            }
        }


        /// <summary>
        /// 读取证券代码表
        /// </summary>
        /// <returns></returns>
        public DataTable GetStockCode()
        {
            //代码表文件的完整路径
            string filePath = DataPath + Market + "\\STKINFO70.DAT";

            //判断文件是否存在
            this.CheckFile(filePath);
            if (message == filePath + "文件不存在")
            {
                return null;
            }

            //建立存放代码表的Datatable
            DataTable dtCode = new DataTable();
            dtCode.Columns.Add("证券代码", typeof(string));
            dtCode.Columns.Add("证券名称", typeof(string));
            dtCode.Columns.Add("速记码", typeof(string));

            //打开数据文件，建立文件流
            FileStream fs = null;
            BinaryReader br = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                br = new BinaryReader(fs);
            }
            catch
            {
                message = "读取文件" + filePath + "失败";
                return null;
            }
            finally
            {
                message = "读取文件" + filePath + "成功";
            }

            //存放代码的开始位置
            int startAddress = 0x44A6;

            //每个代码块的大小
            int recordSize = 320;

            //从起始位置开始读，12开始的4个字节为代码表总数
            fs.Position = 0xC;// 12;

            //文件中证券总数
            int stockCount = br.ReadInt32();

            for (int i = 0; i < stockCount; i++)
            {
                //实例化一个证券代码
                StockCode sc = new StockCode();

                //文件流的位置，开始位置+第几块*每块大小
                fs.Position = startAddress + i * recordSize;

                //证券代码,10个字节，后面是\0，去掉
                sc.code =
                    this.Market + System.Text.Encoding.Default.GetString(br.ReadBytes(10)).Replace("\0", "");
                if (string.IsNullOrEmpty(sc.code))
                {
                    break;
                }

                //以下代码有点冗余，有空再改

                //判断该代码是否是需要的编码开头的代码
                foreach (string s in this.SegularCode)
                {
                    if (sc.code.Substring(0, s.Length) == s)
                    {
                        //证券名称，32个字节
                        sc.name = System.Text.Encoding.Default.GetString(br.ReadBytes(32)).Replace("\0", "");

                        //速记码
                        sc.sjm = ChinesePinYin.GetChineseSpell(sc.name);

                        //将代码信息保存到表中
                        DataRow dRow = dtCode.NewRow();
                        dRow["证券代码"] = sc.code;
                        dRow["证券名称"] = sc.name;
                        dRow["速记码"] = sc.sjm;
                        dtCode.Rows.Add(dRow);
                    }
                }

                //判断该代码是否是指定的代码，如指数
                foreach (string s in this.SpecificCode)
                {
                    if (sc.code == s)
                    {
                        //证券名称，32个字节
                        sc.name = System.Text.Encoding.Default.GetString(br.ReadBytes(32)).Replace("\0", "");

                        //将代码信息保存到表中
                        DataRow dRow = dtCode.NewRow();
                        dRow["证券代码"] = sc.code;
                        dRow["证券名称"] = sc.name;
                        dRow["速记码"] = ChinesePinYin.GetChineseSpell(sc.name);
                        dtCode.Rows.Add(dRow);
                    }
                }
            }
            br.Close();
            fs.Close();
            fs.Dispose();
            return dtCode;
        }


        /// <summary>
        /// 日线、5分钟数据结构
        /// </summary>
        struct DayLine
        {
            /// <summary>
            /// 日期、时间 ，取到的4字节需要通过转换，日线为
            /// </summary>
            public String m_time;

            /// <summary>
            /// 开盘价，4字节
            /// </summary>
            public Single m_fOpen;

            /// <summary>
            /// 最高价，4字节
            /// </summary>
            public Single m_fHigh;

            /// <summary>
            /// 最低价，4字节
            /// </summary>
            public Single m_fLow;

            /// <summary>
            /// 收盘价，4字节
            /// </summary>
            public Single m_fClose;

            /// <summary>
            /// 成交量，4字节
            /// </summary>
            public Single m_fVolume;

            /// <summary>
            /// 成交额，4字节
            /// </summary>
            public Single m_fAmount;

            /// <summary>
            /// 涨数,仅大盘有效，数据不是很正确，弃用，用程序计算取代
            /// </summary>
            public Int16 m_wAdvance;

            /// <summary>
            /// 跌数,仅大盘有效，数据不是很正确，弃用，用程序计算取代
            /// </summary>
            public Int16 m_wDecline;
        }

        ///// <summary>
        ///// 大智慧日线文件数据头格式，共24个字节(没什么用，可以去掉，证券总数用变量直接表示即可
        ///// </summary>
        //struct DayHead
        //{
        //    /// <summary>
        //    /// 0x00-0x03 头4个字节为日线文件标识
        //    /// </summary>
        //    public int dayFlag;

        //    /// <summary>
        //    /// 0x04-0x0B 中间8个字节前四个未知，后四个保留
        //    /// </summary>
        //    public Int64 retainWord;

        //    /// <summary>
        //    /// 0x0C-0x0F 4字节，表示该文件中包含的证券总数
        //    /// </summary>
        //    public int stockCount;

        //    /// <summary>
        //    /// 0x10-0x13 需添加之起始块号，就是文件的最后,计算方法是 0x41000 + 这个数字 * 8192
        //    /// 本程序没用到
        //    /// </summary>  
        //    public int nextBlockAdd;

        //    /// <summary>
        //    /// 当前最后一个空块号 就是说写数据，就在这个地方写,写完就在上面地方新增加新的块
        //    /// </summary>                                      
        //    public int currentBlockAdd;

        //}

        /// <summary>
        /// 大智慧日线、5分钟文件股票记录索引格式， 从18h开始至 40017h 每64byte为一条股票数据分配记录，总共有多少由0x0c提供
        /// </summary>
        struct DayIndex
        {
            /// <summary>
            /// 18h-21h 10个字节是股票代码
            /// </summary>
            public string stockCode;

            /// <summary>
            /// 22h-25h  4个字节 该股票的日线记录数
            /// </summary>
            public Int32 dataCount;

            /// <summary>
            /// 0x26-0x57 每个股票块相对41000h的偏移量，没有数据块后，偏移量为FF FF(-1)，每个数据块8K，可容纳256条记录
            /// 一共25个偏移量，每个2字节，共50字节，25*256=6400 可存25年日线数据
            /// </summary>
            public Int16[] blockOffSet;
        }


        /// <summary>
        /// 返回日线数据、5分钟数据
        /// </summary>
        /// <param name="dataType">读取数据类型，“DAY，5MIN”</param>
        /// <param name="dataCode">证券代码，例如SH600050，SZ000005，如果是获取全部数据则传入NULL</param>
        /// <returns></returns>
        public DataTable GetStockDataLine(string dataType, string dataCode)
        {
            //数据文件的完整路径
            string filePath = "";

            //读取不同的文件
            switch (dataType)
            {
                case "DAY":
                    filePath = DataPath + Market + "\\DAY.DAT";
                    break;
                case "5MIN":
                    filePath = DataPath + Market + "\\MIN.DAT";
                    break;
                default:
                    filePath = DataPath + Market + "\\DAY.DAT"; ;
                    break;
            }

            //判断文件是否存在
            this.CheckFile(filePath);
            if (message == filePath + "文件不存在")
            {
                return null;
            }

            //构建保存日线、分钟线的DataTable
            DataTable dtData = new DataTable();
            dtData.Columns.Add("证券代码", typeof(string));
            dtData.Columns.Add("日期", typeof(string));
            dtData.Columns.Add("开盘", typeof(string));
            dtData.Columns.Add("最高", typeof(string));
            dtData.Columns.Add("最低", typeof(string));
            dtData.Columns.Add("收盘", typeof(string));
            dtData.Columns.Add("成交量", typeof(string));
            dtData.Columns.Add("成交额", typeof(string));
            //dtData.Columns.Add("上涨数", typeof(string));//数据不是很正确，弃用，用程序计算取代
            //dtData.Columns.Add("下跌数", typeof(string));//数据不是很正确，弃用，用程序计算取代

            //打开数据文件，建立文件流
            FileStream fs = null;
            BinaryReader br = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                br = new BinaryReader(fs);
            }
            catch
            {
                message = "读取文件" + filePath + "失败";
                return null;
            }
            finally
            {
                message = "读取文件" + filePath + "成功";
            }

            /*程序读取逻辑
             * 1、读取证券总数
             * 2、读取证券数据索引，取出每个证券的数据条数、每个块的偏移量
             * 3、从每个块中取出每条记录
             */

            //从0C开始读，0C-0F 表示证券总数
            fs.Position = 0x0c; //12

            //DAY.DAT所保存的股票数量
            int stockCount = br.ReadInt32();

            //从18h开始至 40017h 每64byte为一条股票数据分配记录，总共有多少由0x0c提供
            //数据索引开始位置
            int indexStartAddress = 0x18;  //18h       

            //数据索引块大小
            int indexBlock = 0x40; // 64

            //数据索引块数量，每个股票25个块
            int indexBlockNum = 25;

            //证券数据的开始位置
            int dataStarAddress = 0x41000;

            //证券数据的块大小,8k
            int dataBlock = 8192;

            //证券记录块中记录数，每条记录的大小32字节，8K可记录256条
            int dataNum = 256;

            //遍历每条股票
            for (int i = 0; i < stockCount; i++)
            {
                //实例化数据索引结构
                DayIndex dataIndex = new DayIndex();

                //实例化日线索引结构中的数据块偏移量数组，否则下面用到时会报错
                dataIndex.blockOffSet = new Int16[indexBlockNum];

                //设置当前位置
                fs.Position = indexStartAddress + i * indexBlock;

                //取证券代码,10个字节，后面是\0，去掉
                dataIndex.stockCode =
                    this.Market + System.Text.Encoding.Default.GetString(br.ReadBytes(10)).Replace("\0", "");

                //判断是否取到证券代码
                if (string.IsNullOrEmpty(dataIndex.stockCode))
                {
                    break;
                }
                //如果股票代码在静态代码表中，判断需要获取的单个代码
                DataRow[] rows = Main.staticStockCode.Select("证券代码 = '" + dataIndex.stockCode + "'");
                bool isAllCode = true;
                if (dataCode == null) //传入的证券代码
                {
                    isAllCode = rows.Length > 0;
                }
                else
                {
                    //如果是查询单个证券，则当前证券代码=传入的证券代码而且传入的证券代码在静态代码表中
                    isAllCode = (dataIndex.stockCode == dataCode && rows.Length > 0);
                }
                if (isAllCode == true)
                {
                    //读4字节该股票代码的日线记录数
                    dataIndex.dataCount = br.ReadInt32();

                    //读25次，每次两字节，共50字节，记录块的偏移量，没有数据块后，偏移量为FF FF(-1)
                    for (int k = 0; k < indexBlockNum; k++)
                    {
                        dataIndex.blockOffSet[k] = br.ReadInt16();
                    }

                    //开始遍历该股票的所有日线记录，一直读到股票日线数量为止
                    int l = 0;
                    //for (int l = 0; l < dataIndex.dataCount; l++) //用for死循环，l不累加，待查
                    while (l < dataIndex.dataCount)
                    {
                        //每个记录块8K大小，包含256条记录，每条记录32字节，从41000h开始的8KB为第0号数据存储块，以后类推
                        //遍历偏移量
                        for (int m = 0; m < indexBlockNum; m++)
                        {
                            Int16 offset = dataIndex.blockOffSet[m]; //偏移量

                            //偏移量为-1，说明没有数据了
                            if (offset == -1)
                            {
                                break;
                            }

                            //转到41000h开始的第offset块数据，位置为偏移量*8K加上起始位置41000h
                            fs.Position = dataStarAddress + offset * dataBlock;

                            //每条记录32字节,共256条记录
                            for (int n = 0; n < dataNum; n++)
                            {
                                //t==0表示没有数据了，256条没有填满,或者在文件最后由于有几条记录就只写几条数据，读出会溢出
                                if (l >= dataIndex.dataCount)
                                {
                                    break;
                                }
                                //每个32字节块的前4个字节转成INT32表示时间
                                //日线表示19700101开始的天数
                                //5分钟表示19700101开始的秒数
                                Int32 t = br.ReadInt32();
                                //UInt32 t = br.ReadUInt32(); 
                                if (t == 0)
                                {
                                    break;
                                }

                                //实例化证券数据结构
                                DayLine dzhDay = new DayLine();

                                //计算日期还是时间
                                switch (dataType)
                                {
                                    case "DAY":
                                        dzhDay.m_time =
                                            date19700101.AddDays(t / 86400).ToString("yyyy-MM-dd");
                                        break;
                                    case "5MIN":
                                        dzhDay.m_time =
                                            date19700101.AddSeconds(t).ToString("yyyy-MM-dd HH:mm:ss");
                                        break;
                                    default:
                                        dzhDay.m_time =
                                            date19700101.AddDays(t / 86400).ToString("yyyy-MM-dd");
                                        break;
                                }

                                //接着读4*6+2+2共28字节
                                dzhDay.m_fOpen = br.ReadSingle();
                                dzhDay.m_fHigh = br.ReadSingle();
                                dzhDay.m_fLow = br.ReadSingle(); ;
                                dzhDay.m_fClose = br.ReadSingle(); ;
                                dzhDay.m_fVolume = br.ReadSingle();
                                dzhDay.m_fAmount = br.ReadSingle(); ;
                                dzhDay.m_wAdvance = br.ReadInt16();
                                dzhDay.m_wDecline = br.ReadInt16();

                                //将读出的一条日线数据保存到表格中
                                DataRow dRow = dtData.NewRow();
                                dRow["证券代码"] = dataIndex.stockCode;
                                dRow["日期"] = dzhDay.m_time;
                                dRow["开盘"] = dzhDay.m_fOpen.ToString("F2");
                                dRow["最高"] = dzhDay.m_fHigh.ToString("F2");
                                dRow["最低"] = dzhDay.m_fLow.ToString("F2");
                                dRow["收盘"] = dzhDay.m_fClose.ToString("F2");
                                dRow["成交量"] = dzhDay.m_fVolume.ToString("F0");
                                dRow["成交额"] = dzhDay.m_fAmount.ToString("F0");
                                //dRow["上涨数"] = dzhDay.m_wAdvance.ToString("F0");
                                //dRow["下跌数"] = dzhDay.m_wDecline.ToString("F0");
                                dtData.Rows.Add(dRow);
                                l++;
                            }
                        }
                    }
                }
            }
            br.Close();
            fs.Close();
            fs.Dispose();
            return dtData;
        }

        /// <summary>
        /// 返回证券逐笔成交数据(未完成，大智慧的逐笔成交文件显示的数据不完全，可能加密了，准备放弃）
        /// </summary>
        /// <param name="dataCode">证券代码，例如SH600050，SZ000005</param>
        /// <returns></returns>
        public DataTable GetStockZhuBi(string dataCode)
        {
            //数据文件的完整路径
            //string filePath = DataPath + Market + "\\temp\\" + dataCode + ".L2D";
            string filePath = @"F:\1021\data\SH\TEMP\600000.L2D";
            //判断文件是否存在
            this.CheckFile(filePath);
            if (message == filePath + "文件不存在")
            {
                return null;
            }

            //构建保存日线、分钟线的DataTable
            DataTable dtData = new DataTable();
            dtData.Columns.Add("成交时间", typeof(string));
            dtData.Columns.Add("成交价格", typeof(string));
            dtData.Columns.Add("成交量", typeof(string));
            dtData.Columns.Add("方向", typeof(string));
            dtData.Columns.Add("绝对值", typeof(string));
            dtData.Columns.Add("c6", typeof(string));
            dtData.Columns.Add("c7", typeof(string));
            dtData.Columns.Add("c8", typeof(string));
            dtData.Columns.Add("序号", typeof(string));
            dtData.Columns.Add("偏移", typeof(string));


            //打开数据文件，建立文件流
            FileStream fs = null;
            BinaryReader br = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                br = new BinaryReader(fs);
            }
            catch
            {
                message = "读取文件" + filePath + "失败";
                return null;
            }
            finally
            {
                message = "读取文件" + filePath + "成功";
            }

            /*程序读取逻辑
             * 1、读取证券总数
             * 2、读取证券数据索引，取出每个证券的数据条数、每个块的偏移量
             * 3、从每个块中取出每条记录
             */

            //0x28-0x2B 表示证券总数
            fs.Position = 0x28; //12

            //DAY.DAT所保存的股票数量
            int stockCount = br.ReadInt32();

            ////从18h开始至 40017h 每64byte为一条股票数据分配记录，总共有多少由0x0c提供
            ////数据索引开始位置
            //int indexStartAddress = 0x18;  //18h       

            ////数据索引块大小
            //int indexBlock = 0x14; //20

            ////数据索引块数量，每个股票25个块
            //int indexBlockNum = 25;

            //证券数据的开始位置
            int dataStarAddress = 1764;//0x06E4;

            //证券数据的块大小,8k
            int dataBlock = 20;

            ////证券记录块中记录数，每条记录的大小32字节，8K可记录256条
            //int dataNum = 256;

            ////遍历每条股票
            //for (int i = 0; i < stockCount; i++)
            ////for (int i = 0; i < 1; i++)
            //{
            //    try
            //    {
            //        //设置当前位置
            //        fs.Position = dataStarAddress - i * dataBlock;
            ////fs.Position = dataStarAddress;
            //        int m_1 = br.ReadInt32();
            //        Single m_2 = br.ReadSingle();
            //        int m_3 = br.ReadInt16();
            //        int m_4 = br.ReadInt16();
            //        int m_5 = br.ReadInt16();
            //        int m_6 = br.ReadInt16();
            //        int m_7 = br.ReadInt16();
            //        int m_8 = br.ReadInt16();

            //        string m1 = date19700101.AddSeconds(m_1).ToString("yyyy-MM-dd HH:mm:ss");
            //        //string m1 = date19700101.AddSeconds(1319209177).ToString("yyyy-MM-dd HH:mm:ss");
            //        string m2 = m_2.ToString("F2"); //价格
            //        string m3 = m_3.ToString("F2"); //成交量
            //        string m4 = m_4.ToString("F2"); //成交方向
            //        string m5 = m_5.ToString("F2"); //成交量绝对值
            //        string m6 = m_6.ToString("F2");
            //        string m7 = m_7.ToString("F2");
            //        string m8 = m_8.ToString("F2");



            //        //将读出的一条日线数据保存到表格中
            //        DataRow dRow = dtData.NewRow();
            //        dRow["成交时间"] = m1;
            //        dRow["成交价格"] = m2;
            //        dRow["成交量"] = m3;
            //        dRow["方向"] = m4;
            //        dRow["绝对值"] = m5;
            //        dRow["c6"] = m6;
            //        dRow["c7"] = m7;
            //        dRow["c8"] = m8;
            //        dRow["序号"] = i.ToString("F0"); ;
            //        dRow["偏移"] = fs.Position.ToString("F0");

            //        dtData.Rows.Add(dRow);
            //    }
            //    catch { }
            //}


            //遍历所有数据，判断有效时间
            fs.Position = 0;
            while (fs.Position < fs.Length)
            {
                try
                {
                    Int32 t = br.ReadInt32();
                    string m1 = date19700101.AddSeconds(t).ToString("yyyy-MM-dd HH:mm:ss");
                    if (m1.Substring(0, 10) == "2011-10-21")
                    {
                        DataRow dRow = dtData.NewRow();
                        dRow["成交时间"] = m1;
                        dRow["成交价格"] = "";
                        dRow["成交量"] = "";
                        dRow["方向"] = "";
                        dRow["绝对值"] = "";
                        dRow["c6"] = "";
                        dRow["c7"] = "";
                        dRow["c8"] = "";
                        dRow["序号"] = "";
                        dRow["偏移"] = fs.Position.ToString("F0");

                        dtData.Rows.Add(dRow);
                    }
                    fs.Position = fs.Position - 3;
                }
                catch { }

            }
            br.Close();
            fs.Close();
            fs.Dispose();
            return dtData;
        }
    }
}
