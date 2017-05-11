define([
    'jquery',
    'js/app/common/getQuery',
    'shopCar',
    'searchProduct',
    'js/app/service/productService',
    'angular',
    'bootstrap',
    'angularjs-bootstrap',
    'angular-translate',
    'angular-translate-loader-static-files'
], function ($, getQuery, ShopCarController,SearchProductController, ProductService) {
    "use strict";

    var alAbout = function(options){
        this.init.apply(this, arguments);
    };

    alAbout.prototype.init = function(){
        var that = this;
        that.slide();
    };

    alAbout.prototype.slide = function(){
        var $alCarousel =  $('.al-carousel');
        $alCarousel.carousel({
            'interval': 5000,
            'wrap': true
        })
    };

    var headerApp = angular.module('aboutModule', ['ui.bootstrap', 'pascalprecht.translate']);
    headerApp.config(['$translateProvider', function ($translateProvider) {
        $translateProvider.useStaticFilesLoader({
            files: [{
                prefix: '/i18n/',
                suffix: '.json'
            }]
        });
        $translateProvider.determinePreferredLanguage();
        var lang = window.localStorage.lang || 'zh-CN';
        //$translateProvider.preferredLanguage(lang);
        $translateProvider.fallbackLanguage(lang);
        $translateProvider.useSanitizeValueStrategy('escape');

    }]);
    headerApp.factory('productService', ProductService);
    headerApp.controller('ShopCarController', ShopCarController);
    headerApp.controller('ShowSubController', function ($scope) {

    });
    headerApp.controller('SearchProductController', SearchProductController);
    headerApp.controller('AllureProductController', function ($scope, productService) {
        
    });
    headerApp.controller('switchLanguageCtrl', function ($scope, $translate) {
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
    angular.bootstrap(document, ['aboutModule']);
    return alAbout;

});