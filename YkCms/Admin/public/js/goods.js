/*
产品操作类
*/
$(function () {
});
//操作栏
var goodstoolbar = [{
    text: '添加产品',
    iconCls: 'icon-add',
    handler: function () {
        $('#goodsWin').window({
            title: '添加产品',
            closed: false,
            shadow: false,
            onClose: function () {
                clearGoodsForm();
            }
        });
    }
}, {
    text: '修改产品',
    iconCls: 'icon-edit',
    handler: function () {
        getGoodsModel();
    }
}, {
    text: '删除产品',
    iconCls: 'icon-remove',
    handler: function () {
        deleteGoods();
    }
}];
//取消添加产品表单
function cancelGoodsWin() {
    $('#goodsWin').window({
        closed: true
    });
    clearGoodsForm();
}
//获取产品列表
function getGoodsList() {
    $('#goodsList').datagrid({
        title: '产品列表',
        url: dealAjaxUrl('../public/ajax/ajax.ashx'), //请求地址
        columns: [[
            { field: 'GoodsID', width: 30, align: '', checkbox: true },
            { field: 'GoodsName', title: '产品名称', width: 150, align: 'center' },
            { field: 'GoodsUnit', title: '单位', width: 50, align: 'center' },
            { field: 'SortName', title: '类别', width: 60, align: 'center' },
            { field: 'Price', title: '单价', width: 100, align: 'center' },
            { field: 'Discount', title: '折扣', width: 100, align: 'center' },
            { field: 'Count', title: '数量', width: 80, align: 'center' },
            { field: 'Describe', title: '描述', width: 150, align: 'center' },
            { field: 'AdminName', title: '创建人', width: 100, align: 'center' },
            { field: 'CreateTime', title: '创建时间', width: 150, align: 'center' }
        ]],
        loadMsg: '正在加载数据，请稍候……',
        rownumbers: true, //显示记录数
        queryParams: { 'action': 'getGoodsList' }, //查询参数
        toolbar: goodstoolbar
    });
}
//清空产品表单
function clearGoodsForm() {
    $('#goodsid').val(0);
    $('#typeid').val(0);
    $('#goodsname').val('');
    $('#unit').val('');
    $('#price').val(0);
    $('#disocout').val(0);
    $('#count').val(0);
    $('#image').val('');
    $('#describe').val('');
}
//产品添加
function addGoodsModel() {
    if ($('#goodsForm').form('validate')) {
        $.ajax({
            url: dealAjaxUrl('../public/ajax/ajax.ashx'),
            data: 'action=addGoodsModel&' + $('#goodsForm').serialize(),
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    alertInfo('操作提示', data.msg);
                    cancelGoodsWin();
                    getGoodsList();
                }
                else {
                    alertError('错误提示', data.ex);
                }
            }
        });
    }
}
//获取产品
function getGoodsModel() {
    var goodsID = '';
    var rows = $('#goodsList').datagrid('getSelections'); //getSelections获取多行(注：getSelected可获取单行)
    if (rows.length == 1) {
        goodsID = rows[0].GoodsID;
        $.ajax({
            url: dealAjaxUrl('../public/ajax/ajax.ashx'),
            data: { 'action': 'getGoodsModel', 'goodsid': goodsID },
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    $('#goodslist-parentid').val(data.msg.ParentID);
                    $('#goodslist-goodsid').val(data.msg.GoodsID);
                    $('#goodslist-goodsname').val(data.msg.GoodsName);
                    $('#goodslist-goodsurl').val(data.msg.GoodsUrl);
                    $('#goodslist-goodsattributeid').val(data.msg.GoodsAttributeID);
                    $('#goodslist-goodstemplateid').val(data.msg.GoodsTemplateID);
                    $('#goodslist-bannerurl').val(data.msg.BannerUrl);
                    if (data.msg.State = 0)
                        $('#goodslist-close').attr("checked", "checked");
                    else
                        $('#goodslist-open').attr("checked", "checked");
                    $('#goodslist-pagetitle').val(data.msg.PageTitle);
                    $('#goodslist-pagekeywords').val(data.msg.PageKeywords);
                    $('#goodslist-pagedesc').val(data.msg.PageDesc);
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
    $('#goodsWin').window({
        title: '修改产品',
        closed: false,
        shadow: false
    });
}
//删除产品
function deleteGoods() {
    var goodsIDs = '';
    var rows = $('#goodsList').datagrid('getSelections'); //getSelections获取多行(注：getSelected可获取单行)
    if (rows.length > 0) {
        for (var i = 0; i < rows.length; i++) {
            if (i != 0)
                goodsIDs += ',';
            goodsIDs += rows[i].GoodsID;
        }
        $.messager.confirm('操作提示', '正在执行删除操作，请确定？', function (r) {
            if (r) {
                $.ajax({
                    url: dealAjaxUrl('../public/ajax/ajax.ashx'),
                    data: { 'action': 'deleteGoods', 'goodsids': goodsIDs },
                    dataType: 'json',
                    type: 'POST',
                    success: function (data) {
                        if (data.msgOK) {
                            alertInfo('操作提示', data.msg);
                            getGoodsList();
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
//查询产品
function searchGoods() {
    var goodsname = $("#search-goods-goodsname").val();
    var starttime = $("#search-goods-starttime").datebox("getValue");
    var endtime = $("#search-goods-endtime").datebox("getValue");
    $('#goodsList').datagrid({
        url: dealAjaxUrl('../public/ajax/ajax.ashx'), //请求地址
        queryParams: { 'action': 'searchGoods', 'goodsname': goodsname, 'starttime': starttime, 'endtime': endtime }
    });
}
