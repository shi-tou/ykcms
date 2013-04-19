using System;
using System.Collections.Generic;
using System.Web;
using YK.Model;
using YK.BLL;
using YK.Common;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace YkCms.AppCode
{
    /// <summary>
    /// 文件：AjaxGroup
    /// 描述：文章操作类
    /// 创建人：杨良斌
    /// </summary>
    public class AjaxNews
    {
        AdminInfo adminInfo = Function.GetCookiAdmin();
        News news = new News();
        SysLog log = new SysLog();
        /// <summary>
        /// 添加/修改文章
        /// </summary>
        public void AddNewsModel()
        {
            int newsid = RequestHelper.GetRequestInt("newsid", 0);
            int typeid = RequestHelper.GetRequestInt("typeid", 0);
            string title = RequestHelper.GetRequestStr("title", "");
            string content = RequestHelper.GetRequestStr("content", "");
            int state = RequestHelper.GetRequestInt("state", 0);
            string pagetitle = RequestHelper.GetRequestStr("pagetitle", "");
            string pagekeywords = RequestHelper.GetRequestStr("pagekeywords", "");
            string pagedesc = RequestHelper.GetRequestStr("pagedesc", "");
            NewsInfo ninfo = new NewsInfo();
            ninfo.TypeID = typeid;
            ninfo.Title = title;
            ninfo.Content = content;
            ninfo.State = state;
            
            ninfo.PageTitle = pagetitle;
            ninfo.PageKeyword = pagekeywords;
            ninfo.PageDesc = pagedesc;
            ninfo.Author = adminInfo.AdminName;
            ninfo.AdminID = adminInfo.AdminID;
            ninfo.CreateTime = DateTime.Now;
            if (newsid == 0)//添加
            {
                news.Add(ninfo);
                log.Add(new SysLogInfo("文章管理", "添加", "添加标题为【" + title + "】的文章信息", Function.GetIP(), adminInfo.AdminID, adminInfo.AdminName, DateTime.Now));
                AjaxMsg.msg = "\"msg\":\"添加成功\"";
            }
            else//修改
            {
                ninfo.NewsID = newsid;
                news.Update(ninfo);
                log.Add(new SysLogInfo("文章管理", "修改", "修改编号为【" + newsid + "】的文章名称为", Function.GetIP(), adminInfo.AdminID, adminInfo.AdminName, DateTime.Now));
                AjaxMsg.msg = "\"msg\":\"修改成功\"";
            }
        }
        /// <summary>
        /// 获取文章列表
        /// </summary>
        public void GetNewsList()
        {
            DataTable dt = news.GetNewsFroJoin("");
            AjaxMsg.msg = "\"rows\":" + JsonConvert.SerializeObject(dt, new DataTableConverter()) + ",\"total\":" + dt.Rows.Count;
        }
        /// <summary>
        /// 删除新闻
        /// </summary>
        public void DeleteNews()
        {
            string newsids = RequestHelper.GetRequestStr("newsids", "0");
            news.DeleteList(newsids);
            new SysLog().Add(new SysLogInfo("文章管理", "删除", "删除了编号为【" + newsids + "】的文章信息。", Function.GetIP(), adminInfo.AdminID, adminInfo.AdminName, DateTime.Now));
            AjaxMsg.msg = "\"msg\":\"删除成功\"";
        }
        /// <summary>
        /// 获取一条新闻
        /// </summary>
        public void GetNewsModel()
        {
            string newsid = RequestHelper.GetRequestStr("newsid", "0");
            DataTable dt=news.GetList("NewsID=" + newsid).Tables[0];
            if (dt.Rows.Count == 1)
            {
                AjaxMsg.msg = "\"msg\":" + JsonConvert.SerializeObject(dt, new DataTableConverter()).Replace("[", "").Replace("]", "");
            }
            else
            {
                AjaxMsg.msgOK = false;
                AjaxMsg.ex = "\"msg\":\"数据不存在！\"";
            }
            
        }
    }
}