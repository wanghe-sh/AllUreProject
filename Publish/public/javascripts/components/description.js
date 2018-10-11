define([
	'angular',
	// 'jquery',
	'lodash',
	'angular-ui-bootstrap'
], function(angular, _) {
	return angular
		.module('c-description', ['ui.bootstrap'])
		.directive('description', ['$modal', '$parse',
			function($modal, $parse) {

				function vedioCtrl($scope, $modalInstance, description) {
					$scope.model = {
						description: description
					}

					$scope.ok = function() {
						$modalInstance.close($scope.model.description);
					};
					$scope.cancel = function() {
						$modalInstance.dismiss('cancel');
					};
				}

				return {
					restrict: 'A',
					replace: true,
					link: function(scope, element, attr) {
						element.on('click', function(event) {
							var modalInstance = $modal.open({
								templateUrl: '/public/partials/description.html?' + (new Date).getTime(),
								controller: vedioCtrl,
								resolve: {
									description: function() {
										var getter = $parse(attr['ngModel']);
										return getter(scope);
									}
								}
							});
							modalInstance.result.then(function(result) {
								$parse(attr['ngModel']).assign(scope, result);
							});
						});
					}
				}
			}
		]);
});