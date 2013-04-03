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
//取消添加栏目模板表单
function cancelTemplateWin0() {
    $('#templateWin0').window({
        closed: true
    });
    clearTemplateForm();
}
//获取栏目模板列表
function getTemplateList() {
    $('#templateList').datagrid({
        title: '栏目模板列表',
        url: dealAjaxUrl('../public/ajax/ajax.ashx'), //请求地址
        columns: [[
            { field: 'TemplateID', width: 30, align: '', checkbox: true },
            { field: 'TemplateName', title: '模板名称', width: 150, align: 'center' },
            { field: 'TemplateUrl', title: '模板路径', width: 150, align: 'center' },
            { field: 'TemplateDesc', title: '模板描述', width: 300, align: 'center' },
            { field: 'AdminName', title: '添加人', width: 100, align: 'center' },
            { field: 'CreateTime', title: '添加时间', width: 150, align: 'center' }
        ]],
        loadMsg: '正在加载数据，请稍候……',
        rownumbers: true, //显示记录数
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
    CKEDITOR.instances.templatesource.setData('');  
}
//栏目模板添加
function addTemplateModel() {
    var data;
    if ($('#templateForm').form('validate')) {
        $.ajax({
            url: dealAjaxUrl('../public/ajax/ajax.ashx'),
            data: { 'action': 'addTemplateModel', 'templateid': $('#templatelist-templateid').val(), 'templatename': $('#templatelist-templatename').val(),
                'templateurl': $('#templatelist-templateurl').val(), 'templatedesc': $('#templatelist-templatedesc').val(), 'templatesource': CKEDITOR.instances.templatesource.getData()
            },
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    alertInfo('操作提示', data.msg);
                    cancelTemplateWin0();
                    getTemplateList();
                }
                else {
                    alertError('错误提示', data.ex);
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
        $('#templateWin').window({
            title: '修改栏目模板',
            closed: false,
            content: "<iframe scrolling=\"no\" frameborder=\"0\" src=\"ModifyTemplate.aspx?tid="+ templateID +"\" style=\"width:100%;\" onload=\"Javascript:SetWinHeight(this,350);\"></iframe>"
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
}
//删除栏目模板
function deleteTemplate() {
    var templateIDs = '';
    var rows = $('#templateList').datagrid('getSelections'); //getSelections获取多行(注：getSelected可获取单行)
    if (rows.length > 0) {       
        for (var i = 0; i < rows.length; i++) {
            if (i != 0)
                templateIDs += ',';
            templateIDs += rows[i].TemplateID;
        }
        $.messager.confirm('操作提示', '正在执行删除操作，请确定？', function (r) {
            if (r) {
                $.ajax({
                    url: dealAjaxUrl('../public/ajax/ajax.ashx'),
                    data: { 'action': 'deleteTemplate', 'templateids': templateIDs },
                    dataType: 'json',
                    type: 'POST',
                    success: function (data) {
                        if (data.msgOK) {
                            alertInfo('操作提示', data.msg);
                            getTemplateList();
                        }
                        else {
                            alertError('错误提示', data.ex);
                        }
                    }
                });
            } 
        });
    }
    else {
        alertInfo('操作提示', '请选择要删除的记录！');
    }
}
//查询栏目模板
function searchTemplate() {
    var templatename = $("#search-template-templatename").val();
    var starttime = $("#search-template-starttime").datebox("getValue");
    var endtime = $("#search-template-endtime").datebox("getValue");
    $('#templateList').datagrid({
        url: dealAjaxUrl('../public/ajax/ajax.ashx'), //请求地址
        queryParams: { 'action': 'searchTemplate', 'templatename': templatename, 'starttime': starttime, 'endtime': endtime }
    });
}
