<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysLog.aspx.cs" Inherits="YkCms.Sys.SysLog" %>
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
        <script src="../public/js/syslog.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(function () {
                getSyslogList();
            });
        </script>
    </head>
    <body>
        <!--end 脚本-->
        <div class="main">
            <!--start 搜索-->
            <div class="easyui-panel search" title="条件搜索" data-options="iconCls:'icon-search'">
                <table class="searchtable">
                    <tr>
                        <td>关键字：</td>
                        <td><input id="search-syslog-key" name="search-syslog-key" class="tb" /></td>
                        <td>操作时间：</td>
                        <td><input id="search-syslog-starttime" name="search-syslog-starttime" class="easyui-datebox" />-<input id="search-syslog-endtime" name="search-syslog-endtime" class="easyui-datebox" /></td>
                        <td><a class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="searchSyslog();">查询</a></td>
                    </tr>
                </table>
            </div>
            <!--end 搜索-->
            <!--start 占位-->
            <div class="blank"></div>
            <!--end 占位-->
            <!--start 列表-->
            <table id="syslogList" data-options="rownumbers:true,singleSelect:true,pagination:true">
            </table>
            <!--end 列表-->
            <!--start 占位-->
            <div class="blank"></div>
            <!--end 占位-->
        </div>
    </body>
</html>