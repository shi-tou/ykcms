using System;
using System.Data;
using System.Text;
using System.Data.OleDb;
using YK.IDAL;
using YK.DBUtility;
using System.Data.SqlClient;
namespace YK.DAL
{
    /// <summary>
    /// 描述:日志管理--数据访问层
    /// 作者:杨良斌
    /// 时间:2012-08-15
    /// </summary>
	public partial class SysLog:ISysLog
	{
		public SysLog()
		{}
		#region  Method
		/// <summary>
		/// 得到最大ID
		/// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("SysLogID", "SysLog");
        }
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int SysLogID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(*) from SysLog");
			strSql.Append(" where SysLogID=@SysLogID");
			SqlParameter[] parameters = {
					new SqlParameter("@SysLogID", SqlDbType.Int,4)
            };
			parameters[0].Value = SysLogID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 增加一条数据
		/// </summary>
        public void Add(YK.Model.SysLogInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SysLog(");
			strSql.Append("Area,Type,Detail,IP,AdminID,AdminName,CreateTime)");
			strSql.Append(" values (");
			strSql.Append("@Area,@Type,@Detail,@IP,@AdminID,@AdminName,@CreateTime)");
			SqlParameter[] parameters = {
					new SqlParameter("@Area", SqlDbType.VarChar,50),
					new SqlParameter("@Type", SqlDbType.VarChar,100),
					new SqlParameter("@Detail", SqlDbType.VarChar,50),
                    new SqlParameter("@IP", SqlDbType.VarChar,50),
					new SqlParameter("@AdminID", SqlDbType.Int,4),
					new SqlParameter("@AdminName", SqlDbType.VarChar,20),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)};
			parameters[0].Value = model.Area;
			parameters[1].Value = model.Type;
			parameters[2].Value = model.Detail;
            parameters[3].Value = model.IP;
			parameters[4].Value = model.AdminID;
			parameters[5].Value = model.AdminName;
			parameters[6].Value = model.CreateTime;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(YK.Model.SysLogInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SysLog set ");
			strSql.Append("Area=@Area,");
			strSql.Append("Type=@Type,");
			strSql.Append("Detail=@Detail,");
            strSql.Append("IP=@IP,");
			strSql.Append("AdminID=@AdminID,");
			strSql.Append("AdminName=@AdminName,");
			strSql.Append("CreateTime=@CreateTime");
			strSql.Append(" where SysLogID=@SysLogID");
			SqlParameter[] parameters = {
					new SqlParameter("@Area", SqlDbType.VarChar,50),
					new SqlParameter("@Type", SqlDbType.VarChar,100),
					new SqlParameter("@Detail", SqlDbType.VarChar,50),
                    new SqlParameter("@IP", SqlDbType.VarChar,50),
					new SqlParameter("@AdminID", SqlDbType.Int,4),
					new SqlParameter("@AdminName", SqlDbType.VarChar,20),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@SysLogID", SqlDbType.Int,4)};
			parameters[0].Value = model.Area;
			parameters[1].Value = model.Type;
			parameters[2].Value = model.Detail;
            parameters[3].Value = model.IP;
			parameters[4].Value = model.AdminID;
			parameters[5].Value = model.AdminName;
			parameters[6].Value = model.CreateTime;
			parameters[7].Value = model.SysLogID;
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int SysLogID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SysLog ");
			strSql.Append(" where SysLogID=@SysLogID");
			SqlParameter[] parameters = {
					new SqlParameter("@SysLogID", SqlDbType.Int,4)
            };
			parameters[0].Value = SysLogID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string SysLogIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SysLog ");
			strSql.Append(" where SysLogID in ("+SysLogIDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public YK.Model.SysLogInfo GetModel(int SysLogID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select SysLogID,Area,Type,Detail,IP,AdminID,AdminName,CreateTime from SysLog ");
            strSql.Append(" where SysLogID=@SysLogID");
            SqlParameter[] parameters = {
					new SqlParameter("@SysLogID", SqlDbType.Int,4)
            };
            parameters[0].Value = SysLogID;

            YK.Model.SysLogInfo model = new YK.Model.SysLogInfo();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["SysLogID"] != null && ds.Tables[0].Rows[0]["SysLogID"].ToString() != "")
                {
                    model.SysLogID = int.Parse(ds.Tables[0].Rows[0]["SysLogID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Area"] != null && ds.Tables[0].Rows[0]["Area"].ToString() != "")
                {
                    model.Area = ds.Tables[0].Rows[0]["Area"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Type"] != null && ds.Tables[0].Rows[0]["Type"].ToString() != "")
                {
                    model.Type = ds.Tables[0].Rows[0]["Type"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Detail"] != null && ds.Tables[0].Rows[0]["Detail"].ToString() != "")
                {
                    model.Detail = ds.Tables[0].Rows[0]["Detail"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IP"] != null && ds.Tables[0].Rows[0]["IP"].ToString() != "")
                {
                    model.IP = ds.Tables[0].Rows[0]["IP"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AdminID"] != null && ds.Tables[0].Rows[0]["AdminID"].ToString() != "")
                {
                    model.AdminID = int.Parse(ds.Tables[0].Rows[0]["AdminID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AdminName"] != null && ds.Tables[0].Rows[0]["AdminName"].ToString() != "")
                {
                    model.AdminName = ds.Tables[0].Rows[0]["AdminName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CreateTime"] != null && ds.Tables[0].Rows[0]["CreateTime"].ToString() != "")
                {
                    model.CreateTime = DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select SysLogID,Area,Type,Detail,IP,AdminID,AdminName,CreateTime ");
			strSql.Append(" FROM SysLog ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}		
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(string TableName,string KeyField,int PageSize,int PageIndex,string strWhere)
		{
            DataSet ds = new DataSet();
            int Pages = PageSize * (PageIndex - 1) + 1;
            StringBuilder strSql1 = new StringBuilder();
            StringBuilder strSql2 = new StringBuilder();
            strSql1.AppendFormat("select top {2} * from {0} where {1} >=(select max({1}) from (select top {3} {1} from {0} where {4} order by {1} asc))",TableName,KeyField,PageSize,Pages,strWhere);
            strSql2.AppendFormat("select count(*) from {0} where {1}",TableName,strWhere);
            DataTable dt1 = DbHelperSQL.Query(strSql1.ToString()).Tables[0];
            DataTable dt2 = DbHelperSQL.Query(strSql2.ToString()).Tables[0];
            dt1.TableName = "tList";
            dt2.TableName = "tCount";
            ds.Tables.Add(dt1.Copy());
            ds.Tables.Add(dt2.Copy());
            return ds;
		} 
		#endregion  Method
	}
}

