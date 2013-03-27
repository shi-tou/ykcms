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
using YK.DBUtility;

namespace YkCms.AppCode
{
    /// <summary>
    /// 文件：AjaxSyslog
    /// 描述：日志操作类
    /// 创建人：杨良斌
    /// </summary>
    public class AjaxSyslog
    {
        AdminInfo adminInfo = Function.GetCookiAdmin();
        SysLog log = new SysLog();
        /// <summary>
        /// 获取日志列表
        /// </summary>
        public void GetSyslogList()
        {
            int pagesize = RequestHelper.GetRequestInt("rows", 10);
            int pageindex = RequestHelper.GetRequestInt("page", 1);
            DataSet ds = new DataSet();
            if (adminInfo.AdminID == 1)
            {
                ds = PageHelper.GetPageList("SysLog", "*", "SysLogID", pagesize, pageindex, 1, " 1=1 ");
            }
            else
            {
                ds = PageHelper.GetPageList("SysLog", "*", "SysLogID", pagesize, pageindex, 1, " AdminID=" + adminInfo.AdminID.ToString()); 
            }
            AjaxMsg.msg = "\"rows\":" + JsonHelper.ToJson(ds.Tables[0], "") + ",\"total\":" + ds.Tables[1].Rows[0]["Total"];
        }
       
        /// <summary>
        /// 删除日志
        /// </summary>
        public void DeleteSyslog()
        {
            string syslogIDs = RequestHelper.GetRequestStr("syslogIDs", "0");
            log.DeleteList(syslogIDs);
            new SysLog().Add(new SysLogInfo("日志管理", "删除", "删除了编号为【" + syslogIDs + "】的日志信息。", Function.GetIP(), adminInfo.AdminID, adminInfo.AdminName, DateTime.Now));
            AjaxMsg.msg = "\"msg\":\"删除成功\"";
        }       
        /// <summary>
        /// 查询日志
        /// </summary>
        /// <param name="adminid"></param>
        public void SearchSyslog()
        {
            int pagesize = RequestHelper.GetRequestInt("rows", 10);
            int pageindex = RequestHelper.GetRequestInt("page", 1);
            string key = RequestHelper.GetRequestStr("key", "");
            string starttime = RequestHelper.GetRequestStr("starttime", "");
            string endtime = RequestHelper.GetRequestStr("endtime", "");
            StringBuilder sbSql = new StringBuilder(" 1=1 ");
            if (adminInfo.AdminID != 1)
            {
                sbSql.Append(" and AdminID=" + adminInfo.AdminID);
            }
            if (key != "")
                sbSql.AppendFormat(" and Detail like '%{0}%' ", key);          
            if (starttime != "")
                sbSql.AppendFormat(" and datediff(d,'{0}',CreateTime) >= 0 ", starttime);
            if (endtime != "")
                sbSql.AppendFormat(" and datediff(d,'{0}',CreateTime) <= 0 ", endtime);
            DataSet ds = ds = PageHelper.GetPageList("SysLog", "*", "SysLogID", pagesize, pageindex, 1, sbSql.ToString());
            AjaxMsg.msg = "\"rows\":" + JsonHelper.ToJson(ds.Tables[0], "") + ",\"total\":" + ds.Tables[1].Rows[0]["Total"];
        }       
    }
}