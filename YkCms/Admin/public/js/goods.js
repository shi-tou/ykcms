/*
产品操作类
*/
$(function () {
    //上传插件
    $("#image_upload").uploadify({
        'uploader': '../public/uploadify/uploadify.swf',
        'script': '../public/ajax/UploadHandler.ashx',
        'cancelImg': '../public/uploadify/cancel.png',
        'folder': '/Files/UploadFile/',
        'queueID': 'queueDiv',
        'buttonImg': "../public/images/btn_upload.gif",
        'height': 23,
        'width': 70,
        'fileExt': "*.jpg;*.jpeg;*.gif;*.png;*.bmp",
        'fileDesc': "请选择格式为GIF、JPG、PNG或BMP的图片",
        'auto': false,
        'multi': false,
        'sizeLimit': 512000,
        'onError': function (event, ID, fileObj, errorObj) {
            if (errorObj.type == "File Size")
                alert("对不起，上传的图片不能超过500K");
            else
                alert(errorObj.type + ' Error: ' + errorObj.info);
        },
        'onComplete': function (event, ID, fileObj, response, data) {
            if (response != "0") {
                $("#image").val(response);
            }
        }
    });
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
            { field: 'Price', title: '单价', width: 100, align: 'center', formatter: formatMoney },
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
//格式化权限组名
function formatMoney(val, row) {
    return formatRMB(val);
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
                    $('#goodsid').val(data.msg.GoodsID);
                    $('#goodsname').val(data.msg.GoodsName);
                    $('#unit').val(data.msg.Unit);
                    $('#price').val(data.msg.Price);
                    $('#discount').val(data.msg.Discount);
                    $('#count').val(data.msg.Count);
                    $('#image').val(data.msg.Image);
                    $('#describe').val(data.msg.Describe);
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
