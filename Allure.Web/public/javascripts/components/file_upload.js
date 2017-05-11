define([
	'angular',
	// 'jquery',
	'lodash',
	'webuploader'
], function(angular, _, WebUploader) {
	return angular
		.module('c-fileupload', [])
		.directive('fileupload', ['$parse', '$rootScope',

			function($parse, $rootScope) {
				return {
					restrict: 'A',
					// require: 'ngModel',
					link: function(scope, element, attr) {
						var domain = '';
						var uploader = WebUploader.create({

							// 选完文件后，是否自动上传。
							auto: true,

							// swf文件路径
							// swf: BASE_URL + '/js/Uploader.swf',

							// 文件接收服务端。
							server: domain + '/home/upload',

							// 选择文件的按钮。可选。
							// 内部根据当前运行是创建，可能是input元素，也可能是flash.
							pick: element,

							// 只允许选择图片文件。
							accept: {
								title: 'Images',
								extensions: 'gif,jpg,jpeg,bmp,png',
								mimeTypes: 'image/*'
							}
						});

						function success(file, res) {
							$parse(attr['ngModel']).assign(scope, res.image);
							scope.$apply();
						}

						function error(file) {

						}

						// 文件上传成功，给item添加成功class, 用样式标记上传成功。
						uploader.on('uploadSuccess', success);

						// 文件上传失败，显示上传出错。
						uploader.on('uploadError', success);
					}
				}
			}
		]);
});