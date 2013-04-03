<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifyTemplate.aspx.cs" Inherits="YkCms.ModifyTemplate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/jquery-easyui-1.3.2/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/jquery-easyui-1.3.2/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../public/css/admin.css" rel="stylesheet" type="text/css" />
    <script src="/jquery-easyui-1.3.2/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="/jquery-easyui-1.3.2/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../public/js/customValidate.js?t=<%=DateTime.Now.ToString() %>" type="text/javascript"></script>
    <script src="../public/js/msgbox.js" type="text/javascript"></script>
    <script src="../public/js/common.js" type="text/javascript"></script>
    <script src="../public/js/template.js?t=<%=DateTime.Now.ToString() %>" type="text/javascript"></script>
</head>
<body>
    <div class="main">
        <form id="templateForm" runat="server">
        <input id="templatelist-templateid" name="templatelist-templateid" type="hidden" />
        <div class="easyui-tabs" data-options="border:false">
            <div title="基本信息" style="padding: 10px">
                <dl>
                    <dt>模板名称:</dt>
                    <dd>
                        <input class="easyui-validatebox" type="text" id="templatelist-templatename" name="templatelist-templatename" data-options="required:true" missingmessage="请输入权限组名称！" validtype="checkTemplateName" /></dd>
                    <dt>模板Url:</dt>
                    <dd>
                        <input class="easyui-validatebox" type="text" id="templatelist-templateurl" name="templatelist-templateurl" /></dd>
                    <dt>模板描述:</dt>
                    <dd>
                        <textarea class="textarea" id="templatelist-templatedesc" name="templatelist-templatedesc"></textarea></dd>
                </dl>
            </div>
            <div title="模板内容" style="padding: 10px">
                <CKEditor:CKEditorControl ID="templatesource" Name="templatesource" BasePath="/ckeditor" runat="server"></CKEditor:CKEditorControl>
            </div>
        </div>
        </form>
    </div>
</body>
</html>
