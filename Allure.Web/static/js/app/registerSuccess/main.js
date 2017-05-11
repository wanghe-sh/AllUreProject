define([
    'jquery',
    'js/app/common/getQuery',
    'shopCar',
    'js/app/service/productService',
    'angular',
    'bootstrap',
    'angularjs-bootstrap',
    'angular-translate',
    'angular-translate-loader-static-files'
], function ($, getQuery, ShopCarController, ProductService, $translate) {
    "use strict";

    var registerSuccess = function (options) {
        this.init.apply(this, arguments);
    };

    registerSuccess.prototype.init = function(){
        var that = this;
    };

    
    var registerSuccessApp = angular.module('registerSuccessModule', ['ui.bootstrap', 'pascalprecht.translate']);
    registerSuccessApp.config(['$translateProvider', function ($translateProvider) {
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
    registerSuccessApp.factory('productService', ProductService);
    registerSuccessApp.factory('T', ['$translate', function ($translate) {
        var T = {
            T: function (key) {
                if (key) {
                    return $translate.instant(key);
                }
                return key;
            }
        }
        return T;
    }]);

    registerSuccessApp.controller('ShopCarController', ShopCarController);
    registerSuccessApp.controller('ShowSubController', function ($scope) {

    });
    
    registerSuccessApp.controller('AllureProductController', function ($scope, productService, $translate,T) {
        $translate('shopcarsempty').then(function (translations) {
            
        });
    });
    registerSuccessApp.controller('switchLanguageCtrl', function ($translate,$scope) {
        $scope.addLanguage = function (languageCode) {
            var hash = window.location.href.toLowerCase();
            var urlQuery = getQuery.query();
            urlQuery.lang = languageCode;
            var query = '?';
            $.each(urlQuery, function (p, v) {
                query = query + p + '=' + v + '&';
            });

            window.location.href = window.location.origin  + window.location.pathname + query.replace(/&$/, '');
            $translate.use(languageCode);
            window.localStorage.lang = languageCode;
        }
    });
    angular.bootstrap(document, ['registerSuccessModule']);

    return registerSuccess;

});