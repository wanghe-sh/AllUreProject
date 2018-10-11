define([], function(angular, unit) {

	function getChineseOrDefault(ary) {
		var language = _.find(ary, function(localized) {
			return localized.LanguageCode === "zh-CN";
		});
		if (!language) {
			language = ary[0];
		}
		return language;
	}

	return {
		getChineseOrDefault: getChineseOrDefault
	}
});