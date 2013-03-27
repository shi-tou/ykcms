<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysLog.aspx.cs" Inherits="YkCms.Sys.SysLog" %>
<!--start 脚本-->
<script src="public/js/syslog.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        getSyslogList();
    });
</script>
<!--end 脚本-->
<div class="main">
    <!--start 搜索-->
    <div class="easyui-panel search" title="条件搜索" data-options="iconCls:'icon-search'">
        <table>
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
