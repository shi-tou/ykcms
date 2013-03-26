using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using YK.IDAL;
using YK.DBUtility;
namespace YK.DAL
{
	/// <summary>
	/// 数据访问类:AdminGroup
	/// </summary>
	public partial class AdminGroup:IAdminGroup
	{
		public AdminGroup()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("GroupID", "AdminGroup"); 
		}
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int GroupID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AdminGroup");
			strSql.Append(" where GroupID=@GroupID");
			SqlParameter[] parameters = {
					new SqlParameter("@GroupID", SqlDbType.Int,4)
};
			parameters[0].Value = GroupID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 是否存在指定名称的记录
        /// </summary>
        public bool Exists(string GroupName, int GroupID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from AdminGroup");
            strSql.Append(" where GroupName=@GroupName and GroupID <> @GroupID");
            SqlParameter[] parameters = {
					new SqlParameter("@GroupName", SqlDbType.VarChar,50),
                    new SqlParameter("@GroupID", SqlDbType.Int,4)
            };
            parameters[0].Value = GroupName;
            parameters[1].Value = GroupID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(YK.Model.AdminGroupInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AdminGroup(");
			strSql.Append("GroupName,Describe,GroupAuth,CreateTime,AdminID)");
			strSql.Append(" values (");
			strSql.Append("@GroupName,@Describe,@GroupAuth,@CreateTime,@AdminID)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@GroupName", SqlDbType.VarChar,10),
					new SqlParameter("@Describe", SqlDbType.VarChar,50),
					new SqlParameter("@GroupAuth", SqlDbType.VarChar,500),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@AdminID", SqlDbType.Int,4)};
			parameters[0].Value = model.GroupName;
			parameters[1].Value = model.Describe;
			parameters[2].Value = model.GroupAuth;
			parameters[3].Value = model.CreateTime;
			parameters[4].Value = model.AdminID;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(YK.Model.AdminGroupInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AdminGroup set ");
			strSql.Append("GroupName=@GroupName,");
			strSql.Append("Describe=@Describe,");
			strSql.Append("GroupAuth=@GroupAuth,");
			strSql.Append("CreateTime=@CreateTime,");
			strSql.Append("AdminID=@AdminID");
			strSql.Append(" where GroupID=@GroupID");
			SqlParameter[] parameters = {
					new SqlParameter("@GroupName", SqlDbType.VarChar,10),
					new SqlParameter("@Describe", SqlDbType.VarChar,50),
					new SqlParameter("@GroupAuth", SqlDbType.VarChar,500),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@AdminID", SqlDbType.Int,4),
					new SqlParameter("@GroupID", SqlDbType.Int,4)};
			parameters[0].Value = model.GroupName;
			parameters[1].Value = model.Describe;
			parameters[2].Value = model.GroupAuth;
			parameters[3].Value = model.CreateTime;
			parameters[4].Value = model.AdminID;
			parameters[5].Value = model.GroupID;

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
		public bool Delete(int GroupID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from AdminGroup ");
			strSql.Append(" where GroupID=@GroupID");
			SqlParameter[] parameters = {
					new SqlParameter("@GroupID", SqlDbType.Int,4)
};
			parameters[0].Value = GroupID;

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
		public bool DeleteList(string GroupIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from AdminGroup ");
			strSql.Append(" where GroupID in ("+GroupIDlist + ")  ");
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
		public YK.Model.AdminGroupInfo GetModel(int GroupID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 GroupID,GroupName,Describe,GroupAuth,CreateTime,AdminID from AdminGroup ");
			strSql.Append(" where GroupID=@GroupID");
			SqlParameter[] parameters = {
					new SqlParameter("@GroupID", SqlDbType.Int,4)
};
			parameters[0].Value = GroupID;

			YK.Model.AdminGroupInfo model=new YK.Model.AdminGroupInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["GroupID"]!=null && ds.Tables[0].Rows[0]["GroupID"].ToString()!="")
				{
					model.GroupID=int.Parse(ds.Tables[0].Rows[0]["GroupID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GroupName"]!=null && ds.Tables[0].Rows[0]["GroupName"].ToString()!="")
				{
					model.GroupName=ds.Tables[0].Rows[0]["GroupName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Describe"]!=null && ds.Tables[0].Rows[0]["Describe"].ToString()!="")
				{
					model.Describe=ds.Tables[0].Rows[0]["Describe"].ToString();
				}
				if(ds.Tables[0].Rows[0]["GroupAuth"]!=null && ds.Tables[0].Rows[0]["GroupAuth"].ToString()!="")
				{
					model.GroupAuth=ds.Tables[0].Rows[0]["GroupAuth"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CreateTime"]!=null && ds.Tables[0].Rows[0]["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AdminID"]!=null && ds.Tables[0].Rows[0]["AdminID"].ToString()!="")
				{
					model.AdminID=int.Parse(ds.Tables[0].Rows[0]["AdminID"].ToString());
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
			strSql.Append("select a.* ,b.AdminName");
			strSql.Append(" FROM AdminGroup as a join Admin as b on b.AdminID=a.AdminID  ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" GroupID,GroupName,Describe,GroupAuth,CreateTime,AdminID ");
			strSql.Append(" FROM AdminGroup ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "AdminGroup";
			parameters[1].Value = "GroupID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
	}
}

