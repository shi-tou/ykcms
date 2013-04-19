using System;
using System.Collections.Generic;
using System.Web;
using YkCms.AppCode;
using System.Web.SessionState;
using YK.Common;
namespace YkCms.ajax
{
    /// <summary>
    /// ajax 的摘要说明
    /// </summary>
    public class ajax : IHttpHandler,IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string strJson = "";
            string method = "";
            try
            {
                method = context.Request["action"].ToString();
                switch (method)
                {
                    #region 登录/退出
                    //登录系统
                    case "login":
                        new AjaxAdmin().Login();
                        break;
                    //退出系统
                    case "loginOut":
                        new AjaxAdmin().Logout();
                        break;
                    #endregion

                    #region 权限组
                    //检查权限组名称是否重复
                    case "checkGroupName":
                        new AjaxGroup().CheckGroupName();
                        break;
                    //添加权限组
                    case "addGroupModel":
                        new AjaxGroup().AddGroupModel();
                        break;
                    //获取管理组列表
                    case "getGroupList":
                        new AjaxGroup().GetGroupList();
                        break;                                      
                    //批量删除管理组
                    case "deleteGroup":
                        new AjaxGroup().DeleteGroup();
                        break;
                    //获取管理组
                    case "getGroupModel":
                        new AjaxGroup().GetGroupModel();
                        break;
                    //获取管理组
                    case "searchGroup":
                        new AjaxGroup().SearchGroup();
                        break;                    
                    #endregion

                    #region 管理员
                    //检查检查管理员帐号是否重复
                    case "checkAdminAccount":
                        new AjaxAdmin().checkAdminAccount();
                        break;
                    //添加/修改管理员
                    case "addAdminModel":
                        new AjaxAdmin().AddAdminModel();
                        break;
                    //获取管理员列表
                    case "getAdminList":
                        new AjaxAdmin().GetAdminList();
                        break;
                    //获取管理员列表
                    case "getAdminModel":
                        new AjaxAdmin().GetAdminModel();
                        break;
                    //删除管理员
                    case "deleteAdmin":
                        new AjaxAdmin().DeleteAdmin();
                        break;
                    //获取管理组
                    case "searchAdmin":
                        new AjaxAdmin().SearchAdmin();
                        break;
                    //获取管理组
                    case "modifyAdminModel":
                        new AjaxAdmin().ModifyAdminModel();
                        break;
                    #endregion

                    #region 日志管理
                    //获取日志列表
                    case "getSyslogList":
                        new AjaxSyslog().GetSyslogList();
                        break;
                    //删除日志
                    case "deleteSyslog":
                        new AjaxSyslog().DeleteSyslog();
                        break;
                    //搜索日志
                    case "searchSyslog":
                        new AjaxSyslog().SearchSyslog();
                        break;
                    #endregion

                    #region 栏目属性管理
                    //获取栏目属性列表
                    case "checkSortAttributeName":
                        new AjaxSortAttribute().GetSortAttributeList();
                        break;
                    //获取栏目属性列表
                    case "getSortAttributeList":
                        new AjaxSortAttribute().GetSortAttributeList();
                        break;
                    //添加/修改栏目属性
                    case "addSortAttributeModel":
                        new AjaxSortAttribute().AddSortAttributeModel();
                        break;
                    //获取栏目属性实体
                    case "getSortAttributeModel":
                        new AjaxSortAttribute().GetSortAttributeModel();
                        break;
                    //删除栏目属性
                    case "deleteSortAttribute":
                        new AjaxSortAttribute().DeleteSortAttribute();
                        break;
                    //查询栏目属性
                    case "searchSortAttribute":
                        new AjaxSortAttribute().SearchSortAttribute();
                        break;
                    #endregion

                    #region 栏目模板管理
                    //获取模板名称列表
                    case "checkTemplateName":
                        new AjaxTemplate().CheckTemplateName();
                        break;
                    //获取栏目模板列表
                    case "getTemplateList":
                        new AjaxTemplate().GetTemplateList();
                        break;
                    //添加栏目模板
                    case "addTemplateModel":
                        new AjaxTemplate().AddTemplateModel();
                        break;
                    //获取模板实体
                    case "getTemplateModel":
                        new AjaxTemplate().GetTemplateModel();
                        break;
                    //删除栏目模板
                    case "deleteTemplate":
                        new AjaxTemplate().DeleteTemplate();
                        break;
                    #endregion

                    #region 栏目管理
                    //检查栏目名
                    case "checkSortName":
                        new AjaxSort().CheckSortName();
                        break;
                    //获取栏目列表
                    case "getSortList":
                        new AjaxSort().GetSortList();
                        break;
                    //添加栏目
                    case "addSortModel":
                        new AjaxSort().AddSortModel();
                        break;
                    //获取栏目单个信息
                    case "getSortModel":
                        new AjaxSort().GetSortModel();
                        break;
                    //修改栏目
                    //case "UpdateSortModel":
                    //    new AjaxSort().UpdateSortModel();
                    //    break;
                    //删除栏目（子级栏目将一起删除）
                    case "deleteSort":
                        new AjaxSort().DeleteSort();
                        break;
                    #endregion

                    #region 数据字典管理
                    ////获取数据字典列表
                    //case "GetDictionaryList":
                    //    new AjaxDictionary().GetDictionaryList();
                    //    break;
                    ////添加数据字典
                    //case "AddDictionaryModel":
                    //    new AjaxDictionary().AddDictionaryModel();
                    //    break;
                    ////获取数据字典实体
                    //case "GetDictionaryModel":
                    //    new AjaxDictionary().GetDictionaryModel();
                    //    break;
                    ////修改数据字典
                    //case "UpdateDictionaryModel":
                    //    new AjaxDictionary().UpdateDictionaryModel();
                    //    break;
                    ////删除数据字典
                    //case "DelDictionaryModel":
                    //    new AjaxSortTemplate().DeleteSortTemplateModel();
                    //    break;
                    #endregion

                    #region 新闻管理
                    //获取新闻列表
                    case "getNewsList":
                        new AjaxNews().GetNewsList();
                        break;
                    //添加新闻
                    case "addNewsModel":
                        new AjaxNews().AddNewsModel();
                        break;
                    ////批量删除新闻
                    //case "DelNewsTypeList":
                    //    new AjaxNews().DelNewsTypeList();
                    //    break;
                    //获取新闻类别信息
                    case "getNewsModel":
                        new AjaxNews().GetNewsModel();
                        break;
                    ////修改新闻类别信息
                    //case "EditNewsTypeModel":
                    //    new AjaxNews().EditNewsTypeModel();
                    //    break;
                    ////分页获取新闻列表
                    //case "GetNewsList":
                    //    new AjaxNews().GetNewsList();
                    //    break;
                    ////添加新闻
                    //case "AddNewsModel":
                    //    new AjaxNews().AddNewsModel();
                    //    break;
                    ////批量删除新闻
                    //case "DelNewsList":
                    //    new AjaxNews().DelNewsList();
                    //    break;
                    ////修改新闻
                    //case "EditNewsModel":
                    //    new AjaxNews().EditNewsModel();
                    //    break;
                    #endregion

                    #region 产品管理
                    //获取产品列表
                    case "getGoodsList":
                        new AjaxGoods().GetGoodsList();
                        break;
                    //添加产品
                    case "addGoodsModel":
                        new AjaxGoods().AddGoodsModel();
                        break;
                    //删除产品
                    case "deleteGoods":
                        new AjaxGoods().DeleteGoods();
                        break;
                    case "getGoodsModel":
                        new AjaxGoods().GetGoodsModel();
                        break;
                    #endregion

                    #region 友链管理
                    ////获取友链列表
                    //case "GetFriendLinkList":
                    //    new AjaxFriendLink().GetFriendLinkList();
                    //    break;
                    ////添加友链
                    //case "AddFriendLinkModel":
                    //    new AjaxFriendLink().AddFriendLinkModel();
                    //    break;
                    ////批量删除友链
                    //case "DelFriendLinkList":
                    //    new AjaxFriendLink().DelFriendLinkList();
                    //    break;
                    ////修改产品
                    //case "EditFriendLinkModel":
                    //    new AjaxFriendLink().EditFriendLinkModel();
                    //    break;
                    #endregion

                    default:
                        AjaxMsg.ex = "错误的action参数！";
                        AjaxMsg.msgOK = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                AjaxMsg.ex = JsonHelper.JsonCharFilter(ex.Message);
                AjaxMsg.msgOK = false;
            }
            //判断是否发生异常
            if (AjaxMsg.msgOK)
            {
                if (AjaxMsg.msg == "")
                    strJson = "{\"msgOK\":" + AjaxMsg.msgOK.ToString().ToLower() + "}";
                else
                    strJson = "{\"msgOK\":" + AjaxMsg.msgOK.ToString().ToLower() + "," + AjaxMsg.msg + "}";
            }
            else
            {
                if (AjaxMsg.ex == "")
                    strJson = "{\"msgOK\":" + AjaxMsg.msgOK.ToString().ToLower() + "}";
                else
                    strJson = "{\"msgOK\":" + AjaxMsg.msgOK.ToString().ToLower() + ",\"ex\":\"" + AjaxMsg.ex + "\"}";
            }
            AjaxMsg.Clear();
            context.Response.ContentType = "text/plain";
            context.Response.Write(strJson);
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