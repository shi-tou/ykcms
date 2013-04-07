/*
Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:;
    config.language = 'zh-cn';
    config.skin = 'kama'; //'kama'（默认）、'office2003'、'v2'
    config.uiColor = '#E0EDEE'; //背景颜色
    config.toolbar = 'MyToolbar'; //工具栏
    config.fontSize_defaultLabel = '12px'; //字体默认大小
    config.toolbar_MyToolbar = [{ name: 'document', items: ['Source'] }, { name: 'basicstyles', items: ['Bold', '-', 'RemoveFormat'] }, { name: 'clipboard', items: ['Undo', 'Redo', "-", "Find", "Replace"] }, { name: 'insert', items: ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'] }, { name: 'links', items: ['Link', 'Unlink', 'Anchor']}];
    config.filebrowserBrowseUrl = '/ckfinder/ckfinder.html'; //不要写成"~/ckfinder/..."或者"/ckfinder/..."
    config.filebrowserImageBrowseUrl = '/ckfinder/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/ckfinder/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
    config.filebrowserFlashUploadUrl = '/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
    config.baseFloatZIndex = 10000;
    config.filebrowserWindowWidth = '800px';  //“浏览服务器”弹出框的size设置
    config.filebrowserWindowHeight = '400px';
};
