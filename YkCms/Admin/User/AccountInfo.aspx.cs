using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YkCms.AppCode;

namespace YkCms.User
{
    public partial class AccountInfo : System.Web.UI.Page
    {
        public string GroupSelectHtml = "";
        public int AdminID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            GroupSelectHtml = new AjaxGroup().GetGroupSelectHtml(true);
            AdminID = Function.GetCookiAdmin().AdminID;
        }
    }
}