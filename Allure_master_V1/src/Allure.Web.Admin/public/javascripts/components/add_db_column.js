define([
	'angular',
	'lodash',
	'/public/javascripts/models/languages.js',
	'angular-ui-bootstrap'
], function(angular, _) {
	return angular
		.module('c-addColumn', ['ui.bootstrap', 'allureLanguageServices'])
		.directive('addColumn', ['$modal', 'allureLanguage',
			function($modal, allureLanguage) {

				function columnCtrl($scope, $modalInstance, waitColumns, hasColumns, languages) {

					$scope.column = {};
					$scope.alerts = [];
					$scope.languages = languages;

					function findSameIdorText(list) {
						var result = _.find(list, function(item) {
							return $scope.column.id == item.id;
						});
						return !!result;
					}

					function verify(ary) {
						var hasExist = false;

						hasExist = hasExist || findSameIdorText(hasColumns);

						hasExist = hasExist || findSameIdorText(waitColumns);

						return hasExist;
					}


					$scope.ok = function() {
						$scope.closeAlert();

						var isExist = verify(waitColumns);
						if (!isExist) {
							isExist = verify(hasColumns);
						}
						if (isExist) {
							$scope.alerts.push({
								msg: '字段名或字段值已存在'
							});
							return;
						}

						if (!$scope.column.languages || Object.keys($scope.column.languages).length < languages.length) {
							$scope.alerts.push({
								msg: '字段名或字段值已存在为空'
							});
							return;
						}
						$modalInstance.close($scope.column);
					};
					$scope.cancel = function() {
						$modalInstance.dismiss('cancel');
					};

					$scope.closeAlert = function() {
						$scope.alerts.splice(0);
					};

					$scope.$watch("$parent['tableColumnError']", function(newValue) {
						if (newValue) {
							$scope.alerts.push({
								msg: 'tableColumnError'
							})
						}
					});
				}

				return {
					restrict: 'A',
					// require: 'ngModel',
					// scope: {
					// wColumns: '=waitColumns',
					// 	hColumns: '=hasColumns',
					// model: "=ngModel"
					// },
					replace: true,
					link: function(scope, element, attr) {
						element.on('click', function(event) {

							allureLanguage.getLanguageCurrent()
								.then(function(ary) {
									var modalInstance = $modal.open({
										resolve: {
											waitColumns: function() {
												return scope['waitColumns'];
											},
											hasColumns: function() {
												return scope['hasColumns']
											},
											languages: function() {
												return ary;
											}
										},
										templateUrl: '/public/partials/add_db_column.html?' + (new Date).getTime(),
										controller: columnCtrl
									});
									modalInstance.result.then(function(result) {
										scope['newColumn'] = result;
									});
								});
						});
					}
				}
			}
		])
		.directive('addSingleColumn', ['$modal',
			function($modal) {

				function columnCtrl($scope, $modalInstance, waitColumns, hasColumns, currentTable, currentSelected) {

					$scope.alerts = [];
					$scope.currentTable = currentTable;

					if (currentSelected) {
						$scope.column = _.clone(findColumn());
					} else {
						$scope.column = {};
					}

					function findColumn() {
						return _.find(waitColumns.concat(hasColumns), function(item) {
							return currentSelected == item[currentTable.key];
						});
					}

					function findSameIdorText(list) {
						var result = _.find(list, function(item) {
							return $scope.column[currentTable.value] == item[currentTable.value] &&
								$scope.column[currentTable.key] != item[currentTable.key];
						});
						return !!result;
					}

					function verify(ary) {
						var hasExist = false;

						hasExist = hasExist || findSameIdorText(hasColumns);

						hasExist = hasExist || findSameIdorText(waitColumns);

						return hasExist;
					}
					$scope.ok = function() {
						$scope.closeAlert();

						if (!$scope.column[currentTable.value]) {
							$scope.alerts.push({
								msg: '请填写字段名'
							});
							return;
						}

						var isExist = verify(waitColumns);
						if (!isExist) {
							isExist = verify(hasColumns);
						}
						if (isExist) {
							$scope.alerts.push({
								msg: '字段名已存在'
							});
							return;
						}

						$modalInstance.close($scope.column);
					};
					$scope.cancel = function() {
						$modalInstance.dismiss('cancel');
					};
					$scope.closeAlert = function() {
						$scope.alerts.splice(0);
					};
					// $scope.$watch("$parent['tableColumnError']", function(newValue) {
					// 	if (newValue) {
					// 		$scope.alerts.push({
					// 			msg: 'tableColumnError'
					// 		})
					// 	}
					// });
				}

				return {
					restrict: 'A',
					// require: 'ngModel',
					// scope: {
					// wColumns: '=waitColumns',
					// 	hColumns: '=hasColumns',
					// model: "=ngModel"
					// },
					replace: true,
					link: function(scope, element, attr) {
						element.on('click', function(event) {
							var modalInstance = $modal.open({
								resolve: {
									waitColumns: function() {
										return scope['waitColumns'];
									},
									hasColumns: function() {
										return scope['hasColumns']
									},
									currentTable: function() {
										return scope['currentTable']
									},
									currentSelected: function() {
										if (element.attr('edit')) {
											return scope.selected.hasValue.length > 0 ? scope.selected.hasValue[0] : scope.selected.waitValue[0];
										}
									}
								},
								templateUrl: '/public/partials/add_db_single_column.html?' + (new Date).getTime(),
								controller: columnCtrl
							});
							modalInstance.result.then(function(result) {
								scope['newColumn'] = result;
							});
						});
					}
				}
			}
		])
});