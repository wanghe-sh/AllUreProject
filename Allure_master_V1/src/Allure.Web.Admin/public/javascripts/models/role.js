define([
	'angular',
	'lodash'
], function(angular, _) {
	angular.module('allureRoleServices', []).
	factory('allureRole', [
		function() {
			return {
				ROLE: [{
					value: 1,
					text: '客服'
				}, {
					value: 3,
					text: '库管'
				}, {
					value: 5,
					text: '管理员'
				}, {
					value: 2,
					text: '仓库'
				}, {
					value: 4,
					text: '物流'
				}, {
					value: 0,
					text: '客户'
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