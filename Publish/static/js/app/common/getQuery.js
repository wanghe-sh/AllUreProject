define([], function () {
    return {
        query: function () {
            var vars = {}, hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                if (hashes && hashes[0].indexOf('=') > -1) {
                    hash = hashes[i].split('=');
                    vars[hash[0]] = hash[1];
                }
            }
            return vars;
        }
    }
});