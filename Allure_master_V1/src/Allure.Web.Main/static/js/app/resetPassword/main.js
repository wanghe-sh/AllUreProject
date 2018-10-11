define([
    'jquery',
    'js/app/common/getQuery',
    'shopCar',
    'angular',
    'bootstrap',
    'angularjs-bootstrap',
    'validate'
], function ($, getQuery, ShopCarController) {
    "use strict";

    var resetPassword = function(options){
        this.init.apply(this, arguments);
    };

    resetPassword.prototype.init = function(){
        var that = this;
        that.validate();
    };
    
    resetPassword.prototype.validate = function(){
        var that = this;
        var $form = $('.register-form');
        $form.validate({
            'rules': {
                'userPassword': {
                    required: true,
                    rangelength: [5,15]
                },
                'userConfirmPassword': {
                    required: true,
                    equalTo: '#userPassword'
                }
            },

            'messages': {
                'userPassword': {
                    required: "please input password",
                    rangelength: 'please input valid password'
                },
                'userConfirmPassword': {
                    required: "please input confirm password",
                    equalTo: "please input same password"
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

    var resetPasswordApp = angular.module('resetPasswordModule', ['ui.bootstrap']);
    resetPasswordApp.controller('ShopCarController', ShopCarController);
    resetPasswordApp.controller('ShowSubController', function ($scope) {

    });
    resetPasswordApp.controller('switchLanguageCtrl', function ($scope) {
        $scope.addLanguage = function (languageCode) {
            var hash = window.location.href.toLowerCase();
            var urlQuery = getQuery.query();
            urlQuery.lang = languageCode;
            var query = '?';
            $.each(urlQuery, function (p, v) {
                query = query + p + '=' + v + '&';
            });
            window.location.href = window.location.origin + query.replace(/&$/, '');

        }
    });
    angular.bootstrap(document, ['resetPasswordModule']);

    return resetPassword;

});