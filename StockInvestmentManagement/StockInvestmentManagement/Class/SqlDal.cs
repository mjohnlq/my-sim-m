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
        /// 将d表值参数，传递给存储过程，由存储过程在数据库内进行比较、更新处理
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
    }
}
