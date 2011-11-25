using System;
using System.Collections.Generic;
using System.Text;
using PersistenceLayer;
using System.Data;
using BusinessEntity;
using System.Data.Common;
using System.Data.SqlClient;

namespace StockInvestmentManagement
{
    public class SqlDal
    {
        string source = "Data Source=CWX99-PC;" +
                            "Initial Catalog=SIM;" +
                            "Integrated Security=SSPI;" +
                            "Encrypt=False";

        /// <summary>
        /// 读取所有系统设置选项并保存到静态哈希表
        /// </summary>
        public void GetAppSetup()
        {
            RetrieveCriteria rc = new RetrieveCriteria(typeof(Entity系统选项表));
            DataTable dt = rc.AsDataTable();
            foreach (DataRow row in dt.Rows)
            {
                Main.staticAppSetup.Add(row["关键字"].ToString(), row["值"].ToString());
            }
        }

        /// <summary>
        /// 返回某个系统选项的值
        /// </summary>
        /// <param name="setupKey">关键字</param>
        /// <returns></returns>
        public string GetAppSetupValue(string setupKey)
        {
            RetrieveCriteria rc = new RetrieveCriteria(typeof(Entity系统选项表));
            Condition c = rc.GetNewCondition();
            c.AddEqualTo(Entity系统选项表.__关键字, setupKey);
            Entity系统选项表 ex = (Entity系统选项表)rc.AsEntity();
            return ex.值;
        }

        /// <summary>
        /// 获取证券代码表
        /// </summary>
        /// <returns></returns>
        public DataTable GetStockCode()
        {
            RetrieveCriteria rc = new RetrieveCriteria(typeof(Entity代码表));
            return rc.AsDataTable();
        }

        /// <summary>
        /// 获取行情表
        /// </summary>
        /// <returns></returns>
        public DataTable GetStockHq()
        {
            RetrieveCriteria rc = new RetrieveCriteria(typeof(Entity行情表));
            return rc.AsDataTable();
        }

        /// <summary>
        /// 获取分钟线表
        /// </summary>
        /// <returns></returns>
        public DataTable GetStockMin()
        {
            RetrieveCriteria rc = new RetrieveCriteria(typeof(Entity分钟线表));
            return rc.AsDataTable();
        }

        /// <summary>
        /// 将静态代码表更新到数据库中
        /// </summary>
        /// <param name="dt"></param>
        public void UpdateStockCodeTVP(DataTable dt)
        {
            IDataParameter para = Query.GetParameter("SIM"); //创建一个参数 
            para.ParameterName = "@tvp";  //定义参数名 
            para.Value = dt;           //定义参数值 
            IDataParameter[] paras = new IDataParameter[1];  //定义参数数组 
            paras[0] = para;         //给数组赋个参数 
            Query.RunProcedure("UpdateStockCodeWithTVP", paras, "SIM"); //执行存储过程 
        }

        /// <summary>
        /// 将行情数据列表更新到行情表
        /// </summary>
        /// <param name="dt"></param>
        public void UpdateHqTVP(DataTable dt)
        {
            IDataParameter para = Query.GetParameter("SIM"); //创建一个参数 
            para.ParameterName = "@tvp";  //定义参数名 
            para.Value = dt;           //定义参数值 
            IDataParameter[] paras = new IDataParameter[1];  //定义参数数组 
            paras[0] = para;         //给数组赋个参数 
            Query.RunProcedure("UpdateHqWithTVP", paras, "SIM"); //执行存储过程 
        }

        /// <summary>
        /// 将分时线数据更新到数据库
        /// </summary>
        /// <param name="zqdm">该表所属的证券代码</param>
        /// <param name="dt">该证券代码的分时线数据表</param>
        public void UpdateMinTVP(string zqdm, DataTable dt)
        {
            IDataParameter para = Query.GetParameter("SIM"); //创建一个参数 
            para.ParameterName = "@tvp";  //定义参数名 
            para.Value = dt;           //定义参数值 

            IDataParameter para1 = Query.GetParameter("SIM"); //创建一个参数 
            para1.ParameterName = "@zqdm";  //定义参数名 
            para1.Value = zqdm;           //定义参数值 

            IDataParameter[] paras = new IDataParameter[2];  //定义参数数组 
            paras[0] = para;         //给数组赋个参数 
            paras[1] = para1;
            Query.RunProcedure("UpdateMinWithTVP", paras, "SIM"); //执行存储过程 
        }

        /// <summary>
        /// 在大批量插入主笔成交数据前禁用索引，插入后启用索引
        /// </summary>
        public void DisableOrEnableZhuBiTableIndex(string dir)
        {
            IDataParameter para = Query.GetParameter("SIM"); //创建一个参数 
            para.ParameterName = "@dir";  //定义参数名 
            para.Value = dir;           //定义参数值 

            IDataParameter[] paras = new IDataParameter[1];  //定义参数数组 
            paras[0] = para;         //给数组赋个参数 
            Query.RunProcedure("DisableOrEnableZhuBiTableIndex", paras, "SIM"); //执行存储过程 
        }

