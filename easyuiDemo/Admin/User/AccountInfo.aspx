<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountInfo.aspx.cs" Inherits="YkCms.User.AccountInfo" %>
<!--start 脚本-->
<script src="public/js/customValidate.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        getLoginAdminModel('<%=AdminID %>');
    });
</script>
<!--end 脚本-->
<div class="main">
    <!--start 搜索-->
    <div class="easyui-panel" title="当前登录管理员信息">
        <input type="hidden" id="accountinfo-adminid" name="accountinfo-adminid" />
        <dl id="accountinfo">
            <dt>管理员帐号：</dt>
            <dd><input id="accountinfo-adminaccount" name="accountinfo-adminaccount" class="tb readonly"  readonly="readonly" /></dd>
            <dt>管理员姓名：</dt>
            <dd><input id="accountinfo-adminname" name="accountinfo-adminname" class="tb" /></dd>
            <dt>帐号状态：</dt>
            <dd>
                <input type="radio" id="accountinfo-open" name="accountinfo-state" value="1" checked="checked" disabled="disabled" />启用
                <input type="radio" id="accountinfo-close" name="accountinfo-state" value="0"  readonly="readonly" disabled="disabled"  />禁用
            </dd>
            <dt>管理员描述：</dt>
            <dd><textarea class="textarea readonly" id="accountinfo-admindesc" name="accountinfo-admindesc" readonly="readonly" /></dd>
            <dt>最后登录IP：</dt>
            <dd><input id="accountinfo-lastloginip" name="accountinfo-lastloginip" class="tb readonly"  readonly="readonly" /></dd>
            <dt>最后登录时间：</dt>
            <dd><input id="accountinfo-lastlogintime" name="accountinfo-lastlogintime" class="tb readonly"  readonly="readonly" /></dd>
            <dt>创建人：</dt>
            <dd><input id="accountinfo-createadminname" name="accountinfo-createadminname" class="tb readonly"  readonly="readonly" /></dd>
            <dt>添加时间：</dt>
            <dd><input id="accountinfo-createtime" name="accountinfo-createtime" class="tb readonly" readonly="readonly" /></dd>
            <dt></dt><dd><a id="adminModelAdd" class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="modifyAdminModel();" >保 存</a></dd>
        </dl>
         <div class="blank"></div>
        </div>
    </div>
    <!--end 搜索-->
</div>
