define([
    'jquery',
    'js/app/common/getQuery',
    'shopCar',
    'addProduct',
    'js/app/service/productService',
    'jquery-cookie',
    'angular',
    'bootstrap',
    'angularjs-bootstrap'
], function ($, getQuery, ShopCarController, AddProductController, ProductService) {
    "use strict";

    var productDetail = function(options){
        this.init.apply(this, arguments);
    };

    productDetail.prototype.init = function(){
        var that = this;
        that.collapse();
    };
    
    productDetail.prototype.collapse = function(){
        var that = this;
        $('.collapse').collapse();
    };

    var productDetailApp = angular.module('productDetailModule', ['ui.bootstrap']);
    productDetailApp.factory('productService', ProductService);
    productDetailApp.controller('ShopCarController', ShopCarController);

    productDetailApp.controller('AddProductController', AddProductController);

    productDetailApp.controller('AllureProductController', function ($scope, productService) {
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
    productDetailApp.controller('ShowSubController', function ($scope) {

    });
    productDetailApp.controller('switchLanguageCtrl', function ($scope) {
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
    angular.bootstrap(document, ['productDetailModule']);

    return productDetail;
});