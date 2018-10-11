define([
	'angular',
	'jquery',
	'lodash',
	'language',
	'multizoom',
	'angular-route',
	'/public/javascripts/models/production.js',
	'd-imgholder',
	'c-imageupload'
], function(angular, $, _, language, imgholder) {
	var app = angular.module('allure', ['d-imgholder', 'c-imageupload', 'ngRoute', 'productionServices']);

	// app.config(['$routeProvider',
	// 	function($routeProvider) {
	// 		$routeProvider
	// 			.when('/:id', {
	// 				// templateUrl: 'partials/phone-detail.html',
	// 				controller: 'ProductionDetailCtrl',
	// 				resolve: {
	// 					'production': ['$routeParams', 'Production',
	// 						function($routeParams, Production) {
	// 							console.log( $routeParams.id)
	// 							return Production.get({
	// 								id: $routeParams.id
	// 							});
	// 						}
	// 					]
	// 				}
	// 			});
	// 	}
	// ]);

	app.init = function() {
		angular.bootstrap(document, ['allure']);
	};

	app.controller('ProductionListCtrl', ['$scope',
		function($scope) {
			$scope.isShowSearch = false;
			$scope.toggleBtn = '+';

			$scope.toggleAddress = function(e) {
				$scope.isShowSearch = !$scope.isShowSearch;
				if ($scope.isShowSearch) {
					$scope.toggleBtn = '-';
				} else {
					$scope.toggleBtn = '+';
				}
			}
		}
	]);
	app.controller('ProductionDetailCtrl', ['$scope', '$document', '$timeout', 'Production', '$routeParams',
		function($scope, $document, $timeout, Production, $routeParams) {

			function refreshImages() {
				$timeout(function() {
					$('.featuredimagezoomerhidden').remove();
					$('.zoomtracker').remove();
					featuredimagezoomer.loadinggif = '/public/test/images/spinningred.gif';
					$('#multizoom').addimagezoom({ // multi-zoom: options same as for previous Featured Image Zoomer's addimagezoom unless noted as '- new'
						speed: 1500, // duration of fade in for new zoomable images (in milliseconds, optional) - new
						descpos: true, // if set to true - description position follows image position at a set distance, defaults to false (optional) - new
						imagevertcenter: true, // zoomable image centers vertically in its container (optional) - new
						magvertcenter: true, // magnified area centers vertically in relation to the zoomable image (optional) - new
						zoomrange: [3, 10],
						magnifiersize: [300, 300],
						magnifierpos: 'right',
						cursorshade: true //<-- No comma after last option!
					});
				}, 0);
			}

			var data = Production.get({
				id: 11
			}, function(res) {
				$scope.product = res;

				$scope.images = $scope.product.images;

				$scope.$watch('images', function(newValue) {
					$scope.product.images = newValue;
					refreshImages();
				});
			});
		}
	]);
	app.init();

	return app;
});