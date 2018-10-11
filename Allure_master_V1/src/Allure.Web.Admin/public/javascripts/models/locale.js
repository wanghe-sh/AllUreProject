define([
	'angular',
	'angular-resource'
], function(angular) {
	angular.module('allureLocaleServices', ['ngResource']).
	factory('Locale', ['$resource',
		function($resource) {
			return $resource('/Locale/list', {}, {
				query: {
					method: 'GET',
					isArray: true
				},
			});
		}
	]);
});