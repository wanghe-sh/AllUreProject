define([
	'angular',
	'unit',
	'when',
	'angular-resource'
], function(angular, unit, when) {
	angular.module('allureCatergoryServices', ['ngResource']).
	factory('Catergory', ['$resource',
		function($resource) {

			var resource = $resource('/Category/list', {}, {
				query: {
					method: 'GET',
					isArray: true
				}
			});

			return {
				resource: resource,
				getCategoryChinese: function() {
					var getChineseOrDefault = unit.getChineseOrDefault;

					var deferred = when.defer();
					var largeCatergories = [],
						smallCatergories = [];

					resource.query(function(res) {
						_.each(res, function(item) {
							var language = getChineseOrDefault(item.Localized);
							if (language) {
								language.Id = item.Id;
								largeCatergories.push(language);
							}
							_.each(item.Children, function(item) {
								var language = getChineseOrDefault(item.Localized);
								if (language) {
									language.Id = item.Id;
									language.ParentId = item.ParentId;
									smallCatergories.push(language);
								}
							});
						});

						deferred.resolve({
							largeCatergories: largeCatergories,
							smallCatergories: smallCatergories,
						});
					});


					return deferred.promise;
				}
			}
		}
	]);
});