using System;
using System.Data;

namespace StockInvestmentManagement
{
    /// <summary>
    /// 读取各种证券数据的封装类
    /// </summary>
    public class GetStockData
    {
        #region 读取代码表
        /// <summary>
        /// 读取大智慧股票代码表到静态表
        /// </summary>
        private void GetStockCodeByDZH()
        {
            DzhData dzhData = new DzhData();
            dzhData.DataPath = Main.staticAppSetup["DzhPath"].ToString();

            //上证指数、深成指数
            //dzhData.SpecificCode = new string[] { "SH000001", "SZ3990001" };
            dzhData.SpecificCode = Main.staticAppSetup["SpecificCode"].ToString();

            //沪深A股、深圳创业板
            dzhData.SegularCode = Main.staticAppSetup["SegularCode"].ToString();

            dzhData.Market = "SH";
            DataTable dtSh = dzhData.GetStockCode();
            dzhData.Market = "SZ";
            DataTable dtSZ = dzhData.GetStockCode();

            Main.staticStockCode = dtSh.Copy();
            Main.staticStockCode.Merge(dtSZ);
        }

        /// <summary>
        /// 网页方式读取证券代码
        /// </summary>
        private void GetStockCodeByWeb()
        {
            HtmlRegex hr = new HtmlRegex();
            string content = hr.GetHtmlCode(@"http://quote.eastmoney.com/stocklist.html");
            Main.staticStockCode = hr.RegexStockCode(content);
        }

        /// <summary>
        /// 封装后的读取证券代码方法
        /// </summary>
        public void GetStockCode()
        {
            string stockCodeSouce = Main.staticAppSetup["StockCodeSouce"].ToString();
            switch (stockCodeSouce)
            {
                case "0":
                    GetStockCodeByDZH();  //从大智慧读取
                    break;
                case "1":
                    GetStockCodeByWeb();  //从网页读
                    break;
                default:
                    GetStockCodeByDZH();
                    break;
            }
        }

        #endregion
    }
}
