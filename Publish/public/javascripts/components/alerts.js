define([
	'angular',
	'angular-ui-bootstrap'
], function(angular) {
	return angular
		.module('c-alerts', ['ui.bootstrap'])
		.directive('allureAlerts', [
			function() {
				return {
					restrict: 'E',
					scope: {
						model: '=ngModel'
					},
					require: 'ngModel',
					link: function(scope, element, attr, ctlr) {
						scope.$watchCollection('model', function(newValue) {
							console.log(newValue)
							if (newValue && newValue.length > 0) {
								var alertStr = '';
								for (var i = 0; i < newValue.length; i++) {
									alertStr += newValue[i].msg + '\n';
								}
							    alert(alertStr);
							}
						});
					}
				}
			}
		]);
});