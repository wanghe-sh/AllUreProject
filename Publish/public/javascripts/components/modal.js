define([
	'angular',
	'jquery',
	'lodash',
	'webuploader',
	'language',
	'angular-ui-bootstrap'
], function(angular, $, _, WebUploader, language) {
	return angular
		.module('c-modal', ['ui.bootstrap'])
		.directive('modal', ['$modal',
			function($modal) {

				return {
					restrict: 'A',
					replace: true,
					link: function(scope, element, attr) {
						element.on('click', function(event) {
							var templateUrl = element.attr('templateUrl'),
								controller = element.attr('controller');
							var modalInstance = $modal.open({
								templateUrl: templateUrl + '?' + (new Date).getTime(),
								controller: controller
							});
							// modalInstance.result.then(function(rets) {
							// 	scope[element.attr('options')] = rets;
							// });
						});
					}
				}
			}
		]);
});