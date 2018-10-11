define([
    'jquery',
    'js/app/common/getQuery',
    'shopCar',
    'angular',
    'bootstrap',
    'angularjs-bootstrap',
    'validate',
    'angular-translate',
    'angular-translate-loader-static-files'
], function ($, getQuery, ShopCarController) {
    "use strict";

    var findPassword = function(options){
        this.init.apply(this, arguments);
    };

    var emailrequired = "";
    var emailemail = "";


    findPassword.prototype.init = function(){
        var that = this;
        that.validate();
    };
    

    findPassword.prototype.validate = function(){
        var that = this;
        
    };

    findPassword.prototype.resetValidate = function () {
        var $form = $('.register-form');
        $form.validate({
            'rules': {
                'email': {
                    required: true,
                    email: true
                }
            },

            'messages': {
                'email': {
                    required: emailrequired,
                    email: emailemail
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
                form.submit();
            }

        });
    }
    var findPasswordApp = angular.module('findPasswordModule', ['ui.bootstrap', 'pascalprecht.translate']);
    findPasswordApp.config(['$translateProvider', function ($translateProvider) {
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
    findPasswordApp.controller('ShopCarController', ShopCarController);
    findPasswordApp.controller('ShowSubController', function ($scope) {

    });
    findPasswordApp.factory('T', ['$translate', function ($translate) {
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
    findPasswordApp.controller('AllureProductController', function ($scope, $translate, T) {

        $translate('shopcarsempty').then(function (translations) {
            emailrequired = T.T('emailrequired');
            emailemail = T.T('emailemail');
            findPassword.prototype.resetValidate();
        });

        
    });

    findPasswordApp.controller('switchLanguageCtrl', function ($translate,$scope) {
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
    angular.bootstrap(document, ['findPasswordModule']);

    return findPassword;

});