define([
	'angular',
	'jquery',
	'lodash',
	'webuploader',
	'language',
	'angular-ui-bootstrap'
], function(angular, $, _, WebUploader, language) {
	return angular
		.module('c-imageupload', ['ui.bootstrap'])
		.directive('imageupload', ['$modal',
			function($modal) {
				var domain = '';

				function ImageUploadCtrl($scope, $modalInstance, images) {
					$scope.images = images;

					$scope.remove = function(index) {
						images.splice(index, 1)
					}

					$scope.ok = function() {
						var rets = [];

						$modalInstance.close($scope.images);
					};
					$scope.cancel = function() {
						$modalInstance.dismiss('cancel');
					};

					$scope.func = function() {

						// 初始化Web Uploader
						var uploader = WebUploader.create({

							// 选完文件后，是否自动上传。
							auto: true,

							// swf文件路径
							// swf: BASE_URL + '/js/Uploader.swf',

							// 文件接收服务端。
							server: domain + '/product/upload',

							// 选择文件的按钮。可选。
							// 内部根据当前运行是创建，可能是input元素，也可能是flash.
							pick: '#filePicker',

							// 只允许选择图片文件。
							accept: {
								title: 'Images',
								extensions: 'gif,jpg,jpeg,bmp,png',
								mimeTypes: 'image/*'
							}
						});

						function success(file, res) {
							images.push({
								small: res.small,
								large: res.large,
								tmb: res.tmb
							});
							$scope.$apply();
						}

						function error(file) {

						}

						// 文件上传成功，给item添加成功class, 用样式标记上传成功。
						uploader.on('uploadSuccess', success);

						// 文件上传失败，显示上传出错。
						uploader.on('uploadError', success);
					}
				}

				return {
					restrict: 'A',
					replace: true,
					link: function(scope, element, attr) {
						element.on('click', function(event) {
							var modalInstance = $modal.open({
								templateUrl: '/public/tpls/imageupload.html?' + (new Date).getTime(),
								controller: ImageUploadCtrl,
								resolve: {
									images: function() {
										return _.clone(scope[element.attr('options')]);
									}
								}
							});
							modalInstance.result.then(function(rets) {
								scope[element.attr('options')] = rets;
							});
						});
					}
				}
			}
		]);
});