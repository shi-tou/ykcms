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
	/// Admin
	/// </summary>
	public partial class Admin
	{
		private readonly IAdmin dal=DataAccess.CreateAdmin();
		public Admin()
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
		public bool Exists(int AdminID)
		{
			return dal.Exists(AdminID);
		}
        /// <summary>
        /// 是否存在指定帐号的记录
        /// </summary>
        public bool Exists(string AdminAccount, int AdminID)
        {
            return dal.Exists(AdminAccount, AdminID);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(YK.Model.AdminInfo model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(YK.Model.AdminInfo model)
		{
			return dal.Update(model);
		}
        public bool UpdateIPAndTime(string ip, DateTime dateTime, int adminid)
        {
            return dal.UpdateIPAndTime(ip, dateTime, adminid);
        }
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int AdminID)
		{
			
			return dal.Delete(AdminID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string AdminIDlist )
		{
			return dal.DeleteList(AdminIDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public YK.Model.AdminInfo GetModel(int AdminID)
		{
			return dal.GetModel(AdminID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public YK.Model.AdminInfo GetModelByCache(int AdminID)
		{			
			string CacheKey = "AdminInfoModel-" + AdminID;
			object objModel = YK.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(AdminID);
					if (objModel != null)
					{
						int ModelCache = YK.Common.ConfigHelper.GetConfigInt("ModelCache");
						YK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (YK.Model.AdminInfo)objModel;
		}
        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public YK.Model.AdminInfo GetModelByAccount(string account)
        {
            return dal.GetModelByAccount(account);
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
		public List<YK.Model.AdminInfo> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<YK.Model.AdminInfo> DataTableToList(DataTable dt)
		{
			List<YK.Model.AdminInfo> modelList = new List<YK.Model.AdminInfo>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				YK.Model.AdminInfo model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new YK.Model.AdminInfo();
					if(dt.Rows[n]["AdminID"]!=null && dt.Rows[n]["AdminID"].ToString()!="")
					{
						model.AdminID=int.Parse(dt.Rows[n]["AdminID"].ToString());
					}
					if(dt.Rows[n]["GroupID"]!=null && dt.Rows[n]["GroupID"].ToString()!="")
					{
						model.GroupID=int.Parse(dt.Rows[n]["GroupID"].ToString());
					}
					if(dt.Rows[n]["AdminAccount"]!=null && dt.Rows[n]["AdminAccount"].ToString()!="")
					{
					model.AdminAccount=dt.Rows[n]["AdminAccount"].ToString();
					}
					if(dt.Rows[n]["AdminPwd"]!=null && dt.Rows[n]["AdminPwd"].ToString()!="")
					{
					model.AdminPwd=dt.Rows[n]["AdminPwd"].ToString();
					}
					if(dt.Rows[n]["AdminName"]!=null && dt.Rows[n]["AdminName"].ToString()!="")
					{
					model.AdminName=dt.Rows[n]["AdminName"].ToString();
					}
					if(dt.Rows[n]["State"]!=null && dt.Rows[n]["State"].ToString()!="")
					{
						model.State=int.Parse(dt.Rows[n]["State"].ToString());
					}
					if(dt.Rows[n]["AdminDesc"]!=null && dt.Rows[n]["AdminDesc"].ToString()!="")
					{
					model.AdminDesc=dt.Rows[n]["AdminDesc"].ToString();
					}
					if(dt.Rows[n]["LastLoginIP"]!=null && dt.Rows[n]["LastLoginIP"].ToString()!="")
					{
					model.LastLoginIP=dt.Rows[n]["LastLoginIP"].ToString();
					}
					if(dt.Rows[n]["LastLoginTime"]!=null && dt.Rows[n]["LastLoginTime"].ToString()!="")
					{
						model.LastLoginTime=DateTime.Parse(dt.Rows[n]["LastLoginTime"].ToString());
					}
					if(dt.Rows[n]["CreatetTime"]!=null && dt.Rows[n]["CreatetTime"].ToString()!="")
					{
						model.CreateTime=DateTime.Parse(dt.Rows[n]["CreatetTime"].ToString());
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