        /// <summary>
        /// 将逐笔成交记录插入到数据库,传入表变量方式
        /// </summary>
        /// <param name="zqdm">该表所属的证券代码</param>
        /// <param name="cjrq">成交日期</param>
        /// <param name="dt">该证券代码的逐笔成交记录表</param>
        public void UpdateZhuBi(string zqdm,string cjrq, DataTable dt)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(source))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UpdateZhuBiWithTVP1", conn);
                    cmd.CommandTimeout = 60 * 60 * 60; //1小时
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tvp", dt);
                    cmd.Parameters.AddWithValue("@zqdm", zqdm);
                    cmd.Parameters.AddWithValue("@cjrq", cjrq);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            { 
                
            }
        }


        /// <summary>
        /// 将逐笔成交记录插入到数据库,传入表变量方式BULK INSERT方式
        /// </summary>
        /// <param name="dataFile">逐笔成交文件完整路径</param>
        /// <param name="zqdm">证券代码</param>
        /// <param name="cjrq">成交日期</param>
        /// <param name="fmtFile">格式化文件完整路径</param>
        public void UpdataZhuBi(string dataFile, string zqdm, string cjrq, string fmtFile)
        {
            //使用原始的ADO.NET
            //string source = "Data Source=CWX99-PC;" +
            //                "Initial Catalog=SIM;" +
            //                "Integrated Security=SSPI;" +
            //                "Encrypt=False";
            try
            {
                using (SqlConnection conn = new SqlConnection(source))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UpdateZhuBiWithBULKINSERT", conn);
                    cmd.CommandTimeout = 60 * 60 * 60; //1小时
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@dataFile", dataFile);
                    cmd.Parameters.AddWithValue("@zqdm", zqdm);
                    cmd.Parameters.AddWithValue("@cjrq", cjrq);
                    cmd.Parameters.AddWithValue("@fmtFile", fmtFile);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {

            }
        }

        /// <summary>
        /// 强制释放SQL占用的内存
        /// </summary>
        public void ReclaimMemory()
        {
            //使用原始的ADO.NET
            //string source = "Data Source=CWX99-PC;" +
            //                "Initial Catalog=SIM;" +
            //                "Integrated Security=SSPI;" +
            //                "Encrypt=False";
            try
            {
                using (SqlConnection conn = new SqlConnection(source))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("ReclaimMemory", conn);
                    cmd.CommandTimeout = 60 * 60 * 60; //1小时
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {

            }
        }

        /// <summary>
        /// 取得逐笔成交数据的最大日期
        /// </summary>
        /// <returns></returns>
        public string GetMaxZhuBiDate()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(source))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select MAX(成交日期) from 逐笔成交记录", conn);
                    cmd.CommandTimeout = 60 * 60 * 60; //1小时
                    cmd.CommandType = CommandType.Text;
                    return (string)cmd.ExecuteScalar();
                }
            }
            catch (SqlException e)
            {
                return null;
            }
        }

        /// <summary>
        /// 取得逐笔成交数据的最小日期
        /// </summary>
        /// <returns></returns>
        public string GetMinZhuBiDate()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(source))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("select MIN(成交日期) from 逐笔成交记录", conn);
                    cmd.CommandTimeout = 60 * 60 * 60; //1小时
                    cmd.CommandType = CommandType.Text;
                    return (string)cmd.ExecuteScalar();
                }
            }
            catch (SqlException e)
            {
                return null;
            }
        }

        /// <summary>
        /// 清空所有逐笔数据
        /// </summary>
        public void TruncateZhuBi()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(source))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("TruncateZhuBi", conn);
                    cmd.CommandTimeout = 60 * 60 * 60; //1小时
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {

            }
        }

        /// <summary>
        /// 删除选定日期范围的逐笔数据
        /// </summary>
        public void DeleZhuBiByDate(string maxDate,string minDate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(source))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("DeleZhuBiByDate", conn);
                    cmd.CommandTimeout = 60 * 60 * 60; //1小时
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@maxDate", maxDate);
                    cmd.Parameters.AddWithValue("@minDate", minDate);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {

            }
        }


        /// <summary>
        /// 更新交割单记录
        /// </summary>
        /// <param name="dt"></param>
        public void UpdateJgdWhitTVP(DataTable dt)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(source))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UpdateJgdWithTVP", conn);
                    cmd.CommandTimeout = 60 * 60 * 60; //1小时
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tvp", dt);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {

            }
        }
    }
}
