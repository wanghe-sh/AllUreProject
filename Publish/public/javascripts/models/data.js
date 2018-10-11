define([
	'angular',
	'angular-resource'
], function(angular) {
	var domain = '';
	angular.module('warehouseCodeServices', ['ngResource'])
		.factory('WarehouseCode', ['$resource',
			function($resource) {
				return $resource('/api/warehouses', {}, {
					query: {
						method: 'GET',
						isArray: true
					},
				});
			}
		]);
	angular.module('logisticsServices', ['ngResource'])
		.factory('Logistics', ['$resource',
			function($resource) {
				return $resource('/api/logistics', {}, {
					query: {
						method: 'GET',
						isArray: true
					},
				});
			}
		]);

	angular.module('warehouseProductServices', ['ngResource'])
		.factory('WarehouseProduct', ['$resource',
			function($resource) {
			    return $resource('/api/products/no-:id', {}, {
					get: {
						method: 'GET'
					},
				});
			}
		]);
	angular.module('warehouseOrderServices', ['ngResource'])
		.factory('WarehouseOrder', ['$resource',
			function($resource) {
				return $resource('/api/orders/:id', {}, {
					get: {
						method: 'GET'
					},
				});
			}
		]);
	angular.module('orderStatusServices', ['ngResource'])
		.factory('OrderStatus', ['$resource',
			function($resource) {
				return $resource('/api/order/status', {}, {
					get: {
						method: 'GET',
						isArray: true
					},
				});
			}
		]);
});