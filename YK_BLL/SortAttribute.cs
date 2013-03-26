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
	/// 文件描述：栏目属性业务层
    /// 创建人：杨良斌
    /// 时间：2012-11-29
	/// </summary>
	public partial class SortAttribute
	{
		private readonly ISortAttribute dal=DataAccess.CreateSortAttribute();
		public SortAttribute()
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
		public bool Exists(int SortAttributeID)
		{
			return dal.Exists(SortAttributeID);
		}
        /// <summary>
        /// 是否存在指定名称的记录
        /// </summary>
        public bool Exists(string SortAttributeName, int SortAttribute)
        {
            return dal.Exists(SortAttributeName, SortAttribute);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(YK.Model.SortAttributeInfo model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(YK.Model.SortAttributeInfo model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int SortAttributeID)
		{
			
			return dal.Delete(SortAttributeID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string SortAttributeIDlist )
		{
			return dal.DeleteList(SortAttributeIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public YK.Model.SortAttributeInfo GetModel(int SortAttributeID)
		{
			
			return dal.GetModel(SortAttributeID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public YK.Model.SortAttributeInfo GetModelByCache(int SortAttributeID)
		{
			
			string CacheKey = "SortAttributeInfoModel-" + SortAttributeID;
			object objModel = YK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(SortAttributeID);
					if (objModel != null)
					{
						int ModelCache = YK.Common.ConfigHelper.GetConfigInt("ModelCache");
						YK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (YK.Model.SortAttributeInfo)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<YK.Model.SortAttributeInfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<YK.Model.SortAttributeInfo> DataTableToList(DataTable dt)
		{
			List<YK.Model.SortAttributeInfo> modelList = new List<YK.Model.SortAttributeInfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				YK.Model.SortAttributeInfo model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new YK.Model.SortAttributeInfo();
					if(dt.Rows[n]["SortAttributeID"]!=null && dt.Rows[n]["SortAttributeID"].ToString()!="")
					{
						model.SortAttributeID=int.Parse(dt.Rows[n]["SortAttributeID"].ToString());
					}
					if(dt.Rows[n]["SortAttributeName"]!=null && dt.Rows[n]["SortAttributeName"].ToString()!="")
					{
					model.SortAttributeName=dt.Rows[n]["SortAttributeName"].ToString();
					}
					if(dt.Rows[n]["SortAttributeDesc"]!=null && dt.Rows[n]["SortAttributeDesc"].ToString()!="")
					{
					model.SortAttributeDesc=dt.Rows[n]["SortAttributeDesc"].ToString();
					}					
					if(dt.Rows[n]["AdminID"]!=null && dt.Rows[n]["AdminID"].ToString()!="")
					{
						model.AdminID=int.Parse(dt.Rows[n]["AdminID"].ToString());
					}
					if(dt.Rows[n]["AdminName"]!=null && dt.Rows[n]["AdminName"].ToString()!="")
					{
					model.AdminName=dt.Rows[n]["AdminName"].ToString();
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

