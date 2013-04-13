using System;
using System.Data;
using System.Collections.Generic;
using YK.Common;
using YK.Model;
using YK.DALFactory;
using YK.IDAL;
namespace YK.BLL
{
	/// <summary>
	/// Goods
	/// </summary>
	public partial class Goods
	{
		private readonly IGoods dal=DataAccess.CreateGoods();
		public Goods()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int GoodsID)
		{
			return dal.Exists(GoodsID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(YK.Model.GoodsInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(YK.Model.GoodsInfo model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int GoodsID)
		{
			
			return dal.Delete(GoodsID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string GoodsIDlist )
		{
			return dal.DeleteList(GoodsIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public YK.Model.GoodsInfo GetModel(int GoodsID)
		{
			
			return dal.GetModel(GoodsID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public YK.Model.GoodsInfo GetModelByCache(int GoodsID)
		{
			
			string CacheKey = "GoodsInfoModel-" + GoodsID;
			object objModel = YK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(GoodsID);
					if (objModel != null)
					{
						int ModelCache = YK.Common.ConfigHelper.GetConfigInt("ModelCache");
						YK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (YK.Model.GoodsInfo)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<YK.Model.GoodsInfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<YK.Model.GoodsInfo> DataTableToList(DataTable dt)
		{
			List<YK.Model.GoodsInfo> modelList = new List<YK.Model.GoodsInfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				YK.Model.GoodsInfo model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new YK.Model.GoodsInfo();
					if(dt.Rows[n]["GoodsID"]!=null && dt.Rows[n]["GoodsID"].ToString()!="")
					{
						model.GoodsID=int.Parse(dt.Rows[n]["GoodsID"].ToString());
					}
					if(dt.Rows[n]["GoodsName"]!=null && dt.Rows[n]["GoodsName"].ToString()!="")
					{
					model.GoodsName=dt.Rows[n]["GoodsName"].ToString();
					}
					if(dt.Rows[n]["TypeID"]!=null && dt.Rows[n]["TypeID"].ToString()!="")
					{
						model.TypeID=int.Parse(dt.Rows[n]["TypeID"].ToString());
					}
					if(dt.Rows[n]["Unit"]!=null && dt.Rows[n]["Unit"].ToString()!="")
					{
					model.Unit=dt.Rows[n]["Unit"].ToString();
					}
					if(dt.Rows[n]["Discount"]!=null && dt.Rows[n]["Discount"].ToString()!="")
					{
						model.Discount=decimal.Parse(dt.Rows[n]["Discount"].ToString());
					}
					if(dt.Rows[n]["Price"]!=null && dt.Rows[n]["Price"].ToString()!="")
					{
						model.Price=decimal.Parse(dt.Rows[n]["Price"].ToString());
					}
					if(dt.Rows[n]["Count"]!=null && dt.Rows[n]["Count"].ToString()!="")
					{
						model.Count=int.Parse(dt.Rows[n]["Count"].ToString());
					}
					if(dt.Rows[n]["Describe"]!=null && dt.Rows[n]["Describe"].ToString()!="")
					{
					model.Describe=dt.Rows[n]["Describe"].ToString();
					}
					if(dt.Rows[n]["Image"]!=null && dt.Rows[n]["Image"].ToString()!="")
					{
					model.Image=dt.Rows[n]["Image"].ToString();
					}
					if(dt.Rows[n]["AdminID"]!=null && dt.Rows[n]["AdminID"].ToString()!="")
					{
						model.AdminID=int.Parse(dt.Rows[n]["AdminID"].ToString());
					}
					if(dt.Rows[n]["CreateTime"]!=null && dt.Rows[n]["CreateTime"].ToString()!="")
					{
						model.CreateTime=DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method
	}
}

