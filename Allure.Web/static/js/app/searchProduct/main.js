define(['angular', 'jquery-cookie'], function(){
    
    //var typeNum = 0;
    var DAY = 7;
    //$.cookie.json = true;
   // var product = $.cookie('product');
    //var shopCars = product || [];
    //typeNum = shopCars.length;
    //var productType = {typeNum: typeNum};
    var searchProductController = function ($scope) {
        $scope.searchKey = '';
        $scope.submitSearch = function () {
            if ($scope.searchKey != '') {
                var url = '/search?q=' + $scope.searchKey;
                window.location.href = url;
            }
        };
        $scope.searchKeyup = function(e){
            var keycode = window.event?e.keyCode:e.which;
            if(keycode==13) {
                $scope.submitSearch();
            }
        };
    };

    return searchProductController;
});