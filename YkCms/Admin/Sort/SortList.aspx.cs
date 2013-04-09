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
        public string SortSelectHtml = "";
        public string SortAttributeSelectHtml = "";
        public string SortTemplateSelectHtml = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SortSelectHtml = new AjaxSort().GetSortSelectHtml(true);
                SortAttributeSelectHtml = new AjaxSortAttribute().GetSortAttributeSelectHtml(true);
                SortTemplateSelectHtml = new AjaxTemplate().GetTemplateSelectHtml(true);
            }
        }
    }
}