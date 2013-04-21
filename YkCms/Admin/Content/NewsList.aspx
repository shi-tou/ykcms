<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsList.aspx.cs" Inherits="YkCms.Content.NewsList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/jquery-easyui-1.3.2/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/jquery-easyui-1.3.2/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../public/css/admin.css" rel="stylesheet" type="text/css" />
    <script src="/jquery-easyui-1.3.2/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="/jquery-easyui-1.3.2/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../../ckeditor/ckeditor.js" type="text/javascript"></script>
    <script src="../public/js/customValidate.js" type="text/javascript"></script>
    <script src="../public/js/msgbox.js" type="text/javascript"></script>
    <script src="../public/js/common.js" type="text/javascript"></script>
    <script src="../public/js/news.js?t=<%=DateTime.Now.ToString() %>" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            getNewsList();
            CKEDITOR.replace("content");
        });
        </script>
</head>
<body>
    <div class="main">
            <!--start 搜索-->
            <div class="easyui-panel search" title="条件搜索">
                <table class="searchtable">
                    <tr>
                        <td>类别：</td>
                        <td><select id="searchtypeid"><%=SortSelectHtml %></select></td>
                        <td>添加时间：</td>
                        <td><input id="search-sortlist-starttime" name="search-sortlist-starttime" class="easyui-datebox" />-<input id="search-sortlist-endtime" name="search-sortlist-endtime" class="easyui-datebox" /></td>
                        <td><a class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="searchSort();">查询</a></td>
                    </tr>
                </table>
            </div>
            <!--end 搜索-->
            <!--start 占位-->
            <div class="blank"></div>
            <!--end 占位-->
            <!--start 列表-->
            <table id="newsList">
	        </table>
            <!--end 列表-->
            <!--start 表单窗口-->
            <div id="newsWin" class="easyui-window" title="添加产品" data-options="iconCls:'icon-save',modal:true,top:'20px',closed:true,minimizable:false,maximizable:false,collapsible:false" >
                <form id="newsForm">
                <input id="newsid" name="newsid" type="hidden" />
                <div class="easyui-tabs" data-options="border:false">
                    <div title="基本信息" style="padding:10px">
                        <table class="table">
                            <tr>
                                <td style="width:100px;" class="r">新闻类别:</td>
                                <td><select id="typeid" name="typeid"><%=SortSelectHtml %></select></td>
                            </tr>
                            <tr>
                                <td class="r">文章标题:</td>
                                <td><input class="easyui-validatebox" type="text" id="title" name="title" data-options="required:true" missingmessage="请输入文章标题！" /></td>
                            </tr>
                            <tr>
                                <td style="width:100px;" class="r">页面标题:</td>
                                <td><textarea id="pagetitle" name="pagetitle" class="textarea"></textarea></td>
                            </tr>
                            <tr>
                                <td style="width:100px;" class="r">页面关键词:</td>
                                <td><textarea id="pagekeywords" name="pagekeywords" class="textarea"></textarea></td>
                            </tr>
                            <tr>
                                <td style="width:100px;" class="r">页面描述:</td>
                                <td><textarea id="pagedesc" name="pagedesc" class="textarea"></textarea></td>
                            </tr>
                            <tr>
                                <td class="r">状态</td>
                                <td><input type="radio" id="open" name="state" value="1" checked="checked" />启用<input type="radio" id="close" name="state" value="0" />禁用</td>
                            </tr>                  
                        </table>
                    </div>
                    <div title="文章内容" style="padding:10px">
                        <textarea  cols="90" rows="5" id="content" name="content"></textarea>
                    </div>
                </div>
                </form>
                <div class="blank"></div>
                <div style="text-align: center; padding: 5px">
                    <a class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="addNewsModel();" >确 定</a>
                    <a class="easyui-linkbutton" data-options="iconCls:'icon-no'" onclick="cancelNewsWin();" >取 消</a>
                </div>
            </div>
            <!--end 表单窗口-->
        </div>
</body>
</html>
