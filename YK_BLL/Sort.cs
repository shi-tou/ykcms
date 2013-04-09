using System;
using System.Data;
using System.Collections.Generic;
using YK.Common;
using YK.Model;
using YK.DALFactory;
using YK.IDAL;
using YK.DBUtility;
namespace YK.BLL
{
    /// <summary>
    /// 描述:栏目管理--业务层
    /// 作者:杨良斌
    /// 时间:2012-08-15
    /// </summary>
    public partial class Sort
    {
        private readonly ISort dal = DataAccess.CreateSort();
        public Sort()
        { }
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
        public bool Exists(int SortID)
        {
            return dal.Exists(SortID);
        }
        /// <summary>
        /// 是否存在指定名称的记录
        /// </summary>
        public bool Exists(string SortName, int SortID)
        {
            return dal.Exists(SortName, SortID);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(YK.Model.SortInfo model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(YK.Model.SortInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int SortID)
        {

            return dal.Delete(SortID);
        }
        /// <summary>
        /// 批量删除数据(根据ID列表)
        /// </summary>
        public bool DeleteList(string SortIDlist)
        {
            return dal.DeleteList(SortIDlist);
        }
        /// <summary>
        /// 批量删除数据(其子类也将删除)
        /// </summary>
        public bool DeleteAllSort(string SortIDlist)
        {
            return CommonDbHelper.DeleteAllChilds("Sort", "SortID", "ParentID", "SortID in (" + SortIDlist + ")");
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public YK.Model.SortInfo GetModel(int SortID)
        {
            return dal.GetModel(SortID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public YK.Model.SortInfo GetModelByCache(int SortID)
        {

            string CacheKey = "SortInfoModel-" + SortID;
            object objModel = YK.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(SortID);
                    if (objModel != null)
                    {
                        int ModelCache = YK.Common.ConfigHelper.GetConfigInt("ModelCache");
                        YK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (YK.Model.SortInfo)objModel;
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
        public List<YK.Model.SortInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<YK.Model.SortInfo> DataTableToList(DataTable dt)
        {
            List<YK.Model.SortInfo> modelList = new List<YK.Model.SortInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                YK.Model.SortInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new YK.Model.SortInfo();
                    if (dt.Rows[n]["SortID"] != null && dt.Rows[n]["SortID"].ToString() != "")
                    {
                        model.SortID = int.Parse(dt.Rows[n]["SortID"].ToString());
                    }
                    if (dt.Rows[n]["ParentID"] != null && dt.Rows[n]["ParentID"].ToString() != "")
                    {
                        model.ParentID = int.Parse(dt.Rows[n]["ParentID"].ToString());
                    }
                    if (dt.Rows[n]["SortAttributeID"] != null && dt.Rows[n]["SortAttributeID"].ToString() != "")
                    {
                        model.SortAttributeID = int.Parse(dt.Rows[n]["SortAttributeID"].ToString());
                    }
                    if (dt.Rows[n]["SortName"] != null && dt.Rows[n]["SortName"].ToString() != "")
                    {
                        model.SortName = dt.Rows[n]["SortName"].ToString();
                    }
                    if (dt.Rows[n]["SortUrl"] != null && dt.Rows[n]["SortUrl"].ToString() != "")
                    {
                        model.SortUrl = dt.Rows[n]["SortUrl"].ToString();
                    }
                    if (dt.Rows[n]["SortTemplateID"] != null && dt.Rows[n]["SortTemplateID"].ToString() != "")
                    {
                        model.SortTemplateID = Convert.ToInt16(dt.Rows[n]["SortTemplateID"].ToString());
                    }                   
                    if (dt.Rows[n]["PageTitle"] != null && dt.Rows[n]["PageTitle"].ToString() != "")
                    {
                        model.PageTitle = dt.Rows[n]["PageTitle"].ToString();
                    }
                    if (dt.Rows[n]["PageKeywords"] != null && dt.Rows[n]["PageKeywords"].ToString() != "")
                    {
                        model.PageKeywords = dt.Rows[n]["PageKeywords"].ToString();
                    }
                    if (dt.Rows[n]["PageDesc"] != null && dt.Rows[n]["PageDesc"].ToString() != "")
                    {
                        model.PageDesc = dt.Rows[n]["PageDesc"].ToString();
                    }
                    if (dt.Rows[n]["BannerUrl"] != null && dt.Rows[n]["BannerUrl"].ToString() != "")
                    {
                        model.BannerUrl = dt.Rows[n]["BannerUrl"].ToString();
                    }
                    if (dt.Rows[n]["State"] != null && dt.Rows[n]["State"].ToString() != "")
                    {
                        model.State = Convert.ToInt16(dt.Rows[n]["State"].ToString());
                    }
                    if (dt.Rows[0]["AdminID"] != null && dt.Rows[0]["AdminID"].ToString() != "")
                    {
                        model.AdminID = Convert.ToInt16(dt.Rows[0]["AdminID"].ToString());
                    }
                    if (dt.Rows[0]["AdminName"] != null && dt.Rows[0]["AdminName"].ToString() != "")
                    {
                        model.AdminName = dt.Rows[0]["AdminName"].ToString();
                    }
                    if (dt.Rows[n]["CreateTime"] != null && dt.Rows[n]["CreateTime"].ToString() != "")
                    {
                        model.CreateTime = DateTime.Parse(dt.Rows[n]["CreateTime"].ToString());
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
        /// 获得数据列表
        /// </summary>
        public DataTable GetSortFroJoin(string sqlWhere)
        {
            string sql = "select * from v_SortForJoin ";//查询视图
            if (sqlWhere != "")
                sql += " where " + sqlWhere;
            return DbHelperSQL.Query(sql).Tables[0];
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

