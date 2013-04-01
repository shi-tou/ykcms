<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminList.aspx.cs" Inherits="YkCms.User.AdminList" %>
<html>
    <head>
        <link href="/jquery-easyui-1.3.2/themes/default/easyui.css" rel="stylesheet" type="text/css" />
        <link href="/jquery-easyui-1.3.2/themes/icon.css" rel="stylesheet" type="text/css" />
        <link href="../public/css/admin.css" rel="stylesheet" type="text/css" />
        <script src="/jquery-easyui-1.3.2/jquery-1.8.0.min.js" type="text/javascript"></script>
        <script src="/jquery-easyui-1.3.2/jquery.easyui.min.js" type="text/javascript"></script>
        <script src="../public/js/customValidate.js" type="text/javascript"></script>
        <script src="../public/js/msgbox.js" type="text/javascript"></script>
        <script src="../public/js/common.js" type="text/javascript"></script>
        <script src="public/js/admin.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(function () {
                getAdminList();
            });
        </script>
    </head>
    <body>
        <div class="main">
            <!--start 搜索-->
            <div class="easyui-panel search" title="条件搜索">
                <table>
                    <tr>
                        <td>管理员姓名：</td>
                        <td><input id="search-adminlist-adminname" name="search-adminlist-adminname" class="tb" /></td>
                        <td>所属权限组：</td>
                        <td><select id="search-adminlist-groupid" name="search-adminlist-groupid"><%=GroupSelectHtml %></select></td>
                        <td>添加时间：</td>
                        <td><input id="search-adminlist-starttime" name="search-adminlist-starttime" class="easyui-datebox" />-<input id="search-adminlist-endtime" name="search-adminlist-endtime" class="easyui-datebox" /></td>
                        <td><a class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="searchAdmin();">查询</a></td>
                    </tr>
                </table>
            </div>
            <!--end 搜索-->
            <!--start 占位-->
            <div class="blank"></div>
            <!--end 占位-->
            <!--start 列表-->
            <table id="adminList">
            </table>
            <!--end 列表-->
            <!--start 表单窗口-->
            <div id="adminWin" class="easyui-window" title="添加管理员" data-options="iconCls:'icon-save',modal:true,top:'20px',closed:true,minimizable:false,maximizable:false,collapsible:false" >
                <form id="adminForm">
                <input id="adminlist-adminid" name="adminlist-adminid" type="hidden" />
                <dl>
                    <dt>管理帐号：</dt>
                    <dd><input class="easyui-validatebox" type="text" id="adminlist-adminaccount" name="adminlist-adminaccount" data-options="required:true" missingmessage="请输入管理帐号！" validtype="checkAdminAccount" /></dd>
                    <dt>管理密码：</dt>
                    <dd><input class="easyui-validatebox" type="password" id="adminlist-adminpwd" name="adminlist-adminpwd" data-options="required:true" missingmessage="请输入管理密码！" validType="safepass" /></dd>
                    <dt>确认密码：</dt>
                    <dd><input class="easyui-validatebox" type="password" id="adminlist-adminpwd0" name="adminlist-adminpwd0" data-options="required:true" missingmessage="请输入确认密码！" validType="equalTo['#adminlist-adminpwd']" /></dd>
                    <dt>管理员姓名：</dt>
                    <dd><input class="easyui-validatebox" type="text" id="adminlist-adminname" name="adminlist-adminname" data-options="required:true" missingmessage="请输入姓名！" /></dd>
                    <dt>所属管理组：</dt>
                    <dd><select id="adminlist-groupid" name="adminlist-groupid"><%=GroupSelectHtml %></select></dd>
                    <dt>状态：</dt>
                    <dd><input type="radio" id="adminlist-open" name="adminlist-state" value="1" checked="checked" />启用<input type="radio" id="adminlist-close" name="adminlist-state" value="0" />禁用</dd>
                    <dt>描述：</dt>
                    <dd><textarea id="adminlist-admindesc" name="adminlist-admindesc" class="textarea" /></dd>
                </dl>
                </form>
                <div class="blank"></div>
                <div style="text-align: center; padding: 5px">
                    <a id="adminModelAdd" class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="addAdminModel();" >确 定</a>
                    <a id="resetadminForm" class="easyui-linkbutton" data-options="iconCls:'icon-no'" onclick="cancelAdminWin();" >取 消</a>
                </div>
            </div>
            <!--end 表单窗口-->
        </div>
    </body>
</html>