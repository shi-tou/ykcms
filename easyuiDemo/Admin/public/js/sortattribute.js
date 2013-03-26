/*
管理员、栏目属性操作类
*/
$(function () {
});
//操作栏
var satoolbar = [{
    text: '添加栏目属性',
    iconCls: 'icon-add',
    handler: function () {
        $('#sortattributeWin').window({
            title: '添加栏目属性',
            closed: false,
            onClose: function () {
                clearSortAttributeForm();
            }
        });
    }
}, {
    text: '修改栏目属性',
    iconCls: 'icon-edit',
    handler: function () {
        getSortAttributeModel();
    }
}, {
    text: '删除栏目属性',
    iconCls: 'icon-remove',
    handler: function () {
        deleteSortAttribute();
    }
}];
//取消添加栏目属性表单
function cancelSortAttributeWin() {
    $('#sortattributeWin').window({
        closed: true
    });
    clearSortAttributeForm();
}
//获取栏目属性列表
function getSortAttributeList() {
    $('#sortattributeList').datagrid({
        title:'栏目属性列表',
        url: dealAjaxUrl('public/ajax/ajax.ashx'),//请求地址
        columns: [[
            {field:'SortAttributeID',width:30,align:'',checkbox:true},
            { field: 'SortAttributeName', title: '栏目属性名称', width: 150, align: 'center' },
            { field: 'SortAttributeDesc', title: '栏目属性描述', width: 200, align: 'center' },
            { field: 'AdminName', title: '添加人', width: 100, align: 'center' },
            { field: 'CreateTime', title: '添加时间', width: 150, align: 'center' }
        ]],
        loadMsg:'正在加载数据，请稍候……',
        rownumbers:true,//显示记录数
        queryParams: { 'action': 'getSortAttributeList' },//查询参数
        toolbar: satoolbar
    });
}
//清空栏目属性表单
function clearSortAttributeForm() {
    $('#sortattribute-sortattributeid').val('');
    $('#sortattribute-sortattributename').val('');
    $('#sortattribute-sortattributedesc').val('');   
}
//栏目属性添加
function addSortAttributeModel() {
    var data;
    if ($('#sortattributeForm').form('validate')) {
        $.ajax({
            url: dealAjaxUrl('public/ajax/ajax.ashx'),
            data: 'action=addSortAttributeModel&' + $('#sortattributeForm').serialize(),
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    alert(data.msg);
                    cancelSortAttributeWin();
                    getSortAttributeList();
                }
                else {
                    alert(data.ex);
                }
            }
        });
    }
}
//获取栏目属性
function getSortAttributeModel() {
    var sortattributeID = '';
    var rows = $('#sortattributeList').datagrid('getSelections'); //getSelections获取多行(注：getSelected可获取单行)
    if (rows.length == 1) {
        sortattributeID = rows[0].SortAttributeID;
        $.ajax({
            url: dealAjaxUrl('public/ajax/ajax.ashx'),
            data: { 'action': 'getSortAttributeModel', 'sortattributeid': sortattributeID },
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    $('#adminsortattribute-sortattributeid').val(data.msg.SortAttributeID);
                    $('#adminsortattribute-sortattributename').val(data.msg.SortAttributeName);
                    $('#adminsortattribute-describe').val(data.msg.Describe);
                    $('#adminsortattribute-sortattributeauth').val(data.msg.SortAttributeAuth);
                    var p = data.msg.SortAttributeAuth.split(',');
                    $.each(p, function (index, obj) {
                        $('#p-' + obj).attr('checked', 'checked');
                    });
                }
                else {
                    alert(data.ex);
                }
            }
        });
    }
    else if (rows.length == 0) {
        alert('请选择要修改的记录！');
        return;
    }
    else {
        alert('每次只能修改一条记录！');
        return;
    }
    $('#sortattributeWin').window({
        title: '修改栏目属性',
        closed: false
    });
}
//删除栏目属性
function deleteSortAttribute() {
    var sortattributeIDs = '';
    var rows = $('#sortattributeList').datagrid('getSelections'); //getSelections获取多行(注：getSelected可获取单行)
    if (rows.length > 0) {
        if (!confirm("正在执行删除操作，请确定？")) {
            return;
        }
        for (var i = 0; i < rows.length; i++) {
            if (i != 0)
                sortattributeIDs += ',';
            sortattributeIDs += rows[i].SortAttributeID;
        }
        $.ajax({
            url: dealAjaxUrl('public/ajax/ajax.ashx'),
            data: { 'action': 'deleteSortAttribute', 'sortattributeids': sortattributeIDs },
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    alert(data.msg);
                    getSortAttributeList();
                }
                else {
                    alert(data.ex);
                }
            }
        });
    }
    else {
        alert('请选择要删除的记录！');
    }
}
//查询栏目属性
function searchSortAttribute() {
    var sortattributename = $("#search-adminsortattribute-sortattributename").val();
    var starttime = $("#search-adminsortattribute-starttime").datebox("getValue");
    var endtime = $("#search-adminsortattribute-endtime").datebox("getValue");
    $('#sortattributeList').datagrid({
        url: dealAjaxUrl('public/ajax/ajax.ashx'), //请求地址      
        queryParams: { 'action': 'searchSortAttribute', 'sortattributename': sortattributename, 'starttime': starttime, 'endtime': endtime }
    });
}
