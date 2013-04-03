using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YK.Common;
using YK.Model;
using YK.BLL;

namespace YkCms
{
    public partial class ModifyTemplate : System.Web.UI.Page
    {
        public int TemplateID
        {
            get { return RequestHelper.GetRequestInt("tid", 0); }
        }
        public TemplateInfo tinfo = new TemplateInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTemplate();
            }
            this.templatesource.config.toolbar = new object[]{
                new object[] { "Source" },
                new object[] { "Cut", "Copy", "Paste", "PasteText", "PasteFromWord"},
                new object[] { "Undo", "Redo", "-", "Find", "Replace" },
                new object[] { "Bold", "Italic", "Underline", "Strike" }
            };
        }
        public void BindTemplate()
        {
            if (TemplateID != 0)
            {
                tinfo = new Template().GetModel(TemplateID);
            }
        }
    }
}