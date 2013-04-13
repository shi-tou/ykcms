using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YkCms.AppCode;

namespace YkCms.Content
{
    public partial class GoodsList : System.Web.UI.Page
    {
        public string SortSelectHtml = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SortSelectHtml = new AjaxSort().GetSortSelectHtmlByID(2);
        }
    }
}