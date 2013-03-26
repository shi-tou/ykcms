using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YkCms.AppCode;

namespace YkCms.User
{
    public partial class AdminList : System.Web.UI.Page
    {
        public string GroupSelectHtml = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GroupSelectHtml = new AjaxGroup().GetGroupSelectHtml(true);
            }
        }      
    }
}