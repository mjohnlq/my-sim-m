using System;
using System.Collections.Generic;
using System.Text;
using PersistenceLayer;
using System.Data;
using BusinessEntity;

namespace StockInvestmentManagement
{
    public class SqlDal
    {

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
    }
}
