define([
    'jquery',
    'js/app/common/getQuery',
    'shopCar',
    'js/app/service/productService',
    'angular',
    'bootstrap',
    'angularjs-bootstrap',
    'validate',
    'angular-translate',
    'angular-translate-loader-static-files'
], function ($, getQuery, ShopCarController, ProductService) {
    "use strict";

    var login = function(options){
        this.init.apply(this, arguments);
    };

    var emailrequired = "";
    var emailemail = "";
    var plainTextPasswordrequired = "";
    var plainTextPasswordrangelength = "";
    var returnUrl = "";

    login.prototype.init = function () {
        var that = this;
        that.validate();
    };
    
    login.prototype.validate = function () {
        var that = this;
        
    };

    login.prototype.resetValidate = function () {
        var $form = $('.register-form');
        $form.validate({
            'rules': {
                'email': {
                    required: true,
                    email: true
                },
                'plainTextPassword': {
                    required: true,
                    rangelength: [5, 15]
                }
            },

            'messages': {
                'email': {
                    required: emailrequired,
                    email: emailemail
                },
                'plainTextPassword': {
                    required: plainTextPasswordrequired,
                    rangelength: plainTextPasswordrangelength
                }
            },

            'success': function (label) {
                label.parents('.form-group').removeClass('has-error');
            },

            'errorPlacement': function (error, element) {
                element.parents('.form-group').addClass('has-error');
                error.appendTo(element.parent());
            },

            'submitHandler': function (form) {
                var urlQuery = getQuery.query();
                if (urlQuery["ReturnUrl"] != undefined && ($("#returnUrl").val() == undefined || $("#returnUrl").val() == "")) {
                    $("#returnUrl").val(urlQuery["ReturnUrl"]);
                }
                form.submit();
            }

        });
    };


    var loginApp = angular.module('loginModule', ['ui.bootstrap', 'pascalprecht.translate']);
    loginApp.config(['$translateProvider', function ($translateProvider) {
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
        // Enable escaping of HTML
        $translateProvider.useSanitizeValueStrategy('escape');
    }]);
    loginApp.factory('productService', ProductService);
    loginApp.controller('ShopCarController', ShopCarController);
    loginApp.controller('ShowSubController', function ($scope) {

    });
    loginApp.factory('T', ['$translate', function ($translate) {
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
    loginApp.controller('AllureProductController', function ($scope, productService, $translate, T) {

        $translate('shopcarsempty').then(function (translations) {

            emailrequired = T.T('emailrequired');
            emailemail = T.T('emailemail');
            
            plainTextPasswordrequired = T.T('plainTextPasswordrequired');
            plainTextPasswordrangelength = T.T('plainTextPasswordrangelength');
            

            login.prototype.resetValidate();
        });

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
    loginApp.controller('switchLanguageCtrl', function ($translate, $scope) {
        $scope.addLanguage = function (languageCode) {
            var hash = window.location.href.toLowerCase();
            var urlQuery = getQuery.query();
            urlQuery.lang = languageCode;
            var query = '?';
            $.each(urlQuery, function (p, v) {
                query = query + p + '=' + v + '&';
            });
            window.location.href = window.location.href.split('?')[0] + query.replace(/&$/, '');
            $translate.use(languageCode);
            window.localStorage.lang = languageCode;

        }
    });
    angular.bootstrap(document, ['loginModule']);

    return login;

});