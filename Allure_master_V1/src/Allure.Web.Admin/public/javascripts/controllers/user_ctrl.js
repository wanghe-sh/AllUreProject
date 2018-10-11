define([
	'angular',
	'jquery',
	'lodash',
	'language',
	'angular-ui-bootstrap',
	'checklist-model',
	'c-userrole'
], function(angular, $, _, language) {
	var app = angular.module('allure', ['c-userrole']);
	app.init = function() {
		angular.bootstrap(document, ['allure']);
	};
	app.controller('UserCtrl', ['$scope',
		function($scope) {
			$scope.EMAIL_REGEXP = /^[a-z0-9!#$%&'*+/=?^_`{|}~.-]+@[a-z0-9-]+(\.[a-z0-9-]+)*$/;

			$scope.STATUS = [{
				value: 0,
				text: '有效'
			}, {
				value: 1,
				text: '无效'
			}];

			$scope.GENDER = [{
				value: 0,
				text: '女士'
			}, {
				value: 1,
				text: '先生'
			}];

			$scope.user = {};
			$scope.alerts = [];
			$scope.roles = [];
			$scope.isShowAddress = false;
			$scope.toggleBtn = '+';

			$scope.closeAlert = function() {
				$scope.alerts.splice(0);
			};

			$scope.toggleAddress = function(e) {
				$scope.isShowAddress = !$scope.isShowAddress;
				if ($scope.isShowAddress) {
					$scope.toggleBtn = '-';
				} else {
					$scope.toggleBtn = '+';
				}
			}

			$scope.submit = function() {
				$scope.closeAlert();

				var form = $scope.form;
				var errorEls = [],
					errorTips = language['user']['input_valid'];
				if (!form.roles.$modelValue || form.roles.$modelValue.length === 0) {
					form.roles.$setValidity('required', false);
				} else {
					form.roles.$setValidity('required', true);
				}

				_.each(form, function(item) {
					if (item.$error && item.$valid === false) {
						var name = item.$name,
							error = item.$error;
						for (var key in error) {
							if (error[key] === true) {
								errorEls.push(errorTips[name][key])
							}
						}
					}
				});
				_.each(errorEls, function(error) {
					// errs += error + ''
					$scope.alerts.push({
						msg: error
					});
				})
				return false;
			};
		}
	]);


	app.init();

	return app;
});


/*
	angular.module("template/accordion/accordion-group.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/accordion/accordion.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/alert/alert.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/carousel/carousel.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/carousel/slide.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/datepicker/datepicker.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/datepicker/day.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/datepicker/month.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/datepicker/popup.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/datepicker/year.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/modal/backdrop.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/modal/window.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/pagination/pager.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/pagination/pagination.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/popover/popover.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/progressbar/bar.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/progressbar/progress.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/progressbar/progressbar.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/rating/rating.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/tabs/tab.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/tabs/tabset.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/timepicker/timepicker.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/tooltip/tooltip-html-unsafe-popup.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/tooltip/tooltip-popup.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/typeahead/typeahead-match.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	angular.module("template/typeahead/typeahead-popup.html", []).run(["$templateCache",
		function($templateCache) {
			$templateCache.put('');
		}
	]);
	*/