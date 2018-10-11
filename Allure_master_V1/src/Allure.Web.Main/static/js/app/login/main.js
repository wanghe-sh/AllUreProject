define([
    'jquery',
    'js/app/common/getQuery',
    'shopCar',
    'js/app/service/productService',
    'angular',
    'bootstrap',
    'angularjs-bootstrap',
    'validate'
], function ($, getQuery, ShopCarController, ProductService) {
    "use strict";

    var login = function(options){
        this.init.apply(this, arguments);
    };

    login.prototype.init = function () {
        var that = this;
        that.validate();
    };
    
    login.prototype.validate = function () {
        var that = this;
        var $form = $('.register-form');
        $form.validate({
            'rules': {
                'email': {
                    required: true,
                    email: true
                },
                'plainTextPassword': {
                    required: true,
                    rangelength: [5,15]
                }
            },

            'messages': {
                'email': {
                    required: "please input email",
                    email: "please valid email"
                },
                'plainTextPassword': {
                    required: "please input password",
                    rangelength: 'please input valid password'
                }
            },

            'success': function(label){
                label.parents('.form-group').removeClass('has-error');
            },

            'errorPlacement': function(error, element){
                element.parents('.form-group').addClass('has-error');
                error.appendTo(element.parent());  
            },

            'submitHandler':function(form){
                form.submit();
            }    

        });
    };

    var loginApp = angular.module('loginModule', ['ui.bootstrap']);
    loginApp.factory('productService', ProductService);
    loginApp.controller('ShopCarController', ShopCarController);
    loginApp.controller('ShowSubController', function ($scope) {

    });
    loginApp.controller('AllureProductController', function ($scope, productService) {
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
    loginApp.controller('switchLanguageCtrl', function ($scope) {
        $scope.addLanguage = function (languageCode) {
            var hash = window.location.href.toLowerCase();
            var urlQuery = getQuery.query();
            urlQuery.lang = languageCode;
            var query = '?';
            $.each(urlQuery, function (p, v) {
                query = query + p + '=' + v + '&';
            });
            window.location.href = window.location.href.split('?')[0] + query.replace(/&$/, '');

        }
    });
    angular.bootstrap(document, ['loginModule']);

    return login;

});