define([
	'angular',
	'lodash'
], function(angular, _) {
	return angular
		.module('c-updateInputImage', [])
		.directive('updateInputImage', [
			function() {
				var reg = /^image\//ig;
				return {
					restrict: 'E',
					// replace: true,
					require: 'ngModel',
					template: '<span class="btn btn-default btn-file"><span>上传图片 <input id="updateFiles" type="file" /></span></span>',
					scope: {
						model: '=ngModel',
						alerts: '=ngAlerts'
					},
					controller: ['$scope', function($scope) {}],
					link: function(scope, element, attributes) {
						element.bind("change", function(changeEvent) {
							var file = changeEvent.target.files[0];
							scope.alerts.splice(0);
							reg.lastIndex = 0;
							if (reg.exec(file.type)) {
								scope.model.push(changeEvent.target.files[0]);
								scope.$apply();
							} else {
								scope.alerts.push({
									msg: '上传图片格式不正确'
								});
								scope.$apply();
							}
						});
					}
				}
			}
		]);
});