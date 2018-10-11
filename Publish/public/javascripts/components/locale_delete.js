define([
	'angular',
	'angular-ui-bootstrap'
], function(angular, _) {
	return angular
		.module('c-localeDelete', ['ui.bootstrap'])
		.directive('localeDelete', ['$modal', '$http',
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
					    element.on('click', function (event) {
					        
							var modalInstance = $modal.open({
								templateUrl: '/public/partials/admin/product/delete_category.html?' + (new Date).getTime(),
								controller: ['$scope', '$modalInstance', modalCtrl]
							});
							modalInstance.result.then(function() {
							    var postUrl;
							    postUrl = '/api/locales/' + scope.model.id;
								
							    $http({ url: postUrl, method: "DELETE" })
									.success(function() {
									    window.location.reload();
									})
									.error(function(msg) {
									    alert(msg);
									});
							});
						});
					}
				}
			}
		]);
});