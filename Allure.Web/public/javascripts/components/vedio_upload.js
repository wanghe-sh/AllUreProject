define([
	'angular',
	'jquery',
	'lodash',
	'angular-ui-bootstrap'
], function(angular, $, _, WebUploader, language) {
	return angular
		.module('c-vedioupload', ['ui.bootstrap'])
		.directive('vedioupload', ['$modal',
			function($modal) {

				function vedioCtrl($scope, $modalInstance, url) {
					$scope.vedio = {
						url: url
					}
					$scope.ok = function() {
						$modalInstance.close($scope.vedio.url);
					};
					$scope.cancel = function() {
						$modalInstance.dismiss('cancel');
					};
					// $scope.change = function() {
					// 	console.log($scope.vedio.url)
					// }
				}

				return {
					restrict: 'A',
					replace: true,
					link: function(scope, element, attr) {
						element.on('click', function(event) {
							var modalInstance = $modal.open({
								templateUrl: '/public/partials/product/flashURL.html?' + (new Date).getTime(),
								controller: vedioCtrl,
								resolve: {
									url: function() {
										return _.clone(scope[element.attr('url')]);
									}
								}
							});
							modalInstance.result.then(function(result) {
								scope[element.attr('url')] = result
								console.log(scope[element.attr('url')])
							});
						});
					}
				}
			}
		]);
});