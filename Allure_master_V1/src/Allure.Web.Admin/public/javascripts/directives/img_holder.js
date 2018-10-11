define([
	'angular',
	'holder'
], function(angular, Holder) {

	return angular.module('d-imgholder', []).directive('imgholder', function() {
		return {
			link: function(scope, element, attrs) {
				attrs.$set('data-src', attrs.holderSrc);
				Holder.run({
					images: element[0]
				});
			}
		};
	});
});