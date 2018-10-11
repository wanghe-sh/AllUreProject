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
    'jquery-validate',
    'js/plugin/json/tool',
    'angular-translate',
    'angular-translate-loader-static-files'
], function ($, getQuery, ShopCarController, SearchProductController,ProductService) {
    "use strict";
    
    var $form = $('form.submit-order-form');
    var editOrder = function(options){
        this.init.apply(this, arguments);
    };

    editOrder.prototype.init = function () {
        var that = this;
    };

    var editOrderApp = angular.module('editOrderApp', ['ui.bootstrap', 'pascalprecht.translate']);
    editOrderApp.config(['$translateProvider', function ($translateProvider) {
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
    editOrderApp.factory('productService', ProductService);

    editOrderApp.controller('ShopCarController', ShopCarController);

    editOrderApp.controller('ShowSubController', function ($scope) {

    });
    editOrderApp.controller('SearchProductController', SearchProductController);

    editOrderApp.controller('alertCtrl', function ($scope) {
        $scope.closeAlert = function(index) {
            $scope.alerts.splice(index, 1);
        };
    });
    editOrderApp.controller('AllureProductController', function ($scope, productService) {
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
    editOrderApp.controller('switchLanguageCtrl', function ($translate,$scope) {
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
    editOrderApp.controller('orderSubmitFormCtrl', function ($scope, $http, $timeout) {
        $scope.submitOrder = function ($event) {
            $("#submitOrder").attr("disabled", "disabled");
            var details = [];
            if (!$form.valid()) {
                $("#submitOrder").removeAttr("disabled");
                return;
            }
            angular.forEach($scope.productType.shopCars, function (p, v) {
                details.push({
                    ProductId: p.id,
                    Count: p.number
                });
            });
            var data = {
                'Id': $scope.product.id,
                "WillCheck":  $scope.product.willCheck,
                "CheckAddress": $scope.product.checkAddress,
                "CheckContact": $scope.product.checkContact,
                "ReceiverName": $scope.product.receiverName,
                "ReceiverContact": $scope.product.receiverContact,
                "ReceiverAddress": $scope.product.receiverAddress,
                "ReceiverPostCode": $scope.product.receiverPostCode,
                "Details": details
            };
            $http.post('/order/save', data).success(function (data) {
                if (data && data.indexOf('ok') > -1) {
                    $scope.alerts = [
                        { type: 'success', msg: 'Save success, back to order list page after 2 seconds' }
                    ];
                    $timeout(function(){
                        window.location.href = '/order/list';
                    }, 2000);
                } else {
                    $scope.alerts = [
                        { type: 'danger', msg: 'Server error' }
                    ];
                }
            }).error(function(data, status){
                if(status !== 200){
                     $scope.alerts = [
                        { type: 'danger', msg: 'Request error' }
                    ];
                }
            });
        };
    });
    angular.bootstrap(document, ['editOrderApp']);

    $form.validate({
        'rules': {
            'willCheck': {
                'required': true
            },
            'checkAddress': {
                'required': true
            },
            'checkContact': {
                'required': true
            },
            'receiverName': {
                'required': true
            },
            'receiverContact': {
                'required': true
            },
            'receiverPostCode': {
                'required': true
            },
            'receiverAddress': {
                'required': true
            }

        },
        'messages': {
            'willCheck': {
                'required': 'Please check order status'
            },
            'checkAddress': {
                'required': 'Please input check address'
            },
            'checkContact': {
                'required': 'Please input check contact call'
            },
            'receiverName': {
                'required': 'Please input receiver name'
            },
            'receiverContact': {
                'required': 'Please input receiver contact'
            },
            'receiverPostCode': {
                'required': 'Please input receiver postCode'
            },
            'receiverAddress': {
                'required': 'Please input receiver address'
            }
        },
        'errorElement': 'div',
        'errorClass': 'hint-error',
        'highlight': function (element, errorClass, validClass) {
            return false;
        },
        'errorPlacement': function (error, element) {
            element.closest('.line').after(error);
        },
        'submitHandler': function (form) {

        }
    });

    $('span.arrow').bind('click', function () {
        var $me = $(this);
        if ($me.hasClass('glyphicon-chevron-down')) {
            $(this).closest('h4.title').nextAll('.line-wrap').addClass('hide');
            $me.removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
        } else {
            $(this).closest('h4.title').nextAll('.line-wrap').removeClass('hide');
            $me.removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');
        }
    });

    return editOrder;
});