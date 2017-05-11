define([
	'angular',
	'angular-resource'
], function(angular) {
	angular.module('allureLocaleServices', ['ngResource']).
	factory('Locale', ['$resource',
		function($resource) {
			return $resource('/api/locales', {}, {
				query: {
					method: 'GET',
					isArray: true
				},
			});
		}
	]);
});