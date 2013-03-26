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
                    case "getSyslogList":
                        new AjaxSyslog().GetSyslogList();
                        break;
                    case "deleteSyslog":
                        new AjaxSyslog().DeleteSyslog();
                        break;
                    case "searchSyslog":
                        new AjaxSyslog().SearchSyslog();
                        break;
                    #endregion

                    #region 栏目属性管理
                    //获取栏目属性列表
                    case "checkSortAttributeName":
                        new AjaxSortAttribute().GetSortAttributeList();
                        break;
                    ////获取栏目属性列表
                    //case "GetSortAttributeList":
                    //    new AjaxSortAttribute().GetSortAttributeList();
                    //    break;
                    ////添加栏目属性
                    //case "AddSortAttributeModel":
                    //    new AjaxSortAttribute().AddSortAttributeModel();
                    //    break;
                    ////获取属性实体
                    //case "GetSortAttributeModel":
                    //    new AjaxSortAttribute().GetSortAttributeModel();
                    //    break;
                    ////修改栏目属性
                    //case "UpdateSortAttributeModel":
                    //    new AjaxSortAttribute().UpdateSortAttributeModel();
                    //    break;
                    ////删除栏目属性
                    //case "DelSortAttributeModel":
                    //    new AjaxSortAttribute().DeleteSortAttributeModel();
                    //    break;
                    #endregion

                    #region 栏目模板管理
                    ////获取栏目模板列表
                    //case "GetSortTemplateList":
                    //    new AjaxSortTemplate().GetSortTemplateList();
                    //    break;
                    ////添加栏目模板
                    //case "AddSortTemplateModel":
                    //    new AjaxSortTemplate().AddSortTemplateModel();
                    //    break;
                    ////获取模板实体
                    //case "GetSortTemplateModel":
                    //    new AjaxSortTemplate().GetSortTemplateModel();
                    //    break;
                    ////修改栏目模板
                    //case "UpdateSortTemplateModel":
                    //    new AjaxSortTemplate().UpdateSortTemplateModel();
                    //    break;
                    ////删除栏目模板
                    //case "DelSortTemplateModel":
                    //    new AjaxSortTemplate().DeleteSortTemplateModel();
                    //    break;
                    #endregion

                    #region 栏目管理
                    ////获取栏目列表
                    //case "GetSortList":
                    //    new AjaxSort().GetSortList();
                    //    break;
                    ////添加栏目
                    //case "AddSortModel":
                    //    new AjaxSort().AddSortModel();
                    //    break;
                    //case "GetSortModel":
                    //    new AjaxSort().GetSortModel();
                    //    break;
                    ////修改栏目
                    //case "UpdateSortModel":
                    //    new AjaxSort().UpdateSortModel();
                    //    break;
                    ////删除栏目（子级栏目将一起删除）
                    //case "DelSortModel":
                    //    new AjaxSort().DeleteSortModel();
                    //    break;
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

                    #region 系统操作日志
                    ////获取日志列表
                    //case "GetSysLogList":
                    //    new AjaxSysLog().GetSysLogList();
                    //    break;
                    ////删除日志
                    //case "DelSysLogList":
                    //    new AjaxSysLog().DelSysLogList();
                    //    break;
                    #endregion

                    #region 新闻管理
                    ////获取新闻类别列表
                    //case "GetNewsTypeList":
                    //    new AjaxNews().GetNewsTypeList();
                    //    break;
                    ////添加新闻类别
                    //case "AddNewsTypeModel":
                    //    new AjaxNews().AddNewsTypeModel();
                    //    break;
                    ////批量删除新闻类别
                    //case "DelNewsTypeList":
                    //    new AjaxNews().DelNewsTypeList();
                    //    break;
                    ////获取新闻类别信息
                    //case "GetNewsTypeModel":
                    //    new AjaxNews().GetNewsTypeModel();
                    //    break;
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
                    ////获取产品类别列表
                    //case "GetProductTypeList":
                    //    new AjaxProducts().GetProductTypeList();
                    //    break;
                    ////添加产品类别
                    //case "AddProductTypeModel":
                    //    new AjaxProducts().AddProductTypeModel();
                    //    break;
                    ////批量删除新闻类别
                    //case "DelProductTypeList":
                    //    new AjaxProducts().DelProductTypeList();
                    //    break;
                    ////获取新闻类别信息
                    //case "GetProductTypeModel":
                    //    new AjaxProducts().GetProductTypeModel();
                    //    break;
                    ////修改新闻类别信息
                    //case "EditProductTypeModel":
                    //    new AjaxProducts().EditProductTypeModel();
                    //    break;
                    ////分页获取产品列表
                    //case "GetProductsList":
                    //    new AjaxProducts().GetProductsList();
                    //    break;
                    ////添加产品
                    //case "AddProductsModel":
                    //    new AjaxProducts().AddProductsModel();
                    //    break;
                    ////批量删除产品
                    //case "DelProductsList":
                    //    new AjaxProducts().DelProductsList();
                    //    break;
                    ////修改产品
                    //case "EditProductsModel":
                    //    new AjaxProducts().EditProductsModel();
                    //    break;
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