<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TemplateList.aspx.cs" Inherits="YkCms.TemplateList" %>

<%@ Register Assembly="CKEditor.Net" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<!--start 脚本-->
<script src="public/js/customValidate.js" type="text/javascript"></script>
<script src="public/js/template.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
       getTemplateList();
    });
</script>
<!--end 脚本-->
<div class="main">
    <!--start 搜索-->
    <div class="easyui-panel search" title="条件搜索">
        <table>
            <tr>
                <td>模板名称：</td>
                <td><input id="search-templatelist-templatename" name="search-templatelist-templatename" class="tb" /></td>
                <td>添加时间：</td>
                <td><input id="search-templatelist-starttime" name="search-templatelist-starttime" class="easyui-datebox" />-<input id="search-templatelist-endtime" name="search-templatelist-endtime" class="easyui-datebox" /></td>
                <td><a class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="searchTemplate();">查询</a></td>
            </tr>
        </table>
    </div>
    <!--end 搜索-->
    <!--start 占位-->
    <div class="blank"></div>
    <!--end 占位-->
    <!--start 列表-->
    <table id="templateList">
    </table>
    <!--end 列表-->
    <!--start 表单窗口-->
    <div id="templateWin" class="easyui-window" title="添加模板" data-options="iconCls:'icon-save',modal:true,closed:true,minimizable:false,maximizable:false,collapsible:false" >
        <form id="templateForm" runat="server">
        <input id="templatelist-templateid" name="templatelist-templateid" type="hidden" />
        <dl>
            <dt>模板名称:</dt>
            <dd><input class="easyui-validatebox" type="text" id="templatelist-templatename" name="templatelist-templatename" data-options="required:true" missingmessage="请输入权限组名称！" validtype="checkTemplateName" /></dd>
            <dt>模板名称:</dt>
            <dd><input class="easyui-validatebox" type="text" id="templatelist-templateurl" name="templatelist-templateurl" /></dd>
            <dt>模板描述:</dt>
            <dd><textarea class="textarea" id="templatelist-templatedesc" name="templatelist-templatedesc"></textarea></dd>
            <dt>模板内容</dt>
            <dd><CKEditor:CKEditorControl ID="templatesource" Name="templatesource" BasePath="/ckeditor" runat="server"></CKEditor:CKEditorControl></dd>
        </dl>
        </form>
        <div class="blank"></div>
        <div style="text-align: center; padding: 5px">
            <a class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="addTemplateModel();" >确 定</a>
            <a class="easyui-linkbutton" data-options="iconCls:'icon-no'" onclick="cancelTemplateWin();" >取 消</a>
        </div>
    </div>
    <!--end 表单窗口-->
</div>