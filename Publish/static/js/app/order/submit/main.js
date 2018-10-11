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
    var submitOrder = function(options){
        this.init.apply(this, arguments);
    };

    submitOrder.prototype.init = function () {
        var that = this;
    };

    var submitOrderApp = angular.module('submitOrderApp', ['ui.bootstrap', 'pascalprecht.translate']);
    submitOrderApp.config(['$translateProvider', function ($translateProvider) {
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
    submitOrderApp.factory('productService', ProductService);
    submitOrderApp.factory('T', ['$translate', function ($translate) {
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
    submitOrderApp.controller('ShopCarController', ShopCarController);

    submitOrderApp.controller('ShowSubController', function ($scope) {

    });
    submitOrderApp.controller('SearchProductController', SearchProductController);

    submitOrderApp.controller('alertCtrl', function ($scope) {
        $scope.closeAlert = function(index) {
            $scope.alerts.splice(index, 1);
        };
    });
    var willCheck = "";
    var checkAddress = "";
    var checkContact = "";
    var receiverName = "";
    var receiverContact = "";
    var receiverPostCode = "";
    var receiverAddress = "";
    submitOrderApp.controller('AllureProductController', function ($scope, productService, $translate, T) {
        $translate('shopcarsempty').then(function (translations) {

            willCheck = T.T('requiredwillCheck');
            checkAddress = T.T('requiredcheckAddress');
            checkContact = T.T('requiredcheckContact');
            receiverName = T.T('requiredreceiverName');
            receiverContact = T.T('requiredreceiverContact');
            receiverPostCode = T.T('requiredreceiverPostCode');
            receiverAddress = T.T('requiredreceiverAddress');

            submitOrder.prototype.resetValidate();
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
    submitOrderApp.controller('switchLanguageCtrl', function ($translate,$scope) {
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
    submitOrderApp.controller('orderSubmitFormCtrl', function ($scope, $http, $timeout) {

        $scope.product = {};

        $scope.getCheckAddress = function () {

            $http.get('/api/checkaddresses/1')
                .success(function (data) {
                    $scope.product.checkAddress = data.address;
                })
                .error(function () {

                });            
        }
        $scope.getCheckAddress();

        $scope.submitOrder = function ($event) {
            $("#submitOrder").attr("disabled","disabled"); 
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
                "WillCheck":  $scope.product.willCheck,
                "CheckAddress": $scope.product.checkAddress,
                "CheckContact": $scope.product.checkContact,
                "ReceiverName": $scope.product.receiverName,
                "ReceiverContact": $scope.product.receiverContact,
                "ReceiverAddress": $scope.product.receiverAddress,
                "ReceiverPostCode": $scope.product.receiverPostCode,
                "Details": details
            };
            $http.post('/order/submit', data).success(function (data) {
                if (data && data.indexOf('ok') > -1) {
                    $.cookie('product', [], { expires: 0, path: '/' });
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
    angular.bootstrap(document, ['submitOrderApp']);

    
    submitOrder.prototype.resetValidate = function () {
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
                    'required': willCheck
                },
                'checkAddress': {
                    'required': checkAddress
                },
                'checkContact': {
                    'required': checkContact
                },
                'receiverName': {
                    'required': receiverName
                },
                'receiverContact': {
                    'required': receiverContact
                },
                'receiverPostCode': {
                    'required': receiverPostCode
                },
                'receiverAddress': {
                    'required': receiverAddress
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
    }
    

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

    return submitOrder;
});