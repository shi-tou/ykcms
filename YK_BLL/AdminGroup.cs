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
	/// AdminGroup
	/// </summary>
	public partial class AdminGroup
	{
		private readonly IAdminGroup dal=DataAccess.CreateAdminGroup();
		public AdminGroup()
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
		public bool Exists(int GroupID)
		{
			return dal.Exists(GroupID);
		}
        /// <summary>
        /// 是否存在指定名称的记录
        /// </summary>
        public bool Exists(string GroupName,int GroupID)
        {
            return dal.Exists(GroupName, GroupID);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(YK.Model.AdminGroupInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(YK.Model.AdminGroupInfo model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int GroupID)
		{
			
			return dal.Delete(GroupID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string GroupIDlist )
		{
			return dal.DeleteList(GroupIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public YK.Model.AdminGroupInfo GetModel(int GroupID)
		{
			
			return dal.GetModel(GroupID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public YK.Model.AdminGroupInfo GetModelByCache(int GroupID)
		{
			
			string CacheKey = "AdminGroupInfoModel-" + GroupID;
            object objModel = YK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(GroupID);
					if (objModel != null)
					{
                        int ModelCache = YK.Common.ConfigHelper.GetConfigInt("ModelCache");
                        YK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (YK.Model.AdminGroupInfo)objModel;
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
		public List<YK.Model.AdminGroupInfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<YK.Model.AdminGroupInfo> DataTableToList(DataTable dt)
		{
            List<YK.Model.AdminGroupInfo> modelList = new List<YK.Model.AdminGroupInfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
                YK.Model.AdminGroupInfo model;
				for (int n = 0; n < rowsCount; n++)
				{
                    model = new YK.Model.AdminGroupInfo();
					if(dt.Rows[n]["GroupID"]!=null && dt.Rows[n]["GroupID"].ToString()!="")
					{
						model.GroupID=int.Parse(dt.Rows[n]["GroupID"].ToString());
					}
					if(dt.Rows[n]["GroupName"]!=null && dt.Rows[n]["GroupName"].ToString()!="")
					{
					model.GroupName=dt.Rows[n]["GroupName"].ToString();
					}
					if(dt.Rows[n]["Describe"]!=null && dt.Rows[n]["Describe"].ToString()!="")
					{
					model.Describe=dt.Rows[n]["Describe"].ToString();
					}
					if(dt.Rows[n]["GroupAuth"]!=null && dt.Rows[n]["GroupAuth"].ToString()!="")
					{
					model.GroupAuth=dt.Rows[n]["GroupAuth"].ToString();
					}
					if(dt.Rows[n]["CreateTime"]!=null && dt.Rows[n]["CreateTime"].ToString()!="")
					{
						model.CreateTime=DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
					}
					if(dt.Rows[n]["AdminID"]!=null && dt.Rows[n]["AdminID"].ToString()!="")
					{
						model.AdminID=int.Parse(dt.Rows[n]["AdminID"].ToString());
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

