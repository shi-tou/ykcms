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
    /// 文件：AjaxAdmin
    /// 描述：管理员操作类
    /// 创建人：杨良斌
    /// </summary>
    public class AjaxAdmin
    {        
        Admin admin = new Admin();
        SysLog log = new SysLog();
        /// <summary>
        /// 登录
        /// </summary>
        public void Login()
        {
            string adminaccount = RequestHelper.GetRequestStr("adminaccount", "");
            string adminpwd = RequestHelper.GetRequestStr("adminpwd", "");
            string checkCode = RequestHelper.GetRequestStr("checkCode","");
            string code = HttpContext.Current.Session["CheckCode"] == null ? "" : HttpContext.Current.Session["CheckCode"].ToString();
            if (checkCode != code)
            {
                AjaxMsg.msgOK = false;
                AjaxMsg.ex = "验证码不正确！";
            }
            else
            {
                AdminInfo ainfo = admin.GetModelByAccount(adminaccount);
                if (ainfo == null)
                {
                    AjaxMsg.msgOK = false;
                    AjaxMsg.ex = "管理账号不存在！";
                }
                else
                {
                    if (adminpwd == DESEncrypt.Decrypt(ainfo.AdminPwd))
                    {
                        HttpContext.Current.Session["AdminAccount"] = ainfo.AdminAccount;
                        // 登陆成功，将身份验证票据保存到Cookie
                        FormsAuthentication.Initialize();
                        FormsAuthenticationTicket Ticket = new FormsAuthenticationTicket(1, ainfo.AdminAccount, DateTime.Now, DateTime.Now.AddMinutes(60), false, Function.SerializeAdmin(ainfo),
                            FormsAuthentication.FormsCookiePath);//建立身份验证票对象
                        string HashTicket = FormsAuthentication.Encrypt(Ticket); //加密序列化验证票为字符串
                        HttpCookie UserCookie = new HttpCookie(FormsAuthentication.FormsCookieName, HashTicket);
                        //更新管理员登录ip及最后登录时间
                        UpdateAdminIpAndTime(ainfo.AdminID);
                        //生成Cookie
                        HttpContext.Current.Response.Cookies.Add(UserCookie); //输出Cookie                       
                        log.Add(new SysLogInfo("系统操作", "登录", "登录系统", Function.GetIP(), ainfo.AdminID, ainfo.AdminName, DateTime.Now));
                        AjaxMsg.msgOK = true;
                        AjaxMsg.msg = "";
                    }
                    else
                    {
                        AjaxMsg.msgOK = false;
                        AjaxMsg.ex = "管理密码错误！";
                    }
                }
            }
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        public void Logout()
        {
            try
            {
                FormsAuthentication.SignOut();
                Function.DeleteCookieAdmin();
                AjaxMsg.msgOK = true;
            }
            catch {
                AjaxMsg.msgOK = false;
            }
        }
        /// <summary>
        /// 检查管理员帐号
        /// </summary>
        public void checkAdminAccount()
        {
            int adminid = RequestHelper.GetRequestInt("adminid", 0);
            string adminaccount = RequestHelper.GetRequestStr("adminaccount", "");
            if (admin.Exists(adminaccount, adminid))
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
        /// 添加/修改管理员
        /// </summary>
        public void AddAdminModel()
        {
            int adminid = RequestHelper.GetRequestInt("adminlist-adminid", 0);
            string adminaccount = RequestHelper.GetRequestStr("adminlist-adminaccount", "");
            string adminpwd = RequestHelper.GetRequestStr("adminlist-adminpwd", "");
            string adminname = RequestHelper.GetRequestStr("adminlist-adminname", "");
            int groupid = RequestHelper.GetRequestInt("adminlist-groupid", 0);
            int state = RequestHelper.GetRequestInt("adminlist-state", 0);
            string admindesc = RequestHelper.GetRequestStr("adminlist-admindesc", "");
            string ip = Function.GetIP();
            AdminInfo ainfo = new AdminInfo();
            ainfo.AdminAccount = adminaccount;
            
            ainfo.AdminName = adminname;
            ainfo.GroupID = groupid;
            ainfo.State = state;
            ainfo.AdminDesc = admindesc;
            ainfo.CreateAdminID = Function.AdminInfo.AdminID;
            ainfo.LastLoginIP = ip;
            ainfo.LastLoginTime = DateTime.Now;
            ainfo.CreateTime = DateTime.Now;
            if (adminid == 0)//添加
            {
                ainfo.AdminPwd = DESEncrypt.Encrypt(adminpwd);
                admin.Add(ainfo);
                new SysLog().Add(new SysLogInfo("管理员管理", "添加", "添加账号为【" + adminaccount + "】的管理员信息", ip, Function.AdminInfo.AdminID, Function.AdminInfo.AdminName, DateTime.Now));
                AjaxMsg.msg = "\"msg\":\"添加成功\"";
            }
            else
            {
                ainfo = admin.GetModel(adminid);
                ainfo.AdminName = adminname;
                ainfo.AdminDesc = admindesc;
                ainfo.GroupID = groupid;
                ainfo.State = state;
                admin.Update(ainfo);
                log.Add(new SysLogInfo("管理员管理", "修改", "修改管理帐号为【" + adminaccount + "】的管理员信息", ip, Function.AdminInfo.AdminID, Function.AdminInfo.AdminName, DateTime.Now));
                AjaxMsg.msg = "\"msg\":\"修改成功\"";
            }
        }
        /// <summary>
        /// 获取管理员列表
        /// </summary>
        public void GetAdminList()
        {                        
            DataSet ds = admin.GetAllList();
            AjaxMsg.msg = "\"rows\":" + JsonHelper.ToJson(ds.Tables[0], "") + ",\"total\":" + ds.Tables[0].Rows.Count;
        }
        /// <summary>
        /// 获取管理员
        /// </summary>
        public void GetAdminModel()
        {
            int adminid = RequestHelper.GetRequestInt("adminid", 0);
            AdminInfo ainfo = admin.GetModelByCache(adminid);
            AjaxMsg.msg = "\"msg\":{\"AdminID\":" + ainfo.AdminID.ToString() + ",\"AdminAccount\":\"" + ainfo.AdminAccount + "\",\"AdminPwd\":\"" + ainfo.AdminPwd + "\",\"AdminName\":\"" +
                ainfo.AdminName + "\",\"GroupID\":\"" + ainfo.GroupID.ToString() + "\",\"State\":\"" + ainfo.State.ToString() + "\",\"AdminDesc\":\"" + ainfo.AdminDesc + "\",\"LastLoginIP\":\"" + ainfo.LastLoginIP
                + "\",\"LastLoginTime\":\"" + ainfo.LastLoginTime.ToString() +"\",\"CreateAdminName\":\"" + (admin.GetModel(ainfo.CreateAdminID) == null ? "系统创建" : admin.GetModel(ainfo.CreateAdminID).AdminName) + "\",\"CreateTime\":\"" + ainfo.CreateTime.ToString() + "\"}";
        }
        /// <summary>
        /// 删除管理员
        /// </summary>
        public void DeleteAdmin()
        {
            string adminids = RequestHelper.GetRequestStr("adminids", "0");
            admin.DeleteList(adminids);
            new SysLog().Add(new SysLogInfo("管理员管理", "删除", "删除了编号为【" + adminids + "】的管理员信息。", Function.GetIP(), Function.AdminInfo.AdminID, Function.AdminInfo.AdminName, DateTime.Now));
            AjaxMsg.msg = "\"msg\":\"删除成功\"";
        }       
        /// <summary>
        /// 查询管理员
        /// </summary>
        /// <param name="adminid"></param>
        public void SearchAdmin()
        {
            string adminname = RequestHelper.GetRequestStr("adminname", "");
            int groupid = RequestHelper.GetRequestInt("groupid", 0);
            string starttime = RequestHelper.GetRequestStr("starttime", "");
            string endtime = RequestHelper.GetRequestStr("endtime", "");
            StringBuilder sbSql = new StringBuilder(" 1=1 ");
            if (adminname != "")
                sbSql.AppendFormat(" and a.AdminName like '%{0}%' ", adminname);
            if (groupid != 0)
                sbSql.AppendFormat(" and a.GroupID={0}", groupid);
            if (starttime != "")
                sbSql.AppendFormat(" and datediff(d,'{0}',a.CreateTime) >= 0 ", starttime);
            if (endtime != "")
                sbSql.AppendFormat(" and datediff(d,'{0}',a.CreateTime) <= 0 ", endtime);
            DataSet ds = admin.GetList(sbSql.ToString());
            AjaxMsg.msg = "\"rows\":" + JsonHelper.ToJson(ds.Tables[0], "") + ",\"total\":" + ds.Tables[0].Rows.Count;
        }
        /// <summary>
        /// 更新管理员登录ip及最后登录时间
        /// </summary>
        /// <param name="adminid"></param>
        public void UpdateAdminIpAndTime(int adminid)
        {
            admin.UpdateIPAndTime(Function.GetIP(), DateTime.Now, adminid);   
        }
        /// <summary>
        /// 添加/修改管理员
        /// </summary>
        public void ModifyAdminModel()
        {
            int adminid = RequestHelper.GetRequestInt("adminid", 0);
            string adminname = RequestHelper.GetRequestStr("adminname", "");
            AdminInfo ainfo = new AdminInfo();
            ainfo = admin.GetModel(adminid);

            if (ainfo != null)//添加
            {
                ainfo.AdminName = adminname;
                admin.Update(ainfo);
                new SysLog().Add(new SysLogInfo("修改帐号信息", "修改", "添加账号为【" + ainfo.AdminAccount + "】的管理员信息", Function.GetIP(), Function.AdminInfo.AdminID, Function.AdminInfo.AdminName, DateTime.Now));
                AjaxMsg.msg = "\"msg\":\"修改成功！\"";
            }
            else
            {
                AjaxMsg.msgOK = false;
                AjaxMsg.msg = "\"msg\":\"修改失败！\"";
            }
        }
    }
}