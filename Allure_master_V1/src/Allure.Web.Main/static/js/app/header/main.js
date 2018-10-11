define([
    'jquery',
    'angular',
    'bootstrap',
    'angularjs-bootstrap',
    'shopCar'
], function($) {
    "use strict";

    var allureMenu = function(options){
        this.init.apply(this, arguments);
    };

    allureMenu.prototype.init = function(){
        var that = this;
        that.dropdown();
    };
    
    allureMenu.prototype.dropdown = function(){
        var $menuList = $('.menu-ul-list > li:not(".search")');
        $menuList.mouseenter(function(){
            $(this).find('ul').addClass('show');
        });
        $menuList.mouseleave(function(){
            $(this).find('ul').removeClass('show');
        });
    };

    var homeApp = angular.module('homeModule', ['ui.bootstrap', 'shopCarModule']);
    homeApp.controller('DropdownCtrl', function ($scope) {
        $scope.status = {
            isopen: false
        };
    });
    angular.bootstrap(document, ['homeModule']);
    
    return allureMenu;

});