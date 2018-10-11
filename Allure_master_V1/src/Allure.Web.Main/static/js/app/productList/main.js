define([
    'jquery',
    'js/app/common/getQuery',
    'shopCar',
    'js/app/service/productService',
    'jquery-cookie',
    'angular',
    'bootstrap',
    'angularjs-route',
    'angularjs-bootstrap'
], function ($, getQuery, ShopCarController, ProductService) {
    "use strict";

    var productList = function(options){
        this.init.apply(this, arguments);
    };

    productList.prototype.init = function(){
        var that = this;
    };

    var productListApp = angular.module('productListApp', ['ui.bootstrap', 'ngRoute']);
    productListApp.factory('productService', ProductService);
    var ITEMSPERPAGE = 8;
    var CURRENTPAGE = 1;
    productListApp.controller('ListController', function ($scope, $attrs, $http, $location) {
        window.$listScope = $scope;
        var categoryUrl = $attrs.categoryUrl;
        var totalDetails = null;
        $scope.currentPage = CURRENTPAGE;
        $scope.priceGap = {
            minPrice: 0,
            maxPrice: null
        };
        $scope.details = null;

        $scope.compare = function (minPrice, maxPrice) {
            $scope.priceGap.minPrice = minPrice;
            $scope.priceGap.maxPrice = maxPrice;
        };

        $scope.priceSection = function (detail) {
            if (detail.Price > $scope.priceGap.minPrice && (detail.Price <= $scope.priceGap.maxPrice || !$scope.priceGap.maxPrice)) {
                return true;
            }
        };

        $http.get(categoryUrl).success(function (data) {
            if (data && data.length) {
                totalDetails = data;
                $scope.details = data.slice(0, ITEMSPERPAGE);
                $scope.itemsPerPage = ITEMSPERPAGE;
                $scope.totalItems = data.length;
            }
        });

        var changePage = function () {
            $scope.details = totalDetails.slice(ITEMSPERPAGE * ($scope.currentPage - 1), ITEMSPERPAGE * $scope.currentPage);
        };


        $scope.pageChanged = function () {
            changePage();
        };

    });

    productListApp.controller('ShopCarController', ShopCarController);

    productListApp.controller('ShowSubController', function ($scope) {

    });
    productListApp.controller('AllureProductController', function ($scope, productService) {
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
    productListApp.controller('switchLanguageCtrl', function ($scope) {
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
    angular.bootstrap(document, ['productListApp']);
    return productList;
});