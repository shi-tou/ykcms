/*
管理员、权限组操作类
*/
$(function () {
});
//操作栏
var toolbar = [{
    text: '添加权限组',
    iconCls: 'icon-add',
    handler: function () {
        $('#groupWin').window({
            title: '添加权限组',
            closed: false,
            onClose: function () {
                clearGroupForm();
            }
        });
    }
}, {
    text: '修改权限组',
    iconCls: 'icon-edit',
    handler: function () {
        getGroupModel();
    }
}, {
    text: '删除权限组',
    iconCls: 'icon-remove',
    handler: function () {
        deleteGroup();
    }
}];
//取消添加权限组表单
function cancelGroupWin() {
    $('#groupWin').window({
        closed: true
    });
    clearGroupForm();
}
//获取权限组列表
function getGroupList() {
    $('#groupList').datagrid({
        title:'权限组列表',
        url: dealAjaxUrl('public/ajax/ajax.ashx'),//请求地址
        columns: [[
            {field:'GroupID',width:30,align:'',checkbox:true},
            { field: 'GroupName', title: '权限组名称', width: 150, align: 'center' },
            { field: 'Describe', title: '权限描述', width: 200, align: 'center' },
            { field: 'AdminName', title: '添加人', width: 100, align: 'center' },
            { field: 'CreateTime', title: '添加时间', width: 150, align: 'center' }
        ]],
        loadMsg:'正在加载数据，请稍候……',
        rownumbers:true,//显示记录数
        queryParams: { 'action': 'getGroupList' },//查询参数
        toolbar: toolbar
    });
}
//清空权限组表单
function clearGroupForm() {
    $('#admingroup-groupid').val('');
    $('#admingroup-groupname').val('');
    $('#admingroup-describe').val('');
    var p = $('#groupForm input:checked');
    p.each(function (index, obj) {
        $(obj).attr('checked', false);
    });
}
//权限组添加
function addGroupModel() {
    var data;
    if ($('#groupForm').form('validate')) {
        $.ajax({
            url: dealAjaxUrl('public/ajax/ajax.ashx'),
            data: 'action=addGroupModel&' + $('#groupForm').serialize(),
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    alertInfo('操作提示', data.msg);
                    cancelGroupWin();
                    getGroupList();
                }
                else {
                    alertError('错误提示', data.ex);
                }
            }
        });
    }
}
//获取权限组
function getGroupModel() {
    var groupID = '';
    var rows = $('#groupList').datagrid('getSelections'); //getSelections获取多行(注：getSelected可获取单行)
    if (rows.length == 1) {
        groupID = rows[0].GroupID;
        $.ajax({
            url: dealAjaxUrl('public/ajax/ajax.ashx'),
            data: { 'action': 'getGroupModel', 'groupid': groupID },
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    $('#admingroup-groupid').val(data.msg.GroupID);
                    $('#admingroup-groupname').val(data.msg.GroupName);
                    $('#admingroup-describe').val(data.msg.Describe);
                    $('#admingroup-groupauth').val(data.msg.GroupAuth);
                    var p = data.msg.GroupAuth.split(',');
                    $.each(p, function (index, obj) {
                        $('#p-' + obj).attr('checked', 'checked');
                    });
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
    $('#groupWin').window({
        title: '修改权限组',
        closed: false
    });
}
//删除权限组
function deleteGroup() {
    var groupIDs = '';
    var rows = $('#groupList').datagrid('getSelections'); //getSelections获取多行(注：getSelected可获取单行)
    if (rows.length > 0) {
        if (!confirmBox('操作提示', '正在执行删除操作，请确定？')) {
            return;
        }
        for (var i = 0; i < rows.length; i++) {
            if (i != 0)
                groupIDs += ',';
            groupIDs += rows[i].GroupID;
        }
        $.ajax({
            url: dealAjaxUrl('public/ajax/ajax.ashx'),
            data: { 'action': 'deleteGroup', 'groupids': groupIDs },
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    alertInfo('操作提示', data.msg);
                    getGroupList();
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
//查询权限组
function searchGroup() {
    var groupname = $("#search-admingroup-groupname").val();
    var starttime = $("#search-admingroup-starttime").datebox("getValue");
    var endtime = $("#search-admingroup-endtime").datebox("getValue");
    $('#groupList').datagrid({
        url: dealAjaxUrl('public/ajax/ajax.ashx'), //请求地址      
        queryParams: { 'action': 'searchGroup', 'groupname': groupname, 'starttime': starttime, 'endtime': endtime }
    });
}
