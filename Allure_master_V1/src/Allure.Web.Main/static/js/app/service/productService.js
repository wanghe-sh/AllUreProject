define([
    'jquery',
    'jquery-cookie',
    'angular',
    'bootstrap',
    'angularjs-bootstrap'
], function ($) {
    "use strict";

    $.cookie.json = true;
    var DAY = 7;
    var productService = function () {
        var productTotalType = {typeNum: 0};
        return {
            getAllProduct: function () {
                var product = $.cookie('product');
                var shopCars = product || [];
                return shopCars;
            },
            getProductTypeNum: function () {
                var shopCars = this.getAllProduct();
                var typeNum = shopCars.length;
                return typeNum;
            },
            setAllProduct: function (productData) {
                var sevenDaysFromNow = new Date();
                sevenDaysFromNow.setDate(sevenDaysFromNow.getDate() + 7);
                $.cookie('product', productData, { expires: sevenDaysFromNow, path: '/' });
            },
            deleteProduct: function () {

            }
        }
    };
    return productService;
});