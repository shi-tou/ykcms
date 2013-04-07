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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace YkCms.AppCode
{
    /// <summary>
    /// 文件：AjaxGroup
    /// 描述：仅限组操作类
    /// 创建人：杨良斌
    /// </summary>
    public class AjaxTemplate
    {
        AdminInfo adminInfo = Function.GetCookiAdmin();
        Template template = new Template();
        SysLog log = new SysLog();
        /// <summary>
        /// 验证栏目模板名称
        /// </summary>
        public void CheckTemplateName()
        {
            int templateid = RequestHelper.GetRequestInt("templateid", 0);
            string templatename = RequestHelper.GetRequestStr("templatename", "");
            if (template.Exists(templatename, templateid))
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
        /// 添加/修改栏目模板
        /// </summary>
        public void AddTemplateModel()
        {
            int templateid = RequestHelper.GetRequestInt("templateid", 0);
            string templatename = RequestHelper.GetRequestStr("templatename", "");
            string templateurl = RequestHelper.GetRequestStr("templateurl", "");
            string templatedesc = RequestHelper.GetRequestStr("templatedesc", "");
            string templatesource = RequestHelper.GetRequestStr("templatesource", "");

            TemplateInfo sinfo = new TemplateInfo();
            sinfo.TemplateName = templatename;
            sinfo.TemplateUrl = templatedesc;
            sinfo.TemplateDesc = templatedesc;
            sinfo.TemplateSource = templatesource;
            sinfo.AdminID = adminInfo.AdminID;
            sinfo.AdminName = adminInfo.AdminName;
            sinfo.CreateTime = DateTime.Now;
            if (templateid == 0)//添加
            {
                template.Add(sinfo);
                log.Add(new SysLogInfo("栏目模板管理", "添加", "添加名称为【" + templatename + "】的栏目模板信息", Function.GetIP(), adminInfo.AdminID, adminInfo.AdminName, DateTime.Now));
                AjaxMsg.msg = "\"msg\":\"添加成功\"";
            }
            else//修改
            {
                sinfo.TemplateID = templateid;
                template.Update(sinfo);
                log.Add(new SysLogInfo("栏目模板管理", "修改", "修改编号为【" + templateid + "】的栏目模板名称为", Function.GetIP(), adminInfo.AdminID, adminInfo.AdminName, DateTime.Now));
                AjaxMsg.msg = "\"msg\":\"修改成功\"";
            }
        }       
        /// <summary>
        /// 获取栏目模板列表
        /// </summary>
        public void GetTemplateList()
        {
            DataSet ds = template.GetAllList();
            AjaxMsg.msg = "\"rows\":" + JsonConvert.SerializeObject(ds.Tables[0],new DataTableConverter()) + ",\"total\":" + ds.Tables[0].Rows.Count;
            //AjaxMsg.msg = "\"rows\":" + ds.Tables[0], "") + ",\"total\":" + ds.Tables[0].Rows.Count;
        }
        /// <summary>
        /// 删除栏目模板
        /// </summary>
        public void DeleteTemplate()
        {
            string templateids = RequestHelper.GetRequestStr("templateids", "0");
            template.DeleteList(templateids);
            log.Add(new SysLogInfo("栏目模板管理", "删除", "删除了编号为【" + templateids + "】的栏目模板信息", Function.GetIP(), adminInfo.AdminID, adminInfo.AdminName, DateTime.Now));
            AjaxMsg.msg = "\"msg\":\"删除成功\"";
        }
        /// <summary>
        /// 获取栏目模板
        /// </summary>
        public void GetTemplateModel()
        {
            int templateid = RequestHelper.GetRequestInt("templateid", 0);
            TemplateInfo sinfo = template.GetModelByCache(templateid);
            if (sinfo != null)
            {
                AjaxMsg.msg = "\"msg\":{\"TemplateID\":" + sinfo.TemplateID + ",\"TemplateName\":\"" + sinfo.TemplateName + "\",\"TemplateUrl\":\"" + sinfo.TemplateUrl + "\",\"TemplateDesc\":\"" + sinfo.TemplateDesc + "\",\"TemplateSource\":" + JsonConvert.SerializeObject(sinfo.TemplateSource)
                    + ",\"AdminName\":\"" + sinfo.AdminName + "\",\"CreateTime\":\"" + sinfo.CreateTime + "\"}";
            }
            else
            {
                AjaxMsg.msgOK = false;
                AjaxMsg.ex = "找不到数据！";
            }
        }
        /// <summary>
        /// 查询栏目模板
        /// </summary>
        /// <param name="adminid"></param>
        public void SearchTemplate()
        {
            string templatename = RequestHelper.GetRequestStr("templatename", "");
            string starttime = RequestHelper.GetRequestStr("starttime", "");
            string endtime = RequestHelper.GetRequestStr("endtime", "");
            StringBuilder sbSql = new StringBuilder(" 1=1 ");
            if (templatename != "")
                sbSql.AppendFormat(" and TemplateName like '%{0}%' ", templatename);
            if (starttime != "")
                sbSql.AppendFormat(" and datediff(d,'{0}',CreateTime) >= 0 ", starttime);
            if (endtime != "")
                sbSql.AppendFormat(" and datediff(d,'{0}',CreateTime) <= 0 ", endtime);
            DataSet ds = template.GetList(sbSql.ToString());
            AjaxMsg.msg = "\"rows\":" + JsonHelper.ToJson(ds.Tables[0], "") + ",\"total\":" + ds.Tables[0].Rows.Count;
        }
        /// <summary>
        /// 获取栏目模板信息，并返回select的HTML
        /// </summary>
        public string GetTemplateSelectHtml(bool flag)
        {
            StringBuilder sbHtml;
            if (flag)
                sbHtml = new StringBuilder("<option value=\"0\">-请选择-</option>");
            else
                sbHtml = new StringBuilder();
            DataSet ds = template.GetAllList();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sbHtml.Append("<option value=\"" + dr["TemplateID"].ToString() + "\">" + dr["TemplateName"].ToString() + "</option>");
            }
            return sbHtml.ToString();
        }
    }
}