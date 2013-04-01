<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SortAttributeList.aspx.cs" Inherits="YkCms.SortAttributeList" %>
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
        <script src="../public/js/sortattribute.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(function () {
                getSortAttributeList();
            });
        </script>
    </head>
    <body>
        <div class="main">
            <!--start 搜索-->
            <div class="easyui-panel search" title="条件搜索">
                <table>
                    <tr>
                        <td>栏目属性名称：</td>
                        <td><input id="search-sortattribute-sortattributename" name="search-sortattribute-sortattributename" class="tb" /></td>
                        <td>添加时间：</td>
                        <td><input id="search-sortattribute-starttime" name="search-sortattribute-starttime" class="easyui-datebox" />-<input id="search-sortattribute-endtime" name="search-sortattribute-endtime" class="easyui-datebox" /></td>
                        <td><a class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="searchSortAttribute();">查询</a></td>
                    </tr>
                </table>
            </div>
            <!--end 搜索-->
            <!--start 占位-->
            <div class="blank"></div>
            <!--end 占位-->
            <!--start 列表-->
            <table id="sortattributeList">
            </table>
            <!--end 列表-->
            <!--start 表单窗口-->
            <div id="sortattributeWin" class="easyui-window" title="添加栏目属性" data-options="iconCls:'icon-save',modal:true,top:'20px',closed:true,minimizable:false,maximizable:false,collapsible:false" >
                <form id="sortattributeForm">
                <input id="sortattribute-sortattributeid" name="sortattribute-sortattributeid" type="hidden" />
                <dl>
                    <dt>栏目属性名称:</dt>
                    <dd><input class="easyui-validatebox" type="text" id="sortattribute-sortattributename" name="sortattribute-sortattributename" data-options="required:true" missingmessage="请输入权限组名称！" validtype="checkSortAttributeName" /></dd>
                    <dt>栏目属性描述:</dt>
                    <dd><textarea class="textarea" id="sortattribute-sortattributedesc" name="sortattribute-sortattributedesc" ></textarea></dd>            
                </dl>
                </form>
                <div class="blank"></div>
                <div style="text-align: center; padding: 5px">
                    <a class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="addSortAttributeModel();" >确 定</a>
                    <a class="easyui-linkbutton" data-options="iconCls:'icon-no'" onclick="cancelSortAttributeWin();" >取 消</a>
                </div>
            </div>
            <!--end 表单窗口-->
        </div>
    </body>
</html>