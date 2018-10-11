define([
	'angular',
	'lodash',
	'jquery',
	'/public/javascripts/models/production.js',
	'angular-ui-bootstrap',
	'd-enter'
], function(angular, _, $) {
	var c = 0;
	return angular
		.module('c-selectimage', ['d-enter', 'ui.bootstrap', 'productionServices'])
		.directive('selectimage', ['$modal', '$parse', 'LargeCatergory', 'SmallCatergory', 'Production',
			function($modal, $parse, LargeCatergory, SmallCatergory, Production) {

				function imageCtrl($scope, $modalInstance) {
					$scope.product = {};

					LargeCatergory.query(function(res) {
						$scope.largeCatergories = res;
					});

					var firstLargeSelected = true;
					$scope.$watch('product.largeCatergory', function(newValue) {

						if ($scope.product) {
							if (firstLargeSelected) {
								firstLargeSelected = false;
							} else {
								$scope.product.smallCatergory = '';
							}
							SmallCatergory.query({
								id: $scope.product.largeCatergory
							}, function(res) {
								$scope.smallCatergories = res;
							});
						}
					});

					$scope.setImage = function(index) {
						$scope.currentImage = $scope.product.images[index].small;
					};

					function func() {
						$('.bxslider').bxSlider({
							// mode: 'fade'
							pager: false,
							slideWidth: 100,
							minSlides: 2,
							maxSlides: 2,
							slideMargin: 10,
							moveSlides: 1,
							infiniteLoop: false,
							hideControlOnEnd: true
							// adaptiveHeight: true
						});
					};


					var completedImage;

					$scope.queryProduct = function() {
						if (!$scope.product.id) return;

						completedImage = 0;
						Production.get({
							id: $scope.product.id
						}, function(res) {
							_.each(res, function(value, key) {
								$scope.product[key] = value;
							});
						});
					}
					$scope.completedLoad = function() {
						if (++completedImage == $scope.product.images.length) {
							func();
						}
					}

					$scope.ok = function() {
						$modalInstance.close($scope.currentImage);
					};
					$scope.cancel = function() {
						$modalInstance.dismiss('cancel');
					};
				}

				return {
					restrict: 'A',
					replace: true,
					link: function(scope, element, attr) {
						element.on('click', function(event) {
							var modalInstance = $modal.open({
								templateUrl: '/public/partials/home/select_image.html?' + (new Date).getTime(),
								// windowTemplateUrl:'/public/partials/home/select_image.html?' + (new Date).getTime(),
								controller: imageCtrl,
								resolve: {

								}
							});
							modalInstance.result.then(function(result) {
								$parse(attr['ngModel']).assign(scope, result);
							});
						});
					}
				}
			}
		])
		.directive('imageonload', ['$parse',
			function($parse) {
				return {
					restrict: 'A',
					link: function(scope, element, attrs) {
						element.bind('load', function(e) {
							if (e.isDefaultPrevented) {
								console.log(++c);
								scope.$apply(function() {
									scope.$eval(attrs.imageonload);
								});
							}
						});
					}
				}
			}
		]);
});