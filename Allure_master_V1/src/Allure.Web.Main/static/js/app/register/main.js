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

    var register = function(options){
        this.init.apply(this, arguments);
    };

    register.prototype.init = function(){
        var that = this;
        that.validate();
    };
    
    register.prototype.validate = function(){
        var that = this;
        var $form = $('.register-form');
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
                    rangelength: [5, 10]
                },
                'lastName': {
                    required: true,
                    rangelength: [5, 10]
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
                    rangelength: [5,15]
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
                    required: "please input email",
                    email: "please valid email",
                    remote: "Email address already in use. Please use other email."
                },
                'gender': {
                    required: "please select gender"
                },
                'firstName': {
                    required: "please input first name",
                    rangelength: "please input character's number 5~10"
                },
                'lastName': {
                    required: "please input last name",
                    rangelength: "please input character's number 5~10"
                },
                'telephone': {
                    required: "please input fixed line phone",
                    number: "please input number"
                },
                'mobile': {
                    required: "please input mobile",
                    number: "please input number"
                },
                'plainTextPassword': {
                    required: "please input password",
                    rangelength: 'please input valid password'
                },
                'plainTextConfirmPassword': {
                    required: "please input confirm password",
                    equalTo: "please input same password"
                },
                'address': {
                    required: "please input address",
                },
                'postCode': {
                    required: "please input post code",
                },
                'company': {
                    required: "please input company",
                },
                'agreeProtocol': {
                    required: "please check protocol",
                }
            },

            'success': function(label){
                label.parents('.form-group').removeClass('has-error');
            },

            'errorPlacement': function (error, element) {
                var $formGroup = element.closest('.form-group');
                $formGroup.addClass('has-error');
                error.appendTo($formGroup.find('.col-sm-8'));
            },

            'submitHandler':function(form){
                form.submit();
            }    

        });
    };

    var registerApp = angular.module('registerModule', ['ui.bootstrap', 'pascalprecht.translate']);
    registerApp.config(['$translateProvider', function ($translateProvider) {
        $translateProvider.useStaticFilesLoader({
            files: [{
                prefix: '/i18n/',
                suffix: '.json'
            }]
        });
        $translateProvider.determinePreferredLanguage();
        var lang = window.localStorage.lang || 'en-US';
        //$translateProvider.preferredLanguage(lang);
        $translateProvider.fallbackLanguage(lang);
    }]);
    registerApp.factory('productService', ProductService);
    registerApp.controller('ShopCarController', ShopCarController);
    registerApp.controller('ShowSubController', function ($scope) {

    });
    registerApp.controller('AllureProductController', function ($scope, productService) {
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
    registerApp.controller('switchLanguageCtrl', function ($translate,$scope) {
        $scope.addLanguage = function (languageCode) {
            //var hash = window.location.href.toLowerCase();
            //var urlQuery = getQuery.query();
            //urlQuery.lang = languageCode;
            //var query = '?';
            //$.each(urlQuery, function (p, v) {
            //    query = query + p + '=' + v + '&';
            //});
            //window.location.href = window.location.origin + query.replace(/&$/, '');
            $translate.use(languageCode);
            window.localStorage.lang = languageCode;
        }
    });
    angular.bootstrap(document, ['registerModule']);

    return register;

});