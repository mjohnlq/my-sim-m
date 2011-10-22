using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Data;

namespace StockInvestmentManagement
{
    /// <summary>
    /// 网页内容提取
    /// </summary>
    public class HtmlRegex
    {
        /// <summary>
        /// 传入网址，返回页面源代码,gb2312
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetHtmlCode(string url)
        {
            string content = "";
            try
            {
                //以下注释掉的代码也是常用实现方法之一

                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);//声明一个HttpWebRequest请求                
                //request.Timeout = 30000;//设置连接超时时间                
                //request.Headers.Set("Pragma", "no-cache");
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //Stream streamReceive = response.GetResponseStream();
                //System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("GB2312");
                //StreamReader streamReader = new StreamReader(streamReceive, encoding);
                //content = streamReader.ReadToEnd();

                WebClient wc = new WebClient();  //首先创建一个能够从URI中接收数据的WebClient的对象  
                //wc.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 6.0)"); //设置标头header  
                Stream str = wc.OpenRead(url);  //从URI打开可读流 并返回网络流对象
                StreamReader sr = new StreamReader(str, System.Text.Encoding.GetEncoding("GB2312"));
                //以网络流对象为参数实例化一个读取流,设置默认编码  
                content = sr.ReadToEnd();       //读到末尾  
                str.Close();                    //关闭网络流  
            }
            catch (Exception ex)
            {
                content = "";   
            }
            return content;
        }

        /// <summary>
        /// 解析网页源代码，提取证券代码名称（东方财富网证券列表）
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public DataTable RegexStockCode(string content)
        {
            if (content.Length == 0)
                return null;

            DataTable dt = new DataTable();

            //构建显示证券代码的表格
            dt.Columns.Add("ID", typeof(Int16));
            dt.Columns.Add("证券代码", typeof(string));
            dt.Columns.Add("证券名称", typeof(string));
            dt.Columns.Add("速记码", typeof(string));

            string stock; //每行证券信息
            string gpsc; //证券市场
            string gpdm;
            string gpmc;

            //正则获取<li><a target="_blank" href="http://quote.eastmoney.com/sz300223.html">北京君正(300223)</a></li>这种段落
            Match m;
            Regex r = new Regex(@"<li><a target=.*?</a></li>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            for (m = r.Match(content); m.Success; m = m.NextMatch())
            {

                //每行证券链接
                stock = m.Groups[0].Value.ToString();

                //市场
                if (stock.IndexOf(@"/sh") > 0)
                {
                    gpsc = "SH";
                }
                else
                {
                    gpsc = "SZ";
                }

                //完整行：<li><a target="_blank" href="http://quote.eastmoney.com/sh500001.html">基金金泰(500001)</a></li>
                //证券名称 
                //截取html开始到证券代码最后一位之间的字符串
                stock = stock.Substring(stock.IndexOf("html"));

                //从第七位到倒数第七位之间为证券名称
                gpmc = stock.Substring(6, stock.Length - 23);

                //最后六位为证券代码
                gpdm = stock.Substring(gpmc.Length + 7, 6);

                DataRow row = dt.NewRow();
                row["证券代码"] = gpsc+gpdm;
                row["证券名称"] = gpmc;
                row["速记码"] = ChinesePinYin.GetChineseSpell(gpmc);

                //查找静态证券代码表，如果存在，则ID为证券代码ID，不存在则ID=0
                DataRow[] rows = Main.staticStockCode.Select("证券代码 = '" + row["证券代码"] + "'");
                if (rows.Length>0)
                {
                    row["ID"] = rows[0]["ID"];
                }
                else
                {
                    row["ID"] = 0;
                }

                dt.Rows.Add(row);
            }

            ////添加上证指数和深成指数（添加一次后就不用了）
            //DataRow sh = dt.NewRow();
            //sh["证券代码"] = "999999SH";
            //sh["证券名称"] = "上证指数";
            //sh["速记码"] = "SZZS";
            ////查找静态证券代码表，如果存在，则ID为证券代码ID，不存在则ID=0
            //DataRow[] rowsh = Main.staticStockCode.Select("证券代码 = '" + sh["证券代码"] + "'");
            //if (rowsh.Length > 0)
            //{
            //    sh["ID"] = rowsh[0]["ID"];
            //}
            //else
            //{
            //    sh["ID"] = 0;
            //}

            //dt.Rows.Add(sh);

            //DataRow sz = dt.NewRow();
            //sz["证券代码"] = "399001SZ";
            //sz["证券名称"] = "深成指数";
            //sz["速记码"] = "SCZS";
            ////查找静态证券代码表，如果存在，则ID为证券代码ID，不存在则ID=0
            //DataRow[] rowsz = Main.staticStockCode.Select("证券代码 = '" + sz["证券代码"] + "'");
            //if (rowsz.Length > 0)
            //{
            //    sz["ID"] = rowsh[0]["ID"];
            //}
            //else
            //{
            //    sz["ID"] = 0;
            //}
            //dt.Rows.Add(sz);

            return dt;
        }
    }
}
