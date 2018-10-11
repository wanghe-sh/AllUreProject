define([
	'angular',
	'jquery',
	'angular-ui-bootstrap'
], function(angular, $) {
	return angular
		.module('c-datepicker', ['ui.bootstrap'])
		.directive('alluredatepicker', ['$compile',
			function($compile) {
				return {
					restrict: 'E',
					replace: true,
					scope: {
						// ngModel: '='
						model: '=ngModel'
					},
					require: 'ngModel',
					template: '<div class="input-group pull-left"><input type="text" class="form-control" datepicker-popup="yyyy-MM-dd" is-open="openedStart"  ng-model="model" disabled="true"/><span class="input-group-btn"><button type="button" class="btn btn-default" ng-click="open($event)"><i class="glyphicon glyphicon-calendar"></i></button></span></div>',
					// template: '<div class="some"><label>{{label}}</label>' + '<input></div>',
					link: function(scope, element, attr, ctlr) {
						if (attr.datepickerWidth) {
							element.css('width', attr.datepickerWidth)
						}

						scope.open = function($event) {
							$event.preventDefault();
							$event.stopPropagation();
							scope.openedStart = true;
						}

						// scope.$watch('date', function(val) {
						// 	if (!val) return;
						// 	scope.model = val;
						// });

						// scope.$watch('model', function() {
						// 	scope.date = scope.model;
						// });
					}
				}
			}
		]);
});