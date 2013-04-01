using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using YK.DALFactory;
using YK.IDAL;

namespace YK.BLL
{
    /// <summary>
    /// 描述:栏目管理--业务层
    /// 作者:杨良斌
    /// 时间:2012-08-15
    /// </summary>
    public class Template
    {
        private readonly ITemplate dal = DataAccess.CreateTemplate();
        public Template()
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
        public bool Exists(int TemplateID)
        {
            return dal.Exists(TemplateID);
        }
        /// <summary>
        /// 是否存在指定名称的记录
        /// </summary>
        public bool Exists(string TemplateName, int TemplateID)
        {
            return dal.Exists(TemplateName, TemplateID);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(YK.Model.TemplateInfo model)
        {
            dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(YK.Model.TemplateInfo model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int TemplateID)
        {

            return dal.Delete(TemplateID);
        }
        /// <summary>
        /// 批量删除数据(根据ID列表)
        /// </summary>
        public bool DeleteList(string TemplateIDlist)
        {
            return dal.DeleteList(TemplateIDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public YK.Model.TemplateInfo GetModel(int TemplateID)
        {

            return dal.GetModel(TemplateID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public YK.Model.TemplateInfo GetModelByCache(int TemplateID)
        {

            string CacheKey = "TemplateInfoModel-" + TemplateID;
            object objModel = YK.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(TemplateID);
                    if (objModel != null)
                    {
                        int ModelCache = YK.Common.ConfigHelper.GetConfigInt("ModelCache");
                        YK.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (YK.Model.TemplateInfo)objModel;
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
        public List<YK.Model.TemplateInfo> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<YK.Model.TemplateInfo> DataTableToList(DataTable dt)
        {
            List<YK.Model.TemplateInfo> modelList = new List<YK.Model.TemplateInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                YK.Model.TemplateInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new YK.Model.TemplateInfo();
                    if (dt.Rows[n]["TemplateID"] != null && dt.Rows[n]["TemplateID"].ToString() != "")
                    {
                        model.TemplateID = int.Parse(dt.Rows[n]["TemplateID"].ToString());
                    }
                    if (dt.Rows[n]["TemplateName"] != null && dt.Rows[n]["TemplateName"].ToString() != "")
                    {
                        model.TemplateName = dt.Rows[n]["TemplateName"].ToString();
                    }
                    if (dt.Rows[n]["TemplateUrl"] != null && dt.Rows[n]["TemplateUrl"].ToString() != "")
                    {
                        model.TemplateUrl = dt.Rows[n]["TemplateUrl"].ToString();
                    }
                    if (dt.Rows[n]["TemplateSource"] != null && dt.Rows[n]["TemplateSource"].ToString() != "")
                    {
                        model.TemplateSource = dt.Rows[n]["TemplateSource"].ToString();
                    }
                    if (dt.Rows[n]["TemplateDesc"] != null && dt.Rows[n]["TemplateDesc"].ToString() != "")
                    {
                        model.TemplateDesc = dt.Rows[n]["TemplateDesc"].ToString();
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
        #endregion  Method
    }
}
