/*
管理员、权限组操作类
*/
$(function () {
});
//清空登录表单
function resetLoginForm() {
    $('#loginForm').form('clear')
    $('#errMsg').html('');
}
//管理员登录
function adminLogin() {
    if ($('#loginForm').form('validate')) {
        $.ajax({
            url: dealAjaxUrl('public/ajax/ajax.ashx'),
            data: 'action=login&' + $('#loginForm').serialize(),
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    location.href = 'index.aspx'
                }
                else {
                    alertError('错误提示', data.ex);
                }
            }
        });
    }
}
//管理员退出
function adminLoginOut() {
    if (confirm("正在退出系统，请确定？")) {
        $.ajax({
            url: dealAjaxUrl('public/ajax/ajax.ashx'),
            data: { 'action': 'loginOut' },
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    location.href = 'login.aspx'
                }
                else {
                    alertError('错误提示', data.ex);
                }
            }
        });
    }
}
//操作栏
var admintoolbar = [{
    text: '添加管理员',
    iconCls: 'icon-add',
    handler: function () {
        $('#adminWin').window({
            title: '添加管理员',
            closed: false,
            onClose: function () {
                clearAdminForm();
            }
        });
    }
}, {
    text: '修改管理员',
    iconCls: 'icon-edit',
    handler: function () {
        getAdminModel();
    }
}, {
    text: '删除管理员',
    iconCls: 'icon-remove',
    handler: function () {
        deleteAdmin();
    }
}];
//取消添加管理员表单
function cancelAdminWin() {
    $('#adminWin').window({
        closed: true
    });
    clearAdminForm();
}
//获取管理员列表
function getAdminList() {
    $('#adminList').datagrid({
        title: '管理员列表',
        url: dealAjaxUrl('public/ajax/ajax.ashx'), //请求地址
        columns: [[
            { field: 'AdminID', width: 30, align: 'center', checkbox: true,editor:isAdmin },
            { field: 'AdminAccount',title: '管理帐号', width: 100, align: 'center'},
            { field: 'AdminName', title: '管理员姓名', width: 100, align: 'center' },
            { field: 'GroupName', title: '所属权限组', width: 100, align: 'center',formatter: formatGroupName },
            { field: 'AdminDesc', title: '描述', width: 200, align: 'center' },
            { field: 'State', title: '状态', width: 50, align: 'center', formatter: formatState },
            { field: 'LastLoginIP', title: '最后登录IP', width: 150, align: 'center' },
            { field: 'LastLoginTime', title: '最后登录时间', width: 150, align: 'center' },
            { field: 'CreateAdminName', title: '添加人', width: 100, align: 'center', formatter: formatCreateAdminName },
            { field: 'CreateTime', title: '添加时间', width: 120, align: 'center' }
        ]],
        loadMsg: '正在加载数据，请稍候……',
        rownumbers: true, //显示记录数
        queryParams: { 'action': 'getAdminList' }, //查询参数
        toolbar: admintoolbar
    });
}
//格式化权限组名
function formatGroupName(val, row) {
    if (val == '')
        return '无';
    else
        return val;
}
//格式化创建人
function formatCreateAdminName(val, row) {
    if (val == '')
        return '<span style="color:red;">系统创建</span>';
    else
        return val;
}
//格式化状态
function formatState(val, row) {
    if (val == '0')
        return '<span style="color:red;">禁用</span>';
    else
        return '启用';
}
function isAdmin(val, row) {
    if (val == 'admin')
        return checkbox.disabled = false;
    else
        return checkbox.disabled = true;
}
//清空管理员表单
function clearAdminForm() {
    $('#adminlist-adminid').val('');
    cancelReadonly($('#adminlist-adminaccount'));
    $('#adminlist-adminaccount').val('');
    cancelReadonly($('#adminlist-adminpwd'));
    $('#adminlist-adminpwd').val('');
    cancelReadonly($('#adminlist-adminpwd0'))
    $('#adminlist-adminpwd0').val('');
    $('#adminlist-adminname').val('');
    $('#adminlist-admindesc').val('');
    cancelDisable($('#adminlist-groupid'))
    $('#adminlist-groupid').val(0);
    $('#adminlist-open').attr("checked", true);
    $('#adminlist-groupid').attr('disabled', false);
}
//添加管理员
function addAdminModel() {
    var data;
    if ($('#adminForm').form('validate')) {
        $.ajax({
            url: dealAjaxUrl('public/ajax/ajax.ashx'),
            data: 'action=addAdminModel&' + $('#adminForm').serialize(),
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    alertInfo('操作提示', data.msg);
                    cancelAdminWin();
                    getAdminList();
                }
                else {
                    alertError('错误提示', data.ex);
                }
            }
        });
    }
} 
//删除管理员
function deleteAdmin() {
    var adminIDs = '';
    var rows = $('#adminList').datagrid('getSelections'); //getSelections获取多行(注：getSelected可获取单行)
    if (rows.length > 0) {
        for (var i = 0; i < rows.length; i++) {
            if (rows[i].AdminID == 1) {//如果是admin，则不允许修改权限组
                alertWaring('警告','系统创建的管理员不允许删除！');
                return;
            }
            if (i != 0)
                adminIDs += ',';
            adminIDs += rows[i].AdminID;
        }
        if (!confirmBox('操作提示','正在执行删除操作，请确定？')) {
            return;
        }
        $.ajax({
            url: dealAjaxUrl('public/ajax/ajax.ashx'),
            data: { 'action': 'deleteAdmin', 'adminids': adminIDs },
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    alertInfo('操作提示', data.msg);
                    getAdminList();
                }
                else {
                    alertError('错误提示', data.ex);
                }
            }
        });
    }
    else {
        alertInfo('操作提示', '请选择要删除的记录');
    }
}
//修改时获取管理员信息
function getAdminModel() {
    var adminID = '';
    var rows = $('#adminList').datagrid('getSelections'); //getSelections获取多行(注：getSelected可获取单行)
    if (rows.length == 1) {
        adminID = rows[0].AdminID;
        if (adminID == 1) {//如果是admin，则不允许修改权限组
            alertWaring('警告','系统创建的管理员不允许删除！');
            return;
         }
        $.ajax({
            url: dealAjaxUrl('public/ajax/ajax.ashx'),
            data: { 'action': 'getAdminModel', 'adminid': adminID },
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    $('#adminlist-adminid').val(data.msg.AdminID);
                    $('#adminlist-adminaccount').val(data.msg.AdminAccount);
                    setReadonly($('#adminlist-adminaccount'));
                    $('#adminlist-adminpwd').val(data.msg.AdminPwd);
                    setReadonly($('#adminlist-adminpwd'));
                    $('#adminlist-adminpwd0').val(data.msg.AdminPwd);
                    setReadonly($('#adminlist-adminpwd0'));
                    $('#adminlist-groupid').val(data.msg.GroupID);
                    $('#adminlist-adminname').val(data.msg.AdminName);
                    $('#adminlist-admindesc').val(data.msg.AdminDesc);
                    if (data.msg.State == 1) {
                        $('#adminlist-open').attr("checked", true);
                    }
                    else {
                        $('#adminlist-close').attr("checked", true);
                    }
                }
                else {
                    alertError('错误提示', data.ex);
                }
            }
        });
    }
    else if (rows.length == 0) {
        alertInfo('操作提示', '请选择要修改的记录');
        return;
    }
    else {
        alertInfo('操作提示', '每次只能修改一条记录!');
        return;
    }
    $('#adminWin').window({
        title: '修改权限组',
        closed: false
    });
}
//查询管理员
function searchAdmin() {
    var adminname = $('#search-adminlist-adminname').val();
    var groupid = $('#search-adminlist-groupid').val();
    var starttime = $('#search-adminlist-starttime').datebox('getValue');
    var endtime = $('#search-adminlist-endtime').datebox('getValue');
    $('#adminList').datagrid({
        url: dealAjaxUrl('public/ajax/ajax.ashx'), //请求地址      
        queryParams: { 'action': 'searchAdmin', 'adminname': adminname, 'groupid': groupid, 'starttime': starttime, 'endtime': endtime }
    });
}
//获取当前登录管理员信息
function getLoginAdminModel(adminid) {
    $.ajax({
        url: dealAjaxUrl('public/ajax/ajax.ashx'),
        data: { 'action': 'getAdminModel', 'adminid': adminid },
        dataType: 'json',
        type: 'POST',
        success: function (data) {
            if (data.msgOK) {
                debugger
                $('#accountinfo-adminid').val(data.msg.AdminID);
                $('#accountinfo-adminaccount').val(data.msg.AdminAccount);
                $('#accountinfo-groupid').val(data.msg.GroupID);
                $('#accountinfo-adminname').val(data.msg.AdminName);
                if (data.msg.State == 1) {
                    $('#accountinfo-open').attr('checked', true);
                }
                else {
                    $('#accountinfo-close').attr('checked', true);
                }
                $('#accountinfo-admindesc').val(data.msg.AdminDesc); //
                $('#accountinfo-lastloginip').val(data.msg.LastLoginIP);
                $('#accountinfo-lastlogintime').val(data.msg.LastLoginTime);
                $('#accountinfo-createadminname').val(data.msg.CreateAdminName);
                $('#accountinfo-createtime').val(data.msg.CreateTime);
            }
            else {
                alertError('错误提示', data.ex);
            }
        }
    });
}
//修改当前登录管理员信息
function modifyAdminModel() {
    var data;
    var adminid = $('#accountinfo-adminid').val();
    var adminname = $('#accountinfo-adminname').val();
    if ($('#adminForm').form('validate')) {
        $.ajax({
            url: dealAjaxUrl('public/ajax/ajax.ashx'),
            data: { 'action': 'modifyAdminModel', 'adminid': adminid, 'adminname': adminname },
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    alertInfo('操作提示', data.msg);
                }
                else {
                    alertError('错误提示', data.ex);
                }
            }
        });
    }
} 