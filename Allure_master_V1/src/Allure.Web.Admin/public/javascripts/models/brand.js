define([
	'angular',
	'angular-resource'
], function(angular) {
	angular.module('allureBrandServices', ['ngResource']).
	factory('Brand', ['$resource',
		function($resource) {
			return $resource('/Brand/list', {}, {
				query: {
					method: 'GET',
					isArray: true
				},
			});
		}
	]);
});