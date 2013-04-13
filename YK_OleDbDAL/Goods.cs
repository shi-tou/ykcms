using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using YK.IDAL;
using YK.DBUtility;//Please add references
namespace YK.SQLServerDAL
{
	/// <summary>
	/// 数据访问类:Goods
	/// </summary>
	public partial class Goods:IGoods
	{
		public Goods()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("GoodsID", "Goods"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int GoodsID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Goods");
			strSql.Append(" where GoodsID=@GoodsID");
			SqlParameter[] parameters = {
					new SqlParameter("@GoodsID", SqlDbType.Int,4)
			};
			parameters[0].Value = GoodsID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(YK.Model.GoodsInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Goods(");
			strSql.Append("GoodsName,TypeID,Unit,Discount,Price,Count,Describe,Image,AdminID,CreateTime)");
			strSql.Append(" values (");
			strSql.Append("@GoodsName,@TypeID,@Unit,@Discount,@Price,@Count,@Describe,@Image,@AdminID,@CreateTime)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@GoodsName", SqlDbType.VarChar,100),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@Unit", SqlDbType.VarChar,50),
					new SqlParameter("@Discount", SqlDbType.Float,8),
					new SqlParameter("@Price", SqlDbType.Money,8),
					new SqlParameter("@Count", SqlDbType.Int,4),
					new SqlParameter("@Describe", SqlDbType.VarChar,200),
					new SqlParameter("@Image", SqlDbType.VarChar,100),
					new SqlParameter("@AdminID", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime)};
			parameters[0].Value = model.GoodsName;
			parameters[1].Value = model.TypeID;
			parameters[2].Value = model.Unit;
			parameters[3].Value = model.Discount;
			parameters[4].Value = model.Price;
			parameters[5].Value = model.Count;
			parameters[6].Value = model.Describe;
			parameters[7].Value = model.Image;
			parameters[8].Value = model.AdminID;
			parameters[9].Value = model.CreateTime;

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
		public bool Update(YK.Model.GoodsInfo model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Goods set ");
			strSql.Append("GoodsName=@GoodsName,");
			strSql.Append("TypeID=@TypeID,");
			strSql.Append("Unit=@Unit,");
			strSql.Append("Discount=@Discount,");
			strSql.Append("Price=@Price,");
			strSql.Append("Count=@Count,");
			strSql.Append("Describe=@Describe,");
			strSql.Append("Image=@Image,");
			strSql.Append("AdminID=@AdminID,");
			strSql.Append("CreateTime=@CreateTime");
			strSql.Append(" where GoodsID=@GoodsID");
			SqlParameter[] parameters = {
					new SqlParameter("@GoodsName", SqlDbType.VarChar,100),
					new SqlParameter("@TypeID", SqlDbType.Int,4),
					new SqlParameter("@Unit", SqlDbType.VarChar,50),
					new SqlParameter("@Discount", SqlDbType.Float,8),
					new SqlParameter("@Price", SqlDbType.Money,8),
					new SqlParameter("@Count", SqlDbType.Int,4),
					new SqlParameter("@Describe", SqlDbType.VarChar,200),
					new SqlParameter("@Image", SqlDbType.VarChar,100),
					new SqlParameter("@AdminID", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@GoodsID", SqlDbType.Int,4)};
			parameters[0].Value = model.GoodsName;
			parameters[1].Value = model.TypeID;
			parameters[2].Value = model.Unit;
			parameters[3].Value = model.Discount;
			parameters[4].Value = model.Price;
			parameters[5].Value = model.Count;
			parameters[6].Value = model.Describe;
			parameters[7].Value = model.Image;
			parameters[8].Value = model.AdminID;
			parameters[9].Value = model.CreateTime;
			parameters[10].Value = model.GoodsID;

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
		public bool Delete(int GoodsID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Goods ");
			strSql.Append(" where GoodsID=@GoodsID");
			SqlParameter[] parameters = {
					new SqlParameter("@GoodsID", SqlDbType.Int,4)
			};
			parameters[0].Value = GoodsID;

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
		public bool DeleteList(string GoodsIDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from Goods ");
			strSql.Append(" where GoodsID in ("+GoodsIDlist + ")  ");
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
		public YK.Model.GoodsInfo GetModel(int GoodsID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 GoodsID,GoodsName,TypeID,Unit,Discount,Price,Count,Describe,Image,AdminID,CreateTime from Goods ");
			strSql.Append(" where GoodsID=@GoodsID");
			SqlParameter[] parameters = {
					new SqlParameter("@GoodsID", SqlDbType.Int,4)
			};
			parameters[0].Value = GoodsID;

			YK.Model.GoodsInfo model=new YK.Model.GoodsInfo();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["GoodsID"]!=null && ds.Tables[0].Rows[0]["GoodsID"].ToString()!="")
				{
					model.GoodsID=int.Parse(ds.Tables[0].Rows[0]["GoodsID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["GoodsName"]!=null && ds.Tables[0].Rows[0]["GoodsName"].ToString()!="")
				{
					model.GoodsName=ds.Tables[0].Rows[0]["GoodsName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["TypeID"]!=null && ds.Tables[0].Rows[0]["TypeID"].ToString()!="")
				{
					model.TypeID=int.Parse(ds.Tables[0].Rows[0]["TypeID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Unit"]!=null && ds.Tables[0].Rows[0]["Unit"].ToString()!="")
				{
					model.Unit=ds.Tables[0].Rows[0]["Unit"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Discount"]!=null && ds.Tables[0].Rows[0]["Discount"].ToString()!="")
				{
					model.Discount=decimal.Parse(ds.Tables[0].Rows[0]["Discount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Price"]!=null && ds.Tables[0].Rows[0]["Price"].ToString()!="")
				{
					model.Price=decimal.Parse(ds.Tables[0].Rows[0]["Price"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Count"]!=null && ds.Tables[0].Rows[0]["Count"].ToString()!="")
				{
					model.Count=int.Parse(ds.Tables[0].Rows[0]["Count"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Describe"]!=null && ds.Tables[0].Rows[0]["Describe"].ToString()!="")
				{
					model.Describe=ds.Tables[0].Rows[0]["Describe"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Image"]!=null && ds.Tables[0].Rows[0]["Image"].ToString()!="")
				{
					model.Image=ds.Tables[0].Rows[0]["Image"].ToString();
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
			strSql.Append("select GoodsID,GoodsName,TypeID,Unit,Discount,Price,Count,Describe,Image,AdminID,CreateTime ");
			strSql.Append(" FROM Goods ");
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
			strSql.Append(" GoodsID,GoodsName,TypeID,Unit,Discount,Price,Count,Describe,Image,AdminID,CreateTime ");
			strSql.Append(" FROM Goods ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM Goods ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
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
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.GoodsID desc");
			}
			strSql.Append(")AS Row, T.*  from Goods T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
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
			parameters[0].Value = "Goods";
			parameters[1].Value = "GoodsID";
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

