define([
	'angular',
], function(angular) {
	angular.module('allureLanguageServices', []).
	factory('allureLanguage', ['$http', '$q',
		function($http, $q) {
			return {
				getLanguageCurrent: function() {
					var deferred = $q.defer();
					$http.get('/api/languages')
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
							return item.code === code;
						});
					if (result) {
						description = result.description;
						return this.getLanguageShorthand(description);
					}
					return code;
				},
				getLanguageShorthand: function (description) {
				    if (description == null) {
				        return "";
				    }
				    var index = description.indexOf('(');
					if (index > 0) {
						return description.substring(0, description.indexOf('('));
					} else {
						return description;
					}
				},
				initCategoryLanguages: function (obj, languages) {
					var self = this;
					_.each(languages, function(language) {
					    var localized = _.find(obj.localizations, function (item) {
					        return item.languageCode === language.code;
					    });
						var title = self.getLanguageShorthand(language.description);
						if (localized) {
						    localized.title = title;
						    localized.isDefault = language.isDefault;
						} else {
						    obj.localizations.push({
								title: title,
								languageCode: language.code,
								isDefault: language.isDefault
							});
						}
					});
				}
			}
		}
	]);
});