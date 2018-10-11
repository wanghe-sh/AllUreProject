define([
    'jquery',
    'js/app/common/getQuery',
    'shopCar',
    'js/app/service/productService',
    'jquery-cookie',
    'angular',
    'bootstrap',
    'angularjs-bootstrap'
], function ($, getQuery, ShopCarController, ProductService) {
    "use strict";

    var orderList = function(options){
        this.init.apply(this, arguments);
    };

    orderList.prototype.init = function () {
        var that = this;
        
    };

    var orderListApp = angular.module('orderListModule', ['ui.bootstrap']);
    orderListApp.factory('productService', ProductService);
    orderListApp.controller('ShopCarController', ShopCarController);
    orderListApp.controller('ShowSubController', function ($scope) {

    });
    orderListApp.controller('AllureProductController', function ($scope, productService) {
        $scope.productType = {};
        var typeNum = productService.getProductTypeNum();
        $scope.productType.shopCars = productService.getAllProduct();
        $scope.productType.typeNum = typeNum;
        $scope.productType.sum = function () {
            var total = 0;
            angular.forEach($scope.productType.shopCars, function (item) {
                total += item.number * item.price;
            });
            return total;
        };
    });
    orderListApp.controller('switchLanguageCtrl', function ($scope) {
        $scope.addLanguage = function (languageCode) {
            var hash = window.location.href.toLowerCase();
            var urlQuery = getQuery.query();
            urlQuery.lang = languageCode;
            var query = '?';
            $.each(urlQuery, function (p, v) {
                query = query + p + '=' + v + '&';
            });
            window.location.href = window.location.origin + query.replace(/&$/, '');

        }
    });
    angular.bootstrap(document, ['orderListModule']);

    

    return orderList;
});