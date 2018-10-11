define([
	'angular',
	'angular-resource'
], function(angular) {
	var domain = '';
	angular.module('warehouseCodeServices', ['ngResource'])
		.factory('WarehouseCode', ['$resource',
			function($resource) {
				return $resource('/warehouse/list', {}, {
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
				return $resource(domain + '/logistics', {}, {
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
				return $resource(domain + '/product/warehouse/:id', {}, {
					get: {
						method: 'GET'
					},
				});
			}
		]);
	angular.module('warehouseOrderServices', ['ngResource'])
		.factory('WarehouseOrder', ['$resource',
			function($resource) {
				return $resource(domain + '/product/warehouse/order/:id', {}, {
					get: {
						method: 'GET'
					},
				});
			}
		]);
	angular.module('orderStatusServices', ['ngResource'])
		.factory('OrderStatus', ['$resource',
			function($resource) {
				return $resource(domain + '/order/status', {}, {
					get: {
						method: 'GET',
						isArray: true
					},
				});
			}
		]);
});