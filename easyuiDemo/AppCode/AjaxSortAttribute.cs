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
    public class AjaxSortAttribute
    {
        SortAttribute sortAttribute = new SortAttribute();
        SysLog log = new SysLog();
        /// <summary>
        /// 验证栏目属性名称
        /// </summary>
        public void CheckGroupName()
        {
            int groupid = RequestHelper.GetRequestInt("sortattributename", 0);
            string groupname = RequestHelper.GetRequestStr("sortattributeid", "");
            if (sortAttribute.Exists(groupname, groupid))
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
        public void AddGroupModel()
        {
            int sortattributeid = RequestHelper.GetRequestInt("sortattribute-sortattributeid", 0);
            string sortattributename = RequestHelper.GetRequestStr("sortattribute-sortattributename", "");
            string sortattributedesc = RequestHelper.GetRequestStr("sortattribute-sortattributedesc", "");

            SortAttributeInfo ainfo = new SortAttributeInfo();
            ainfo.SortAttributeName = sortattributename;
            ainfo.SortAttributeDesc = sortattributedesc;            
            ainfo.AdminID = Function.AdminInfo.AdminID;
            ainfo.AdminName = Function.AdminInfo.AdminName;
            ainfo.CreateTime = DateTime.Now;
            if (sortattributeid == 0)//添加
            {
                sortAttribute.Add(ainfo);
                log.Add(new SysLogInfo("栏目属性管理", "添加", "添加名称为【" + sortattributename + "】的栏目属性信息", Function.GetIP(), Function.AdminInfo.AdminID, Function.AdminInfo.AdminName, DateTime.Now));
                AjaxMsg.msg = "\"msg\":\"添加成功\"";
            }
            else//修改
            {
                ainfo.SortAttributeID = sortattributeid;
                sortAttribute.Update(ainfo);
                log.Add(new SysLogInfo("栏目属性管理", "修改", "修改编号为【" + sortattributeid + "】的栏目属性名称为", Function.GetIP(), Function.AdminInfo.AdminID, Function.AdminInfo.AdminName, DateTime.Now));
                AjaxMsg.msg = "\"msg\":\"修改成功\"";
            }
        }
        /// <summary>
        /// 获取权限组列表
        /// </summary>
        public void GetSortAttributeList()
        {
            DataSet ds = sortAttribute.GetAllList();
            AjaxMsg.msg = "\"rows\":" + JsonHelper.ToJson(ds.Tables[0], "") + ",\"total\":" + ds.Tables[0].Rows.Count;
        }
        /// <summary>
        /// 删除权限组
        /// </summary>
        public void DeleteSortAttribute()
        {
            string sortattributeids = RequestHelper.GetRequestStr("sortattributeids", "0");
            sortAttribute.DeleteList(sortattributeids);
            log.Add(new SysLogInfo("栏目属性管理", "删除", "删除了编号为【" + sortattributeids + "】的栏目属性信息", Function.GetIP(), Function.AdminInfo.AdminID, Function.AdminInfo.AdminName, DateTime.Now));
            AjaxMsg.msg = "\"msg\":\"删除成功\"";
        }
        /// <summary>
        /// 获取权限组
        /// </summary>
        public void GetGroupModel()
        {
            int sortattributeid = RequestHelper.GetRequestInt("sortattributeid", 0);
            SortAttributeInfo ainfo = sortAttribute.GetModelByCache(sortattributeid);
            //AjaxMsg.msg = "\"msg\":{\"GroupID\":" + ainfo.GroupID + ",\"GroupName\":\"" + ainfo.GroupName + "\",\"GroupAuth\":\"" + ainfo.GroupAuth + "\",\"Describe\":\"" +
            //    ainfo.Describe + "\"}";
        }
        /// <summary>
        /// 查询权限组
        /// </summary>
        /// <param name="adminid"></param>
        public void SearchSortAttribute()
        {
            string sortattributename = RequestHelper.GetRequestStr("sortattributename", "");
            string starttime = RequestHelper.GetRequestStr("starttime", "");
            string endtime = RequestHelper.GetRequestStr("endtime", "");
            StringBuilder sbSql = new StringBuilder(" 1=1 ");
            if (sortattributename != "")
                sbSql.AppendFormat(" and SortAttributeName like '%{0}%' ", sortattributename);
            if (starttime != "")
                sbSql.AppendFormat(" and datediff(d,'{0}',CreateTime) >= 0 ", starttime);
            if (endtime != "")
                sbSql.AppendFormat(" and datediff(d,'{0}',CreateTime) <= 0 ", endtime);
            DataSet ds = sortAttribute.GetList(sbSql.ToString());
            AjaxMsg.msg = "\"rows\":" + JsonHelper.ToJson(ds.Tables[0], "") + ",\"total\":" + ds.Tables[0].Rows.Count;
        }
        /// <summary>
        /// 获取权限组信息，并返回select的HTML
        /// </summary>
        public string GetSortAttributeSelectHtml(bool flag)
        {
            StringBuilder sbHtml;
            if (flag)
                sbHtml = new StringBuilder("<option value=\"0\">-请选择-</option>");
            else
                sbHtml = new StringBuilder();
            DataSet ds = sortAttribute.GetAllList();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sbHtml.Append("<option value=\"" + dr["SortAttributeID"].ToString() + "\">" + dr["SortAttributeName"].ToString() + "</option>");
            }
            return sbHtml.ToString();
        }
    }
}