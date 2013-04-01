<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="YkCms.Login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>云客CMS系统后台登录</title>
    <link href="../../jquery-easyui-1.3.2/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../jquery-easyui-1.3.2/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="public/css/login.css" rel="stylesheet" type="text/css" />    
    <script src="../jquery-easyui-1.3.2/jquery-1.8.0.min.js" type="text/javascript"></script>
    <script src="../jquery-easyui-1.3.2/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="public/js/msgbox.js" type="text/javascript"></script>
    <script src="public/js/common.js" type="text/javascript"></script>
    <script src="public/js/admin.js" type="text/javascript"></script>
</head>
<body style=" background:#fff;">
    <div class="login">
        <div class="easyui-panel" data-options="iconCls:'icon-tip'" title="管理后台登录">
            <form id="loginForm" method="post">
            <dl>
                <dt style="color:Red;">初始账号：</dt><dd style="color:Red;">admin/admin</dd>
                <dt>管理账号：</dt>
                <dd><input name="adminaccount" id="adminaccount" type="text" value="admin" class="text easyui-validatebox" data-options="required:true" missingMessage="请输入管理账号" /></dd>
                <dt>管理密码：</dt>
                <dd><input name="adminpwd" id="adminpwd" type="password" value="admin" class="text easyui-validatebox" data-options="required:true" missingMessage="请输入管理密码" /></dd>
            </dl>
            </form>
            <dl>
                <dt></dt>
                <dd>
                    <a class="easyui-linkbutton" onclick="adminLogin();" >登 录</a>
                    <a class="easyui-linkbutton" onclick="resetLoginForm();" >重 置</a>
                    <span id="errMsg" class="err"></span>  
                </dd>
            </dl>
        </div>
        <div class="copyright">版权所有-云客科技工作室 Copyright©2013 http://www.yunksoft.com</div>
    </div>
</body>
</html>
