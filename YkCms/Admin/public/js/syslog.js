/*
日志操作类
*/
//操作栏
var admintoolbar = [{
    text: '删除管理员',
    iconCls: 'icon-remove',
    handler: function () {
        deleteSyslog();
    }
}];

//获取管理员列表
function getSyslogList(pagesize,pageindex) {
    $('#syslogList').datagrid({
        title: '操作日志列表',
        url: dealAjaxUrl('../public/ajax/ajax.ashx'), //请求地址
        columns: [[
            { field: 'SysLogID', width: 30, align: 'center', checkbox: true },
            { field: 'Area', title: '操作区域', width: 100 },
            { field: 'Type', title: '操作类型', width: 100 },
            { field: 'Detail', title: '操作详细', width: 250  },
            { field: 'IP', title: 'IP地址', width: 100},
            { field: 'AdminName', title: '操作人', width: 100 },
            { field: 'CreateTime', title: '操作时间', width: 150 }
        ]],
        loadMsg: '正在加载数据，请稍候……',
        rownumbers: true, //显示记录数
        queryParams: { 'action': 'getSyslogList' }, //查询参数，easyui插件会加上默认参数page（当前页）rows（每页记录数）
        toolbar: admintoolbar,
        pagination: true        
    });
    var p = $('#syslogList').datagrid('getPager');
    setPager(p, function () {
        $('#syslogList').datagrid({
            url: dealAjaxUrl('../public/ajax/ajax.ashx'), //请求地址
            queryParams: { 'action': 'getSyslogList'} //查询参数,easyui插件会加上默认参数page（当前页）rows（每页记录数）
        });
    });
}
//删除日志
function deleteSyslog() {
    var syslogIDs = '';
    var rows = $('#syslogList').datagrid('getSelections'); //getSelections获取多行(注：getSelected可获取单行)
    if (rows.length > 0) {
        if (!confirmBox('操作提示', '正在执行删除操作，请确定？')) {
            return;
        }
        for (var i = 0; i < rows.length; i++) {           
            if (i != 0)
                syslogIDs += ',';
            syslogIDs += rows[i].SysLogID;
        }
        $.ajax({
            url: dealAjaxUrl('../public/ajax/ajax.ashx'),
            data: { 'action': 'deleteSyslog', 'syslogIDs': syslogIDs },
            dataType: 'json',
            type: 'POST',
            success: function (data) {
                if (data.msgOK) {
                    alertInfo('操作提示', data.msg);
                    getSyslogList();
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
//查询日志
function searchSyslog() {
    var key = $('#search-syslog-key').val();
    var starttime = $('#search-syslog-starttime').datebox('getValue');
    var endtime = $('#search-syslog-endtime').datebox('getValue');
    $('#syslogList').datagrid({
        url: dealAjaxUrl('../public/ajax/ajax.ashx'), //请求地址      
        queryParams: { 'action': 'searchSyslog', 'key': key, 'starttime': starttime, 'endtime': endtime }
    });
}