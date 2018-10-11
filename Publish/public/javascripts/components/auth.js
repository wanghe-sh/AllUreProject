define([
	'angular'
], function(angular) {
	/*
		登录后的依据,cookie 还是 session
		login和单页是2个页面，进入单页前需要做登录判断
		进入单页后，js去读页面上的用户信息（后端付给隐藏域）
		根据权限移除或增加页面上得对应权限按钮
		如果用户进入非自己权限的页面，那么将被重定向到指定页面

		后端也需做数据操作的验证
	*/


	angular.module('angular').run([
		'$rootScope',
		'$location',
		jcs.modules.auth.services.authorization,
		function($rootScope, $location, authorization) {
			$rootScope.$on('$routeChangeStart', function(event, next) {
				var authorised;
				if (next.access !== undefined) {
					authorised = authorization.authorize(next.access.loginRequired,
						next.access.permissions,
						next.access.permissionCheckType);
					if (authorised === jcs.modules.auth.enums.authorised.loginRequired) {
						$location.path(jcs.modules.auth.routes.login);
					} else if (authorised === jcs.modules.auth.enums.authorised.notAuthorised) {
						$location.path(jcs.modules.auth.routes.notAuthorised).replace();
					}
				}
			});
		}
	]);

	angular.module(jcs.modules.auth.name).factory(jcs.modules.auth.services.authentication, [
        '$q',
        '$timeout',
        'eventbus',
        function ($q, $timeout, eventbus) {
            var currentUser,
                createUser = function (name, permissions) {
                    return {
                        name: name,
                        permissions: permissions
                    };
                },
                login = function (email, password) {
                    var defer = $q.defer();

                    // only here to simulate a network call!!!!!
                    $timeout(function () {
                        // for the sake of the demo this is hard code
                        // however this would always be a call to the server.
                        email = email.toLowerCase();
                        if (email === 'admin@test.com') {
                            currentUser = createUser('Admin User', ['Admin']);
                        } else if (email === 'manager@test.com') {
                            currentUser = createUser('Manager User', ['UserManager']);
                        } else if (email === 'user@test.com') {
                            currentUser = createUser('Normal User', ['User']);
                        } else {
                            defer.reject('Unknown Username / Password combination');
                            return;
                        }

                        defer.resolve(currentUser);
                        eventbus.broadcast(jcs.modules.auth.events.userLoggedIn, currentUser);
                    }, 1000);

                    return defer.promise;
                },
                logout = function () {
                    // we should only remove the current user.
                    // routing back to login login page is something we shouldn't
                    // do here as we are mixing responsibilities if we do.
                    currentUser = undefined;
                    eventbus.broadcast(jcs.modules.auth.events.userLoggedOut);
                },
                getCurrentLoginUser = function () {
                    return currentUser;
                };

            return {
                login: login,
                logout: logout,
                getCurrentLoginUser: getCurrentLoginUser
            };
        }
    ]);

	angular.module(jcs.modules.auth.name).factory(jcs.modules.auth.services.authorization, [
		'authentication',
		function(authentication) {
			var authorize = function(loginRequired, requiredPermissions, permissionCheckType) {
				var result = jcs.modules.auth.enums.authorised.authorised,
					user = authentication.getCurrentLoginUser(),
					loweredPermissions = [],
					hasPermission = true,
					permission, i;

				permissionCheckType = permissionCheckType || jcs.modules.auth.enums.permissionCheckType.atLeastOne;
				if (loginRequired === true && user === undefined) {
					result = jcs.modules.auth.enums.authorised.loginRequired;
				} else if ((loginRequired === true && user !== undefined) &&
					(requiredPermissions === undefined || requiredPermissions.length === 0)) {
					// Login is required but no specific permissions are specified.
					result = jcs.modules.auth.enums.authorised.authorised;
				} else if (requiredPermissions) {
					loweredPermissions = [];
					angular.forEach(user.permissions, function(permission) {
						loweredPermissions.push(permission.toLowerCase());
					});

					for (i = 0; i < requiredPermissions.length; i += 1) {
						permission = requiredPermissions[i].toLowerCase();

						if (permissionCheckType === jcs.modules.auth.enums.permissionCheckType.combinationRequired) {
							hasPermission = hasPermission && loweredPermissions.indexOf(permission) > -1;
							// if all the permissions are required and hasPermission is false there is no point carrying on
							if (hasPermission === false) {
								break;
							}
						} else if (permissionCheckType === jcs.modules.auth.enums.permissionCheckType.atLeastOne) {
							hasPermission = loweredPermissions.indexOf(permission) > -1;
							// if we only need one of the permissions and we have it there is no point carrying on
							if (hasPermission) {
								break;
							}
						}
					}

					result = hasPermission ?
						jcs.modules.auth.enums.authorised.authorised :
						jcs.modules.auth.enums.authorised.notAuthorised;
				}

				return result;
			};

			return {
				authorize: authorize
			};
		}
	]);
});