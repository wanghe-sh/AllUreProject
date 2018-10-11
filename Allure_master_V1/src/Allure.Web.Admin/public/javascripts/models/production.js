define([
	'angular',
	'angular-resource'
], function(angular) {
	var domain = '';
	angular.module('productionServices', ['ngResource']).
	factory('Production', ['$resource',
		function($resource) {
			return $resource(domain + '/product/:id', {}, {
				// query: {
				// 	method: 'GET'
				// },
				get: {
					method: 'GET'
				},
				update: {
					method: 'PUT'
				},
				save: {
					method: 'POST'
				},
				remove: {
					method: 'DELETE'
				}
			});
		}
	]);
});