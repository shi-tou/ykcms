<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SortAttribute.aspx.cs" Inherits="YkCms.Sort.SortAttribute" %>
<!--start 脚本-->
<script src="public/js/customValidate.js" type="text/javascript"></script>
<script src="public/js/group.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        getSortAttributeList();
    });
</script>
<!--end 脚本-->
<div class="main">
    <!--start 搜索-->
    <div class="easyui-panel search" title="条件搜索">
        <table>
            <tr>
                <td>栏目属性名称：</td>
                <td><input id="search-sortattribute-groupname" name="search-sortattribute-groupname" class="tb" /></td>
                <td>添加时间：</td>
                <td><input id="search-sortattribute-starttime" name="search-sortattribute-starttime" class="easyui-datebox" />-<input id="search-sortattribute-endtime" name="search-sortattribute-endtime" class="easyui-datebox" /></td>
                <td><a class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="searchGroup();">查询</a></td>
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
    <div id="sortattributeWin" class="easyui-window" title="添加栏目属性" data-options="iconCls:'icon-save',modal:true,closed:true,minimizable:false,maximizable:false,collapsible:false" >
        <form id="sortattributeForm">
        <input id="sortattribute-sortattributeid" name="sortattribute-sortattributeid" type="hidden" />
        <dl>
            <dt>栏目属性名称:</dt>
            <dd><input class="easyui-validatebox" type="text" id="sortattribute-sortattributename" name="sortattribute-groupname" data-options="required:true" missingmessage="请输入权限组名称！" validtype="checkSortAttributeName" /></dd>
            <dt>栏目属性描述:</dt>
            <dd><textarea class="textarea" id="sortattribute-sortattributedesc" name="sortattribute-sortattributedesc" /></dd>            
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
