/**
 * @license Copyright (c) 2003-2021, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';


    config.syntaxhighlight_lang = 'csharp';
    config.syntaxhighlight_hideControls = true;
    config.language = '~/Areas/Admin/Helpers/ckeditor/lang/vi.js';
  /*  config.filebrowserBrowseUrl = '/Areas/Admin/Helpers/ckeditor/ckfinder/ckfinder.html';*/
    //config.filebrowserImageBrowseUrl = '/Areas/Admin/Helpers/ckeditor/ckfinder.html?Type=Images';
    //config.filebrowserFlashBrowseUrl = '/Areas/Admin/Helpers/ckeditor/ckfinder.html?Type=Flash';
    //config.filebrowserUploadUrl = '/Areas/Admin/Helpers/ckeditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    //config.filebrowserImageUploadUrl = '/Data/';
    //config.filebrowserFlashUploadUrl = '/Areas/Admin/Helpers/ckeditor/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
    //CKFinder.setupCKEditor(null, '/Areas/Admin/Helpers/ckeditor/ckfinder/'); 
};
