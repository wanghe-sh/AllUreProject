define(['jquery', 'angular', 'jquery-cookie', 'jquery.magnify'], function ($) {
    $.cookie.json = true;
    var DAY = 7;
    var addProductController = function ($scope, $attrs, productService) {
        $scope.product = {
            id: $attrs.productId,
            name: $attrs.productName,
            brand: $attrs.productBrand,
            price: $attrs.productPrice,
            imgUrl: $attrs.productImgUrl,
            number: 1,
            productnumber: $attrs.productNumber
        };
        $scope.addShopCar = function (productId, $event) {
            $scope.product.number = $("#ipt_Qty").val();

            $event.preventDefault();
            $event.stopPropagation();
            if (!/^[1-9]+$/.test($scope.product.number)) {
                return;
            }
            var productData = $.cookie('product');
            if(!productData){
                productData = [];
            }
            var flag = false;
            if (productData.length > 0) {
                
                $.each(productData, function(p, v){
                    if (v.id === $scope.product.id) {
                        flag = true;
                        v.number = $scope.product.number;
                        productService.setAllProduct(productData);
                        return false;
                    }
                });
                if(!flag){
                    productData.push($scope.product);
                    productService.setAllProduct(productData);
                }
            }else{
                productData.push($scope.product);
                productService.setAllProduct(productData);
            }
            var typeNum = productService.getProductTypeNum();
            $scope.productType.typeNum = typeNum;
            $scope.productType.shopCars = productService.getAllProduct();
            //$scope.productType.status.isopen = true;
        };
        $scope.changeImageUrl = function (imgUrl) {
            $("#largeimg").attr("data-magnify-src", imgUrl);
            $("#largeimg").attr("src", imgUrl);
            $("#largeimghref").attr("href", imgUrl);

            $('.zoom').magnify();

        };

        $('.zoom').magnify();
    };
    return addProductController;
});