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

    var resetPassword = function(options){
        this.init.apply(this, arguments);
    };

    var plainTextPasswordrequired = "";
    var plainTextPasswordrangelength = "";
    var plainTextConfirmPasswordrequired = "";
    var plainTextConfirmPasswordequalTo = "";

    resetPassword.prototype.init = function(){
        var that = this;
        that.validate();
    };
    
    resetPassword.prototype.validate = function(){
        var that = this;

    };
    resetPassword.prototype.resetValidate = function () {
        var $form = $('.register-form');
        $.validator.addMethod(
            "regex",
            function (value, element, regexp) {
                var re = new RegExp(regexp);
                return this.optional(element) || re.test(value);
            },
            plainTextPasswordrangelength
        );
        $form.validate({
            'rules': {
                'userPassword': {
                    required: true,
                    rangelength: [5, 15],
                    regex: /^(?=.*\d+)(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z]{5,15}$/                    
                },
                'userConfirmPassword': {
                    required: true,
                    equalTo: '#userPassword'
                }
            },

            'messages': {
                'userPassword': {
                    required: plainTextPasswordrequired,
                    rangelength: plainTextPasswordrangelength
                },
                'userConfirmPassword': {
                    required: plainTextConfirmPasswordrequired,
                    equalTo: plainTextConfirmPasswordequalTo
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
    var resetPasswordApp = angular.module('resetPasswordModule', ['ui.bootstrap', 'pascalprecht.translate']);
    resetPasswordApp.config(['$translateProvider', function ($translateProvider) {
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
    }]);
    resetPasswordApp.factory('T', ['$translate', function ($translate) {
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
    resetPasswordApp.controller('ShopCarController', ShopCarController);
    resetPasswordApp.controller('ShowSubController', function ($scope) {

    });
    resetPasswordApp.controller('AllureProductController', function ($scope, $translate, T) {

        $translate('shopcarsempty').then(function (translations) {

            
            plainTextPasswordrequired = T.T('plainTextPasswordrequired');
            plainTextPasswordrangelength = T.T('plainTextPasswordrangelength');
            plainTextConfirmPasswordrequired = T.T('plainTextConfirmPasswordrequired');
            plainTextConfirmPasswordequalTo = T.T('plainTextConfirmPasswordequalTo');
            

            resetPassword.prototype.resetValidate();
        });
        
    });
    resetPasswordApp.controller('switchLanguageCtrl', function ($translate, $scope) {
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
    angular.bootstrap(document, ['resetPasswordModule']);

    return resetPassword;

});