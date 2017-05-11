define([
	'angular',
	'angular-resource'
], function(angular) {
	var domain = '';
	angular.module('orderServices', ['ngResource']).
	factory('Order', ['$resource',
		function($resource) {
			return $resource(domain + '/order/search', {}, {
				query: {
					method: 'GET',
					isArray: true
				}
			});
		}
	]);
});