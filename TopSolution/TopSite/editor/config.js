/**
 * @license Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here.
	// For the complete reference:
	// http://docs.ckeditor.com/#!/api/CKEDITOR.config

	// The toolbar groups arrangement, optimized for two toolbar rows.
	config.toolbarGroups = [
		{ name: 'clipboard',   groups: [ 'clipboard', 'undo' ] },
		{ name: 'editing',     groups: [ 'find', 'selection', 'spellchecker' ] },
		{ name: 'links' },
		{ name: 'insert' },
		{ name: 'forms' },
		{ name: 'tools' },
		{ name: 'document',	   groups: [ 'mode', 'document', 'doctools' ] },
		{ name: 'others' },
		'/',
		{ name: 'basicstyles', groups: [ 'basicstyles', 'cleanup' ] },
		{ name: 'paragraph',   groups: [ 'list', 'indent', 'blocks', 'align' ] },
		{ name: 'styles' },
		{ name: 'colors' },
		{ name: 'about' }
	];

	// Remove some buttons, provided by the standard plugins, which we don't
	// need to have in the Standard(s) toolbar.
	config.removeButtons = 'Underline,Subscript,Superscript';

    // 集成ckfinder
	var ckfinderPath = "/ckfinder";
	//主要是改这里的路径代码
	config.filebrowserBrowseUrl = ckfinderPath + '/ckfinder.html';
	config.filebrowserImageBrowseUrl = ckfinderPath + '/ckfinder.html?Type=Images';
	config.filebrowserFlashBrowseUrl = ckfinderPath + '/ckfinder.html?Type=Flash';
	config.filebrowserUploadUrl = ckfinderPath + '/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
	config.filebrowserImageUploadUrl = ckfinderPath + '/core/connector/aspx/connector.aspx?command=QuickUpload&type=Images';
	config.filebrowserFlashUploadUrl = ckfinderPath + '/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';
};
