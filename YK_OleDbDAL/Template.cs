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
	public partial class Template:ITemplate
	{
        public Template()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
            return DbHelperSQL.GetMaxID("TemplateID", "Template"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int TemplateID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Template");
			strSql.Append(" where TemplateID=@TemplateID");
            SqlParameter[] parameters = {
					new SqlParameter("@TemplateID", SqlDbType.Int,4)
};
			parameters[0].Value = TemplateID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
		}
        /// <summary>
        /// 是否存在指定名称的记录
        /// </summary>
        public bool Exists(string TemplateName, int TemplateID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Template");
            strSql.Append(" where TemplateName=@TemplateName and TemplateID <> @TemplateID");
            SqlParameter[] parameters = {
					new SqlParameter("@TemplateName", SqlDbType.VarChar,50),
                    new SqlParameter("@TemplateID", SqlDbType.Int,4)
            };
            parameters[0].Value = TemplateName;
            parameters[1].Value = TemplateID;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(YK.Model.TemplateInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Template(");
            strSql.Append("TemplateName,TemplateUrl,TemplateDesc,TemplateSource,AdminID,AdminName,CreateTime)");
			strSql.Append(" values (");
            strSql.Append("@TemplateName,@TemplateUrl,@TemplateDesc,@TemplateSource,@AdminID,@AdminName,@CreateTime)");
			SqlParameter[] parameters = {
					new SqlParameter("@TemplateName", SqlDbType.VarChar,50),
                    new SqlParameter("@TemplateUrl", SqlDbType.VarChar,50),
					new SqlParameter("@TemplateDesc", SqlDbType.VarChar,200),
                    new SqlParameter("@TemplateSource", SqlDbType.Text),
					new SqlParameter("@AdminID", SqlDbType.Int,4),
					new SqlParameter("@AdminName", SqlDbType.VarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)};
			parameters[0].Value = model.TemplateName;
            parameters[1].Value = model.TemplateUrl;
			parameters[2].Value = model.TemplateDesc;
            parameters[3].Value = model.TemplateSource;
			parameters[4].Value = model.AdminID;
			parameters[5].Value = model.AdminName;
			parameters[6].Value = model.CreateTime;

			DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(YK.Model.TemplateInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Template set ");
			strSql.Append("TemplateName=@TemplateName,");
            strSql.Append("TemplateUrl=@TemplateUrl,");
			strSql.Append("TemplateDesc=@TemplateDesc,");
            strSql.Append("TemplateSource=@TemplateSource,");
			strSql.Append("AdminID=@AdminID,");
			strSql.Append("AdminName=@AdminName,");
			strSql.Append("CreateTime=@CreateTime");
			strSql.Append(" where TemplateID=@TemplateID");
			SqlParameter[] parameters = {
					new SqlParameter("@TemplateName", SqlDbType.VarChar,50),
                    new SqlParameter("@TemplateUrl", SqlDbType.VarChar,50),
					new SqlParameter("@TemplateDesc", SqlDbType.VarChar,200),
                    new SqlParameter("@TemplateSource", SqlDbType.Text),
					new SqlParameter("@AdminID", SqlDbType.Int,4),
					new SqlParameter("@AdminName", SqlDbType.VarChar,50),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@TemplateID", SqlDbType.Int,4)};
			parameters[0].Value = model.TemplateName;
            parameters[1].Value = model.TemplateUrl;
			parameters[2].Value = model.TemplateDesc;
            parameters[3].Value = model.TemplateSource;
			parameters[4].Value = model.AdminID;
			parameters[5].Value = model.AdminName;
			parameters[6].Value = model.CreateTime;
			parameters[7].Value = model.TemplateID;

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
		public bool Delete(int TemplateID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Template ");
			strSql.Append(" where TemplateID=@TemplateID");
			SqlParameter[] parameters = {
					new SqlParameter("@TemplateID", SqlDbType.Int,4)
};
			parameters[0].Value = TemplateID;

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
		public bool DeleteList(string TemplateIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Template ");
			strSql.Append(" where TemplateID in ("+TemplateIDlist + ")  ");
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
		public YK.Model.TemplateInfo GetModel(int TemplateID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select TemplateID,TemplateName,TemplateUrl,TemplateDesc,TemplateSource,AdminID,AdminName,CreateTime from Template ");
			strSql.Append(" where TemplateID=@TemplateID");
			SqlParameter[] parameters = {
					new SqlParameter("@TemplateID", SqlDbType.Int,4)
};
			parameters[0].Value = TemplateID;

			YK.Model.TemplateInfo model=new YK.Model.TemplateInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["TemplateID"]!=null && ds.Tables[0].Rows[0]["TemplateID"].ToString()!="")
				{
					model.TemplateID=int.Parse(ds.Tables[0].Rows[0]["TemplateID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TemplateName"]!=null && ds.Tables[0].Rows[0]["TemplateName"].ToString()!="")
				{
					model.TemplateName=ds.Tables[0].Rows[0]["TemplateName"].ToString();
				}
                if (ds.Tables[0].Rows[0]["TemplateUrl"] != null && ds.Tables[0].Rows[0]["TemplateUrl"].ToString() != "")
                {
                    model.TemplateUrl = ds.Tables[0].Rows[0]["TemplateUrl"].ToString();
                }
				if(ds.Tables[0].Rows[0]["TemplateDesc"]!=null && ds.Tables[0].Rows[0]["TemplateDesc"].ToString()!="")
				{
					model.TemplateDesc=ds.Tables[0].Rows[0]["TemplateDesc"].ToString();
				}
                if (ds.Tables[0].Rows[0]["TemplateSource"] != null && ds.Tables[0].Rows[0]["TemplateSource"].ToString() != "")
                {
                    model.TemplateSource = ds.Tables[0].Rows[0]["TemplateSource"].ToString();
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
            strSql.Append("select TemplateID,TemplateName,TemplateUrl,TemplateDesc,TemplateSource,AdminID,AdminName,CreateTime ");
			strSql.Append(" FROM Template ");
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
			parameters[0].Value = "Template";
			parameters[1].Value = "TemplateID";
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

