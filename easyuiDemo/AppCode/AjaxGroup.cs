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
    public class AjaxGroup
    {
        AdminGroup adminGroup = new AdminGroup();
        SysLog log = new SysLog();
        /// <summary>
        /// 验证权限组名称
        /// </summary>
        public void CheckGroupName()
        {
            int groupid = RequestHelper.GetRequestInt("groupid", 0);
            string groupname = RequestHelper.GetRequestStr("groupname", "");
            if (adminGroup.Exists(groupname, groupid))
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
        /// 添加/修改权限组
        /// </summary>
        public void AddGroupModel()
        {
            int groupid = RequestHelper.GetRequestInt("admingroup-groupid", 0);
            string groupname = RequestHelper.GetRequestStr("admingroup-groupname", "");
            string describe = RequestHelper.GetRequestStr("admingroup-describe", "");
            string groupauth = RequestHelper.GetRequestStr("admingroup-groupauth", "");

            AdminGroupInfo ainfo = new AdminGroupInfo();
            ainfo.GroupName = groupname;
            ainfo.Describe = describe;
            ainfo.GroupAuth = groupauth;
            ainfo.CreateTime = DateTime.Now;
            ainfo.AdminID = Function.AdminInfo.AdminID;
            if (groupid == 0)//添加
            {
                adminGroup.Add(ainfo);
                log.Add(new SysLogInfo("权限组管理", "添加", "添加名称为【" + groupname + "】的权限组信息", Function.GetIP(), Function.AdminInfo.AdminID, Function.AdminInfo.AdminName, DateTime.Now));
                AjaxMsg.msg = "\"msg\":\"添加成功\"";
            }
            else//修改
            {
                ainfo.GroupID = groupid;
                adminGroup.Update(ainfo);
                log.Add(new SysLogInfo("权限组管理", "修改", "修改编号为【" + groupid + "】的权限组名称为", Function.GetIP(), Function.AdminInfo.AdminID, Function.AdminInfo.AdminName, DateTime.Now));
                AjaxMsg.msg = "\"msg\":\"修改成功\"";
            }
        }
        /// <summary>
        /// 获取权限组列表
        /// </summary>
        public void GetGroupList()
        {
            DataSet ds = adminGroup.GetAllList();
            AjaxMsg.msg = "\"rows\":" + JsonHelper.ToJson(ds.Tables[0], "") + ",\"total\":" + ds.Tables[0].Rows.Count;
        }
        /// <summary>
        /// 删除权限组
        /// </summary>
        public void DeleteGroup()
        {
            string groupids = RequestHelper.GetRequestStr("groupids", "0");
            adminGroup.DeleteList(groupids);
            log.Add(new SysLogInfo("管理员管理", "删除", "删除了编号为【" + groupids + "】的管理员信息", Function.GetIP(), Function.AdminInfo.AdminID, Function.AdminInfo.AdminName, DateTime.Now));
            AjaxMsg.msg = "\"msg\":\"删除成功\"";
        }
        /// <summary>
        /// 获取权限组
        /// </summary>
        public void GetGroupModel()
        {
            int groupID = RequestHelper.GetRequestInt("groupid", 0);
            AdminGroupInfo ainfo = adminGroup.GetModelByCache(groupID);
            AjaxMsg.msg = "\"msg\":{\"GroupID\":" + ainfo.GroupID + ",\"GroupName\":\"" + ainfo.GroupName + "\",\"GroupAuth\":\"" + ainfo.GroupAuth + "\",\"Describe\":\"" +
                ainfo.Describe + "\"}";
        }
        /// <summary>
        /// 查询权限组
        /// </summary>
        /// <param name="adminid"></param>
        public void SearchGroup()
        {
            string groupname = RequestHelper.GetRequestStr("groupname", "");
            string starttime = RequestHelper.GetRequestStr("starttime", "");
            string endtime = RequestHelper.GetRequestStr("endtime", "");
            StringBuilder sbSql = new StringBuilder(" 1=1 ");
            if (groupname != "")
                sbSql.AppendFormat(" and GroupName like '%{0}%' ", groupname);
            if (starttime != "")
                sbSql.AppendFormat(" and datediff(d,'{0}',CreateTime) >= 0 ", starttime);
            if (endtime != "")
                sbSql.AppendFormat(" and datediff(d,'{0}',CreateTime) <= 0 ", endtime);
            DataSet ds = adminGroup.GetList(sbSql.ToString());
            AjaxMsg.msg = "\"rows\":" + JsonHelper.ToJson(ds.Tables[0], "") + ",\"total\":" + ds.Tables[0].Rows.Count;
        }
        /// <summary>
        /// 获取权限组信息，并返回select的HTML
        /// </summary>
        public string GetGroupSelectHtml(bool flag)
        {
            StringBuilder sbHtml;
            if (flag)
                sbHtml = new StringBuilder("<option value=\"0\">-请选择-</option>");
            else
                sbHtml = new StringBuilder();
            DataSet ds = adminGroup.GetAllList();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sbHtml.Append("<option value=\""+ dr["GroupID"].ToString() +"\">"+ dr["GroupName"].ToString()+"</option>");
            }
            return sbHtml.ToString();
        }
    }
}