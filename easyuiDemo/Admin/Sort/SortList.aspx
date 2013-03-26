<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SortList.aspx.cs" Inherits="YkCms.SortList" %>
<!--start 脚本-->
<script src="public/js/customValidate.js" type="text/javascript"></script>
<script src="public/js/sort.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () {
        getSortList();
    });
</script>
<!--end 脚本-->
<div class="main">
    <!--start 搜索-->
    <div class="easyui-panel search" title="条件搜索">
        <table>
            <tr>
                <td>栏目名称：</td>
                <td><input id="search-sortlist-sortname" name="search-sortlist-sortname" class="tb" /></td>
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
    <table id="sortList">
	</table>
    <!--end 列表-->
    <!--start 表单窗口-->
    <div id="sortWin" class="easyui-window" title="添加栏目" data-options="iconCls:'icon-save',modal:true,closed:true,minimizable:false,maximizable:false,collapsible:false" >
        <form id="sortForm">
        <input id="sortlist-sortid" name="sortlist-sortid" type="hidden" />
        <div class="easyui-tabs" data-options="border:false">
		    <div title="基本信息" style="padding:10px">
                <dl>
                    <dt>上级栏目:</dt>
                    <dd><select id="sortlist-parentid" name="sortlist-parentid"></select></dd>
                    <dt>栏目名称:</dt>
                    <dd><input class="easyui-validatebox" type="text" id="sortlist-sortname" name="sortlist-sortname" data-options="required:true" missingmessage="请输入权限组名称！" validtype="checkSortName" /></dd>
                    <dt>栏目URL:</dt>
                    <dd><input class="easyui-validatebox" type="text" id="sortlist-sorturl" name="sortlist-sorturl"/></dd>
                    <dt>栏目属性:</dt>
                    <dd><select id="sortlist-sortattributeid" name="sortlist-sortattributeid"><%=SortAttributeSelectHtml %></select></dd>
                    <dt>栏目模板:</dt>
                    <dd><select id="sortlist-sorttemplateid" name="sortlist-sorttemplateid"></select></dd>
                    <dt>栏目Banner:</dt>
                    <dd><input class="easyui-validatebox" id="sortlist-bannerurl" name="sortlist-bannerurl" /></dd>
                    <dt>状态:</dt>
                    <dd><input type="radio" id="sortlist-open" name="sortlist-state" value="1" checked="checked" />启用<input type="radio" id="sortlist-close" name="sortlist-state" value="0" />禁用</dd>
                </dl>
		    </div>
		    <div title="SEO信息" style="padding:10px">
                <dl>
                    <dt>页面标题:</dt>
                    <dd><textarea id="sortlist-pagetitle" name="sortlist-pagetitle" class="textarea"></textarea></dd>
                    <dt>页面关键词:</dt>
                    <dd><textarea id="sortlist-pagekeywords" name="sortlist-pagekeywords" class="textarea"></textarea></dd>
                    <dt>页面描述:</dt>
                    <dd><textarea id="sortlist-pagedesc" name="sortlist-pagedesc" class="textarea"></textarea></dd>
                </dl>
		    </div>
	    </div>            
        </form>
        <div class="blank"></div>
        <div style="text-align: center; padding: 5px">
            <a class="easyui-linkbutton" data-options="iconCls:'icon-ok'" onclick="addSortModel();" >确 定</a>
            <a class="easyui-linkbutton" data-options="iconCls:'icon-no'" onclick="cancelSortWin();" >取 消</a>
        </div>
    </div>
    <!--end 表单窗口-->
</div>

