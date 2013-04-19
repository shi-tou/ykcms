/*
文章操作类
*/
//操作栏
var newstoolbar = [{
    text: '添加文章',
    iconCls: 'icon-add',
    handler: function () {
        $('#newsWin').window({
            title: '添加文章',
            closed: false,
            shadow: false,
            onClose: function () {
                clearNewsForm();
            }
        });
    }
}, {
    text: '修改文章',
    iconCls: 'icon-edit',
    handler: function () {
        getNewsModel();
    }
}, {
    text: '删除文章',
    iconCls: 'icon-remove',
    handler: function () {
        deleteNews();
    }
},{
    text: '刷新列表',
    iconCls: 'icon-reload',
    handler: function () {
        getNewsList();
    }
}
];
//取消添加文章表单
function cancelNewsWin() {
    $('#newsWin').window({
        closed: true
    });
    clearNewsForm();
}
//获取文章列表
function getNewsList() {
    $('#newsList').datagrid({
        title: '文章列表',
        url: dealAjaxUrl('../public/ajax/ajax.ashx'), //请求地址
        columns: [[
            { field: 'NewsID', width: 30, align: '', checkbox: true },
            { field: 'Title', title: '文章名称', width: 150, align: 'center' },           
            { field: 'SortName', title: '类别', width: 60, align: 'center' },
            { field: 'State', title: '状态', width: 100, align: 'center', formatter: formatState },
            { field: 'AdminName', title: '作者', width: 100, align: 'center' },
            { field: 'CreateTime', title: '发表时间', width: 150, align: 'center' }
        ]],
        loadMsg: '正在加载数据，请稍候……',
        rownumbers: true, //显示记录数
        queryParams: { 'action': 'getNewsList' }, //查询参数
        toolbar: newstoolbar
    });
}
//格式化权限组名
function formatState(val, row) {
    if (val == 1)
        return "启用";
    else
        return "禁用";
}
//清空文章表单
function clearNewsForm() {
    $('#newsid').val(0);
    $('#typeid').val(0);
    $('#title').val('');
    $('#pagetitle').val('');
    $('#pagekeywords').val('');
    $('#pagedesc').val('');
    CKEDITOR.instances.content.setData('');
}
//文章添加
function addNewsModel() {
    if ($('#newsForm').form('validate')) {
        $.ajax({
            url: dealAjaxUrl('../public/ajax/ajax.ashx'),
            data: { 'action': 'addNewsModel', 'newsid': $('#newsid').val(), 'typeid': $('#typeid').val(), 'title': $('#title').val(), 'pagetitle': $('#pagetitle').val(),
                'pagekeywords': $('#pagekeywords').val(), 'pagedesc': $('#pagedesc').val(),'state':$('input:radio[name="state"]:checked').val(),'content': CKEDITOR.instances.content.getData()
            },
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    alertInfo('操作提示', data.msg);
                    cancelNewsWin();
                    getNewsList();
                }
                else {
                    alertError('错误提示', data.ex);
                }
            }
        });
    }
}
//获取文章
function getNewsModel() {
    var newsID = '';
    var rows = $('#newsList').datagrid('getSelections'); //getSelections获取多行(注：getSelected可获取单行)
    if (rows.length == 1) {
        newsID = rows[0].NewsID;
        $.ajax({
            url: dealAjaxUrl('../public/ajax/ajax.ashx'),
            data: { 'action': 'getNewsModel', 'newsid': newsID },
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    $('#newsid').val(data.msg.NewsID);
                    $('#typeid').val(data.msg.TypeID);
                    $('#title').val(data.msg.Title);
                    $('#pagetitle').val(data.msg.PageTitle);
                    $('#pagekeywords').val(data.msg.PageKeyword);
                    $('#pagedesc').val(data.msg.PageDesc);
                    $('#describe').val(data.msg.Describe);
                    CKEDITOR.instances.content.setData(data.msg.Content);
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
    $('#newsWin').window({
        title: '修改文章',
        closed: false,
        shadow: false
    });
}
//删除文章
function deleteNews() {
    var newsIDs = '';
    var rows = $('#newsList').datagrid('getSelections'); //getSelections获取多行(注：getSelected可获取单行)
    if (rows.length > 0) {
        for (var i = 0; i < rows.length; i++) {
            if (i != 0)
                newsIDs += ',';
            newsIDs += rows[i].NewsID;
        }
        $.messager.confirm('操作提示', '正在执行删除操作，请确定？', function (r) {
            if (r) {
                $.ajax({
                    url: dealAjaxUrl('../public/ajax/ajax.ashx'),
                    data: { 'action': 'deleteNews', 'newsids': newsIDs },
                    dataType: 'json',
                    type: 'POST',
                    success: function (data) {
                        if (data.msgOK) {
                            alertInfo('操作提示', data.msg);
                            getNewsList();
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
        alertInfo('操作提示', '请选择要删除的记录');
    }
}
//查询文章
function searchNews() {
    var newsname = $("#search-news-newsname").val();
    var starttime = $("#search-news-starttime").datebox("getValue");
    var endtime = $("#search-news-endtime").datebox("getValue");
    $('#newsList').datagrid({
        url: dealAjaxUrl('../public/ajax/ajax.ashx'), //请求地址
        queryParams: { 'action': 'searchNews', 'newsname': newsname, 'starttime': starttime, 'endtime': endtime }
    });
}
