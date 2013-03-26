using System;
using System.Data;
using System.Text;
using YK.IDAL;
using YK.DBUtility;
using System.Data.SqlClient;//Please add references
namespace YK.DAL
{
    /// <summary>
    /// 描述:栏目管理--数据访问层
    /// 作者:杨良斌
    /// 时间:2012-08-15
    /// </summary>
	public partial class Sort:ISort
	{
		public Sort()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("SortID", "Sort"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int SortID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Sort");
			strSql.Append(" where SortID=@SortID");
			SqlParameter[] parameters = {
					new SqlParameter("@SortID", SqlDbType.Int,4)
};
			parameters[0].Value = SortID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}
        /// <summary>
        /// 是否存在指定名称的记录
        /// </summary>
        public bool Exists(string SortName, int SortID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Sort");
            strSql.Append(" where SortName=@SortName and SortID <> @SortID");
            SqlParameter[] parameters = {
					new SqlParameter("@SortName", SqlDbType.VarChar,50),
                    new SqlParameter("@SortID", SqlDbType.Int,4)
            };
            parameters[0].Value = SortName;
            parameters[1].Value = SortID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(YK.Model.SortInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Sort(");
            strSql.Append("ParentID,SortAttributeID,SortName,SortUrl,SortTemplateID,PageTitle,PageKeywords,PageDesc,BannerUrl,State,AdminID,AdminName,CreateTime)");
			strSql.Append(" values (");
            strSql.Append("@ParentID,@SortAttributeID,@SortName,@SortUrl,@SortTemplateID,@PageTitle,@PageKeywords,@PageDesc,@BannerUrl,@State,@AdminID,@AdminName,@CreateTime)");
			SqlParameter[] parameters = {
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@SortAttributeID", SqlDbType.Int,4),
					new SqlParameter("@SortName", SqlDbType.VarChar,50),
					new SqlParameter("@SortUrl", SqlDbType.VarChar,100),
					new SqlParameter("@SortTemplateID", SqlDbType.Int,4),
					new SqlParameter("@PageTitle", SqlDbType.VarChar,100),
					new SqlParameter("@PageKeywords", SqlDbType.VarChar,100),
					new SqlParameter("@PageDesc", SqlDbType.VarChar,200),
					new SqlParameter("@BannerUrl", SqlDbType.VarChar,100),
                    new SqlParameter("@State", SqlDbType.Int,4),
                    new SqlParameter("@AdminID", SqlDbType.Int,4),
                    new SqlParameter("@AdminName", SqlDbType.VarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)};
			parameters[0].Value = model.ParentID;
			parameters[1].Value = model.SortAttributeID;
			parameters[2].Value = model.SortName;
			parameters[3].Value = model.SortUrl;
			parameters[4].Value = model.SortTemplateID;
			parameters[5].Value = model.PageTitle;
			parameters[6].Value = model.PageKeywords;
			parameters[7].Value = model.PageDesc;
			parameters[8].Value = model.BannerUrl;
            parameters[9].Value = model.State;
            parameters[10].Value = model.AdminID;
            parameters[11].Value = model.AdminName;
			parameters[12].Value = model.CreateTime;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(YK.Model.SortInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Sort set ");
			strSql.Append("ParentID=@ParentID,");
			strSql.Append("SortAttributeID=@SortAttributeID,");
			strSql.Append("SortName=@SortName,");
			strSql.Append("SortUrl=@SortUrl,");
            strSql.Append("SortTemplateID=@SortTemplateID,");
			strSql.Append("PageTitle=@PageTitle,");
			strSql.Append("PageKeywords=@PageKeywords,");
			strSql.Append("PageDesc=@PageDesc,");
			strSql.Append("BannerUrl=@BannerUrl,");
            strSql.Append("State=@State,");
			strSql.Append("CreateTime=@CreateTime");
			strSql.Append(" where SortID=@SortID");
			SqlParameter[] parameters = {
					new SqlParameter("@ParentID", SqlDbType.Int,4),
					new SqlParameter("@SortAttributeID", SqlDbType.Int,4),
					new SqlParameter("@SortName", SqlDbType.VarChar,50),
					new SqlParameter("@SortUrl", SqlDbType.VarChar,100),
					new SqlParameter("@SortTemplateID", SqlDbType.Int,4),
					new SqlParameter("@PageTitle", SqlDbType.VarChar,100),
					new SqlParameter("@PageKeywords", SqlDbType.VarChar,100),
					new SqlParameter("@PageDesc", SqlDbType.VarChar,200),
					new SqlParameter("@BannerUrl", SqlDbType.VarChar,100),
                    new SqlParameter("@State", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@SortID", SqlDbType.Int,4)};
			parameters[0].Value = model.ParentID;
			parameters[1].Value = model.SortAttributeID;
			parameters[2].Value = model.SortName;
			parameters[3].Value = model.SortUrl;
            parameters[4].Value = model.SortTemplateID;
			parameters[6].Value = model.PageTitle;
			parameters[7].Value = model.PageKeywords;
			parameters[8].Value = model.PageDesc;
			parameters[9].Value = model.BannerUrl;
            parameters[10].Value = model.State;
			parameters[11].Value = model.CreateTime;
			parameters[12].Value = model.SortID;

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
		public bool Delete(int SortID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Sort ");
			strSql.Append(" where SortID=@SortID");
			SqlParameter[] parameters = {
					new SqlParameter("@SortID", SqlDbType.Int,4)
            };
			parameters[0].Value = SortID;
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
		public bool DeleteList(string SortIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Sort ");
			strSql.Append(" where SortID in ("+SortIDlist + ")  ");
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
		public YK.Model.SortInfo GetModel(int SortID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select SortID,ParentID,SortAttributeID,SortName,SortUrl,SortTemplateID,PageTitle,PageKeywords,PageDesc,BannerUrl,State,AdminID,AdminName,CreateTime from Sort ");
			strSql.Append(" where SortID=@SortID");
			SqlParameter[] parameters = {
					new SqlParameter("@SortID", SqlDbType.Int,4)
};
			parameters[0].Value = SortID;

			YK.Model.SortInfo model=new YK.Model.SortInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["SortID"]!=null && ds.Tables[0].Rows[0]["SortID"].ToString()!="")
				{
					model.SortID=int.Parse(ds.Tables[0].Rows[0]["SortID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ParentID"]!=null && ds.Tables[0].Rows[0]["ParentID"].ToString()!="")
				{
					model.ParentID=int.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SortAttributeID"]!=null && ds.Tables[0].Rows[0]["SortAttributeID"].ToString()!="")
				{
					model.SortAttributeID=int.Parse(ds.Tables[0].Rows[0]["SortAttributeID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SortName"]!=null && ds.Tables[0].Rows[0]["SortName"].ToString()!="")
				{
					model.SortName=ds.Tables[0].Rows[0]["SortName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SortUrl"]!=null && ds.Tables[0].Rows[0]["SortUrl"].ToString()!="")
				{
					model.SortUrl=ds.Tables[0].Rows[0]["SortUrl"].ToString();
				}
                if (ds.Tables[0].Rows[0]["SortTemplateID"] != null && ds.Tables[0].Rows[0]["SortTemplateID"].ToString() != "")
				{
                    model.SortTemplateID = Convert.ToInt16(ds.Tables[0].Rows[0]["SortTemplateID"].ToString());
				}				
				if(ds.Tables[0].Rows[0]["PageTitle"]!=null && ds.Tables[0].Rows[0]["PageTitle"].ToString()!="")
				{
					model.PageTitle=ds.Tables[0].Rows[0]["PageTitle"].ToString();
				}
				if(ds.Tables[0].Rows[0]["PageKeywords"]!=null && ds.Tables[0].Rows[0]["PageKeywords"].ToString()!="")
				{
					model.PageKeywords=ds.Tables[0].Rows[0]["PageKeywords"].ToString();
				}
				if(ds.Tables[0].Rows[0]["PageDesc"]!=null && ds.Tables[0].Rows[0]["PageDesc"].ToString()!="")
				{
					model.PageDesc=ds.Tables[0].Rows[0]["PageDesc"].ToString();
				}
				if(ds.Tables[0].Rows[0]["BannerUrl"]!=null && ds.Tables[0].Rows[0]["BannerUrl"].ToString()!="")
				{
					model.BannerUrl=ds.Tables[0].Rows[0]["BannerUrl"].ToString();
				}
                if (ds.Tables[0].Rows[0]["State"] != null && ds.Tables[0].Rows[0]["State"].ToString() != "")
                {
                    model.State = Convert.ToInt16(ds.Tables[0].Rows[0]["State"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AdminID"] != null && ds.Tables[0].Rows[0]["AdminID"].ToString() != "")
                {
                    model.AdminID = Convert.ToInt16(ds.Tables[0].Rows[0]["AdminID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AdminName"] != null && ds.Tables[0].Rows[0]["AdminName"].ToString() != "")
                {
                    model.AdminName = ds.Tables[0].Rows[0]["AdminName"].ToString();
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
            strSql.Append("select SortID,ParentID,SortAttributeID,SortName,SortUrl,SortTemplateID,PageTitle,PageKeywords,PageDesc,BannerUrl,State,AdminID,AdminName,CreateTime ");
			strSql.Append(" FROM Sort ");
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
			parameters[0].Value = "Sort";
			parameters[1].Value = "SortID";
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

