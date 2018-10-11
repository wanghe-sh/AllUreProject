define([
    'jquery',
    'js/app/common/getQuery',
    'shopCar',
    'searchProduct',
    'addProduct',
    'js/app/service/productService',
    'jquery-cookie',
    'angular',
    'bootstrap',
    'angularjs-bootstrap',
    'angular-translate',
    'angular-translate-loader-static-files'
], function ($, getQuery, ShopCarController,SearchProductController, AddProductController , ProductService) {
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

    var productDetailApp = angular.module('productDetailModule', ['ui.bootstrap', 'pascalprecht.translate']);
    productDetailApp.config(['$translateProvider', function ($translateProvider) {
        $translateProvider.useStaticFilesLoader({
            files: [{
                prefix: '/i18n/',
                suffix: '.json'
            }]
        });
        $translateProvider.determinePreferredLanguage();
        var lang = window.localStorage.lang || 'zh-CN';
        $translateProvider.preferredLanguage(lang);
        //$translateProvider.fallbackLanguage(lang);
        $translateProvider.useSanitizeValueStrategy('escape');

    }]);
    productDetailApp.factory('productService', ProductService);

    productDetailApp.controller('ShopCarController', ShopCarController);
    productDetailApp.controller('SearchProductController', SearchProductController);

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
    productDetailApp.controller('switchLanguageCtrl', function ($translate, $scope) {
        $scope.addLanguage = function (languageCode) {
            var hash = window.location.href.toLowerCase();
            var urlQuery = getQuery.query();
            urlQuery.lang = languageCode;
            var query = '?';
            $.each(urlQuery, function (p, v) {
                query = query + p + '=' + v + '&';
            });

            window.location.href = window.location.origin + window.location.pathname + query.replace(/&$/, '');
            $translate.use(languageCode);
            window.localStorage.lang = languageCode;

        }
    });
    angular.bootstrap(document, ['productDetailModule']);

    return productDetail;
});