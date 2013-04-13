<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GoodsList.aspx.cs" Inherits="YkCms.Content.GoodsList" %>
<html>
    <head>
        <title></title>
        <link href="/jquery-easyui-1.3.2/themes/default/easyui.css" rel="stylesheet" type="text/css" />
        <link href="/jquery-easyui-1.3.2/themes/icon.css" rel="stylesheet" type="text/css" />
        <link href="../public/css/admin.css" rel="stylesheet" type="text/css" />
        <script src="/jquery-easyui-1.3.2/jquery-1.8.0.min.js" type="text/javascript"></script>
        <script src="/jquery-easyui-1.3.2/jquery.easyui.min.js" type="text/javascript"></script>
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
                <input id="sortlist-sortid" name="goodsid" type="hidden" />
                <dl>
                    <dt>产品类别</dt>
                    <dd><select id="typeid" name="typeid"><%=SortSelectHtml %></select></dd>
                    <dt>产品名称:</dt>
                    <dd><input class="easyui-validatebox" type="text" id="goodsname" name="goodsname" data-options="required:true" missingmessage="请输入产品名称！" validtype="checkSortName" /></dd>
                    <dt>产品单位:</dt>
                    <dd><input class="easyui-validatebox" type="text" id="unit" name="unit" /></dd>
                    <dt>产品价格:</dt>
                    <dd><input class="easyui-validatebox" id="price" name="price" value="0" data-options="required:true" missingmessage="请输入产品单价！" validtype="money" /></dd>
                    <dt>产品折扣:</dt>
                    <dd><input class="easyui-validatebox" id="disocout" name="disocout" value="0" data-options="required:true" missingmessage="请输入产品折扣！" validtype="number" /></dd>
                    <dt>产品数量:</dt>
                    <dd><input class="easyui-validatebox" id="count" name="count" value="0" data-options="required:true" missingmessage="请输入产品数量！" validtype="number" /></dd>
                    <dt>产品图片:</dt>
                    <dd><input class="easyui-validatebox" id="image" name="image" /></dd>
                    <dt>产品描述:</dt>
                    <dd><textarea class="textarea" id="describe" name="describe" ></textarea></dd>
                </dl>
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
