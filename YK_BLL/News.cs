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
	/// News
	/// </summary>
	public partial class News
	{
		private readonly INews dal=DataAccess.CreateNews();
		public News()
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
		public bool Exists(int NewsID)
		{
			return dal.Exists(NewsID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(YK.Model.NewsInfo model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(YK.Model.NewsInfo model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int NewsID)
		{
			
			return dal.Delete(NewsID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string NewsIDlist )
		{
			return dal.DeleteList(NewsIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public YK.Model.NewsInfo GetModel(int NewsID)
		{
			
			return dal.GetModel(NewsID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public YK.Model.NewsInfo GetModelByCache(int NewsID)
		{
			
			string CacheKey = "NewsInfoModel-" + NewsID;
			object objModel = YK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(NewsID);
					if (objModel != null)
					{
						int ModelCache = YK.Common.ConfigHelper.GetConfigInt("ModelCache");
						YK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (YK.Model.NewsInfo)objModel;
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
		public List<YK.Model.NewsInfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<YK.Model.NewsInfo> DataTableToList(DataTable dt)
		{
			List<YK.Model.NewsInfo> modelList = new List<YK.Model.NewsInfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				YK.Model.NewsInfo model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new YK.Model.NewsInfo();
					if(dt.Rows[n]["NewsID"]!=null && dt.Rows[n]["NewsID"].ToString()!="")
					{
						model.NewsID=int.Parse(dt.Rows[n]["NewsID"].ToString());
					}
					if(dt.Rows[n]["TypeID"]!=null && dt.Rows[n]["TypeID"].ToString()!="")
					{
						model.TypeID=int.Parse(dt.Rows[n]["TypeID"].ToString());
					}
					if(dt.Rows[n]["Title"]!=null && dt.Rows[n]["Title"].ToString()!="")
					{
					model.Title=dt.Rows[n]["Title"].ToString();
					}
					if(dt.Rows[n]["Content"]!=null && dt.Rows[n]["Content"].ToString()!="")
					{
					model.Content=dt.Rows[n]["Content"].ToString();
					}
					if(dt.Rows[n]["LinkUrl"]!=null && dt.Rows[n]["LinkUrl"].ToString()!="")
					{
					model.LinkUrl=dt.Rows[n]["LinkUrl"].ToString();
					}
					if(dt.Rows[n]["Author"]!=null && dt.Rows[n]["Author"].ToString()!="")
					{
					model.Author=dt.Rows[n]["Author"].ToString();
					}
					if(dt.Rows[n]["PageTitle"]!=null && dt.Rows[n]["PageTitle"].ToString()!="")
					{
					model.PageTitle=dt.Rows[n]["PageTitle"].ToString();
					}
					if(dt.Rows[n]["PageKeyword"]!=null && dt.Rows[n]["PageKeyword"].ToString()!="")
					{
					model.PageKeyword=dt.Rows[n]["PageKeyword"].ToString();
					}
					if(dt.Rows[n]["PageDesc"]!=null && dt.Rows[n]["PageDesc"].ToString()!="")
					{
					model.PageDesc=dt.Rows[n]["PageDesc"].ToString();
					}
					if(dt.Rows[n]["State"]!=null && dt.Rows[n]["State"].ToString()!="")
					{
						model.State=int.Parse(dt.Rows[n]["State"].ToString());
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
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method
	}
}

