define([
	'angular',
	'angular-ui-bootstrap'
], function(angular, _) {
	return angular
		.module('c-categoryDelete', ['ui.bootstrap'])
		.directive('categoryDelete', ['$modal', '$http',
			function($modal, $http) {

				function modalCtrl($scope, $modalInstance, waitColumns, hasColumns, languages) {

					$scope.ok = function() {
						$modalInstance.close($scope.column);
					};
					$scope.cancel = function() {
						$modalInstance.dismiss('cancel');
					};
				}

				return {
					restrict: 'A',
					replace: true,
					scope: {
						model: "=ngModel",
						othersModel: '=ngOthers'
					},
					link: function(scope, element, attr) {
						element.on('click', function(event) {
							var modalInstance = $modal.open({
								templateUrl: '/public/partials/product/delete_category.html?' + (new Date).getTime(),
								controller: ['$scope', '$modalInstance', modalCtrl]
							});
							modalInstance.result.then(function() {
								var postUrl;
								if (scope.model.Children) {
									postUrl = '/Category/Delete/' + scope.model.Id;
								} else {
									postUrl = '/Category/DeleteSub/' + scope.model.Id;
								}
								$http.post(postUrl)
									.success(function() {
										for (var i = 0; i < scope.othersModel.length; i++) {
											if (scope.othersModel[i].Id === scope.model.Id) {
												scope.othersModel.splice(i, 1);
												break;
											}
										}
									})
									.error(function() {
										alert('服务器出错');
									});
							});
						});
					}
				}
			}
		]);
});