/*
栏目模板操作类
*/
$(function () {
});
//操作栏
var templatetoolbar = [{
    text: '添加栏目模板',
    iconCls: 'icon-add',
    handler: function () {
        $('#templateWin').window({
            title: '添加栏目模板',
            closed: false,
            onClose: function () {
                clearTemplateForm();
            }
        });
    }
}, {
    text: '修改栏目模板',
    iconCls: 'icon-edit',
    handler: function () {
        getTemplateModel();
    }
}, {
    text: '删除栏目模板',
    iconCls: 'icon-remove',
    handler: function () {
        deleteTemplate();
    }
}];
//取消添加栏目模板表单
function cancelTemplateWin() {
    $('#templateWin').window({
        closed: true
    });
    clearTemplateForm();
}
//获取栏目模板列表
function getTemplateList() {
    $('#templateList').datagrid({
        title:'栏目模板列表',
        url: dealAjaxUrl('public/ajax/ajax.ashx'),//请求地址
        columns: [[
            {field:'TemplateID',width:30,align:'',checkbox:true},
            { field: 'TemplateName', title: '模板名称', width: 150, align: 'center' },
            { field: 'TemplateUrl', title: '模板路径', width: 150, align: 'center' },
            { field: 'TemplateDesc', title: '模板描述', width: 300, align: 'center' },
            { field: 'AdminName', title: '添加人', width: 100, align: 'center' },
            { field: 'CreateTime', title: '添加时间', width: 150, align: 'center' }
        ]],
        loadMsg:'正在加载数据，请稍候……',
        rownumbers:true,//显示记录数
        queryParams: { 'action': 'getTemplateList' }, //查询参数
        toolbar: templatetoolbar
    });
}
//清空栏目模板表单
function clearTemplateForm() {
    $('#templatelist-templateid').val('');
    $('#templatelist-templatename').val('');
    $('#templatelist-templateurl').val('');
    $('#templatelist-templatedesc').val('');
    $('#templatelist-templatesource').val('');   
}
//栏目模板添加
function addTemplateModel() {
    var data;
    if ($('#templateForm').form('validate')) {
        $.ajax({
            url: dealAjaxUrl('public/ajax/ajax.ashx'),
            data: 'action=addTemplateModel&' + $('#templateForm').serialize(),
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    alert(data.msg);
                    cancelTemplateWin();
                    getTemplateList();
                }
                else {
                    alert(data.ex);
                }
            }
        });
    }
}
//获取栏目模板
function getTemplateModel() {
    var templateID = '';
    var rows = $('#templateList').datagrid('getSelections'); //getSelections获取多行(注：getSelected可获取单行)
    if (rows.length == 1) {
        templateID = rows[0].TemplateID;
        $.ajax({
            url: dealAjaxUrl('public/ajax/ajax.ashx'),
            data: { 'action': 'getTemplateModel', 'templateid': templateID },
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    $('#template-templateid').val(data.msg.TemplateID);
                    $('#template-templatename').val(data.msg.TemplateName);
                    $('#template-templateurl').val(data.msg.TemplateUrl);
                    $('#template-templatedesc').val(data.msg.TemplateDesc);
                    $('#template-templatesource').val(data.msg.TemplateSource);
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
    $('#templateWin').window({
        title: '修改栏目模板',
        closed: false
    });
}
//删除栏目模板
function deleteTemplate() {
    var templateIDs = '';
    var rows = $('#templateList').datagrid('getSelections'); //getSelections获取多行(注：getSelected可获取单行)
    if (rows.length > 0) {
        if (!confirm("正在执行删除操作，请确定？")) {
            return;
        }
        for (var i = 0; i < rows.length; i++) {
            if (i != 0)
                templateIDs += ',';
            templateIDs += rows[i].TemplateID;
        }
        $.ajax({
            url: dealAjaxUrl('public/ajax/ajax.ashx'),
            data: { 'action': 'deleteTemplate', 'templateids': templateIDs },
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    alert(data.msg);
                    getTemplateList();
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
//查询栏目模板
function searchTemplate() {
    var templatename = $("#search-template-templatename").val();
    var starttime = $("#search-template-starttime").datebox("getValue");
    var endtime = $("#search-template-endtime").datebox("getValue");
    $('#templateList').datagrid({
        url: dealAjaxUrl('public/ajax/ajax.ashx'), //请求地址
        queryParams: { 'action': 'searchTemplate', 'templatename': templatename, 'starttime': starttime, 'endtime': endtime }
    });
}
