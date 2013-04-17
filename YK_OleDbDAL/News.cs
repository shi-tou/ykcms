using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using YK.IDAL;
using YK.DBUtility;//Please add references
namespace YK.DAL
{
	/// <summary>
	/// 数据访问类:News
	/// </summary>
	public partial class News:INews
	{
		public News()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("NewsID", "News"); 
		}


		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int NewsID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from News");
			strSql.Append(" where NewsID="+NewsID+" ");
			return DbHelperSQL.Exists(strSql.ToString());
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(YK.Model.NewsInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			StringBuilder strSql1=new StringBuilder();
			StringBuilder strSql2=new StringBuilder();
			if (model.NewsID != null)
			{
				strSql1.Append("NewsID,");
				strSql2.Append(""+model.NewsID+",");
			}
			if (model.TypeID != null)
			{
				strSql1.Append("TypeID,");
				strSql2.Append(""+model.TypeID+",");
			}
			if (model.Title != null)
			{
				strSql1.Append("Title,");
				strSql2.Append("'"+model.Title+"',");
			}
			if (model.Content != null)
			{
				strSql1.Append("Content,");
				strSql2.Append("'"+model.Content+"',");
			}
			if (model.LinkUrl != null)
			{
				strSql1.Append("LinkUrl,");
				strSql2.Append("'"+model.LinkUrl+"',");
			}
			if (model.Author != null)
			{
				strSql1.Append("Author,");
				strSql2.Append("'"+model.Author+"',");
			}
			if (model.PageTitle != null)
			{
				strSql1.Append("PageTitle,");
				strSql2.Append("'"+model.PageTitle+"',");
			}
			if (model.PageKeyword != null)
			{
				strSql1.Append("PageKeyword,");
				strSql2.Append("'"+model.PageKeyword+"',");
			}
			if (model.PageDesc != null)
			{
				strSql1.Append("PageDesc,");
				strSql2.Append("'"+model.PageDesc+"',");
			}
			if (model.State != null)
			{
				strSql1.Append("State,");
				strSql2.Append(""+model.State+",");
			}
			if (model.AdminID != null)
			{
				strSql1.Append("AdminID,");
				strSql2.Append(""+model.AdminID+",");
			}
			if (model.CreateTime != null)
			{
				strSql1.Append("CreateTime,");
				strSql2.Append("'"+model.CreateTime+"',");
			}
			strSql.Append("insert into News(");
			strSql.Append(strSql1.ToString().Remove(strSql1.Length - 1));
			strSql.Append(")");
			strSql.Append(" values (");
			strSql.Append(strSql2.ToString().Remove(strSql2.Length - 1));
			strSql.Append(")");
			DbHelperSQL.ExecuteSql(strSql.ToString());
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(YK.Model.NewsInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update News set ");
			if (model.TypeID != null)
			{
				strSql.Append("TypeID="+model.TypeID+",");
			}
			if (model.Title != null)
			{
				strSql.Append("Title='"+model.Title+"',");
			}
			if (model.Content != null)
			{
				strSql.Append("Content='"+model.Content+"',");
			}
			if (model.LinkUrl != null)
			{
				strSql.Append("LinkUrl='"+model.LinkUrl+"',");
			}
			else
			{
				strSql.Append("LinkUrl= null ,");
			}
			if (model.Author != null)
			{
				strSql.Append("Author='"+model.Author+"',");
			}
			if (model.PageTitle != null)
			{
				strSql.Append("PageTitle='"+model.PageTitle+"',");
			}
			else
			{
				strSql.Append("PageTitle= null ,");
			}
			if (model.PageKeyword != null)
			{
				strSql.Append("PageKeyword='"+model.PageKeyword+"',");
			}
			else
			{
				strSql.Append("PageKeyword= null ,");
			}
			if (model.PageDesc != null)
			{
				strSql.Append("PageDesc='"+model.PageDesc+"',");
			}
			else
			{
				strSql.Append("PageDesc= null ,");
			}
			if (model.State != null)
			{
				strSql.Append("State="+model.State+",");
			}
			if (model.AdminID != null)
			{
				strSql.Append("AdminID="+model.AdminID+",");
			}
			if (model.CreateTime != null)
			{
				strSql.Append("CreateTime='"+model.CreateTime+"',");
			}
			int n = strSql.ToString().LastIndexOf(",");
			strSql.Remove(n, 1);
			strSql.Append(" where NewsID="+ model.NewsID+" ");
			int rowsAffected=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rowsAffected > 0)
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
		public bool Delete(int NewsID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from News ");
			strSql.Append(" where NewsID="+NewsID+" " );
			int rowsAffected=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rowsAffected > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string NewsIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from News ");
			strSql.Append(" where NewsID in ("+NewsIDlist + ")  ");
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
		public YK.Model.NewsInfo GetModel(int NewsID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1  ");
			strSql.Append(" NewsID,TypeID,Title,Content,LinkUrl,Author,PageTitle,PageKeyword,PageDesc,State,AdminID,CreateTime ");
			strSql.Append(" from News ");
			strSql.Append(" where NewsID="+NewsID+" " );
			YK.Model.NewsInfo model=new YK.Model.NewsInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString());
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["NewsID"]!=null && ds.Tables[0].Rows[0]["NewsID"].ToString()!="")
				{
					model.NewsID=int.Parse(ds.Tables[0].Rows[0]["NewsID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TypeID"]!=null && ds.Tables[0].Rows[0]["TypeID"].ToString()!="")
				{
					model.TypeID=int.Parse(ds.Tables[0].Rows[0]["TypeID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Title"]!=null && ds.Tables[0].Rows[0]["Title"].ToString()!="")
				{
					model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Content"]!=null && ds.Tables[0].Rows[0]["Content"].ToString()!="")
				{
					model.Content=ds.Tables[0].Rows[0]["Content"].ToString();
				}
				if(ds.Tables[0].Rows[0]["LinkUrl"]!=null && ds.Tables[0].Rows[0]["LinkUrl"].ToString()!="")
				{
					model.LinkUrl=ds.Tables[0].Rows[0]["LinkUrl"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Author"]!=null && ds.Tables[0].Rows[0]["Author"].ToString()!="")
				{
					model.Author=ds.Tables[0].Rows[0]["Author"].ToString();
				}
				if(ds.Tables[0].Rows[0]["PageTitle"]!=null && ds.Tables[0].Rows[0]["PageTitle"].ToString()!="")
				{
					model.PageTitle=ds.Tables[0].Rows[0]["PageTitle"].ToString();
				}
				if(ds.Tables[0].Rows[0]["PageKeyword"]!=null && ds.Tables[0].Rows[0]["PageKeyword"].ToString()!="")
				{
					model.PageKeyword=ds.Tables[0].Rows[0]["PageKeyword"].ToString();
				}
				if(ds.Tables[0].Rows[0]["PageDesc"]!=null && ds.Tables[0].Rows[0]["PageDesc"].ToString()!="")
				{
					model.PageDesc=ds.Tables[0].Rows[0]["PageDesc"].ToString();
				}
				if(ds.Tables[0].Rows[0]["State"]!=null && ds.Tables[0].Rows[0]["State"].ToString()!="")
				{
					model.State=int.Parse(ds.Tables[0].Rows[0]["State"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AdminID"]!=null && ds.Tables[0].Rows[0]["AdminID"].ToString()!="")
				{
					model.AdminID=int.Parse(ds.Tables[0].Rows[0]["AdminID"].ToString());
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
			strSql.Append("select NewsID,TypeID,Title,Content,LinkUrl,Author,PageTitle,PageKeyword,PageDesc,State,AdminID,CreateTime ");
			strSql.Append(" FROM News ");
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
			strSql.Append(" NewsID,TypeID,Title,Content,LinkUrl,Author,PageTitle,PageKeyword,PageDesc,State,AdminID,CreateTime ");
			strSql.Append(" FROM News ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		*/

		#endregion  Method
	}
}

