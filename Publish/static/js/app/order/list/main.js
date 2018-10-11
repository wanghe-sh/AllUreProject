define([
    'jquery',
    'js/app/common/getQuery',
    'shopCar',
    'searchProduct',
    'js/app/service/productService',
    'jquery-cookie',
    'angular',
    'bootstrap',
    'angularjs-bootstrap',
    'angular-translate',
    'angular-translate-loader-static-files'
], function ($, getQuery, ShopCarController, SearchProductController,ProductService) {
    "use strict";

    var orderList = function(options){
        this.init.apply(this, arguments);
    };

    orderList.prototype.init = function () {
        var that = this;
        
    };

    var orderListApp = angular.module('orderListModule', ['ui.bootstrap', 'pascalprecht.translate']);
    orderListApp.config(['$translateProvider', function ($translateProvider) {
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
    orderListApp.factory('productService', ProductService);
    orderListApp.controller('ShopCarController', ShopCarController);
    orderListApp.controller('ShowSubController', function ($scope) {

    });
    orderListApp.controller('SearchProductController', SearchProductController);
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
    orderListApp.controller('switchLanguageCtrl', function ($translate,$scope) {
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
    angular.bootstrap(document, ['orderListModule']);

    

    return orderList;
});