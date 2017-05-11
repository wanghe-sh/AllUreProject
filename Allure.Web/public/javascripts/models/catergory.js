define([
	'angular',
	'unit',
	'when',
	'angular-resource'
], function(angular, unit, when) {
	angular.module('allureCatergoryServices', ['ngResource']).
	factory('Catergory', ['$resource',
		function($resource) {

		    var resource = $resource('/api/categories', {}, {
				query: {
					method: 'get',
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
						    var language = getChineseOrDefault(item.localizations);
							if (language) {
								language.id = item.id;
								largeCatergories.push(language);
							}
							_.each(item.children, function (item) {
							    var language = getChineseOrDefault(item.localizations);
								if (language) {
									language.id = item.id;
									language.parentId = item.parentId;
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