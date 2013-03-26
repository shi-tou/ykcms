using System;
using System.Collections.Generic;
using System.IO;   
using System.Net;
using System.Web;
using YK.Common;
namespace YK_Cms.Manage.ajax
{
    public class UploadHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";

            HttpPostedFile file = context.Request.Files["Filedata"];
            string uploadPath = context.Request["folder"].ToString();
            string dir = context.Request.MapPath(uploadPath);
            if (file != null)
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                string imageName = DateTime.Now.ToString("yyyyMMddHHmmss") + Utils.GetFileExtName(file.FileName);
                file.SaveAs(dir + imageName);
                //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
                context.Response.Write(uploadPath + imageName);
            }
            else
            {
                context.Response.Write("");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
