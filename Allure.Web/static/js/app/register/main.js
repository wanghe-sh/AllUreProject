define([
    'jquery',
    'js/app/common/getQuery',
    'shopCar',
    'subscribedMail',
    'js/app/service/productService',
    'angular',
    'bootstrap',
    'angularjs-bootstrap',
    'validate',
    'angular-translate',
    'angular-translate-loader-static-files'
], function ($, getQuery, ShopCarController, SubscribedMailController, ProductService) {
    "use strict";
    var register = function (options) {
        this.init.apply(this, arguments);
    };

    register.prototype.init = function () {
        var that = this;
        that.validate();
    };

    var emailrequired = "";
    var emailemail = "";
    var emailremote = "";
    var genderrequired = "";
    var firstNamerequired = "";
    var firstNamerangelength = "";
    var lastNamerequired = "";
    var lastNamerangelength = "";
    var telephonerequired = "";
    var telephonenumber = "";
    var mobilerequired = "";
    var mobilenumber = "";
    var plainTextPasswordrequired = "";
    var plainTextPasswordrangelength = "";
    var plainTextConfirmPasswordrequired = "";
    var plainTextConfirmPasswordequalTo = "";
    var addressrequired = "";
    var postCoderequired = "";
    var companyrequired = "";
    var agreeProtocolrequired = "";

    register.prototype.validate = function () {
        var that = this;

    };

    register.prototype.resetValidate = function () {
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
                'email': {
                    required: true,
                    email: true,
                    remote: {
                        url: "/user/EmailExists",
                        type: "post",
                        data: {
                            email: function () {
                                return $("#email").val();
                            }
                        }
                    }
                },
                'gender': {
                    required: true
                },
                'firstName': {
                    required: true,
                    rangelength: [1, 10]
                },
                'lastName': {
                    required: true,
                    rangelength: [1, 10]
                },
                'telephone': {
                    required: false,
                    number: true
                },
                'mobile': {
                    required: false,
                    number: true
                },
                'plainTextPassword': {
                    required: true,
                    rangelength: [5, 15],
                    regex: /^(?=.*\d+)(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z]{5,15}$/
                },
                'plainTextConfirmPassword': {
                    required: true,
                    equalTo: '#plainTextPassword'
                },
                'address': {
                    required: false
                },
                'postCode': {
                    required: false
                },
                'company': {
                    required: false
                },
                'agreeProtocol': {
                    required: true,
                }
            },
            'messages': {
                'email': {
                    required: emailrequired,
                    email: emailemail,
                    remote: emailremote
                },
                'gender': {
                    required: genderrequired
                },
                'firstName': {
                    required: firstNamerequired,
                    rangelength: firstNamerangelength
                },
                'lastName': {
                    required: lastNamerequired,
                    rangelength: lastNamerangelength
                },
                'telephone': {
                    required: telephonerequired,
                    number: telephonenumber
                },
                'mobile': {
                    required: mobilerequired,
                    number: mobilenumber
                },
                'plainTextPassword': {
                    required: plainTextPasswordrequired,
                    rangelength: plainTextPasswordrangelength
                },
                'plainTextConfirmPassword': {
                    required: plainTextConfirmPasswordrequired,
                    equalTo: plainTextConfirmPasswordequalTo
                },
                'address': {
                    required: addressrequired
                },
                'postCode': {
                    required: postCoderequired
                },
                'company': {
                    required: companyrequired
                },
                'agreeProtocol': {
                    required: agreeProtocolrequired
                }
            },

            'success': function (label) {
                label.parents('.form-group').removeClass('has-error');
            },

            'errorPlacement': function (error, element) {
                var $formGroup = element.closest('.form-group');
                $formGroup.addClass('has-error');
                error.appendTo($formGroup.find('.col-sm-8'));
            },

            'submitHandler': function (form) {
                form.submit();
            }

        });
    }

    var registerApp = angular.module('registerModule', ['ui.bootstrap', 'pascalprecht.translate']);
    registerApp.config(['$translateProvider', function ($translateProvider) {
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
    registerApp.factory('productService', ProductService);
    registerApp.factory('T', ['$translate', function ($translate) {
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

    registerApp.controller('ShopCarController', ShopCarController);
    registerApp.controller('ShowSubController', function ($scope) {

    });
    registerApp.controller('SubscribedMailController', SubscribedMailController);
    registerApp.controller('AllureProductController', function ($scope, productService, $translate, T) {
        $translate('shopcarsempty').then(function (translations) {

            emailrequired = T.T('emailrequired');
            emailemail = T.T('emailemail');
            emailremote = T.T('emailremote');
            genderrequired = T.T('genderrequired');
            firstNamerequired = T.T('firstNamerequired');
            firstNamerangelength = T.T('firstNamerangelength');
            lastNamerequired = T.T('lastNamerequired');
            lastNamerangelength = T.T('lastNamerangelength');
            telephonerequired = T.T('telephonerequired');
            telephonenumber = T.T('telephonenumber');
            mobilerequired = T.T('mobilerequired');
            mobilenumber = T.T('mobilenumber');
            plainTextPasswordrequired = T.T('plainTextPasswordrequired');
            plainTextPasswordrangelength = T.T('plainTextPasswordrangelength');
            plainTextConfirmPasswordrequired = T.T('plainTextConfirmPasswordrequired');
            plainTextConfirmPasswordequalTo = T.T('plainTextConfirmPasswordequalTo');
            addressrequired = T.T('addressrequired');
            postCoderequired = T.T('postCoderequired');
            companyrequired = T.T('companyrequired');
            agreeProtocolrequired = T.T('agreeProtocolrequired');

            register.prototype.resetValidate();
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
    registerApp.controller('switchLanguageCtrl', function ($translate, $scope) {
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
    angular.bootstrap(document, ['registerModule']);

    return register;

});