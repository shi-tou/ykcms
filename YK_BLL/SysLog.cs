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
    /// 描述:日志管理--业务层
    /// 作者:杨良斌
    /// 时间:2012-08-15
    /// </summary>
	public partial class SysLog
	{
		private readonly ISysLog dal=DataAccess.CreateSysLog();
		public SysLog()
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
		public bool Exists(int SysLogID)
		{
			return dal.Exists(SysLogID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public void Add(YK.Model.SysLogInfo model)
		{
			dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(YK.Model.SysLogInfo model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int SysLogID)
		{
			
			return dal.Delete(SysLogID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string SysLogIDlist )
		{
			return dal.DeleteList(SysLogIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public YK.Model.SysLogInfo GetModel(int SysLogID)
		{
			
			return dal.GetModel(SysLogID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
        public YK.Model.SysLogInfo GetModelByCache(int SysLogID)
		{
			
			string CacheKey = "SysLogModel-" + SysLogID;
			object objModel = YK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(SysLogID);
					if (objModel != null)
					{
						int ModelCache = YK.Common.ConfigHelper.GetConfigInt("ModelCache");
						YK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
            return (YK.Model.SysLogInfo)objModel;
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
        public List<YK.Model.SysLogInfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<YK.Model.SysLogInfo> DataTableToList(DataTable dt)
		{
            List<YK.Model.SysLogInfo> modelList = new List<YK.Model.SysLogInfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
                YK.Model.SysLogInfo model;
				for (int n = 0; n < rowsCount; n++)
				{
                    model = new YK.Model.SysLogInfo();
					if(dt.Rows[n]["SysLogID"]!=null && dt.Rows[n]["SysLogID"].ToString()!="")
					{
						model.SysLogID=int.Parse(dt.Rows[n]["SysLogID"].ToString());
					}
					if(dt.Rows[n]["Area"]!=null && dt.Rows[n]["Area"].ToString()!="")
					{
					model.Area=dt.Rows[n]["Area"].ToString();
					}
					if(dt.Rows[n]["Type"]!=null && dt.Rows[n]["Type"].ToString()!="")
					{
					model.Type=dt.Rows[n]["Type"].ToString();
					}
					if(dt.Rows[n]["Detail"]!=null && dt.Rows[n]["Detail"].ToString()!="")
					{
					model.Detail=dt.Rows[n]["Detail"].ToString();
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
        public DataSet GetList(string TableName, string KeyField, int PageSize, int PageIndex, string strWhere)
        {
            return dal.GetList(TableName, KeyField, PageSize, PageIndex, strWhere);
        }

		#endregion  Method
	}
}

