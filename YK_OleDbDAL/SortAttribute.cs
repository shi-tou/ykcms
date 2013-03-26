using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using YK.IDAL;
using YK.DBUtility;//Please add references
namespace YK.DAL
{
	/// <summary>
	/// 数据访问类:栏目属性
    /// 创建人：杨良斌
    /// 时间：2012-11-29
	/// </summary>
	public partial class SortAttribute:ISortAttribute
	{
		public SortAttribute()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
            return DbHelperSQL.GetMaxID("SortAttributeID", "SortAttribute"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int SortAttributeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SortAttribute");
			strSql.Append(" where SortAttributeID=@SortAttributeID");
            SqlParameter[] parameters = {
					new SqlParameter("@SortAttributeID", SqlDbType.Int,4)
};
			parameters[0].Value = SortAttributeID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}
        /// <summary>
        /// 是否存在指定名称的记录
        /// </summary>
        public bool Exists(string SortAttributeName, int SortAttributeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from SortAttribute");
            strSql.Append(" where SortAttributeName=@SortAttributeName and SortAttributeID <> @SortAttributeID");
            SqlParameter[] parameters = {
					new SqlParameter("@SortAttributeName", SqlDbType.VarChar,50),
                    new SqlParameter("@SortAttributeID", SqlDbType.Int,4)
            };
            parameters[0].Value = SortAttributeName;
            parameters[1].Value = SortAttributeID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(YK.Model.SortAttributeInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SortAttribute(");
			strSql.Append("SortAttributeName,SortAttributeDesc,AdminID,AdminName,CreateTime)");
			strSql.Append(" values (");
			strSql.Append("@SortAttributeName,@SortAttributeDesc,@AdminID,@AdminName,@CreateTime)");
			SqlParameter[] parameters = {
					new SqlParameter("@SortAttributeName", SqlDbType.VarChar,20),
					new SqlParameter("@SortAttributeDesc", SqlDbType.VarChar,100),
					new SqlParameter("@AdminID", SqlDbType.Int,4),
					new SqlParameter("@AdminName", SqlDbType.VarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)};
			parameters[0].Value = model.SortAttributeName;
			parameters[1].Value = model.SortAttributeDesc;
			parameters[2].Value = model.AdminID;
			parameters[3].Value = model.AdminName;
			parameters[4].Value = model.CreateTime;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(YK.Model.SortAttributeInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SortAttribute set ");
			strSql.Append("SortAttributeName=@SortAttributeName,");
			strSql.Append("SortAttributeDesc=@SortAttributeDesc,");
			strSql.Append("AdminID=@AdminID,");
			strSql.Append("AdminName=@AdminName,");
			strSql.Append("CreateTime=@CreateTime");
			strSql.Append(" where SortAttributeID=@SortAttributeID");
			SqlParameter[] parameters = {
					new SqlParameter("@SortAttributeName", SqlDbType.VarChar,20),
					new SqlParameter("@SortAttributeDesc", SqlDbType.VarChar,100),
					new SqlParameter("@AdminID", SqlDbType.Int,4),
					new SqlParameter("@AdminName", SqlDbType.VarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@SortAttributeID", SqlDbType.Int,4)};
			parameters[0].Value = model.SortAttributeName;
			parameters[1].Value = model.SortAttributeDesc;
			parameters[2].Value = model.AdminID;
			parameters[3].Value = model.AdminName;
			parameters[4].Value = model.CreateTime;
			parameters[5].Value = model.SortAttributeID;

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
		public bool Delete(int SortAttributeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SortAttribute ");
			strSql.Append(" where SortAttributeID=@SortAttributeID");
			SqlParameter[] parameters = {
					new SqlParameter("@SortAttributeID", SqlDbType.Int,4)
};
			parameters[0].Value = SortAttributeID;

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
		public bool DeleteList(string SortAttributeIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SortAttribute ");
			strSql.Append(" where SortAttributeID in ("+SortAttributeIDlist + ")  ");
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
		public YK.Model.SortAttributeInfo GetModel(int SortAttributeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select SortAttributeID,SortAttributeName,SortAttributeDesc,AdminID,AdminName,CreateTime from SortAttribute ");
			strSql.Append(" where SortAttributeID=@SortAttributeID");
			SqlParameter[] parameters = {
					new SqlParameter("@SortAttributeID", SqlDbType.Int,4)
};
			parameters[0].Value = SortAttributeID;

			YK.Model.SortAttributeInfo model=new YK.Model.SortAttributeInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["SortAttributeID"]!=null && ds.Tables[0].Rows[0]["SortAttributeID"].ToString()!="")
				{
					model.SortAttributeID=int.Parse(ds.Tables[0].Rows[0]["SortAttributeID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SortAttributeName"]!=null && ds.Tables[0].Rows[0]["SortAttributeName"].ToString()!="")
				{
					model.SortAttributeName=ds.Tables[0].Rows[0]["SortAttributeName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SortAttributeDesc"]!=null && ds.Tables[0].Rows[0]["SortAttributeDesc"].ToString()!="")
				{
					model.SortAttributeDesc=ds.Tables[0].Rows[0]["SortAttributeDesc"].ToString();
				}				
				if(ds.Tables[0].Rows[0]["AdminID"]!=null && ds.Tables[0].Rows[0]["AdminID"].ToString()!="")
				{
					model.AdminID=int.Parse(ds.Tables[0].Rows[0]["AdminID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AdminName"]!=null && ds.Tables[0].Rows[0]["AdminName"].ToString()!="")
				{
					model.AdminName=ds.Tables[0].Rows[0]["AdminName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["CreateTime"]!=null && ds.Tables[0].Rows[0]["CreateTime"].ToString()!="")
				{
					model.CreateTime=DateTime.Parse(ds.Tables[0].Rows[0]["CreateTime"].ToString());
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
			strSql.Append("select SortAttributeID,SortAttributeName,SortAttributeDesc,AdminID,AdminName,CreateTime ");
			strSql.Append(" FROM SortAttribute ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
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
					new SqlParameter("@IsReCount", SqlDbType.Boolean),
					new SqlParameter("@OrderType", SqlDbType.Boolean),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "SortAttribute";
			parameters[1].Value = "SortAttributeID";
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

