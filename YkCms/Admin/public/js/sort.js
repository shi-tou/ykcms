/*
栏目操作类
*/
$(function () {
});
//操作栏
var sorttoolbar = [{
    text: '添加栏目',
    iconCls: 'icon-add',
    handler: function () {
        $('#sortWin').window({
            title: '添加栏目',
            closed: false,
            onClose: function () {
                clearSortForm();
            }
        });
    }
}, {
    text: '修改栏目',
    iconCls: 'icon-edit',
    handler: function () {
        getSortModel();
    }
}, {
    text: '删除栏目',
    iconCls: 'icon-remove',
    handler: function () {
        deleteSort();
    }
}];
//取消添加栏目表单
function cancelSortWin() {
    $('#sortWin').window({
        closed: true
    });
    clearSortForm();
}
//获取栏目列表
function getSortList() {
    $('#sortList').datagrid({
        title: '栏目列表',
        url: dealAjaxUrl('../public/ajax/ajax.ashx'), //请求地址
        columns: [[
            { field: 'SortID', width: 30, align: '', checkbox: true },
            { field: 'SortName', title: '栏目名称', width: 150, align: 'center' },
            { field: 'SortUrl', title: '栏目描述', width: 300, align: 'center' },
            { field: 'PageTitle', title: 'Title', width: 100, align: 'center' },
            { field: 'PageKeywords', title: 'Keywords', width: 150, align: 'center' },
            { field: 'PageDesc', title: 'Description', width: 150, align: 'center' }
        ]],
        loadMsg: '正在加载数据，请稍候……',
        rownumbers: true, //显示记录数
        queryParams: { 'action': 'getSortList' }, //查询参数
        toolbar: sorttoolbar
    });
}
//清空栏目表单
function clearSortForm() {
    $('#sort-sortid').val('');
    $('#sort-sortname').val('');
    $('#sort-sortdesc').val('');
}
//栏目添加
function addSortModel() {
    var data;
    if ($('#sortForm').form('validate')) {
        $.ajax({
            url: dealAjaxUrl('../public/ajax/ajax.ashx'),
            data: 'action=addSortModel&' + $('#sortForm').serialize(),
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    alertInfo('操作提示', data.msg);
                    cancelSortWin();
                    getSortList();
                }
                else {
                    alertError('错误提示', data.ex);
                }
            }
        });
    }
}
//获取栏目
function getSortModel() {
    var sortID = '';
    var rows = $('#sortList').datagrid('getSelections'); //getSelections获取多行(注：getSelected可获取单行)
    if (rows.length == 1) {
        sortID = rows[0].SortID;
        $.ajax({
            url: dealAjaxUrl('../public/ajax/ajax.ashx'),
            data: { 'action': 'getSortModel', 'sortid': sortID },
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    $('#sort-sortid').val(data.msg.SortID);
                    $('#sort-sortname').val(data.msg.SortName);
                    $('#sort-sortdesc').val(data.msg.SortDesc);
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
    $('#sortWin').window({
        title: '修改栏目',
        closed: false
    });
}
//删除栏目
function deleteSort() {
    var sortIDs = '';
    var rows = $('#sortList').datagrid('getSelections'); //getSelections获取多行(注：getSelected可获取单行)
    if (rows.length > 0) {        
        for (var i = 0; i < rows.length; i++) {
            if (i != 0)
                sortIDs += ',';
            sortIDs += rows[i].SortID;
        }
        $.messager.confirm('操作提示', '正在执行删除操作，请确定？', function (r) {
            if (r) {
                $.ajax({
                    url: dealAjaxUrl('../public/ajax/ajax.ashx'),
                    data: { 'action': 'deleteSort', 'sortids': sortIDs },
                    dataType: 'json',
                    type: 'POST',
                    success: function (data) {
                        if (data.msgOK) {
                            alertInfo('操作提示', data.msg);
                            getSortList();
                        }
                        else {
                            lertError('错误提示', data.ex);
                        }
                    }
                });
            }
        });
    }
    else {
        alertInfo('操作提示', '请选择要删除的记录');
    }
}
//查询栏目
function searchSort() {
    var sortname = $("#search-sort-sortname").val();
    var starttime = $("#search-sort-starttime").datebox("getValue");
    var endtime = $("#search-sort-endtime").datebox("getValue");
    $('#sortList').datagrid({
        url: dealAjaxUrl('../public/ajax/ajax.ashx'), //请求地址
        queryParams: { 'action': 'searchSort', 'sortname': sortname, 'starttime': starttime, 'endtime': endtime }
    });
}
