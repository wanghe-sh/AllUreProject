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
					    element.on('click', function (event) {
					        
							var modalInstance = $modal.open({
								templateUrl: '/public/partials/admin/product/delete_category.html?' + (new Date).getTime(),
								controller: ['$scope', '$modalInstance', modalCtrl]
							});
							modalInstance.result.then(function () {
							    var postUrl;
							    postUrl = '/api/categories/' + scope.model.id;
							    $http.get('/api/categories/' + scope.model.id)
                               .success(function (data) {

							            if (data.children.length > 0) {
							                if (confirm("您要删除的分类包含子分类， 请确认是否要删除？")) {
							                    $http({ url: postUrl, method: "DELETE" })
									                .success(function () {
									                    
									                    window.location.reload();
									                })
									                .error(function () {
									                    alert('服务器出错');
									                });
							                }
							            } else {
							                $http({ url: postUrl, method: "DELETE" })
									            .success(function () {
									                
									                window.location.reload();
									            })
									            .error(function () {
									                alert('服务器出错');
									            });
							            }

							            

                               })
                               .error(function (data) {

                               });

							    //var postUrl;
							    //postUrl = '/api/categories/' + scope.model.id;
								//if (scope.model.Children) {
							    //    postUrl = '/api/categories/' + scope.model.Id;
								//} else {
								//    postUrl = '/api/categories/' + scope.model.Id;
								//}
							    
							});
						});
					}
				}
			}
		]);
});