define(['angular', 'jquery-cookie'], function(){
    
    //var typeNum = 0;
    var DAY = 7;
    //$.cookie.json = true;
   // var product = $.cookie('product');
    //var shopCars = product || [];
    //typeNum = shopCars.length;
    //var productType = {typeNum: typeNum};

    var shopCarController = function ($scope, productService) {
        $scope.isShowShopCar = false;
        $scope.toggleDropdown = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();
            $scope.status.isopen = !$scope.status.isopen;
        };
        $scope.showShopCar = function(){
            $scope.isShowShopCar = !$scope.isShowShopCar;
        };

        $scope.delete = function(index){
            $scope.productType.shopCars.splice(index, 1);
            $scope.productType.typeNum = $scope.productType.shopCars.length;
            $.cookie('product', $scope.productType.shopCars, { expires: DAY, path: '/' });
            $scope.productType.shopCars = productService.getAllProduct();
        };

        $scope.stopPro = function(event){
            event.stopPropagation();
        };

        $scope.validate = function(number){

        };
    };

    return shopCarController;
});