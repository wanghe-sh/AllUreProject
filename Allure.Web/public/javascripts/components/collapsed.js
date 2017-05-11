define([
	'angular',
	'angular-ui-bootstrap'
], function(angular) {
	return angular
		.module('c-collapsed', ['ui.bootstrap'])
		.directive('allureCollapsed', [

			function() {
				return {
					restrict: 'E',
					replace: true,
					scope: {
						model: '=ngModel'
					},
					require: 'ngModel',
					template: '<h5>检索条件<span class="toggle ng-binding" ng-click="toggleCollapsed()">{{toggleBtn}}</span></h5>',
					link: function(scope, element, attr, ctlr) {

						function updateBtn() {
							if (scope.model) {
								scope.toggleBtn = '-';
							} else {
								scope.toggleBtn = '+';
							}
						}

						scope.toggleCollapsed = function() {
							scope.model = !scope.model;
							updateBtn();
						}

						updateBtn();
					}
				}
			}
		]);
});