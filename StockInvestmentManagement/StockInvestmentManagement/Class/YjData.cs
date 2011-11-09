using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace StockInvestmentManagement.Class
{
    /// <summary>
    /// 银江接口数据接收
    /// </summary>
    public class YjData
    {

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
        struct tag_RCV_REPORT_STRUCTExV3
        {
            [FieldOffset(0)]
            public UInt16 m_cbSize;          // 结构大小
            [FieldOffset(2)]
            public UInt32 m_time;            // 成交时间
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


    }
}
