using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YkCms.AppCode;

namespace YkCms
{
    public partial class SortList : System.Web.UI.Page
    {
        public string SortAttributeSelectHtml = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SortAttributeSelectHtml = new AjaxSortAttribute().GetSortAttributeSelectHtml(true);
            }
        }
    }
}