<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="setting.aspx.cs" Inherits="YkCms.Sys.setting" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>系统配置</title>
    <link href="../css/main.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../js/bootstrap.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="main">
        <div class="title">系统配置</div>
        <div class="content">
            <dl>
                <dt> 网站状态：</dt>
                <dd>              
                    <asp:RadioButton ID="rd_StateOpen" runat="server" Text="开放" GroupName="SiteState" /><asp:RadioButton ID="rd_StateClose" runat="server" Text="关闭" GroupName="SiteState" />
                </dd>
                <dt>网站名称：</dt>
                <dd><asp:TextBox ID="tb_SiteName" runat="server" ></asp:TextBox></dd>
                <dt>网站地址：</dt>
                <dd><asp:TextBox ID="tb_SiteUrl" runat="server" ></asp:TextBox></dd>
                <dt>网站标题：</dt>
                <dd><asp:TextBox ID="tb_SiteTitle" runat="server" TextMode="MultiLine" style="width:380px; height:50px" ></asp:TextBox></dd>
                <dt>网站关键字：</dt>
                <dd><asp:TextBox ID="tb_SiteKeywords" runat="server" TextMode="MultiLine" style="width:380px;height:50px" ></asp:TextBox></dd>
                <dt>网站描述：</dt>
                <dd><asp:TextBox ID="tb_SiteDescription" runat="server" TextMode="MultiLine" style="width:380px;height:50px"></asp:TextBox></dd>
                <dt>收发邮件地址：</dt>
                <dd><asp:TextBox ID="tb_SiteMailAddress" type="email" runat="server" placeholder="Email"></asp:TextBox></dd>
                <dt>邮件服务器：</dt>
                <dd><asp:TextBox ID="tb_SiteMailServer" runat="server" ></asp:TextBox></dd>
                <dt>邮件密码：</dt>
                <dd><asp:TextBox ID="tb_SiteMailPassword" runat="server" TextMode="Password" ></asp:TextBox></dd>
                <dt></dt>
                <dd><asp:Button ID="SaveConfig" runat="server" class="btn btn-primary" Text="保存" OnClick="SaveConfig_Click" /><input type="button" class="btn btn-inverse" value="返回" /></dd>
            </dl>
        </div>
    </div>
    </form>
</body>
</html>
