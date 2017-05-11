define([
	'angular',
	'lodash'
], function(angular, _) {
	return angular
		.module('c-multiSelect', [])
		.directive('multiSelect', [
			'$q', '$http',
			function($q, $http) {
				return {
					restrict: 'E',
					require: 'ngModel',
					// scope: {
					// 	// tableName: "=currentTable",
					// 	// languageCode: "=languageCode",
					// 	model: "=ngModel"
					// },
					templateUrl: '/public/partials/db_column.html',
					link: function(scope, elm, attrs) {

						scope.$watch("currentTable", function(newValue) {
							scope.refreshAvailable()
						});

						var createFn = scope.createFn; //scope.$parent['createFn'];
						var addFn = scope.addFn; //scope.$parent['addFn'];
						var removeFn = scope.removeFn; //scope.$parent['removeFn'];
						var updateFn = scope.updateFn;

						scope.selected = {
							currentWaitValue: [],
							currentHasValue: []
						};

						function filterOut(ary, value) {
							var dataIndex = [];
							for (var i = 0; i < ary.length; i++) {
								var item = ary[i];
								if (item[scope.currentTable.key] == value) {
									dataIndex = i;
									break;
								}
							}
							return dataIndex;
						}

						var dataLoading = function(scopeAttr) {
							var loading = $q.defer();
							if (scope[scopeAttr]) {
								loading.resolve(scope[scopeAttr]);
							} else {
								scope.$watch(scopeAttr, function(newValue, oldValue) {
									if (newValue !== undefined)
										loading.resolve(newValue);
								});
							}
							return loading.promise;
						};


						scope.refreshAvailable = function() {
							scope.selected.waitValue = [];
							scope.selected.hasValue = [];
						};

						scope.add = function() {
							if (scope.selected.waitValue.length > 1) {
								alert('仅支持单选');
								return;
							}
							scope.addFn(scope.selected.waitValue[0], function(err) {
								if (err) {
									alert(err);
									return;
								}
								_.each(scope.selected.waitValue, function(value) {
									var index = filterOut(scope.waitColumns, value);
									if (index === undefined) return;
									scope.hasColumns = scope.hasColumns.concat(scope.waitColumns.splice(index, 1));
								});
								scope.refreshAvailable();
							});
						};

						scope.remove = function() {
							if (scope.selected.hasValue.length > 1) {
								alert('仅支持单选');
								return;
							}
							scope.removeFn(scope.selected.hasValue[0], function(err) {
								if (err) {
									alert(err);
									return;
								}
								var index = filterOut(scope.hasColumns, scope.selected.hasValue[0]);
								if (index === undefined) return;

								scope.waitColumns = scope.waitColumns.concat(scope.hasColumns.splice(index, 1));
								scope.refreshAvailable();
							});
						};

						scope.clearSelected = function(list) {
							list.length = 0;
							console.log(scope.selected)
						};

						scope.$watch('newColumn', function(newValue) {
							if (newValue) {
								if (newValue[scope.currentTable.key]) {
									updateFn(newValue).then(function() {
										var s = _.find(scope.waitColumns, function(item) {
											console.log(item[scope.currentTable.key], newValue[scope.currentTable.key])
											return item[scope.currentTable.key] == newValue[scope.currentTable.key];
										});
										if (!s) {
											s = _.find(scope.hasColumns, function(item) {
												return item[scope.currentTable.key] == newValue[scope.currentTable.key];
											});
										}
										if (s) {
											s[scope.currentTable.value] = newValue[scope.currentTable.value];
										}
									}).catch(function() {
										// todo...
									})
								} else {
									createFn(newValue, function(err, data) {
										if (err) {
											alert(err);
										} else {
											console.log(data)
											scope.hasColumns.push(data);
										}
									});
								}
							}
						});

						$q.all([dataLoading("model"), dataLoading("hasColumns"), dataLoading("waitColumns")]).then(function(results) {

							scope.refreshAvailable();
						});
					}
				}
			}
		]);
});