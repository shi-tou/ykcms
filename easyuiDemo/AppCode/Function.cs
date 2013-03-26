using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using YK.Model;
using System.Web.Security;
using System.Text;

namespace YkCms.AppCode
{
    public static class Function
    {
        public static AdminInfo _adminInfo;
        /// <summary>
        /// 全局静态变量
        /// </summary>
        public static AdminInfo AdminInfo
        {
            get
            {
                if (_adminInfo != null)
                {
                    _adminInfo = GetCookiAdmin();
                }
                return _adminInfo;
            }
        }
        #region 读取和保存Cookie中的登陆信息
        /// <summary>
        /// 将当前登陆对象序列化为XML字符串
        /// </summary>
        public static string SerializeAdmin(AdminInfo ainfo)
        {
            StringBuilder sbAdmin = new StringBuilder();
            sbAdmin.AppendFormat("<admin AdminID=\"{0}\" GroupID=\"{1}\" AdminAccount=\"{2}\" AdminPwd=\"{3}\" AdminName=\"{4}\" State=\"{5}\" />",
                ainfo.AdminID, ainfo.GroupID, ainfo.AdminAccount, ainfo.AdminPwd, ainfo.AdminName, ainfo.State);
            return sbAdmin.ToString();
        }

        /// <summary>
        /// 读取cookie中的登陆管理员信息
        /// </summary>
        public static AdminInfo GetCookiAdmin()
        {
            string masterStr = HttpContext.Current.Session["AdminInfo"] != null ? HttpContext.Current.Session["AdminAccount"].ToString() : "";
            if (masterStr == "")
            {
                if ((HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null) && (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value != ""))
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                    masterStr = ticket.UserData;
                    HttpContext.Current.Session.Add("AdminInfo", masterStr);
                }
            }
            if (masterStr == "")
                return null;

            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.LoadXml(masterStr);
            }
            catch { return null; }
            AdminInfo ainfo = new AdminInfo();
            ainfo.AdminID = int.Parse(xmldoc.DocumentElement.Attributes["AdminID"].Value);
            ainfo.GroupID = int.Parse(xmldoc.DocumentElement.Attributes["GroupID"].Value);
            ainfo.AdminAccount = xmldoc.DocumentElement.Attributes["AdminAccount"].Value;
            ainfo.AdminPwd = xmldoc.DocumentElement.Attributes["AdminPwd"].Value;
            ainfo.AdminName=xmldoc.DocumentElement.Attributes["AdminName"].Value;
            ainfo.State = int.Parse(xmldoc.DocumentElement.Attributes["State"].Value);
            return ainfo;
        }

        /// <summary>
        /// 删除登录Cookie
        /// </summary>
        public static void DeleteCookieAdmin()
        {
            HttpContext.Current.Session.Remove("AdminAccount");
        }
        #endregion
        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string userIP;
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] == null) 
                    userIP = HttpContext.Current.Request.UserHostAddress;
            else 
                userIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            return userIP;
        }
    }
}