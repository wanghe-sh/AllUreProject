define([
    'angular',
    'angular-ui-bootstrap'
], function(angular, _) {
    return angular
        .module('c-orderDelete', ['ui.bootstrap'])
        .directive('orderDelete', ['$modal', '$http', '$location',
            function($modal, $http, $location) {

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
                        'orderId': '='
                    },
                    link: function(scope, element, attr) {
                        element.on('click', function(event) {
                            var modalInstance = $modal.open({
                                templateUrl: '/public/partials/order/delete_order.html?' + (new Date).getTime(),
                                controller: ['$scope', '$modalInstance', modalCtrl]
                            });
                            modalInstance.result.then(function() {
                                $http.post('/order/delete/' + scope.orderId).success(function(data){
                                    if(data.data){
                                        alert('删除订单成功');
                                        $location.path('/orders');
                                    }
                                }).error(function(){
                                    alert('服务器出错');
                                });
                            });
                        });
                    }
                }
            }
        ]);
});