using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YK.Config;

namespace YkCms.Sys
{
    public partial class setting : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSetting();
            }
        }
        protected void BindSetting()
        {
            GlobalConfigInfo ginfo = GlobalConfig.GetConfig();
            if (ginfo.SiteState == 0)
                this.rd_StateOpen.Checked = true;
            else
                this.rd_StateClose.Checked = true;
            this.tb_SiteName.Text = ginfo.SiteName;
            this.tb_SiteUrl.Text = ginfo.SiteUrl;
            this.tb_SiteTitle.Text = ginfo.SiteTitle;
            this.tb_SiteKeywords.Text = ginfo.SiteKeywords;
            this.tb_SiteDescription.Text = ginfo.SiteDescription;
            this.tb_SiteMailServer.Text = ginfo.SiteMailServer;
            this.tb_SiteMailAddress.Text = ginfo.SiteMailAddress;
            this.tb_SiteMailPassword.Text = YK.Common.DEncrypt.Decrypt(ginfo.SiteMailPassword);
        }

        protected void SaveConfig_Click(object sender, EventArgs e)
        {
            GlobalConfigInfo ginfo = GlobalConfig.GetConfig();
            ginfo.SiteState = this.rd_StateOpen.Checked ? 0 : 1;
            ginfo.SiteName = this.tb_SiteName.Text;
            ginfo.SiteUrl = this.tb_SiteName.Text;
            ginfo.SiteTitle = this.tb_SiteTitle.Text;
            ginfo.SiteKeywords = this.tb_SiteKeywords.Text;
            ginfo.SiteDescription = this.tb_SiteDescription.Text;
            ginfo.SiteMailServer = this.tb_SiteMailServer.Text;
            ginfo.SiteMailAddress = this.tb_SiteMailAddress.Text;
            ginfo.SiteMailPassword = YK.Common.DEncrypt.Encrypt(this.tb_SiteMailPassword.Text);
            GlobalConfig.SaveConfig(ginfo);
            //new YK.BLL.SysLog().Add(new YK.Model.SysLogInfo("网站基本设置", "修改", "修改了网站基本设置信息,IP地址：" + Function.GetIP(), Function.AdminInfo.AdminID, Function.AdminInfo.AdminName, DateTime.Now));
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "message", "<script language='javascript' defer>alert('保存成功！！');</script>");
        }
    }
}