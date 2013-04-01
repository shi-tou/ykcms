<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminGroup.aspx.cs" Inherits="YkCms.User.AdminGroup" %>
<html>
    <head>
        <title></title>
        <link href="/jquery-easyui-1.3.2/themes/default/easyui.css" rel="stylesheet" type="text/css" />
        <link href="/jquery-easyui-1.3.2/themes/icon.css" rel="stylesheet" type="text/css" />
        <link href="../public/css/admin.css" rel="stylesheet" type="text/css" />
        <script src="/jquery-easyui-1.3.2/jquery-1.8.0.min.js" type="text/javascript"></script>
        <script src="/jquery-easyui-1.3.2/jquery.easyui.min.js" type="text/javascript"></script>
        <script src="../public/js/customValidate.js" type="text/javascript"></script>
        <script src="../public/js/msgbox.js" type="text/javascript"></script>
        <script src="../public/js/common.js" type="text/javascript"></script>
        <script src="../public/js/group.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(function () {
                getGroupList();
            });
        </script>
    </head>
    <body>
        <div class="main">
            <!--start 搜索-->
            <div class="easyui-panel search" title="条件搜索">
                <table style=" line-height:">
                    <tr>
                        <td>权限名称：</td>
                        <td><input id="search-admingroup-groupname" name="search-admingroup-groupname" class="tb" /></td>
                        <td>添加时间：</td>
                        <td><input id="search-admingroup-starttime" name="search-admingroup-starttime" class="easyui-datebox" />-<input id="search-admingroup-endtime" name="search-admingroup-endtime" class="easyui-datebox" /></td>
                        <td><a class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="searchGroup();">查询</a></td>
                    </tr>
                </table>
            </div>
            <!--end 搜索-->
            <!--start 占位-->
            <div class="blank"></div>
            <!--end 占位-->
            <!--start 列表-->
            <table id="groupList">
            </table>
            <!--end 列表-->
            <!--start 表单窗口-->
            <div id="groupWin" class="easyui-window" title="添加权限组" data-options="iconCls:'icon-save',top:'20px',modal:true,closed:true,minimizable:false,maximizable:false,collapsible:false" >
                <form id="groupForm">
                <input id="admingroup-groupid" name="admingroup-groupid" type="hidden" />
                <dl>
                    <dt>权限组名称:</dt>
                    <dd><input class="easyui-validatebox" type="text" id="admingroup-groupname" name="admingroup-groupname" data-options="required:true" missingmessage="请输入权限组名称！" validtype="checkGroupName" /></dd>
                    <dt>权限组描述:</dt>
                    <dd><textarea class="textarea" id="admingroup-describe" name="admingroup-describe" ></textarea></dd>
                    <dt>权限组分配:</dt>
                    <dd>
                        <input id="p-1" class="easyui-validatebox" type="checkbox" name="admingroup-groupauth" value="1" />管理员管理<br />
                        <input id="p-1-1" class="easyui-validatebox" type="checkbox" name="admingroup-groupauth" value="1-1" />管理员添加
                        <input id="p-1-2" class="easyui-validatebox" type="checkbox" name="admingroup-groupauth" value="1-2" />管理员修改
                        <input id="p-1-3" class="easyui-validatebox" type="checkbox" name="admingroup-groupauth" value="1-3" />管理员删除<br />
                        <input id="p-2" class="easyui-validatebox" type="checkbox" name="admingroup-groupauth" value="2" />权限组管理<br />
                        <input id="p-2-1" class="easyui-validatebox" type="checkbox" name="admingroup-groupauth" value="2-1" />权限组添加
                        <input id="p-2-2" class="easyui-validatebox" type="checkbox" name="admingroup-groupauth" value="2-2" />权限组修改
                        <input id="p-2-3" class="easyui-validatebox" type="checkbox" name="admingroup-groupauth" value="2-3" />权限组删除
                    </dd>
                </dl>
                </form>
                <div class="blank"></div>
                <div style="text-align: center; padding: 5px">
                    <a id="groupModelAdd" class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="addGroupModel();" >确 定</a>
                    <a id="resetGroupForm" class="easyui-linkbutton" data-options="iconCls:'icon-no'" onclick="cancelGroupWin();" >取 消</a>
                </div>
            </div>
            <!--end 表单窗口-->
        </div>
    </body>
</html>