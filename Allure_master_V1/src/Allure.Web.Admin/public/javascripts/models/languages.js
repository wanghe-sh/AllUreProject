define([
	'angular',
], function(angular) {
	angular.module('allureLanguageServices', []).
	factory('allureLanguage', ['$http', '$q',
		function($http, $q) {
			return {
				getLanguageCurrent: function() {
					var deferred = $q.defer();
					$http.get('/Language/current')
						.success(function(data) {
							deferred.resolve(data);
						})
						.error(function(data) {

						});
					return deferred.promise;
				},
				getLanguageName: function(languages, code) {
					var description,
						result = _.find(languages, function(item, i) {
							return item.Code === code;
						});
					if (result) {
						description = result.Description;
						return this.getLanguageShorthand(description);
					}
					return code;
				},
				getLanguageShorthand: function(description) {
					var index = description.indexOf('(');
					if (index > 0) {
						return description.substring(0, description.indexOf('('));
					} else {
						return description;
					}
				},
				initCategoryLanguages: function(obj, languages) {
					var self = this;
					_.each(languages, function(language) {
						var localized = _.find(obj.Localized, function(item) {
							return item.LanguageCode === language.Code;
						});
						var title = self.getLanguageShorthand(language.Description);
						if (localized) {
							localized.Title = title;
						} else {
							obj.Localized.push({
								Title: title,
								LanguageCode: language.Code
							});
						}
					});
				}
			}
		}
	]);
});