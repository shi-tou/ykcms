using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace YK.DBUtility
{
    public class CommonDbHelper
    {
        /// <summary>
        /// 获取子父级记录
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="IDName">ID字段名</param>
        /// <param name="PIDName">父ID字段名</param>
        /// <param name="SqlWhere">条件</param>
        /// <returns></returns>
        public static DataSet GetAllChilds(string TableName, string IDName, string PIDName, string SqlWhere)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@TableName", SqlDbType.VarChar,50),
                    new SqlParameter("@IDName", SqlDbType.VarChar,50),
                    new SqlParameter("@PIDName", SqlDbType.VarChar,50),
                    new SqlParameter("@SqlWhere", SqlDbType.VarChar,500)
            };
            parameters[0].Value = TableName;
            parameters[1].Value = IDName;
            parameters[2].Value = PIDName;
            parameters[3].Value = SqlWhere;
            return (DataSet)DbHelperSQL.RunProcedure("[GetAllChilds]", parameters);

        }
        /// <summary>
        /// 删除子父级记录
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="IDName">ID字段名</param>
        /// <param name="PIDName">父ID字段名</param>
        /// <param name="SqlWhere">条件</param>
        /// <returns></returns>
        public static bool DeleteAllChilds(string TableName, string IDName, string PIDName, string SqlWhere)
        {
            int res=0;
            SqlParameter[] parameters = {
					new SqlParameter("@TableName", SqlDbType.VarChar,50),
                    new SqlParameter("@IDName", SqlDbType.VarChar,50),
                    new SqlParameter("@PIDName", SqlDbType.VarChar,50),
                    new SqlParameter("@SqlWhere", SqlDbType.VarChar,500)
            };
            parameters[0].Value = TableName;
            parameters[1].Value = IDName;
            parameters[2].Value = PIDName;
            parameters[3].Value = SqlWhere;
            DbHelperSQL.RunProcedure("[DeleteAllChilds]", parameters, out res);
            if(res>0)
                return true;
            else
                return false;
        }
    }
}
