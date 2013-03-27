using System;
using System.Collections.Generic;
using System.Web;
using YK.Common;
using YK.Model;
using YK.BLL;
using System.Data;
using System.Text;
using System.Data.OleDb;
using System.Web.Security;

namespace YkCms.AppCode
{
    /// <summary>
    /// 文件：AjaxGroup
    /// 描述：仅限组操作类
    /// 创建人：杨良斌
    /// </summary>
    public class AjaxSort
    {
        AdminInfo adminInfo = Function.GetCookiAdmin();
        Sort sort = new Sort();
        SysLog log = new SysLog();
        /// <summary>
        /// 验证栏目属性名称
        /// </summary>
        public void CheckSortName()
        {
            int sortid = RequestHelper.GetRequestInt("sortid", 0);
            string sortname = RequestHelper.GetRequestStr("sortname", "");
            if (sort.Exists(sortname, sortid))
            {
                AjaxMsg.msgOK = false;
                AjaxMsg.msg = "";
            }
            else
            {
                AjaxMsg.msgOK = true;
                AjaxMsg.msg = "";
            }
        }
        /// <summary>
        /// 添加/修改栏目属性
        /// </summary>
        public void AddSortModel()
        {
            int parentid = RequestHelper.GetRequestInt("sortlist-parentid", 0);
            int sortattributeid = RequestHelper.GetRequestInt("sortlist-sortattributeid", 0);
            int sortid = RequestHelper.GetRequestInt("sortlist-sortid", 0);
            string sortname = RequestHelper.GetRequestStr("sortlist-sortname", "");
            string sorturl = RequestHelper.GetRequestStr("sortlist-sorturl", "");
            int sorttemplateid = RequestHelper.GetRequestInt("sortlist-sorttemplateid", 0);
            string pagetitle = RequestHelper.GetRequestStr("sortlist-pagetitle", "");
            string pagekeywords = RequestHelper.GetRequestStr("sortlist-pagekeywords", "");
            string pagedesc = RequestHelper.GetRequestStr("sortlist-pagedesc", "");
            string bannerurl = RequestHelper.GetRequestStr("sortlist-bannerurl", "");
            int state = RequestHelper.GetRequestInt("sortlist-state", 1);
            SortInfo sinfo = new SortInfo();
            sinfo.ParentID = parentid;
            sinfo.SortAttributeID = sortattributeid;
            sinfo.SortName = sortname;
            sinfo.SortUrl = sorturl;
            sinfo.SortTemplateID = sorttemplateid;
            sinfo.PageTitle = pagetitle;
            sinfo.PageKeywords = pagekeywords;
            sinfo.PageDesc = pagedesc;
            sinfo.State = state;
            sinfo.AdminID = adminInfo.AdminID;
            sinfo.AdminName = adminInfo.AdminName;
            sinfo.CreateTime = DateTime.Now;
            if (sortid == 0)//添加
            {
                sort.Add(sinfo);
                log.Add(new SysLogInfo("栏目属性管理", "添加", "添加名称为【" + sortname + "】的栏目属性信息", Function.GetIP(), adminInfo.AdminID, adminInfo.AdminName, DateTime.Now));
                AjaxMsg.msg = "\"msg\":\"添加成功\"";
            }
            else//修改
            {
                sinfo.SortID = sortid;
                sort.Update(sinfo);
                log.Add(new SysLogInfo("栏目属性管理", "修改", "修改编号为【" + sortid + "】的栏目属性名称为", Function.GetIP(), adminInfo.AdminID, adminInfo.AdminName, DateTime.Now));
                AjaxMsg.msg = "\"msg\":\"修改成功\"";
            }
        }
        /// <summary>
        /// 获取栏目属性列表
        /// </summary>
        public void GetSortList()
        {
            DataSet ds = sort.GetAllList();
            AjaxMsg.msg = "\"rows\":" + JsonHelper.ToJson(ds.Tables[0], "") + ",\"total\":" + ds.Tables[0].Rows.Count;
        }
        /// <summary>
        /// 删除栏目属性
        /// </summary>
        public void DeleteSort()
        {
            string sortids = RequestHelper.GetRequestStr("sortids", "0");
            sort.DeleteList(sortids);
            log.Add(new SysLogInfo("栏目属性管理", "删除", "删除了编号为【" + sortids + "】的栏目属性信息", Function.GetIP(), adminInfo.AdminID, adminInfo.AdminName, DateTime.Now));
            AjaxMsg.msg = "\"msg\":\"删除成功\"";
        }
        /// <summary>
        /// 获取栏目属性
        /// </summary>
        public void GetSortModel()
        {
            int sortid = RequestHelper.GetRequestInt("sortid", 0);
            SortInfo sinfo = sort.GetModelByCache(sortid);
            //AjaxMsg.msg = "\"msg\":{\"SortID\":" + sinfo.SortID + ",\"SortName\":\"" + sinfo.SortName + "\",\"SortDesc\":\"" + sinfo.SortDesc + "\",\"AdminName\":\"" +
            //    sinfo.AdminName + "\",\"CreateTime\":\"" + sinfo.CreateTime + "\"}";
        }
        /// <summary>
        /// 查询栏目属性
        /// </summary>
        /// <param name="adminid"></param>
        public void SearchSort()
        {
            string sortname = RequestHelper.GetRequestStr("sortname", "");
            string starttime = RequestHelper.GetRequestStr("starttime", "");
            string endtime = RequestHelper.GetRequestStr("endtime", "");
            StringBuilder sbSql = new StringBuilder(" 1=1 ");
            if (sortname != "")
                sbSql.AppendFormat(" and SortName like '%{0}%' ", sortname);
            if (starttime != "")
                sbSql.AppendFormat(" and datediff(d,'{0}',CreateTime) >= 0 ", starttime);
            if (endtime != "")
                sbSql.AppendFormat(" and datediff(d,'{0}',CreateTime) <= 0 ", endtime);
            DataSet ds = sort.GetList(sbSql.ToString());
            AjaxMsg.msg = "\"rows\":" + JsonHelper.ToJson(ds.Tables[0], "") + ",\"total\":" + ds.Tables[0].Rows.Count;
        }
        /// <summary>
        /// 获取栏目属性信息，并返回select的HTML
        /// </summary>
        public string GetSortSelectHtml(bool flag)
        {
            StringBuilder sbHtml;
            if (flag)
                sbHtml = new StringBuilder("<option value=\"0\">-请选择-</option>");
            else
                sbHtml = new StringBuilder();
            DataSet ds = sort.GetAllList();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sbHtml.Append("<option value=\"" + dr["SortID"].ToString() + "\">" + dr["SortName"].ToString() + "</option>");
            }
            return sbHtml.ToString();
        }
    }
}