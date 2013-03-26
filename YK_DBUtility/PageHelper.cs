using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace YK.DBUtility
{
   public class PageHelper
    {
        /// <summary>
        /// 分页获取数据信息
        /// </summary>
        /// <param name="tabName"></param>
        /// <param name="getFields"></param>
        /// <param name="orderKey"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="isAsc"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public static DataSet GetPageList(string tabName, string getFields, string orderKey, int pageSize, int pageIndex, int isAsc, string strWhere)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@TabName", SqlDbType.VarChar,20),
                    new SqlParameter("@GetFields", SqlDbType.VarChar,500),
                    new SqlParameter("@OrderKey", SqlDbType.VarChar,20),
                    new SqlParameter("@PageSize", SqlDbType.Int,4),
                    new SqlParameter("@PageIndex", SqlDbType.Int,4),
                    new SqlParameter("@isAsc", SqlDbType.Int,4),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,500)
            };
            parameters[0].Value = tabName;
            parameters[1].Value = getFields;
            parameters[2].Value = orderKey;
            parameters[3].Value = pageSize;
            parameters[4].Value = pageIndex;
            parameters[5].Value = isAsc;
            parameters[6].Value = strWhere;
            return (DataSet)DbHelperSQL.RunProcedure("[sp_GetPageList]", parameters);
           
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(string TableName, string KeyField, int PageSize, int PageIndex, string strWhere)
        {
            DataSet ds = new DataSet();
            int Pages = PageSize * (PageIndex - 1) + 1;
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            strSql1.AppendFormat("select top {2} * from {0} where {1} >=(select max({1}) from (select top {3} {1} from {0} where {4} order by {1} asc))", TableName, KeyField, PageSize, Pages, strWhere);
            strSql2.AppendFormat("select count(*) from {0} where {1}", TableName, strWhere);
            DataTable dt1 = DbHelperSQL.Query(strSql1.ToString()).Tables[0];
            DataTable dt2 = DbHelperSQL.Query(strSql2.ToString()).Tables[0];
            dt1.TableName = "tList";
            dt2.TableName = "tCount";
            ds.Tables.Add(dt1.Copy());
            ds.Tables.Add(dt2.Copy());
            return ds;
        } 
    }
}
