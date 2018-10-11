define([
    'jquery',
    'js/app/common/getQuery',
    'shopCar',
    'js/app/service/productService',
    'angular',
    'bootstrap',
    'angularjs-bootstrap'
], function ($, getQuery, ShopCarController, ProductService) {
    "use strict";

    var alHome = function(options){
        this.init.apply(this, arguments);
    };

    alHome.prototype.init = function(){
        var that = this;
        that.slide();
    };

    alHome.prototype.slide = function(){
        var $alCarousel =  $('.al-carousel');
        $alCarousel.carousel({
            'interval': 5000,
            'wrap': true
        })
    };

    var headerApp = angular.module('homeModule', ['ui.bootstrap']);
    headerApp.factory('productService', ProductService);
    headerApp.controller('ShopCarController', ShopCarController);
    headerApp.controller('ShowSubController', function ($scope) {

    });
    headerApp.controller('AllureProductController', function ($scope, productService) {
        $scope.productType = {};
        var typeNum = productService.getProductTypeNum();
        $scope.productType.status = {
            isopen: false
        };
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
    headerApp.controller('switchLanguageCtrl', function ($scope) {
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
    angular.bootstrap(document, ['homeModule']);
    return alHome;

});