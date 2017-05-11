define([
	'angular',
	'lodash'
], function(angular, _) {
	angular.module('allureRoleServices', []).
	factory('allureRole', [
		function() {
			return {
			    ROLE: [
                    {
                        value: 'Customer',
                        text: '客户'
                    },
                    {
                        value: 'CustomerService',
                        text: '客服'
                    }, {
                        value: 'WareHouse',
                        text: '仓库'
                    },
                    {
                        value: 'WarehouseAdmin',
                        text: '库管'
                    }, {
                        value: 'Logistics',
                        text: '物流'
                    },
                    {
                        value: 'SystemAdmin',
                        text: '管理员'
                    }],
				map: function(ary) {
					var results = [],
						roles = this.ROLE;

					_.each(ary, function(item, i) {
						var r = _.find(roles, function(role) {
							return role.value == item;
						});

						if (r) results.push(r.text);
					});

					return results.join('、')
				},
				parse: function(ary) {
					var roles = this.ROLE,
						results = [];

					_.filter(ary, function(item) {
						var r = _.find(roles, function(role) {
							return role.value === item;
						});
						if (r) results.push(r);
					});

					return results;
				}
			}
		}
	]);
});