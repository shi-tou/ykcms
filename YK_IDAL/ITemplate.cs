using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace YK.IDAL
{
    /// <summary>
    /// 描述:栏目模板--接口层
    /// 作者:杨良斌
    /// 时间:2012-08-15
    /// </summary>
    public interface ITemplate
    {
        #region  成员方法
        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int TemplateID);
        bool Exists(string TemplateName, int TemplateID);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        void Add(YK.Model.TemplateInfo model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(YK.Model.TemplateInfo model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int TemplateID);
        bool DeleteList(string TemplateIDlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        YK.Model.TemplateInfo GetModel(int TemplateID);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);
        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);
        #endregion  成员方法
    }
}
