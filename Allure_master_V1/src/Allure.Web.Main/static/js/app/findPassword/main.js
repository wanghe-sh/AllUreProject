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

    var findPassword = function(options){
        this.init.apply(this, arguments);
    };

    findPassword.prototype.init = function(){
        var that = this;
        that.validate();
    };
    
    findPassword.prototype.validate = function(){
        var that = this;
        var $form = $('.register-form');
        $form.validate({
            'rules': {
                'userEmail': {
                    required: true,
                    email: true
                }
            },

            'messages': {
                'userEmail': {
                    required: "please input email",
                    email: "please valid email"
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

    var findPasswordApp = angular.module('findPasswordModule', ['ui.bootstrap']);
    findPasswordApp.controller('ShopCarController', ShopCarController);
    findPasswordApp.controller('ShowSubController', function ($scope) {

    });
    findPasswordApp.controller('switchLanguageCtrl', function ($scope) {
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
    angular.bootstrap(document, ['findPasswordModule']);

    return findPassword;

});