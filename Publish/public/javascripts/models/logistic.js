define([
	'angular',
	'angular-resource'
], function(angular) {
    angular.module('allureLogisticServices', ['ngResource']).
	factory('Logistic', ['$resource',
		function ($resource) {
		    return $resource('/api/logistics', {}, {
				query: {
					method: 'GET',
					isArray: true
				},
			});
		}
	]);
});