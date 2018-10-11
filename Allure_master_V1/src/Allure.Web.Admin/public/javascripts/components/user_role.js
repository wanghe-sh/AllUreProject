define([
	'angular',
	'jquery',
	'lodash',
	'/public/javascripts/models/role.js',
	'angular-ui-bootstrap'
], function(angular, $, _) {
	return angular
		.module('c-userrole', ['ui.bootstrap', 'allureRoleServices'])
		.directive('userrole', ['$modal', 'allureRole',
			function($modal, allureRole) {

				var UserRoleCtrl = function($scope, $modalInstance, values, unlimited) {
					var ROLE = $scope.options = $scope.ROLE = allureRole.ROLE;

					$scope.alerts = [];
					$scope.rets = ['false', 'false', 'false', 'false', 'false', 'false'];

					_.each(values, function(item) {
						var ret = _.find(ROLE, function(role, index) {
							if (role.value == item.value) {
								$scope.rets[index] = 'true';
								return 'true';
							}
						});
					});

					var oldRets = _.clone($scope.rets);

					function checkGroup(newValues, oldValues) {
						var groups = [].slice.call(arguments, 2);
						//当前数据是否在同一组
						//	同一组 return
						//	不同组 
						//		去掉当前选择的内容

						var newGroups = {},
							oldGroups = {};
						_.each(groups, function(group, gIndex) {
							if (!_.isArray(group.indexs)) return;

							var indexs = group.indexs;

							_.each(newValues, function(value, index) {
								if (value == 'true' && _.contains(indexs, index)) {
									newGroups[gIndex] = true;
								}
							});
							_.each(oldValues, function(value, index) {
								if (value == 'true' && _.contains(indexs, index)) {
									oldGroups[gIndex] = true;
								}
							});
						});
						var newKeys = _.keys(newGroups),
							oldKeys = _.keys(oldGroups);

						if (newKeys.length > 1) {
							var diff = _.difference(newKeys, oldKeys);
							_.each(groups[diff[0]].indexs, function(i) {
								newValues[i] = 'false';
							});
							addAlert(groups[oldKeys[0]].msg);
						}
					}

					function addAlert(msg) {
						$scope.alerts.push({
							msg: msg
						});
					};

					$scope.closeAlert = function(index) {
						$scope.alerts.splice(index);
					};

					$scope.change = function() {
						if ($scope.alerts.length > 0) {
							$scope.alerts.splice(0)
						}
						if (unlimited !== "true") {
							checkGroup($scope.rets, oldRets, {
								indexs: [1, 3, 5],
								msg: '在勾选“客服、库管、管理员”情况下无法勾选其它选项'
							}, {
								indexs: [2, 4],
								msg: '在勾选“仓库、物流”情况下无法勾选其它选项'
							}, {
								indexs: [0],
								msg: '在勾选“客户”情况下无法勾选其它选项'
							});
						}
						oldRets = _.clone($scope.rets);
					}
					$scope.ok = function() {
						var rets = [];
						_.each($scope.rets, function(n, index) {
							if (n == 'true') {
								rets.push($scope.ROLE[index])
							}
						})
						$modalInstance.close(rets);
					};
					$scope.cancel = function() {
						$modalInstance.dismiss('cancel');
					};
				};

				return {
					restrict: 'A',
					link: function(scope, element, attr) {

						element.on('click', function(event) {
							var modalInstance = $modal.open({
								templateUrl: '/public/tpls/user/role.html?' + (new Date).getTime(),
								controller: UserRoleCtrl,
								size: attr.size,
								resolve: {
									unlimited: function() {
										return attr.unlimited;
									},
									values: function() {
										return scope[element.attr('role-values')];
									}
								}
							});
							modalInstance.result.then(function(rets) {
								var roleNames = '',
									roles = [];
								_.each(rets, function(role, index) {
									roleNames += role.text + '、';
									roles.push(role.value);
								});
								scope.roles = rets;
								scope.user.roles = roles;
								scope.roleNames = roleNames.substring(0, roleNames.length - 1);
							});
						});
					}
				}
			}
		])
		.directive('rolelist', function() {
			return {
				restrict: 'A',
				link: function(scope, elem, attrs) {
					// var list = scope.list = []; //scope.list
					scope.list = [];

					var handler = function(setup) {
						var checked = elem.prop('checked');
						var index = scope.list.indexOf(1);
						if (checked && index == -1) {
							if (setup) {
								elem.prop('checked', 'false');
							} else {
								scope.list.push();
							}
						} else if (!checked && index != -1) {
							if (setup) {
								elem.prop('checked', 'true');
							} else {
								scope.list.splice(index, 1);
							}
						}
					};
					var setupHandler = handler.bind(null, 'true');
					var changeHandler = handler.bind(null, 'false');

					elem.bind('change', function() {
						scope.$apply(changeHandler);
					});
				}
			};
		});
});