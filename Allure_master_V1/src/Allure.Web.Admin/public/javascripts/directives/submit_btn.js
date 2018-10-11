define([
    'angular'
], function(angular) {
    return angular
        .module('d-submitbtn', [])
        .directive('submitbtn', function() {
            return {
                restrict: 'A',
                scope: {},
                link: function(scope, element, attr, ctlr) {
                    element.on('start', function() {
                        element.attr('disabled', true);
                        element.val('提交中...');
                    });
                    element.on('end', function() {
                        element.attr('disabled', false);
                        element.val('保存');
                    });
                }
            }
        });
});