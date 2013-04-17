<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsList.aspx.cs" Inherits="YkCms.Content.GoodsList" %>
<html>
    <head>
        <title></title>
        <link href="/jquery-easyui-1.3.2/themes/default/easyui.css" rel="stylesheet" type="text/css" />
        <link href="/jquery-easyui-1.3.2/themes/icon.css" rel="stylesheet" type="text/css" />
        <link href="../public/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
        <link href="../public/css/admin.css" rel="stylesheet" type="text/css" />
        <script src="/jquery-easyui-1.3.2/jquery-1.8.0.min.js" type="text/javascript"></script>
        <script src="/jquery-easyui-1.3.2/jquery.easyui.min.js" type="text/javascript"></script>
        <script src="../public/uploadify/jquery.uploadify.v2.1.0.min.js" type="text/javascript"></script>
        <script src="../public/uploadify/swfobject.js" type="text/javascript"></script>
        <script src="../public/js/customValidate.js?t=<%=DateTime.Now.ToString()%>" type="text/javascript"></script>
        <script src="../public/js/msgbox.js" type="text/javascript"></script>
        <script src="../public/js/common.js" type="text/javascript"></script>
        <script src="../public/js/goods.js?t=<%=DateTime.Now.ToString() %>" type="text/javascript"></script>
        <script type="text/javascript">
            $(function () {
                getGoodsList();
            });
        </script>
    </head>
    <body>
        <div class="main">
            <!--start 搜索-->
            <div class="easyui-panel search" title="条件搜索">
                <table>
                    <tr>
                        <td>类别：</td>
                        <td><select id="searchtypeid"><%=SortSelectHtml %></select></td>
                        <td>添加时间：</td>
                        <td><input id="search-sortlist-starttime" name="search-sortlist-starttime" class="easyui-datebox" />-<input id="search-sortlist-endtime" name="search-sortlist-endtime" class="easyui-datebox" /></td>
                        <td><a class="easyui-linkbutton" data-options="iconCls:'icon-search'" onclick="searchSort();">查询</a></td>
                    </tr>
                </table>
            </div>
            <!--end 搜索-->
            <!--start 占位-->
            <div class="blank"></div>
            <!--end 占位-->
            <!--start 列表-->
            <table id="goodsList">
	        </table>
            <!--end 列表-->
            <!--start 表单窗口-->
            <div id="goodsWin" class="easyui-window" title="添加产品" data-options="iconCls:'icon-save',modal:true,top:'20px',closed:true,minimizable:false,maximizable:false,collapsible:false" >
                <form id="goodsForm">
                <input id="goodsid" name="goodsid" type="hidden" />
                <table class="table">
                    <tr>
                        <td style="width:100px;" class="r">产品类别:</td>
                        <td><select id="typeid" name="typeid"><%=SortSelectHtml %></select></td>
                    </tr>
                    <tr>
                        <td class="r">产品名称:</td>
                        <td><input class="easyui-validatebox" type="text" id="goodsname" name="goodsname" data-options="required:true" missingmessage="请输入产品名称！" validtype="checkSortName" /></td>
                    </tr>
                    <tr>
                        <td class="r">产品单位:</td>
                        <td><input class="easyui-validatebox" type="text" id="unit" name="unit" /></td>
                    </tr>
                    <tr>
                        <td class="r">产品价格:</td>
                        <td><input class="easyui-validatebox" id="price" name="price" value="0" data-options="required:true" missingmessage="请输入产品单价！" validtype="money" /></td>
                    </tr>
                    <tr>
                        <td class="r">产品折扣:</td>
                        <td><input class="easyui-validatebox" id="discount" name="discount" value="0" data-options="required:true" missingmessage="请输入产品折扣！" validtype="number" /></td>
                    </tr>
                    <tr>
                        <td class="r">产品数量:</td>
                        <td><input class="easyui-validatebox" id="count" name="count" value="0" data-options="required:true" missingmessage="请输入产品数量！" validtype="number" /></td>
                    </tr>
                    <tr>
                        <td class="r" valign="top">产品图片:</td>
                        <td>
                            <input class="easyui-validatebox" id="image" name="image" /><a id="image_upload" name="image_upload">确 定</a>
                            <div id="queueDiv"></div>
                            <a class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="javascript:$('#image_upload').uploadifyUpload();" >上 传</a>
                            <a class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="javascript:$('#image_upload').uploadifyUpload();" >取 消</a>
                        </td>
                    </tr>
                    <tr>
                        <td class="r">产品描述:</td>
                        <td><textarea class="textarea" id="describe" name="describe" ></textarea></td>
                    </tr>
                </table>
                </form>
                <div class="blank"></div>
                <div style="text-align: center; padding: 5px">
                    <a class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="addGoodsModel();" >确 定</a>
                    <a class="easyui-linkbutton" data-options="iconCls:'icon-no'" onclick="cancelGoodsWin();" >取 消</a>
                </div>
            </div>
            <!--end 表单窗口-->
        </div>
    </body>
</html>
