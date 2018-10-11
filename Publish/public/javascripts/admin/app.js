define([
    'angular',
    'jquery',
    'lodash',
    'language',
    'when',
    'webuploader',
    'unit',
    'bxSlider',
    'multizoom',
    'angular-route',
    '/public/javascripts/models/production.js',
    '/public/javascripts/models/locale.js',
    '/public/javascripts/models/data.js',
    '/public/javascripts/models/order.js',
    '/public/javascripts/models/languages.js',
    '/public/javascripts/models/catergory.js',
    '/public/javascripts/models/role.js',
    '/public/javascripts/models/brand.js',
    '/public/javascripts/models/logistic.js',
    '/public/javascripts/components/add_db_column.js',
    '/public/javascripts/components/category_delete.js',
    '/public/javascripts/components/locale_delete.js',
    '/public/javascripts/components/order_delete.js',
    '/public/javascripts/components/update_input_image.js',
    '/public/javascripts/directives/submit_btn.js',
    'c-vedioupload',
    'd-imgholder',
    'c-imageupload',
    'c-userrole',
    'c-fileupload',
    'c-description',
    'c-selectimage',
    'c-datepicker',
    'c-collapsed',
    'c-alerts',
    'c-multiSelect',
    'd-enter'
], function(angular, $, _, language, when, WebUploader, unit) {

    "use strict";

    var PageSize = 5;
    var ListPageSize = 10;

    var ORDERSTATUS = [

{
    value: 'ToBeContact',
    text: '待联络'
},
{
    value: 'ToBeSeeGoods',
    text: '待看货'
},
{
    value: 'ToBePayTheDeposit',
    text: '待付定金'
},
{
    value: 'ToBePayTheBalance',
    text: '待付余额'
},
{
    value: 'Canceled',
    text: '已取消'
},
{
    value: 'ToBeShip',
    text: '待发货'
},
{
    value: 'Shipped',
    text: '已发货'
},
{
    value: 'Returned',
    text: '已退货'
}];

    function getOrderStatus(value) {
        var r = _.find(ORDERSTATUS, function (status) {
            return status.value == value;
        });
        if (r) return r.text;
    }

    var STATUS = [{
        value: 'Normal',
        text: '有效'
    }, {
        value: 'Disabled',
        text: '无效'
    }, {
        value: 'Unactivated',
        text: '未激活'
    }];

    function getStatus(value) {
        var r = _.find(STATUS, function(status) {
            return status.value == value;
        });
        if (r) return r.text;
    }

    var GENDER = [{
        value: 'Female',
        text: '女士'
    }, {
        value: 'Male',
        text: '先生'
    }];

    function getGender(value) {
        var r = _.find(GENDER, function(gender) {
            return gender.value == value;
        });

        if (r) return r.text;
    }
    
    $.getJSON("/api/users?currentuser=currentuser",
        function (data) {
            $("#username").text(data.firstName + ' ' + data.lastName);
        });

    var app = angular.module('allure', [
        'd-enter', 'c-selectimage', 'c-description', 'c-userrole', 'c-vedioupload', 'c-addColumn', 'd-submitbtn',
        'd-imgholder', 'c-imageupload', 'c-fileupload', 'c-datepicker', 'c-collapsed', 'c-updateInputImage',
        'c-alerts', 'c-categoryDelete', 'c-localeDelete', 'c-orderDelete', 'allureRoleServices', 'allureBrandServices', 'allureLogisticServices',
        'ngRoute', 'productionServices', 'orderServices', 'orderStatusServices', 'c-multiSelect',
        'warehouseCodeServices', 'logisticsServices', 'warehouseProductServices', 'warehouseOrderServices',
        'allureLanguageServices', 'allureCatergoryServices', 'allureLocaleServices'
    ]);

    app.init = function() {
        angular.bootstrap(document, ['allure']);
    };

    app.factory('formDataObject', function() {
        return function(data) {
            var fd = new FormData();
            angular.forEach(data, function(value, key) {
                fd.append(key, value);
            });
            return fd;
        };
    });



    app.config(['$routeProvider', '$sceDelegateProvider',
        function($routeProvider, $sceDelegateProvider) {
            $sceDelegateProvider.resourceUrlWhitelist(['self', 'http://player.youku.com/**', 'http://player.video.qiyi.com/**']);

            $routeProvider
                .when('/home', {
                    templateUrl: '/public/partials/admin/home.html?' + Date.now(),
                    controller: 'HomeCtrl'
                })
                .when('/categories', {
                    templateUrl: '/public/partials/admin/product/categories.html?' + Date.now(),
                    controller: 'CategoriesCtrl'
                })
                .when('/category/main/create', {
                    templateUrl: '/public/partials/admin/product/category-main.html?' + Date.now(),
                    controller: 'CategoryMainCtrl'
                })
                .when('/category/main/edit/:id', {
                    templateUrl: '/public/partials/admin/product/category-main.html?' + Date.now(),
                    controller: 'CategoryMainCtrl'
                })
                .when('/category/main/view/:id', {
                    templateUrl: '/public/partials/admin/product/categoriesdetail.html?' + Date.now(),
                    controller: 'CategoriesDetailCtrl'
                })
                .when('/category/sub/create/:mainId', {
                    templateUrl: '/public/partials/admin/product/category-sub.html?' + Date.now(),
                    controller: 'CategorySubCtrl'
                })
                .when('/category/sub/edit/:mainId/:id', {
                    templateUrl: '/public/partials/admin/product/category-sub.html?' + Date.now(),
                    controller: 'CategorySubCtrl'
                })
                .when('/locales', {
                    templateUrl: '/public/partials/admin/setting/locales.html?' + Date.now(),
                    controller: 'LocalesCtrl'
                })
                .when('/locale/create', {
                    templateUrl: '/public/partials/admin/setting/localecreate.html?' + Date.now(),
                    controller: 'LocaleCtrl'
                })
                .when('/locale/edit/:id', {
                    templateUrl: '/public/partials/admin/setting/localecreate.html?' + Date.now(),
                    controller: 'LocaleCtrl'
                })
                .when('/user', {
                    templateUrl: '/public/partials/admin/user/company.html?v=' + Date.now(),
                    controller: 'UserCompanyCtrl'
                })
                .when('/user/create', {
                    templateUrl: '/public/partials/admin/user/create.html?' + Date.now(),
                    controller: 'UserCreateCtrl'
                })
                .when('/user/:id', {
                    templateUrl: '/public/partials/admin/user/modify.html?' + Date.now(),
                    controller: 'UserModifyCtrl'
                })
                .when('/products', {
                    templateUrl: '/public/partials/admin/product/list.html?' + Date.now(),
                    controller: 'ProductionListCtrl'
                })
                .when('/product/create', {
                    templateUrl: '/public/partials/admin/product/detail.html?' + Date.now(),
                    controller: 'ProductionDetailCtrl'
                })
                .when('/product/:id', {
                    templateUrl: '/public/partials/admin/product/detail.html?' + Date.now(),
                    controller: 'ProductionDetailCtrl'
                })
                .when('/warehouse/list', {
                    templateUrl: '/public/partials/admin/storageoperation/list.html?' + Date.now(),
                    controller: 'StorageOperationListCtrl'
                })
                .when('/warehouse/stores', {
                    templateUrl: '/public/partials/admin/storageoperation/stores.html?' + Date.now(),
                    controller: 'StorageOperationStoresCtrl'
                })
                .when('/warehouse/create', {
                    templateUrl: '/public/partials/admin/storageoperation/create.html?' + Date.now(),
                    controller: 'StorageOperationCreateCtrl'
                })
                .when('/warehouse/create/:id', {
                    templateUrl: '/public/partials/admin/storageoperation/create.html?' + Date.now(),
                    controller: 'StorageOperationCreateCtrl'
                })
                .when('/orders', {
                    templateUrl: '/public/partials/admin/order/list.html?' + Date.now(),
                    controller: 'OrdersCtrl'
                })
                .when('/order/:id', {
                    templateUrl: '/public/partials/admin/order/detail.html?' + Date.now(),
                    controller: 'OrderDetailCtrl'
                })
                //.when('/setting', {
                //    templateUrl: '/public/partials/admin/setting/setting.html?' + Date.now(),
                //    controller: 'SettingCtrl'
                //})
                .when('/languages', {
                    templateUrl: '/public/partials/admin/setting/languages.html?' + Date.now(),
                    controller: 'LanguagesCtrl'
                })
                .when('/languages/create', {
                    templateUrl: '/public/partials/admin/setting/languagecreate.html?' + Date.now(),
                    controller: 'LanguageCreateCtrl'
                })
                .when('/languages/:id', {
                    templateUrl: '/public/partials/admin/setting/languagecreate.html?' + Date.now(),
                    controller: 'LanguageCreateCtrl'
                })
                .when('/brands', {
                    templateUrl: '/public/partials/admin/setting/brands.html?' + Date.now(),
                    controller: 'BrandsCtrl'
                })
                .when('/brands/create', {
                    templateUrl: '/public/partials/admin/setting/brandcreate.html?' + Date.now(),
                    controller: 'BrandCreateCtrl'
                })
                .when('/brands/:id', {
                    templateUrl: '/public/partials/admin/setting/brandcreate.html?' + Date.now(),
                    controller: 'BrandCreateCtrl'
                })
                .when('/checkaddresses/:id', {
                    templateUrl: '/public/partials/admin/setting/checkaddresses.html?' + Date.now(),
                    controller: 'CheckAddressesCtrl'
                })
                .when('/warehouses', {
                    templateUrl: '/public/partials/admin/setting/warehouses.html?' + Date.now(),
                    controller: 'WarehousesCtrl'
                })
                .when('/warehouses/create', {
                    templateUrl: '/public/partials/admin/setting/warehousecreate.html?' + Date.now(),
                    controller: 'WarehouseCreateCtrl'
                })
                .when('/warehouses/:id', {
                    templateUrl: '/public/partials/admin/setting/warehousecreate.html?' + Date.now(),
                    controller: 'WarehouseCreateCtrl'
                })
                .when('/logistics', {
                    templateUrl: '/public/partials/admin/setting/logistics.html?' + Date.now(),
                    controller: 'LogisticsCtrl'
                })
                .when('/logistics/create', {
                    templateUrl: '/public/partials/admin/setting/logisticcreate.html?' + Date.now(),
                    controller: 'LogisticCreateCtrl'
                })
                .when('/logistics/:id', {
                    templateUrl: '/public/partials/admin/setting/logisticcreate.html?' + Date.now(),
                    controller: 'LogisticCreateCtrl'
                })
                .otherwise({
                    redirectTo: '/home'
                });
        }
    ]);

    app.controller('HomeCtrl', ['$scope',
        function($scope) {
            $scope.$emit('loading:hide');

            var pages = $scope.pages = [];

            var furniturePage = {
                addImage: '',
                images: ['http://ceefc02649d25ca315ac-ef93c2ad0985f7464ef79b04e64427fa.r18.cf2.rackcdn.com/home_page/images/23/1_BrisesDePrintemps_NinaHelms.jpg',
                    'http://ceefc02649d25ca315ac-ef93c2ad0985f7464ef79b04e64427fa.r18.cf2.rackcdn.com/home_page/images/24/2_PaintedCabinet_JeffMartinJoinery.jpg'
                ],
                description: '',
                title: '家具',
                subs: [{
                    image: '',
                    href: ''
                }, {
                    image: '',
                    href: ''
                }, {
                    image: '',
                    href: ''
                }]
            };

            var electricalPage = {
                images: [],
                description: '',
                title: '电器',
                subs: [{
                    image: '',
                    href: ''
                }, {
                    image: '',
                    href: ''
                }, {
                    image: '',
                    href: ''
                }]
            };

            var sideboardPage = {
                images: ['http://ceefc02649d25ca315ac-ef93c2ad0985f7464ef79b04e64427fa.r18.cf2.rackcdn.com/home_page/images/23/1_BrisesDePrintemps_NinaHelms.jpg',
                    'http://ceefc02649d25ca315ac-ef93c2ad0985f7464ef79b04e64427fa.r18.cf2.rackcdn.com/home_page/images/24/2_PaintedCabinet_JeffMartinJoinery.jpg'
                ],
                description: '',
                title: '厨具',
                subs: [{
                    image: '',
                    href: ''
                }, {
                    image: '',
                    href: ''
                }, {
                    image: '',
                    href: ''
                }]
            };

            //乐器
            var musicalPage = {
                images: [],
                description: '',
                title: '厨具',
                subs: [{
                    image: '',
                    href: ''
                }, {
                    image: '',
                    href: ''
                }, {
                    image: '',
                    href: ''
                }]
            }

            // $scope.image1 = '';

            // $scope.$watch('$scope.pages', function(value) {
            //     console.log(value)
            // });


            pages.push(furniturePage);
            pages.push(electricalPage);
            pages.push(sideboardPage);
            pages.push(musicalPage);



            for (var i in pages) {
                (function(index) {
                    $scope.$watch('pages[' + index + '].addImage', function(newValue) {
                        if (pages[index].addImage) {
                            pages[index].images.push(newValue);
                            delete pages[index].addImage
                        }

                    })
                })(i);
            }

            $scope.removeSildeImage = function(images, index) {
                images.splice(index, 1);
            }
        }
    ]);

    app.controller('UserCompanyCtrl', ['$scope', '$q', '$http', 'allureRole',
        function($scope, $q, $http, allureRole) {
            $scope.user = {};
            $scope.users = [];
            $scope.roles = [];

            $scope.totalCount = 0;
            $scope.currentPage = 1;
            $scope.size = ListPageSize;

            $scope.roleMap = function (ary) {
                return allureRole.map(ary);
            };


            $scope.getStatus = getStatus;
            $scope.getGender = getGender;

            $scope.STATUS = STATUS;

            $scope.GENDER = GENDER;

            $scope.search = {
                name: '',
                email: '',
                gender: '',
                mobile: '',
                telephone: '',
                company: '',
                roles: null,
                status: '',
                pageSize: $scope.size,
                pageNumber: $scope.currentPage
            }

            $scope.$watch('user.roles', function(value) {
                $scope.search.roles = value;
            });

            function getUsers() {
                var defer = $q.defer();

                $http.get('/api/users', {params: $scope.search})
                    .success(function(data) {
                        defer.resolve(data);
                    })
                    .error(function() {

                    });


                return defer.promise;
            }


            $scope.searchFn = function() {
                getUsers().then(function (data) {
                    $scope.$emit('loading:hide');
                    $scope.totalCount = data.total;
                    $scope.users = data.items;
                });
            }

            $scope.searchFn();



            $scope.pageChanged = function() {
                $scope.search.PageNumber = $scope.currentPage;
                $scope.searchFn();
            };
        }
    ]);
    app.controller('UserCreateCtrl', ['$scope', '$routeParams', '$http', '$q', 'allureRole',
        function($scope, $routeParams, $http, $q, allureRole) {
            var user = $scope.user = {};
            $scope.alerts = [];
            $scope.roles = [];
            $scope.isShowAddress = false;
            $scope.toggleBtn = '+';

            function getUser() {
                var def = $q.defer();

                $http.get('/api/users/' + $routeParams.id)
                    .success(function(data) {
                        def.resolve(data);
                    })
                    .error(function() {

                    });


                return def.promise;
            }

            // title
            if ($routeParams.id) {
                $scope.title = '修改用户';
                $q.all([getUser()]).then(function(ary) {
                    $scope.$emit('loading:hide');
                    var user = ary[0];

                    var roles = allureRole.parse(user.roles);

                    $scope.user = {
                        email: user.email,
                        firstname: user.firstName,
                        lastname: user.lastName,
                        gender: user.gender,
                        company: user.company,
                        mobilephone: user.mobile,
                        phone: user.telephone,
                        roles: user.roles,
                        status: user.status,
                        addresses: user.deliveries
                    };
                    $scope.roleNames = allureRole.map(user.roles);
                    $scope.roles = roles;

                    var len = $scope.user.addresses.length,
                        addresses = [];
                    while (len < 3) {
                        addresses.push({});
                        len++;
                    }
                    $scope.user.addresses = $scope.user.addresses.concat(addresses);
                });
            } else {
                $scope.title = '创建用户';
                user.addresses = [{}, {}, {}];
                user.password = '';
                $scope.$emit('loading:hide');
            }



            $scope.EMAIL_REGEXP = /^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*$/;

            $scope.STATUS = STATUS;

            $scope.GENDER = GENDER;


            $scope.closeAlert = function() {
                $scope.alerts.splice(0);
            };

            $scope.toggleAddress = function(e) {
                $scope.isShowAddress = !$scope.isShowAddress;
                if ($scope.isShowAddress) {
                    $scope.toggleBtn = '-';
                } else {
                    $scope.toggleBtn = '+';
                }
            }
            $scope.isValid;
            $scope.isInvalid;

            $scope.checkEmail = function () {
                var search = {
                    email: $scope.user.email
                }

                var form = $scope.form;
                var errorEls = [],
                    errorTips = language['user']['input_valid'];
                if (!form.roles.$modelValue || form.roles.$modelValue.length === 0) {
                    form.roles.$setValidity('required', false);
                } else {
                    form.roles.$setValidity('required', true);
                }

                _.each(form, function (item) {
                    if (item.$error && item.$valid === false) {
                        var name = item.$name,
                            error = item.$error;
                        if (name == 'email')
                        {
                            $scope.isInvalid = true;
                            $scope.isValid = false;
                            return;
                        }
                    }
                });

                $http.get('/api/users', { params: search })
                    .success(function (data) {
                        if (data.total>0) {
                            $scope.isInvalid = true;
                            $scope.isValid = false;
                        }
                        else {
                            $scope.isInvalid = false;
                            $scope.isValid = true;
                        }
                    })
                    .error(function () {

                    });
            }

            $scope.submit = function() {
                $scope.closeAlert();
                var form = $scope.form;
                var errorEls = [],
                errorTips = language['user']['input_valid'];
                if (!form.roles.$modelValue || form.roles.$modelValue.length === 0) {
                    form.roles.$setValidity('required', false);
                } else {
                    form.roles.$setValidity('required', true);
                }

                _.each(form, function(item) {
                    if (item.$error && item.$valid === false) {
                        var name = item.$name,
                            error = item.$error;
                        for (var key in error) {
                            if (error[key] === true) {
                                errorEls.push(errorTips[name][key])
                            }
                        }
                    }
                });
                _.each(errorEls, function(error) {
                    // errs += error + ''
                    $scope.alerts.push({
                        msg: error
                    });
                });

                if ($scope.user.password != $scope.user.repassword)
                {
                    $scope.alerts.push({
                        msg: '密码和密码确认不匹配'
                    });
                }

                if ($scope.alerts.length !== 0) return false;

                var user = $scope.user;
                var postData = {
                    Email: user.email,
                    // Password: user.Password,
                    FirstName: user.firstname,
                    LastName: user.lastname,
                    Gender: user.gender, //
                    Company: user.company,
                    Mobile: user.mobilephone,
                    Telephone: user.phone,
                    Roles: user.roles,
                    Status: user.status,
                    Deliveries: user.addresses
                };

                var postUrl;
                if ($routeParams.id) {
                    postUrl = '/api/users/' + $routeParams.id;
                    postData.UserId = $routeParams.id;
                } else {
                    postUrl = '/api/users';
                    postData.Password = user.password;
                }

                var submitbtn = angular.element('#submitbtn');
                submitbtn.triggerHandler('start');

                if ($routeParams.id) {
                    $http({
                        method: 'PATCH',
                        url: postUrl,
                        data: postData
                    })
                    .success(function () {
                        alert('保存成功');
                        submitbtn.triggerHandler('end');
                        if (!$routeParams.id) {
                            $scope.roleNames = [];
                            $scope.roles = [];
                            $scope.user = {
                                addresses: [{}, {}, {}]
                            };
                        }
                    })
                    .error(function (msg) {
                        $scope.alerts.length = 0;
                        $scope.alerts.push({
                            msg: msg
                        });
                        submitbtn.triggerHandler('end');
                    });
                } else {
                    $http.post(postUrl, postData)
                    .success(function () {
                        alert('保存成功');
                        submitbtn.triggerHandler('end');
                        if (!$routeParams.id) {
                            $scope.roleNames = [];
                            $scope.roles = [];
                            $scope.user = {
                                addresses: [{}, {}, {}]
                            };
                        }
                    })
                    .error(function (msg) {
                        $scope.alerts.length = 0;
                        $scope.alerts.push({
                            msg: msg
                        });
                        submitbtn.triggerHandler('end');
                    });
                }
                
                return false;
            };
        }
    ]);

    app.controller('UserModifyCtrl', ['$scope', '$routeParams', '$http', '$q', 'allureRole',
            function ($scope, $routeParams, $http, $q, allureRole) {
                var user = $scope.user = {};
                $scope.alerts = [];
                $scope.roles = [];
                $scope.isShowAddress = false;
                $scope.toggleBtn = '+';

                function getUser() {
                    var def = $q.defer();

                    $http.get('/api/users/' + $routeParams.id)
                        .success(function (data) {
                            def.resolve(data);
                        })
                        .error(function () {

                        });


                    return def.promise;
                }

                // title
                if ($routeParams.id) {
                    $scope.title = '修改用户';
                    $q.all([getUser()]).then(function (ary) {
                        $scope.$emit('loading:hide');
                        var user = ary[0];

                        var roles = allureRole.parse(user.roles);

                        $scope.user = {
                            email: user.email,
                            firstname: user.firstName,
                            lastname: user.lastName,
                            gender: user.gender,
                            company: user.company,
                            mobilephone: user.mobile,
                            phone: user.telephone,
                            roles: user.roles,
                            status: user.status,
                            addresses: user.deliveries
                        };
                        $scope.roleNames = allureRole.map(user.roles);
                        $scope.roles = roles;

                        var len = $scope.user.addresses.length,
                            addresses = [];
                        while (len < 3) {
                            addresses.push({ "address": '', "phone": '', "receiver": '', "postCode": '', "userId": $routeParams.id });
                            len++;
                        }
                        $scope.user.addresses = $scope.user.addresses.concat(addresses);
                    });
                } else {
                    $scope.title = '创建用户';
                    user.addresses = [{}, {}, {}];
                    user.password = '';
                    $scope.$emit('loading:hide');
                }



                $scope.EMAIL_REGEXP = /^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~.-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*$/;

                $scope.STATUS = STATUS;

                $scope.GENDER = GENDER;


                $scope.closeAlert = function () {
                    $scope.alerts.splice(0);
                };

                $scope.toggleAddress = function (e) {
                    $scope.isShowAddress = !$scope.isShowAddress;
                    if ($scope.isShowAddress) {
                        $scope.toggleBtn = '-';
                    } else {
                        $scope.toggleBtn = '+';
                    }
                }

                $scope.submit = function () {
                    $scope.closeAlert();

                    var form = $scope.form;
                    var errorEls = [],
                        errorTips = language['user']['input_valid'];
                    if (!form.roles.$modelValue || form.roles.$modelValue.length === 0) {
                        form.roles.$setValidity('required', false);
                    } else {
                        form.roles.$setValidity('required', true);
                    }

                    _.each(form, function (item) {
                        if (item.$error && item.$valid === false) {
                            var name = item.$name,
                                error = item.$error;
                            for (var key in error) {
                                if (error[key] === true) {
                                    errorEls.push(errorTips[name][key])
                                }
                            }
                        }
                    });

                    _.each(errorEls, function (error) {
                        // errs += error + ''
                        $scope.alerts.push({
                            msg: error
                        });
                    });

                    var keepGoing = true;
                    _.each($scope.user.addresses, function (address) {
                        if (keepGoing) {
                            if ((address.address == '' || address.phone == '' || address.receiver == '') && !(address.address == '' && address.phone == '' && address.receiver == '' && address.postCode == '')) {
                                $scope.alerts.push({
                                    msg: '送货地址必须输入地址，收货人，电话'
                                });
                                keepGoing = false;
                            }
                        }
                    });


                    if ($scope.alerts.length !== 0) return false;


                    for (var i = 0; i < $scope.user.addresses.length; i++) {
                        var address = $scope.user.addresses[i];

                        if ((address.address == '' && address.phone == '' && address.receiver == '' && address.postCode == '')) {

                            $scope.user.addresses.splice(i, 1);
                            i--;
                        }
                    }
                    

                    var user = $scope.user;
                    var postData = {
                        Email: user.email,
                        // Password: user.Password,
                        FirstName: user.firstname,
                        LastName: user.lastname,
                        Gender: user.gender, //
                        Company: user.company,
                        Mobile: user.mobilephone,
                        Telephone: user.phone,
                        Roles: user.roles,
                        Status: user.status,
                        Deliveries: user.addresses
                    };

                    var postUrl;
                    if ($routeParams.id) {
                        postUrl = '/api/users/' + $routeParams.id;
                        postData.UserId = $routeParams.id;
                    } else {
                        postUrl = '/api/users';
                        postData.Password = '';
                    }

                    var submitbtn = angular.element('#submitbtn');
                    submitbtn.triggerHandler('start');

                    if ($routeParams.id) {
                        $http({
                            method: 'PATCH',
                            url: postUrl,
                            data: postData
                        })
                        .success(function () {
                            alert('保存成功');
                            submitbtn.triggerHandler('end');
                            window.location.reload();

                            if (!$routeParams.id) {
                                $scope.roleNames = [];
                                $scope.roles = [];
                                $scope.user = {
                                    addresses: [{}, {}, {}]
                                };
                            }
                            
                        })
                        .error(function (msg) {
                            $scope.alerts.length = 0;
                            $scope.alerts.push({
                                msg: msg
                            });
                            submitbtn.triggerHandler('end');
                        });
                    } else {
                        $http.post(postUrl, postData)
                        .success(function () {
                            alert('保存成功');
                            submitbtn.triggerHandler('end');

                            if (!$routeParams.id) {
                                $scope.roleNames = [];
                                $scope.roles = [];
                                $scope.user = {
                                    addresses: [{}, {}, {}]
                                };
                            }
                        })
                        .error(function (msg) {
                            $scope.alerts.length = 0;
                            $scope.alerts.push({
                                msg: msg
                            });
                            submitbtn.triggerHandler('end');
                        });
                    }

                    return false;
                };
            }
    ]);

    app.controller('ProductionListCtrl', ['$scope', '$http', '$q', 'Locale', 'Brand',
        function($scope, $http, $q, Locale, Brand) {

            $scope.totalCount = 0;
            $scope.currentPage = 1;
            $scope.size = PageSize;
            $scope.isCollapsed = true;

            $scope.search = {
                brandId: '',
                number: '',
                locale: '',
                bame: '',
                maxPrice: '',
                minPrice: '',
                orderBy: '',
                orderByTemp:'',
                pageSize: $scope.size,
                pageNumber: $scope.currentPage,
                descending:false
            };
            
            function getList() {
                var defer = $q.defer();
                $scope.search.orderBy = $scope.search.orderByTemp;
                if ($scope.search.orderByTemp.indexOf("DESC")>-1) {
                    $scope.search.descending = true;
                    $scope.search.orderBy = $scope.search.orderByTemp.replace("_DESC", "");
                } else {
                    $scope.search.descending = false;
                }

                $http.get('/api/products', {
                    params: $scope.search })
                    .success(function(data) {
                        defer.resolve(data);
                    })
                    .error(function() {

                    });


                return defer.promise;
            }

            function searchFn() {
                var def = $q.defer();
                getList().then(function(data) {
                    $scope.$emit('loading:hide');
                    $scope.totalCount = data.total;
                    $scope.products = data.items;

                    _.each($scope.products, function (item) {
                        if (item.imageUrl==null) {
                            item.imageUrl = '/public/images/nopic.jpg';
                        }
                        
                    });

                    def.resolve();
                });

                return def.promise;
            }

            function queryLocale() {
                var def = $q.defer();
                $scope.locales = [];
                Locale.query(function (res) {
                    _.each(res, function (item) {
                        var language = unit.getChineseOrDefault(item.localizations);
                        if (language) {
                            language.id = item.id;
                            $scope.locales.push(language);
                        }
                    });
                    def.resolve();
                });

                return def.promise;
            }

            $q.all([searchFn(), Brand.query(), queryLocale()]).then(function (ary) {
                $scope.$emit('loading:hide');
                var data = ary[0];
                $scope.brands = ary[1];
                
            });

            $scope.searchFn = searchFn;


            $scope.orderSearch = function() {
                $scope.pageChanged();
            }

            $scope.pageChanged = function() {
                $scope.$emit('$routeChangeStart');
                $scope.search.PageNumber = $scope.currentPage;
                $scope.searchFn();
            };
        }
    ]);
    app.controller('ProductionDetailCtrl', ['$scope', '$location', '$document', '$timeout', '$routeParams', '$http', '$q',
        'Production', 'Catergory', 'Locale', 'Brand', 'formDataObject', 'allureLanguage',
        function ($scope, $location,$document, $timeout, $routeParams, $http, $q, Production, Catergory, Locale, Brand, formDataObject, allureLanguage) {

            function getInitData() {
                return {
                    State: 0,
                    DisplayOrder: 0,
                    Images: [],
                    localizations: [],
                    vedioURL: ''
                }
            }

            var _smallCatergories = [],
                firstLargeSelected = true,
                languages;

            $scope.alerts = [];
            $scope.locales = [];
            $scope.largeCatergories = [];
            $scope.files = [];
            $scope.postedfiles = [];
            $scope.isCreate = false;

            if ($routeParams.id) {
                $scope.title = '修改产品信息';
            } else {
                $scope.title = '创建产品';
                $scope.isCreate = true;
            }

            function queryCatergory() {
                var def = $q.defer();

                Catergory.getCategoryChinese().then(function(data) {

                    $scope.largeCatergories = [].concat(data.largeCatergories);
                    _smallCatergories = [].concat(data.smallCatergories);
                    $scope.$apply();

                    def.resolve();
                });

                return def.promise;
            }

            function queryLocale() {
                var def = $q.defer();

                Locale.query(function(res) {
                    _.each(res, function(item) {
                        var language = unit.getChineseOrDefault(item.localizations);
                        if (language) {
                            language.id = item.id;
                            $scope.locales.push(language);
                        }
                    });
                    def.resolve();
                });

                return def.promise;
            }

            function queryBrand() {
                var def = $q.defer();

                Brand.query(function(res) {
                    $scope.brands = res;
                    def.resolve();
                });

                return def.promise;
            }

            function queryLanguage() {
                return allureLanguage.getLanguageCurrent();
            }


            function getData() {
                var def = $q.defer();

                if ($routeParams.id) {
                    $http.get('/api/products/' + $routeParams.id)
                        .success(function(data) {
                            data.SubCategoryId = data.categoryId;
                            
                            def.resolve(data);
                        })
                        .error(function() {

                        });
                } else {
                    def.resolve(getInitData());
                }
                return def.promise;
            }

            $q.all([queryLanguage(), getData(), queryCatergory(), queryLocale(), queryBrand()]).then(function(ary) {
                $scope.$emit('loading:hide');
                languages = ary[0];
                $scope.product = ary[1];
                $http.get('/api/categories/' + $scope.product.categoryId).success(function (data) {
                    $scope.product.largeCatergory = data.parentId;
                    //data.LocaleId = data.Locale.id;
                    //data.BrandId = data.Brand.id;

                }).error(function () {

                });
                allureLanguage.initCategoryLanguages($scope.product, languages);

                refreshImages();
            });

            function refreshImages() {
                $timeout(function() {
                    $('.featuredimagezoomerhidden').remove();
                    $('.zoomtracker').remove();
                    featuredimagezoomer.loadinggif = '/public/images/spinningred.gif';
                    $('#multizoom').addimagezoom({ // multi-zoom: options same as for previous Featured Image Zoomer's addimagezoom unless noted as '- new'
                        speed: 1500, // duration of fade in for new zoomable images (in milliseconds, optional) - new
                        descpos: true, // if set to true - description position follows image position at a set distance, defaults to false (optional) - new
                        imagevertcenter: true, // zoomable image centers vertically in its container (optional) - new
                        magvertcenter: true, // magnified area centers vertically in relation to the zoomable image (optional) - new
                        zoomrange: [3, 10],
                        magnifiersize: [300, 300],
                        magnifierpos: 'right',
                        cursorshade: true //<-- No comma after last option!
                    });
                }, 0);
            }

            $scope.$watch('product.largeCatergory', function(newValue) {
                if ($scope.product) {
                    if (firstLargeSelected) {
                        firstLargeSelected = false;
                    } else {
                        $scope.product.SubCategoryId = '';
                    }
                    $scope.smallCatergories = _.filter(_smallCatergories, function(item) {
                        return item.parentId == $scope.product.largeCatergory;
                    });
                }
            });

            $scope.closeAlert = function() {
                $scope.alerts.splice(0);
            };

            $scope.$watch('product.end', function(v) {
                //console.log(v);
            });

            $scope.submitProductData = function () {

                if ($scope.files.length != $scope.postedfiles.length) {
                    return;
                }

                // set temp value to name of localization 
                _.each($scope.product.localizations, function (item) {
                    if (!item.name) {
                        item.name = 'Temp';
                    }
                });

                // set image
                $scope.product.imageIds = [];
                _.each($scope.product.images, function (item) {
                    $scope.product.imageIds.push(item.id);
                });
                _.each($scope.postedfiles, function (item) {
                    $scope.product.imageIds.push(item);
                });

                var postUrl;
                var submitbtn = angular.element('#submitbtn');
                if ($routeParams.id) {
                    postUrl = '/api/products/' + $routeParams.id;

                    $http({
                        method: 'PATCH',
                        url: postUrl,
                        data: $scope.product
                    })
                    .success(function (data) {
                        $scope.files.length = 0;
                        $scope.product = data;
                        //allureLanguage.initCategoryLanguages($scope.category, $scope.languages);
                        alert('保存成功');
                        submitbtn.triggerHandler('end');
                        //var url = ($location.absUrl() + '?' + Date.now());
                        //window.location.href = url;
                        window.location.reload();
                    })
                    .error(function (msg) {
                        $scope.alerts.push({
                            msg: msg
                        });
                        submitbtn.triggerHandler('end');
                    });

                } else {
                    
                    postUrl = '/api/products';
                    $http({
                        method: 'POST',
                        url: postUrl,
                        data: $scope.product
                    })
                    .success(function (data) {
                        $scope.products = data;
                        alert('保存成功');
                        submitbtn.triggerHandler('end');

                        //var url = ($location.absUrl() + '?' + Date.now());
                        //url = url.replace("product/create", "products");
                        //window.location.href = url;
                        window.location.reload();

                    })
                    .error(function (msg, status) {
                        if (status == '409') {
                            $scope.alerts.push({
                                msg: '您创建的产品重复'
                            });
                        }
                        else {
                            $scope.alerts.push({
                                msg: msg
                            });
                        }


                        
                        submitbtn.triggerHandler('end');
                    });
                }
            }

            $scope.save = function() {
                $scope.closeAlert();

                var product = $scope.product,
                    alerts = $scope.alerts;

                if (isNaN(product.categoryId)) {
                    alerts.push({
                        msg: '请选择小分类'
                    });
                }
                if (isNaN(product.brandId)) {
                    alerts.push({
                        msg: '请选择品牌'
                    });
                }
                if (!product.number) {
                    alerts.push({
                        msg: '请填写货号'
                    });
                }
                if (!product.name) {
                    alerts.push({
                        msg: '请填写品名'
                    });
                }
                if (isNaN(product.price)) {
                    alerts.push({
                        msg: '请正确填写价格'
                    });
                }
                if (isNaN(product.displayOrder)) {
                    alerts.push({
                        msg: '请正确填写排序位'
                    });
                }
                if (!product.start || !product.end) {
                    alerts.push({
                        msg: '请正确填写时间段'
                    });
                }
                if (isNaN(product.localeId)) {
                    alerts.push({
                        msg: '请选择产地'
                    });
                }
                var r = _.find(product.localizations, function(item) {
                    if ((!item.description || item.description === '') && item.isDefault == true)
                    {
                        alerts.push({
                            msg: '请填写' + item.title + '描述'
                        });
                    }                       
                });
                if (r) {
                    
                }

                //if (product.images.length === 0 && $scope.files.length === 0) {
                //    alerts.push({
                //        msg: '请上传图片'
                //    });
                //}

                if (alerts.length > 0) {
                    return;
                }

                var postUrl;
                if ($routeParams.id) {
                    postUrl = '/api/product/' + $routeParams.id;
                } else {
                    postUrl = '/api/product';
                }

                var postData = {};
                postData.product = JSON.stringify($scope.product);

                _.each($scope.files, function(file, i) {
                    postData['file' + i] = file;
                });

                var submitbtn = angular.element('#submitbtn');
                submitbtn.triggerHandler('start');

                //upload file
                if ($scope.files.length > 0) {
                    _.each($scope.files, function(file, i) {
                        $http({
                            method: 'POST',
                            url: "/api/images",
                            headers: {
                                'Content-Type': undefined
                            },
                            data: {
                                file: file
                            },
                            transformRequest: formDataObject
                        })
                        .success(function (data) {

                            $scope.postedfiles.push(data.id);
                            $scope.submitProductData();

                        })
                        .error(function (msg) {
                            $scope.alerts.push({
                                msg: msg
                            });
                            submitbtn.triggerHandler('end');
                        });
                    });
                    
                } else {
                    $scope.submitProductData();
                }


                //$http({
                //        method: 'POST',
                //        url: postUrl,
                //        headers: {
                //            'Content-Type': undefined
                //        },
                //        data: postData,
                //        transformRequest: formDataObject
                //    })
                //    .success(function(data) {
                //        alert('保存成功');
                //        submitbtn.triggerHandler('end');
                //        if (!$routeParams.id) {
                //            $scope.product = getInitData();
                //            $scope.files = [];
                //            allureLanguage.initCategoryLanguages($scope.product, languages);
                //        }
                //    })
                //    .error(function() {
                //        $scope.alerts.push({
                //            msg: msg
                //        });
                //        submitbtn.triggerHandler('end');
                //    });
            };


            $scope.removeImage = function(index) {
                $scope.files.splice(index, 1);
            }

            $scope.removeExistImage = function(index) {
                $scope.product.images.splice(index, 1);
            }

            $scope.$on('$routeChangeStart', function(evt, next, current) {
                $('.featuredimagezoomerhidden').remove();
                $('.zoomtracker').remove();
            });

            $scope.productdelete = function() {
                if (confirm("您确定要删除当前数据吗？") === true) {
                    var postUrl = '/api/products/' +$routeParams.id;
                    $http({ url : postUrl, method: "DELETE"
                    })
                        .success(function () {
                            var url = ("/admin/#/products" + '?' +Date.now());
                            window.location.href = url;
                        })
                        .error(function (msg) {
                            alert(msg);
                            });
                            }
                    }
        }
    ]);

    app.controller('StorageOperationCreateCtrl', ['$scope', '$http','$timeout', '$q', 'WarehouseCode', 'Logistics', 'WarehouseProduct', 'WarehouseOrder','$routeParams',
    function ($scope, $http, $timeout, $q, WarehouseCode, Logistics, WarehouseProduct, WarehouseOrder,$routeParams) {
            $scope.$emit('loading:hide');
            $scope.alerts = [];
            $scope.isView = false;
            $scope.closeAlert = function () {
                $scope.alerts.splice(0);
            };

            var currentUser = {
                name: ''
            };
            
            $scope.baseInfo = {
                createBy: currentUser.name,
                updateBy: currentUser.name,
                orderId: null,
                orderNO: null,
                details: [],
                logisticName: null,
                type: 'Out'
            };
            $.getJSON("/api/users?currentuser=currentuser",
                function (data) {
                    $scope.baseInfo.updateBy = (data.firstName + ' ' + data.lastName);
                    $scope.baseInfo.createBy = (data.firstName + ' ' + data.lastName);
                });
            var timerHandle, queryTimerHandle;

            function getTime() {
                var n = new Date;
                return n.getFullYear() + '-' + (n.getMonth() + 1) + '-' + n.getDate() + ' ' + n.getHours() + ':' + n.getMinutes();
            }

            function updateTime() {
                timerHandle = $timeout(function() {
                    $scope.baseInfo.createTime = getTime();
                    updateTime();
                }, 1000 * 60);
            }

            function setCurrentTime() {
                $scope.baseInfo.createTime = getTime();
                $scope.baseInfo.updateTime = getTime();

                //updateTime();
            }

            setCurrentTime();

            function GetStorageOperation() {
                var deferred = $q.defer();
                if ($routeParams.id) {
                    $scope.isView = true;
                    $http.get('/api/storageoperations/' + $routeParams.id).success(function (data) {
                        if (data) {
                            deferred.resolve(data);
                            $scope.baseInfo = data;
                        }
                    }).error(function(data) {

                    });
                } 
                return deferred.promise;
            }

            $scope.updatelogisticExpense = function () {
                {
                    if (angular.equals(parseInt($scope.baseInfo.logisticExpense), NaN)) {
                        $scope.baseInfo.logisticExpense = 0;
                    }
                    else {
                        $scope.baseInfo.logisticExpense = parseInt($scope.baseInfo.logisticExpense);
                    }
                }
            }

            $scope.operationCountUpdate = function (obj) {
                {
                    if (angular.equals(parseInt(obj.warehouse.operationCount), NaN)) {
                        obj.warehouse.operationCount = 0;
                    }
                    else {
                        obj.warehouse.operationCount = parseInt(obj.warehouse.operationCount);
                    }
                }
            }

            $q.all([GetStorageOperation()]).then(function (ary) {
                $scope.baseInfo = ary[0];
            });


            WarehouseCode.query(function (res) {
                $scope.warehouseCodes = res;
            });

            Logistics.query(function(res) {
                $scope.logisticses = res;
            });


            $scope.addNewItem = function () {
                var result = _.find($scope.baseInfo.details, function(item) {
                    return item.isNew == true;
                });
                if (result) {
                    return;
                }
                $scope.newWarehouse = {
                    isNew: true
                }
                $scope.baseInfo.details.push($scope.newWarehouse);
            }
            $scope.removeItem = function(index) {
                if (index) {
                    $scope.baseInfo.details.splice(index, 1);
                } else {
                    $scope.baseInfo.details.splice(0);
                }
            }
            $scope.submitlogisticExpense = function () {
                $scope.closeAlert();
                var alerts = $scope.alerts;

                if ($scope.baseInfo.logisticExpense == null || $scope.baseInfo.logisticExpense === '') {
                    alerts.push({
                        msg: '请输入物流费用'
                    });
                    return;
                }
                var logisticExpense = {
                    logisticExpense: $scope.baseInfo.logisticExpense
                }
                $http({
                    url: '/api/storageoperations/' + $routeParams.id, data: logisticExpense, method: "PATCH"
                })
                    .success(function (data) {
                        alert('更新物流费用成功');
                    }).error(function (msg) {
                        $scope.alerts.push({
                            msg: msg
                        });
                        submitbtn.triggerHandler('end');
                    });
            }

            $scope.queryOrderNumberByNO = function () {
                $http.get('/api/orders', {
                    params: {
                        orderNO: $scope.baseInfo.orderNO,
                        PageSize: 1,
                        PageNumber: 1
                        }
                }).then(function (data) {
                    if (data.data.items.length) {
                        $scope.baseInfo.orderId = data.data.items[0].id;
                        $scope.queryOrderNumber();
                    } else {
                        
                    }
                });
            }
        $scope.queryOrderNumber = function () {
               
                if (queryTimerHandle) {
                    $timeout.cancel(queryTimerHandle);
                    queryTimerHandle = null;
                }
                queryTimerHandle = $timeout(function() {
                    if ($scope.baseInfo.type == 'Out') {
                        var orderNumber = $scope.baseInfo.orderId;
                        WarehouseOrder.get({
                            id: orderNumber
                        }, function (res) {
                            //delete res.operate;
                            $scope.baseInfo.details = [];
                            _.each(res.details, function (item, key) {
                                var product = {};
                                product.orderDetailId = item.id;
                                product.productId = item.product.id;
                                product.productNumber = item.product.number;
                                product.productName = item.product.name;
                                product.deliveryTime = item.deliveryTime;
                                product.originalCount = item.count - item.delivered;
                                product.operationCount = 0;
                                product.brandName = item.product.brandName;
                                $scope.baseInfo.details.push(product);
                            })
                        });
                    }
                }, 100);
            }
        $scope.queryNumber = function () {
                var result = _.find($scope.baseInfo.details, function (item) {
                    return item.isNew == true;
                });
                if (result && result.productNumber) {
                    WarehouseProduct.get({
                        id: result.productNumber
                    }, function (res) {
                        if (res.id != undefined) {
                            result.productId = res.id;
                            result.productName = res.name;
                            result.originalCount = res.current;
                            result.operationCount = res.current;
                            result.brandName = res.brandName;
                            result.smallCatergory = res.categoryName;
                            result.useCount = 0;
                            result.isNew = false;
                        }
                        else {
                            $scope.closeAlert();
                            var alerts = $scope.alerts;
                            alerts.push({
                                msg: '您输入的货号没有找到。请重新输入。'
                            });
                            return;
                        }
                    });
                }
            }

            function mustNumber(newValue, oldValue) {
                if (!newValue) return newValue;

                newValue = +newValue;
                oldValue = oldValue == '' ? '' : +oldValue;
                if (!_.isNumber(newValue) || _.isNaN(newValue)) {
                    if (_.isNumber(oldValue) && !_.isNaN(oldValue)) {
                        return oldValue;
                    } else {
                        return '';
                    }
                }
                return newValue;
            }

            //$scope.$watch('newWarehouse.useCount', function(newValue, oldValue) {

            //    if (!$scope.newWarehouse) return;

            //    var result = mustNumber(newValue, oldValue);
            //    if (result) {
            //        $scope.newWarehouse.useCount = result;
            //    } else {
            //        $scope.newWarehouse.useCount = 0;
            //    }

            //    if ($scope.newWarehouse.count - $scope.newWarehouse.useCount < 0) {
            //        $scope.newWarehouse.useCount = $scope.newWarehouse.count;
            //    }
            //});

            $scope.$watch('baseInfo.orderId', function(newValue, oldValue) {
                $scope.baseInfo.orderId = mustNumber(newValue, oldValue);
            });

            $scope.$watch('baseInfo.type', function (newValue, oldValue) {
                if ($scope.baseInfo.type == undefined) return;

                _.each(['orderNumber', 'systemNumber', 'number', 'warehouseCode', 'operateName', 'logistics', 'logisticsNumber', 'logisticsCost', 'remark', 'time'], function(name) {
                    //delete $scope.baseInfo[name];
                    if (!$routeParams.id) {
                        $scope.baseInfo.details = [];
                    }
                });

                if (newValue == 0 && timerHandle) {
                    $timeout.cancel(timerHandle);
                    timerHandle = null;
                } else if (newValue == 1 && !timerHandle) {
                    setCurrentTime();
                }
                if (newValue == 1) {
                    $scope.baseInfo.operateName = currentUser.name;
                }
            });

            $scope.submit = function () {
                $scope.closeAlert();
                var alerts = $scope.alerts;
                
                if ($scope.baseInfo.type == null) {
                    alerts.push({
                        msg: '请选择入库或出库'
                    });
                    return;
                }
                if ($scope.baseInfo.type == 'Out') { //出库
                    if ($scope.baseInfo.orderId == null) {
                        alerts.push({
                            msg: '请输入订单号'
                        });
                    }
                    if ($scope.baseInfo.warehouseId == null) {
                        alerts.push({
                            msg: '请选择仓库'
                        });
                    }
                    if ($scope.baseInfo.logisticId == null || $scope.baseInfo.logisticId == undefined || $scope.baseInfo.logisticId === '') {
                        alerts.push({
                            msg: '请选择物流公司'
                        });
                    }
                    if ($scope.baseInfo.logisticNumber == null || $scope.baseInfo.logisticNumber == undefined || $scope.baseInfo.logisticNumber === '') {
                        alerts.push({
                            msg: '请输入物流单号'
                        });
                    }
                    if ($scope.baseInfo.details.length == 0) {
                        alerts.push({
                            msg: '货物清单不能为空'
                        });
                    } else {
                        var keepGoing = true;
                        angular.forEach($scope.baseInfo.details, function (item) {
                            if (keepGoing) {
                                if (item.operationCount == null || item.operationCount == undefined || item.operationCount === '' || parseInt(item.operationCount) <= 0) {
                                    alerts.push({
                                        msg: '操作数量不能为空或小于等于0'
                                    });
                                    keepGoing = false;
                                }
                                if ((item.originalCount - item.operationCount) <0 ) {
                                    alerts.push({
                                        msg: '操作数量不能大于当前可用数量'
                                    });
                                    keepGoing = false;
                                }
                            }
                        });
                    }
                } else { //入库
                    if ($scope.baseInfo.warehouseId == null) {
                        alerts.push({
                            msg: '请选择仓库'
                        });
                    }
                    if ($scope.baseInfo.details.length == 0) {
                        alerts.push({
                            msg: '货物清单不能为空'
                        });
                    } else {
                        var keepGoing = true;
                        angular.forEach($scope.baseInfo.details, function (item) {
                            if (keepGoing) {
                                if (item.operationCount == null || item.operationCount == undefined || item.operationCount === '' || parseInt(item.operationCount) <= 0) {
                                    alerts.push({
                                        msg: '操作数量不能为空或小于等于0'
                                    });
                                    keepGoing = false;
                                }
                            }
                        });
                    }
                }

                if (alerts.length > 0) {
                    return;
                }

                var submitbtn = angular.element('#submitbtn');
                submitbtn.triggerHandler('start');

                var postUrl;
                postUrl = '/api/storageoperations';
                $http.post(postUrl, $scope.baseInfo)
                    .success(function () {
                        alert('保存成功');
                        submitbtn.triggerHandler('end');
                        if (!$routeParams.id) {
                            window.location.reload();
                        }
                    })
                    .error(function (msg) {
                        $scope.alerts.length = 0;
                        $scope.alerts.push({
                            msg: msg
                        });
                        submitbtn.triggerHandler('end');
                    });


                return false;
            };
        }
    ]);
    app.controller('StorageOperationListCtrl', ['$scope', 'WarehouseCode', 'Logistics', '$http',
        function($scope, WarehouseCode, Logistics,$http) {
            $scope.open = function () { }
            $scope.$emit('loading:hide');
            $scope.totalCount = 0;
            $scope.currentPage = 1;
            $scope.size = ListPageSize;
            $scope.isCollapsed = true;

            WarehouseCode.query(function(res) {
                $scope.warehouseCodes = res;
            });

            Logistics.query(function(res) {
                $scope.logisticses = res;
            });

            $scope.search = {
                storageoperationid: null,
                orderid: null,
                orderNo: null,
                createdate: null,
                updatedate: null,
                logisticname: '',
                logisticnumber: null,
                createby: null,
                updateby: null,
                type: '',
                warehousename: '',
                pagesize: $scope.size,
                pagenumber: $scope.currentPage
            };

            $scope.searchFn = function () {
                $http.get('/api/storageoperations', {
                    params: $scope.search
                }).then(function (data) {
                    if (data.data.items.length) {
                        $scope.list = data.data.items;
                        $scope.totalCount = data.data.total;
                    } else {
                        $scope.list = {};
                        $scope.totalCount = 0;
                    }
                });
            };

            $scope.pageChanged = function () {
                $scope.search.PageNumber = $scope.currentPage;
                $scope.searchFn();
            };
        }
    ]);
    app.controller('StorageOperationStoresCtrl', ['$scope', 'WarehouseCode', 'Logistics', 'Locale', 'Brand', 'Catergory', '$http', '$q',
    function($scope, WarehouseCode, Logistics,Locale, Brand,Catergory,$http,$q) {
        $scope.totalCount = 0;
        $scope.currentPage = 1;
        $scope.size = ListPageSize;
        $scope.isCollapsed = true;


            var _smallCatergories = [];
            $scope.largeCatergories = [];
            var firstLargeSelected = true;

            $scope.search = {
                parentCategoryId: '',
                PageSize: $scope.size,
                PageNumber: $scope.currentPage
            };

            Locale.query(function (res) {
                $scope.locales = res;
             });

            Brand.query(function (res) {
                $scope.brands = res;
            });


            Catergory.getCategoryChinese().then(function (data) {
                $scope.largeCatergories = [].concat(data.largeCatergories);
                _smallCatergories = [].concat(data.smallCatergories);
                $scope.$apply();
                $scope.$emit('loading:hide');
            });

            $scope.$watch('search.parentCategoryId', function (newValue) {
                    if (firstLargeSelected) {
                        firstLargeSelected = false;
                    } else {
                        $scope.search.subCategoryId = '';
                    }
                    $scope.smallCatergories = _.filter(_smallCatergories, function (item) {
                        return item.parentId == $scope.search.parentCategoryId;
                    });
            });
            $scope.searchFn = function () {
                $http.get('/api/products/storage', {
                    params: $scope.search
                }).then(function (data) {
                    if (data.data.items.length) {
                        $scope.list = data.data.items;
                        $scope.totalCount = data.data.total;
                    } else {
                        $scope.list = {};
                        $scope.totalCount = 0;
                    }
                });
            };

            $scope.pageChanged = function () {
                $scope.search.PageNumber = $scope.currentPage;
                $scope.searchFn();
            };
        }
    ]);
    app.controller('OrdersCtrl', ['$scope', '$q', '$http', 'Order',
        function($scope, $q, $http, Order) {
            $scope.open = function() {}
            $scope.$emit('loading:hide');
            $scope.totalCount = 0;
            $scope.currentPage = 1;
            $scope.size = ListPageSize;
            $scope.isCollapsed = true;

            $scope.getOrderStatus = getOrderStatus;
            $scope.ORDERSTATUS = ORDERSTATUS;
            $scope.search = {
                orderid: null,
                status: null,
                customer: null,
                mintotal: null,
                maxtotal: null,
                mincreatetime: null,
                maxcreatetime: null,
                minupdatetime: null,
                maxupdatetime: null,
                sortby: null,
                PageSize: $scope.size,
                PageNumber: $scope.currentPage
            };

            /*$scope.searchFn = function(){
                Order.query(function(res) {
                    $scope.$emit('loading:hide');
                    $scope.list = res;
                });
            };*/
            var defer = $q.defer();

            $scope.searchFn = function(){
                $http.get('/api/orders', {
                    params: $scope.search
                }).then(function (data) {
                    if(data.data.items.length){
                        $scope.list = data.data.items;
                        $scope.totalCount = data.data.total;
                    } else {
                        $scope.list = {};
                        $scope.totalCount = 0;
                    }
                });
            };
            $scope.sumMoney = function(index){
                var list = $scope.list;
                var productDetails = list[index].Details;
                var countMoney = 0;
                angular.forEach(productDetails, function(item, value) {
                    countMoney = countMoney + item.Product.Price;
                });
                return countMoney;
            }
            $scope.searchFn();
            $scope.pageChanged = function() {
                $scope.search.PageNumber = $scope.currentPage;
                $scope.searchFn();
            };
        }
    ]);
    app.controller('OrderDetailCtrl', ['$scope', '$http', '$location', 'OrderStatus','$routeParams','Logistic', '$q','WarehouseProduct',
        function($scope, $http, $location, OrderStatus,$routeParams,Logistic,$q,WarehouseProduct) {
            $scope.$emit('loading:hide');
            $scope.alerts = [];
            $scope.isCollapsed1 = false;
            $scope.isCollapsed2 = false;
            $scope.isCollapsed3 = false;
            $scope.title = "";
            $scope.isCreate = false;

            $scope.logistics = [];
            Logistic.query(function (res) {
                $scope.logistics = res;
            });

            if ($routeParams.id) {
                $scope.title = '修改订单信息';
            } else {
                $scope.title = '创建订单';
                $scope.isCreate = true;
            }

            $scope.getOrderStatus = getOrderStatus;
            $scope.ORDERSTATUS = ORDERSTATUS;
            var hash = $location.$$path.split('/');
            var orderId = hash[hash.length - 1];
            $scope.UpdateOrder = {
                id: null,
                status: null,
                statuscurrent: null,
                willCheck: null,
                checkAddress: null,
                checkTime: null,
                checkContact: null,
                logisticId: null,
                logisticOrderNumber: null,
                discount: null,
                deposit: null,
                depositReceipt: null,
                remaining: null,
                remainingReceipt: null,
                details: []
            };
            var updateOrderValue = function () {

                $scope.UpdateOrder.id = $scope.OrderInfo.id;
                $scope.UpdateOrder.orderNO = $scope.OrderInfo.orderNO;
                $scope.UpdateOrder.willCheck = $scope.OrderInfo.willCheck;
                $scope.UpdateOrder.status = $scope.OrderInfo.status;
                $scope.UpdateOrder.statuscurrent = $scope.OrderInfo.status;
                $scope.UpdateOrder.checkAddress = $scope.OrderInfo.checkAddress;
                $scope.UpdateOrder.checkTime = $scope.OrderInfo.checkTime;
                $scope.UpdateOrder.checkContact = $scope.OrderInfo.checkContact;
                $scope.UpdateOrder.receiverName = $scope.OrderInfo.delivery.receiver;
                $scope.UpdateOrder.receiverContact = $scope.OrderInfo.delivery.phone;
                $scope.UpdateOrder.receiverAddress = $scope.OrderInfo.delivery.address;
                $scope.UpdateOrder.receiverPostCode = $scope.OrderInfo.delivery.postCode;
                $scope.UpdateOrder.logisticId = $scope.OrderInfo.logisticId;
                $scope.UpdateOrder.logisticOrderNumber = $scope.OrderInfo.logisticOrderNumber;
                $scope.UpdateOrder.discount = $scope.OrderInfo.discount;
                $scope.UpdateOrder.deposit = $scope.OrderInfo.deposit;
                $scope.UpdateOrder.depositReceipt = $scope.OrderInfo.depositReceipt;
                $scope.UpdateOrder.depositDeadline = $scope.OrderInfo.depositDeadline;
                $scope.UpdateOrder.remaining = $scope.OrderInfo.remaining;
                $scope.UpdateOrder.remainingReceipt = $scope.OrderInfo.remainingReceipt;
                $scope.UpdateOrder.remainingDeadline = $scope.OrderInfo.remainingDeadline;
                $scope.UpdateOrder.details = [];
                angular.forEach($scope.OrderInfo.details, function (item, key) {
                    var updateitem = {};
                    updateitem.id = item.id;
                    updateitem.discount = item.discount;
                    updateitem.deliveryTime = item.deliveryTime;
                    updateitem.productId = item.product.id;
                    updateitem.product = {};
                    updateitem.product.number = item.product.number;
                    updateitem.product.name = item.product.name;
                    updateitem.product.price = item.product.price;
                    updateitem.product.brandName = item.product.brandName;
                    updateitem.count = item.count;
                    $scope.UpdateOrder.details.push(updateitem);
                });

                // set ORDERSTATUS
                for (var i = 0; i < $scope.ORDERSTATUS.length; i++) {
                    if ($scope.ORDERSTATUS[i].value != $scope.UpdateOrder.status) {
                        $scope.ORDERSTATUS.splice($scope.ORDERSTATUS[i], 1);
                        i--;
                    } else {
                        break;
                    }
                }
            };
            $scope.queryNumber = function () {
                var result = _.find($scope.UpdateOrder.details, function (item) {
                    return item.isNew == true;
                });

                if (result && result.product.number) {
                    WarehouseProduct.get({
                        id: result.product.number
                    }, function (res) {
                        if (res.id != undefined) {
                            result.productId = res.id;
                            result.product.number = res.number;
                            result.product.name = res.name;
                            result.product.price = res.price;
                            result.product.brandName = res.brandName;
                            result.useCount = 0;
                            result.isNew = false;
                        }
                        else {
                            $scope.closeAlert();
                            var alerts = $scope.alerts;
                            alerts.push({
                                msg: '您输入的货号没有找到。请重新输入。'
                            });
                            return;
                        }
                    });
                }
            }

            $scope.removeItem = function (index) {
                if (index) {
                    $scope.UpdateOrder.details.splice(index, 1);
                } else {
                    $scope.UpdateOrder.details.splice(0);
                }
            }

            $scope.addNewItem = function () {
                var result = _.find($scope.UpdateOrder.details, function (item) {
                    return item.isNew == true;
                });
                if (result) {
                    return;
                }
                $scope.newOrderDetail = {
                    isNew: true,
                    product: { price: 0 },
                    discount: 0,
                    count:0
                }
                $scope.UpdateOrder.details.push($scope.newOrderDetail);
            }
            $scope.amount = function (count, price, discount) {
                
                return count * price - discount;
            };
            $scope.totalAmountMoney = function () {
                if (typeof ($scope.UpdateOrder) == "undefined") return 0;

                var details = $scope.UpdateOrder.details;
                var totalMoney = 0;
                angular.forEach(details, function(detail, index){
                    totalMoney = totalMoney + detail.product.price * detail.count - detail.discount;
                });
                return totalMoney;
            };
            $scope.totalDiscount = function () {
                if (typeof ($scope.UpdateOrder) == "undefined") return 0;

                var details = $scope.UpdateOrder.details;
                var totalDiscount = 0;
                angular.forEach(details, function(detail, index){
                    totalDiscount = totalDiscount + detail.discount * detail.count;
                });
                return totalDiscount;
            };
            $scope.totalPay = function () {
                $scope.remainingPay();
                return $scope.totalAmountMoney() - $scope.UpdateOrder.discount;
            };
            $scope.remainingPay = function () {
                $scope.UpdateOrder.remaining = $scope.totalAmountMoney() - $scope.UpdateOrder.discount - $scope.UpdateOrder.deposit;
            };
            $scope.closeAlert = function () {
                $scope.alerts.splice(0);
            };
            $scope.orderUpdate = function () {
                $scope.closeAlert();
                var alerts = $scope.alerts;

                if ($scope.UpdateOrder.details.length == 0) {
                    alerts.push({
                        msg: '订单明细不能为空'
                    });
                }
                angular.forEach($scope.UpdateOrder.details, function (item, key) {
                    if (!item.productId) {
                        alerts.push({
                            msg: '请输入货号'
                        });
                    }
                    if (!item.count || item.count === '0' || item.count === '') {
                        alerts.push({
                            msg: '数量不能0或空'
                        });
                    }
                });

                if (alerts.length > 0) {
                    return;
                }
                if ($scope.UpdateOrder.status != $scope.UpdateOrder.statuscurrent) {
                    var result = confirm('您更改了订单状态, 您确定要更新当前订单状态为' + $scope.getOrderStatus($scope.UpdateOrder.status) + '?');
                    if (!result) {
                        return;
                    }
                } else {
                    var result = confirm('您确定要更新当前订单吗?');
                    if (!result) {
                        return;
                    }
                }

                var submitbtn = angular.element('#submitbtn');
                submitbtn.triggerHandler('start');
                

                $http({
                    url: '/api/orders/' + $routeParams.id, data: $scope.UpdateOrder, method: "PATCH" })
                    .success(function (data) {
                        alert('更新订单成功');
                        window.location.reload();
                    }).error(function (msg) {
                        $scope.alerts.push({
                            msg: msg
                        });
                        submitbtn.triggerHandler('end');
                    });
            };
            $scope.orderDelete = function(){
                $http.post(' /order/delete/' + orderId).then(function(data){
                    if(data.data){
                        alert('删除订单成功');
                        $location.path('/orders');
                    }
                });
            };

            $http.get('/api/orders/' + $routeParams.id).then(function (data) {
                if(data.data){
                    $scope.OrderInfo = data.data;
                    updateOrderValue();
                }
            });
            $scope.discountUpdate = function (obj) {
                if (angular.equals(parseInt(obj.detail.count), NaN)) {
                    obj.detail.count = 0;
                }
                else {
                    obj.detail.count = parseInt(obj.detail.count);
                }
                
               
                var amount1 = obj.detail.count * obj.detail.product.price - obj.detail.discount;
                if (amount1 < 0) {
                    alert('折扣金额不能大于合计金额');
                    obj.detail.discount = 0;
                }

                if ($scope.totalPay()<0) {
                    alert('折扣金额不能大于总计金额');
                    obj.detail.discount = 0;
                }
            };

            $scope.totaldiscountUpdate = function (obj) {
                if (angular.equals(parseInt(obj.UpdateOrder.discount), NaN)) {
                    obj.UpdateOrder.discount = 0;
                }
                else {
                    obj.UpdateOrder.discount = parseInt(obj.UpdateOrder.discount);
                }

                if ($scope.totalPay() < 0) {
                    alert('订单折扣不能大于总计金额');
                    obj.UpdateOrder.discount = 0;
                }
            };
        }
    ]);

    app.controller('CategoriesCtrl', ['$scope', '$http', '$q', 'allureLanguage',
        function($scope, $http, $q, allureLanguage) {
            function getCategory() {
                var deferred = $q.defer();
                $http.get('/api/categories')
                    .success(function (data) {
                        deferred.resolve(data);
                    })
                    .error(function(data) {

                    });
                return deferred.promise;
            }

            $scope.getLanguageName = function(code) {
                return allureLanguage.getLanguageName($scope.languages, code);
            }

            $q.all([getCategory(), allureLanguage.getLanguageCurrent()]).then(function(ary) {
                $scope.$emit('loading:hide');
                $scope.categories = ary[0];
                $scope.languages = ary[1];
            });
        }
    ]);

    app.controller('CategoriesDetailCtrl', ['$scope', '$http', '$q', '$routeParams', 'allureLanguage',
        function ($scope, $http, $q,$routeParams, allureLanguage) {
            function getCategory() {
                var deferred = $q.defer();
                $http.get('/api/categories/' + $routeParams.id)
                    .success(function (data) {
                        deferred.resolve(data);
                    })
                    .error(function (data) {

                    });
                return deferred.promise;
            }

            $scope.getLanguageName = function (code) {
                return allureLanguage.getLanguageName($scope.languages, code);
            }

            $q.all([getCategory(), allureLanguage.getLanguageCurrent()]).then(function (ary) {
                $scope.$emit('loading:hide');
                $scope.categories = [ary[0]];
                $scope.languages = ary[1];
            });
        }
    ]);

    app.controller('CategoryMainCtrl', ['$scope', '$http', '$q', '$routeParams', 'allureLanguage',
        function($scope, $http, $q, $routeParams, allureLanguage) {
            $scope.alerts = [];

            if ($routeParams.id) {
                $scope.title = '修改主分类';
            } else {
                $scope.title = '创建主分类';
            }

            function getCategory() {
                var def = $q.defer();
                if (!$routeParams.id) {
                    def.resolve();
                    return;
                }
                $http.get('/api/categories/' + $routeParams.id)
                    .success(function(data) {
                        def.resolve(data);
                    })
                    .error(function() {

                    });

                return def.promise;
            }
            $q.all([getCategory(), allureLanguage.getLanguageCurrent()]).then(function(ary) {
                $scope.$emit('loading:hide');

                $scope.category = ary[0] || {
                    localizations: []
                };
                $scope.languages = ary[1];

                allureLanguage.initCategoryLanguages($scope.category, $scope.languages);
            });
            $scope.submitCategory = function() {
                if (!$scope.category) return;
                $scope.alerts.length = 0;
                var errorTips;
                _.each($scope.category.localizations, function (item) {
                    if ((!item.name || item.name === '') && item.isDefault == true) {
                        errorTips = '请不要遗漏填写' + item.title + '名字';
                    }
                });
                if (errorTips) {
                    $scope.alerts.push({
                        msg: errorTips
                    });
                    return;
                }

                var submitbtn = angular.element('#submitbtn');
                submitbtn.triggerHandler('start');

                var postUrl;
                if (!$scope.category.id) {
                    postUrl = '/api/categories';
                    $http.post(postUrl, $scope.category)
                    .success(function (data) {
                        alert('保存成功');
                        submitbtn.triggerHandler('end');
                        $('input[type="text"]').val("");
                    })
                    .error(function (msg) {
                        $scope.alerts.push({
                            msg: msg
                        });
                        submitbtn.triggerHandler('end');
                    });
                } else {
                    postUrl = '/api/categories/' + $scope.category.id;
                    $http({url: postUrl,     data:$scope.category,     method: "PATCH"} )
                    .success(function (data) {
                        alert('保存成功');
                        submitbtn.triggerHandler('end');
                    })
                    .error(function (msg) {
                        $scope.alerts.push({
                            msg: msg
                        });
                        submitbtn.triggerHandler('end');
                    });

                }

                
            }
        }
    ]);

    app.controller('CategorySubCtrl', ['$scope', '$location','$http', '$q', '$routeParams', 'allureLanguage',
        'formDataObject',
        function ($scope, $location, $http, $q, $routeParams, allureLanguage, formDataObject) {
            $scope.alerts = [];
            $scope.files = [];
            $scope.currentFile;

            if ($routeParams.id) {
                $scope.title = '修改子分类';
            } else {
                $scope.title = '创建子分类';
            }

            function getCategory() {
                var def = $q.defer();

                if ($routeParams.id) {
                    $http.get('/api/categories/' + $routeParams.id)
                        .success(function(data) {
                            def.resolve(data);
                        })
                        .error(function() {
                            def.reject('请求出错');
                        });
                } else {
                    def.resolve(null);
                }


                return def.promise;
            }

            $q.all([getCategory(), allureLanguage.getLanguageCurrent()]).then(function(ary) {
                $scope.$emit('loading:hide');
                $scope.category = ary[0];
                $scope.languages = ary[1];

                if (!$scope.category && $routeParams.id) {
                    $scope.alerts.push({
                        msg: '请求出错'
                    });
                    return;
                }
                $scope.category = $scope.category || {
                    localizations: []
                };

                allureLanguage.initCategoryLanguages($scope.category, $scope.languages);
            });

            $scope.removeImage = function() {
                $scope.files.length = 0;
            }

            $scope.submitCategoryData = function () {
                var postUrl;
                var submitbtn = angular.element('#submitbtn');
                if ($routeParams.id) {
                $scope.category.id = $routeParams.id;
                $scope.category.parentId = $routeParams.mainId;
                postUrl = '/api/categories/' + $routeParams.id;

                $http({
                    method: 'PATCH',
                    url: postUrl,
                    data: $scope.category
                })
                .success(function (data) {
                    //$scope.files.length = 0;
                    $scope.category = data;
                    //allureLanguage.initCategoryLanguages($scope.category, $scope.languages);
                    alert('保存成功');
                    submitbtn.triggerHandler('end');
                })
                .error(function (msg) {
                    $scope.alerts.push({
                        msg: msg
                    });
                    submitbtn.triggerHandler('end');
                });

            } else {
                $scope.category.parentId = $routeParams.mainId;
                postUrl = '/api/categories';

                $http({
                    method: 'POST',
                    url: postUrl,
                    data: $scope.category
                })
                .success(function (data) {
                    $scope.category = data;
                    //allureLanguage.initCategoryLanguages($scope.category, $scope.languages);
                    alert('保存成功');
                    submitbtn.triggerHandler('end');
                    
                    var url = ($location.absUrl()+'?' + Date.now());
                        url = url.replace("sub/create", "main/view");
                        window.location.href = url;
                    })
                .error(function (msg) {
                    $scope.alerts.push({
                        msg: msg
                    });
                    submitbtn.triggerHandler('end');
                });
            }
            }

            $scope.submitCategory = function () {

                $scope.alerts.length = 0;
                var errorTips;
                _.each($scope.category.localizations, function (item) {
                    if ((!item.name || item.name === '') && item.isDefault == true) {
                        errorTips = '请不要遗漏填写' + item.title + '名字';
                    }
                });
                //if (!$routeParams.id && $scope.files.length === 0) {
                //    errorTips = '请上传图片';
                //}
                
                if (errorTips) {
                    $scope.alerts.push({
                        msg: errorTips
                    });
                    return;
                }
                $scope.category.id = $scope.category.id;

                var submitbtn = angular.element('#submitbtn');
                submitbtn.triggerHandler('start');
                
                //upload file
                if ($scope.files.length > 0) {
                    $http({
                            method: 'POST',
                            url: "/api/images",
                            headers: {
                                'Content-Type': undefined
                            },
                            data: {
                                file: $scope.files[0]
                            },
                            transformRequest: formDataObject
                        })
                        .success(function(data) {
                            $scope.files.length = 0;

                            $scope.category.imageId = data.id;
                            $scope.submitCategoryData();

                        })
                        .error(function(msg) {
                            $scope.alerts.push({
                                msg: msg
                            });
                            submitbtn.triggerHandler('end');
                        });
                } else {
                    $scope.submitCategoryData();
                }


                return false;
            }
        }
    ]);

    app.controller('LocalesCtrl', ['$scope', '$http', '$q', 'allureLanguage',
    function ($scope, $http, $q, allureLanguage) {
        function getLocale() {
            var deferred = $q.defer();
            $http.get('/api/locales')
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (data) {

                });
            return deferred.promise;
        }

        $scope.getLanguageName = function (code) {
            return allureLanguage.getLanguageName($scope.languages, code);
        }

        $q.all([getLocale(), allureLanguage.getLanguageCurrent()]).then(function (ary) {
            $scope.$emit('loading:hide');
            $scope.locales = ary[0];
            $scope.languages = ary[1];
        });
    }
    ]);

    app.controller('LocaleCtrl', ['$scope', '$http', '$q', '$routeParams', 'allureLanguage',
            function ($scope, $http, $q, $routeParams, allureLanguage) {
                $scope.alerts = [];

                if ($routeParams.id) {
                    $scope.title = '修改地区';
                } else {
                    $scope.title = '创建地区';
                }

                function getLocale() {
                    var def = $q.defer();
                    if (!$routeParams.id) {
                        def.resolve();
                        return;
                    }
                    $http.get('/api/locales/' + $routeParams.id)
                        .success(function (data) {
                            def.resolve(data);
                        })
                        .error(function () {

                        });

                    return def.promise;
                }
                $q.all([getLocale(), allureLanguage.getLanguageCurrent()]).then(function (ary) {
                    $scope.$emit('loading:hide');

                    $scope.locale = ary[0] || {
                        localizations: []
                    };
                    $scope.languages = ary[1];

                    allureLanguage.initCategoryLanguages($scope.locale, $scope.languages);
                });
                $scope.submitLocale = function () {
                    if (!$scope.locale) return;
                    $scope.alerts.length = 0;
                    var errorTips;
                    _.each($scope.locale.localizations, function (item) {
                        if ((!item.name || item.name === '') && item.isDefault == true) {
                            errorTips = '请不要遗漏填写' + item.title + '名字';
                        }
                    });
                    if (errorTips) {
                        $scope.alerts.push({
                            msg: errorTips
                        });
                        return;
                    }

                    var submitbtn = angular.element('#submitbtn');
                    submitbtn.triggerHandler('start');

                    var postUrl;
                    if (!$scope.locale.id) {
                        postUrl = '/api/locales';
                        $http.post(postUrl, $scope.locale)
                        .success(function (data) {
                            alert('保存成功');
                            submitbtn.triggerHandler('end');
                            $('input[type="text"]').val("");
                        })
                        .error(function (msg) {
                            $scope.alerts.push({
                                msg: msg
                            });
                            submitbtn.triggerHandler('end');
                        });
                    } else {
                        postUrl = '/api/locales/' + $scope.locale.id;
                        
                        $http({ url: postUrl, data: $scope.locale, method: "PATCH" })
                        .success(function (data) {
                            alert('保存成功');
                            submitbtn.triggerHandler('end');
                        })
                        .error(function (msg) {
                            $scope.alerts.push({
                                msg: msg
                            });
                            submitbtn.triggerHandler('end');
                        });

                    }


                }
            }
    ]);

    app.controller('SettingCtrl', ['$scope', '$http', '$q', '$timeout', 'allureLanguage',
        function($scope, $http, $q, $timeout, allureLanguage) {
            $scope.waitColumns = [];
            $scope.hasColumns = [];
            $scope.languages = [];
            $scope.currentData;

            $scope.isLanguageSelected = true;

            $scope.dbTables = {
                locale: {
                    name: '产地',
                    language: true,

                    key: 'Id',
                    value: 'Name',
                    status: 'Status'
                },
                brand: {
                    name: '品牌',
                    key: 'Id',
                    value: 'Name',
                    status: 'Status',
                    language: false
                }
            };
            // GET /locale/list

            function getDBTable(tableName) {
                var def = $q.defer();
                $scope.waitColumns.splice(0);
                $scope.hasColumns.splice(0);

                // /locale/list

                $http.get($scope.currentTableName + '/list')
                    .success(function(data) {
                        def.resolve(data);
                    })
                    .error(function(data) {

                    });

                return def.promise;
            }

            var updateDBTable = $scope.updateFn = function(data) {
                var def = $q.defer();
                def.resolve();
                if ($scope.currentTable.language) {
                    var newData = _.find($scope.currentData, function(cData) {
                        return cData[$scope.currentTable.key] === data[$scope.currentTable.key];
                    });
                    //data = 
                }
                console.log(data);
                // $http.post($scope.currentTableName + '/update', data)
                //     .success(function() {
                //         def.resolve();
                //     })
                //     .error(function() {
                //         def.reject();
                //     });
                return def.promise;
            }

            allureLanguage.getLanguageCurrent().then(function(ary) {
                $scope.$emit('loading:hide');

                $scope.languages = $scope.languages.concat(ary);

                // languageCode

                $scope.languageCode = _.find($scope.languages, function(language) {
                    return language.IsDefault;
                }).Code;
            });

            function bindData(data) {
                // var data = $scope.currentData,
                console.log(data);
                var currentTable = $scope.currentTable;

                if ($scope.currentTable.language) {
                    data = _.map($scope.currentData, function(data) {
                        var obj = _.find(data.localizations, function (Localized) {
                            return Localized.languageCode === $scope.languageCode;
                        });
                        var ret = {};
                        try {
                            ret[$scope.currentTable.key] = data[$scope.currentTable.key];
                            ret[$scope.currentTable.value] = obj[$scope.currentTable.value];
                            ret[$scope.currentTable.status] = data[$scope.currentTable.status];
                        } catch (e) {
                            console.log(obj)
                        }
                        return ret
                    });
                }

                _.each(_.filter(data, function(item) {
                    return item[currentTable.status] === 1;
                }), function(item) {
                    console.log(item)
                    $scope.waitColumns.push(item)
                });
                _.each(_.filter(data, function(item) {
                    return item[currentTable.status] === 0;
                }), function(item) {
                    console.log(item)
                    $scope.hasColumns.push(item)
                });
            }



            $scope.$watch('currentTableName', function(newValue) {
                if (!newValue) return;

                $scope.currentTable = _.find($scope.dbTables, function(item, key) {
                    return key === newValue;
                });

                getDBTable().then(function(data) {
                    $scope.currentData = data;
                    bindData(data);
                })
            });
            $scope.$watch('languageCode', function(newValue) {


            });

            $scope.addFn = function(id, callback) {
                var table = _.find($scope.waitColumns, function(item) {
                    return item[$scope.currentTable.key] == id;
                });
                if (!table) return;

                table[$scope.currentTable.status] = 0;
                updateDBTable(table).then(callback).catch(function() {
                    callback('提交失败');
                });
            }

            $scope.removeFn = function(id, callback) {
                var table = _.find($scope.hasColumns, function(item) {
                    return item[$scope.currentTable.key] == id;
                });
                if (!table) return;

                table[$scope.currentTable.status] = 1;
                updateDBTable(table).then(callback).catch(function() {
                    callback('提交失败');
                });
            }

            $scope.createFn = function(data, callback) {

                if ($scope.currentTable.language) {

                } else {
                    createSingleFn(data, callback);
                }
                return;

                var createNewData = [],
                    addData;
                _.each(data.languages, function(language, key) {
                    var pushData = {
                        id: data.id,
                        Code: key,
                        text: data.languages[key],
                        status: 0
                    }
                    createNewData.push(pushData);
                    if (key === $scope.languageCode) {
                        addData = pushData;
                    }
                });

                $http.post('/db/' + $scope.currentTable, createNewData)
                    .success(function() {
                        callback(null, addData);
                    })
                    .error(function() {
                        callback('新增失败')
                    });
            }

            function createSingleFn(data, callback) {
                $http.post($scope.currentTableName + '/create', data)
                    .success(function(data) {
                        callback(null, data);
                    })
                    .error(function() {
                        callback('新增失败')
                    });
            }
        }
    ]);

    app.controller('LanguagesCtrl', ['$scope', '$http', '$q',
        function ($scope, $http, $q) {
            $scope.languages = [];
            function getLanguages() {
                var deferred = $q.defer();
                $http.get('/api/languages')
                    .success(function (data) {
                        deferred.resolve(data);
                        $scope.languages = data;
                    })
                    .error(function (data) {

                    });
                return deferred.promise;
            }

            $q.all([getLanguages()]).then(function (ary) {
                $scope.$emit('loading:hide');
                $scope.languages = ary[0];
            });
        }
    ]);

    app.controller('LanguageCreateCtrl', ['$scope', '$routeParams', '$http', '$q', 'allureRole',
        function ($scope, $routeParams, $http, $q, allureRole) {
            $scope.language = {};
            $scope.alerts = [];
            $scope.isCreate = false;

            function getLanguage() {
                var def = $q.defer();

                $http.get('/api/languages/' + $routeParams.id)
                    .success(function (data) {
                        def.resolve(data);
                    })
                    .error(function () {

                    });
                return def.promise;
            }

            function getLanguagepotential() {
                var def = $q.defer();

                $http.get('/api/languages/potential')
                    .success(function (data) {
                        def.resolve(data);
                    })
                    .error(function () {

                    });
                return def.promise;
            }

            // title
            if ($routeParams.id) {
                $scope.title = '修改语言';
                $q.all([getLanguage(), getLanguagepotential()]).then(function(ary) {
                    $scope.$emit('loading:hide');
                    $scope.language = ary[0];
                    $scope.languagepotential = ary[1];
                    
                });
            } else {
                $scope.title = '创建语言';
                $scope.isCreate = true;

                $q.all([getLanguagepotential()]).then(function(ary) {
                    $scope.$emit('loading:hide');
                    $scope.languagepotential = ary[0];
                });
            }

            $scope.closeAlert = function () {
                $scope.alerts.splice(0);
            };

            $scope.languagechange = function () {
                if ($scope.selectedItem == null) {
                    $scope.language.code = null;
                    $scope.language.description = null;
                } else {
                    $scope.language.code = $scope.selectedItem.code;
                    $scope.language.description = $scope.selectedItem.description;
                }
            }

            $scope.submit = function () {
                $scope.closeAlert();

                var alerts = $scope.alerts;

                if ($scope.language.description==null) {
                    alerts.push({
                        msg: '请选择语言'
                    });
                }
                if (alerts.length > 0) {
                    return;
                }

                var user = $scope.user;
                var postData = $scope.language;

                var submitbtn = angular.element('#submitbtn');
                submitbtn.triggerHandler('start');

                var postUrl;
                if ($routeParams.id) {
                    postUrl = '/api/languages/' + $routeParams.id;
                    $http({url: postUrl,     data:postData,     method: "PATCH"})
                    .success(function () {
                        alert('保存成功');
                        submitbtn.triggerHandler('end');
                        if (!$routeParams.id) {

                        }
                    })
                    .error(function (ex) {
                        $scope.alerts.length = 0;
                            $scope.alerts.push({
                                msg: ex.message
                        });
                        submitbtn.triggerHandler('end');
                    });
                } else {
                    postUrl = '/api/languages';
                    $http.post(postUrl, postData)
                    .success(function () {
                        alert('保存成功');
                        submitbtn.triggerHandler('end');
                        if (!$routeParams.id) {

                        }
                    })
                    .error(function (msg) {
                        $scope.alerts.length = 0;
                        $scope.alerts.push({
                            msg: msg
                        });
                        submitbtn.triggerHandler('end');
                    });
                }


                
                return false;
            };
        }
    ]);

    app.controller('BrandsCtrl', ['$scope', '$http', '$q',
        function ($scope, $http, $q) {
            $scope.brands = [];
            function getBrands() {
                var deferred = $q.defer();
                $http.get('/api/brands')
                    .success(function (data) {
                        deferred.resolve(data);
                        $scope.brands = data;
                    })
                    .error(function (data) {

                    });
                return deferred.promise;
            }

            $q.all([getBrands()]).then(function (ary) {
                $scope.$emit('loading:hide');
                $scope.brands = ary[0];
            });
        }
    ]);

    app.controller('BrandCreateCtrl', ['$scope', '$routeParams', '$http', '$q', 'allureRole',
        function ($scope, $routeParams, $http, $q, allureRole) {
            $scope.brand = {};
            $scope.alerts = [];
            $scope.isCreate = false;

            function getBrand() {
                var def = $q.defer();

                $http.get('/api/brands/' + $routeParams.id)
                    .success(function (data) {
                        def.resolve(data);
                    })
                    .error(function () {

                    });
                return def.promise;
            }

            // title
            if ($routeParams.id) {
                $scope.title = '修改品牌';
                $q.all([getBrand()]).then(function (ary) {
                    $scope.$emit('loading:hide');
                    $scope.brand = ary[0];

                });
            } else {
                $scope.title = '创建品牌';
                $scope.isCreate = true;

                $q.all().then(function (ary) {
                    $scope.$emit('loading:hide');
                    
                });
            }

            $scope.closeAlert = function () {
                $scope.alerts.splice(0);
            };

            $scope.branddelete = function () {

                if (confirm("您确定要删除当前数据吗？") === true) {
                    var postUrl = '/api/brands/' + $routeParams.id;
                    $http({ url: postUrl, method: "DELETE" })
                        .success(function () {
                            var url = ("/admin/#/brands" + '?' + Date.now());
                            window.location.href = url;
                        })
                        .error(function (msg) {
                            alert(msg);
                        });
                }
            }

            function save()
            {
                var user = $scope.user;
                var postData = $scope.brand;

                var submitbtn = angular.element('#submitbtn');
                submitbtn.triggerHandler('start');

                var postUrl;
                if ($routeParams.id) {
                    postUrl = '/api/brands/' + $routeParams.id;
                    $http({ url: postUrl, data: postData, method: "PATCH" })
                    .success(function () {
                        alert('保存成功');
                        submitbtn.triggerHandler('end');
                        if (!$routeParams.id) {

                        }
                    })
                    .error(function (ex) {
                        $scope.alerts.length = 0;
                        $scope.alerts.push({
                            msg: ex.message
                        });
                        submitbtn.triggerHandler('end');
                    });
                } else {
                    postUrl = '/api/brands';
                    $http.post(postUrl, postData)
                    .success(function () {
                        alert('保存成功');
                        submitbtn.triggerHandler('end');
                        if (!$routeParams.id) {

                        }
                    })
                    .error(function (msg) {
                        $scope.alerts.length = 0;
                        $scope.alerts.push({
                            msg: msg
                        });
                        submitbtn.triggerHandler('end');
                    });
                }
            }

            $scope.submit = function () {
                $scope.closeAlert();

                var alerts = $scope.alerts;

                if ($scope.brand.name == null) {
                    alerts.push({
                        msg: '请输入品牌名字'
                    });
                }
                if (alerts.length > 0) {
                    return;
                }

                var user = $scope.user;
                var postData = $scope.brand.name;

                $http.get('/api/brands/name-' + postData)
                    .success(function (data) {
                        if (data == 'null') {
                            save();
                            return;
                        }
                        else {
                            if ($routeParams.id) {
                                if (data.id == $routeParams.id) {
                                    save();
                                }
                                else {
                                    alerts.push({
                                        msg: '品牌名字不能重复'
                                    });
                                }
                            }
                            else {
                                alerts.push({
                                    msg: '品牌名字不能重复'
                                });
                            }
                        }
                    })
                    .error(function () {

                    });            



                return false;
            };
        }
    ]);

    app.controller('CheckAddressesCtrl', ['$scope', '$routeParams', '$http', '$q', 'allureRole',
        function ($scope, $routeParams, $http, $q, allureRole) {
            $scope.checkAddress = {};
            $scope.alerts = [];
            $scope.isCreate = false;

            function getCheckAddress() {
                var def = $q.defer();

                $http.get('/api/checkaddresses/' + $routeParams.id)
                    .success(function (data) {
                        def.resolve(data);
                    })
                    .error(function () {

                    });
                return def.promise;
            }

            // title
            if ($routeParams.id) {
                $scope.title = '修改验货地址';
                $q.all([getCheckAddress()]).then(function (ary) {
                    $scope.$emit('loading:hide');
                    $scope.checkAddress = ary[0];

                });
            } else {
                $scope.title = '创建验货地址';
                $scope.isCreate = true;

                $q.all().then(function (ary) {
                    $scope.$emit('loading:hide');
                    
                });
            }

            $scope.closeAlert = function () {
                $scope.alerts.splice(0);
            };

            
            function save()
            {
                var user = $scope.user;
                var postData = $scope.checkAddress;

                var submitbtn = angular.element('#submitbtn');
                submitbtn.triggerHandler('start');

                var postUrl;
                if ($routeParams.id) {
                    postUrl = '/api/checkaddresses/' + $routeParams.id;
                    $http({ url: postUrl, data: postData, method: "PATCH" })
                    .success(function () {
                        alert('保存成功');
                        submitbtn.triggerHandler('end');
                        if (!$routeParams.id) {

                        }
                    })
                    .error(function (ex) {
                        $scope.alerts.length = 0;
                        $scope.alerts.push({
                            msg: ex.message
                        });
                        submitbtn.triggerHandler('end');
                    });
                } else {
                    postUrl = '/api/checkAddresss';
                    $http.post(postUrl, postData)
                    .success(function () {
                        alert('保存成功');
                        submitbtn.triggerHandler('end');
                        if (!$routeParams.id) {

                        }
                    })
                    .error(function (msg) {
                        $scope.alerts.length = 0;
                        $scope.alerts.push({
                            msg: msg
                        });
                        submitbtn.triggerHandler('end');
                    });
                }
            }

            $scope.submit = function () {
                $scope.closeAlert();

                var alerts = $scope.alerts;

                if ($scope.checkAddress.address == null) {
                    alerts.push({
                        msg: '请输入验货地址'
                    });
                }
                if (alerts.length > 0) {
                    return;
                }

                var user = $scope.user;
                var postData = $scope.checkAddress.address;

                save();
                             
                return false;
            };
        }
    ]);

    app.controller('WarehousesCtrl', ['$scope', '$http', '$q',
        function ($scope, $http, $q) {
            $scope.warehouses = [];
            function getWarehouses() {
                var deferred = $q.defer();
                $http.get('/api/warehouses')
                    .success(function (data) {
                        deferred.resolve(data);
                        $scope.warehouses = data;
                    })
                    .error(function (data) {

                    });
                return deferred.promise;
            }

            $q.all([getWarehouses()]).then(function (ary) {
                $scope.$emit('loading:hide');
                $scope.warehouses = ary[0];
            });
        }
    ]);

    app.controller('WarehouseCreateCtrl', ['$scope', '$routeParams', '$http', '$q', 'allureRole',
        function ($scope, $routeParams, $http, $q, allureRole) {
            $scope.warehouse = {};
            $scope.alerts = [];
            $scope.isCreate = false;

            function getWarehouse() {
                var def = $q.defer();

                $http.get('/api/warehouses/' + $routeParams.id)
                    .success(function (data) {
                        def.resolve(data);
                    })
                    .error(function () {

                    });
                return def.promise;
            }

            // title
            if ($routeParams.id) {
                $scope.title = '修改仓库';
                $q.all([getWarehouse()]).then(function (ary) {
                    $scope.$emit('loading:hide');
                    $scope.warehouse = ary[0];

                });
            } else {
                $scope.title = '创建仓库';
                $scope.isCreate = true;

                $q.all().then(function (ary) {
                    $scope.$emit('loading:hide');

                });
            }

            $scope.closeAlert = function () {
                $scope.alerts.splice(0);
            };

            $scope.warehousedelete = function () {

                if (confirm("您确定要删除当前数据吗？") === true) {
                    var postUrl = '/api/warehouses/' + $routeParams.id;
                    $http({ url: postUrl, method: "DELETE" })
                        .success(function () {
                            var url = ("/admin/#/warehouses" + '?' + Date.now());
                            window.location.href = url;
                        })
                        .error(function (msg) {
                            alert(msg);
                        });
                }
            }

            $scope.submit = function () {
                $scope.closeAlert();

                var alerts = $scope.alerts;

                //if ($scope.warehouse.code == null) {
                //    alerts.push({
                //        msg: '请输入编码'
                //    });
                //}
                if ($scope.warehouse.name == null) {
                    alerts.push({
                        msg: '请输入名字'
                    });
                }
                if (alerts.length > 0) {
                    return;
                }

                var user = $scope.user;
                var postData = $scope.warehouse;

                var submitbtn = angular.element('#submitbtn');
                submitbtn.triggerHandler('start');

                var postUrl;
                if ($routeParams.id) {
                    postUrl = '/api/warehouses/' + $routeParams.id;
                    $http({ url: postUrl, data: postData, method: "PATCH" })
                    .success(function () {
                        alert('保存成功');
                        submitbtn.triggerHandler('end');
                        if (!$routeParams.id) {

                        }
                    })
                    .error(function (ex) {
                        $scope.alerts.length = 0;
                        $scope.alerts.push({
                            msg: ex.message
                        });
                        submitbtn.triggerHandler('end');
                    });
                } else {
                    postUrl = '/api/warehouses';
                    $http.post(postUrl, postData)
                    .success(function () {
                        alert('保存成功');
                        submitbtn.triggerHandler('end');
                        if (!$routeParams.id) {
                            $('input[type="text"]').val("");
                        }
                    })
                    .error(function (msg) {
                        $scope.alerts.length = 0;
                        $scope.alerts.push({
                            msg: msg
                        });
                        submitbtn.triggerHandler('end');
                    });
                }



                return false;
            };
        }
    ]);

    app.controller('LogisticsCtrl', ['$scope', '$http', '$q',
    function ($scope, $http, $q) {
        $scope.logisticses = [];
        function getLogisticses() {
            var deferred = $q.defer();
            $http.get('/api/logistics')
                .success(function (data) {
                    deferred.resolve(data);
                    $scope.logisticses = data;
                })
                .error(function (data) {

                });
            return deferred.promise;
        }

        $q.all([getLogisticses()]).then(function (ary) {
            $scope.$emit('loading:hide');
            $scope.logisticses = ary[0];
        });
    }
    ]);

    app.controller('LogisticCreateCtrl', ['$scope', '$routeParams', '$http', '$q', 'allureRole',
        function ($scope, $routeParams, $http, $q, allureRole) {
            $scope.logistics = {};
            $scope.alerts = [];
            $scope.isCreate = false;

            function getLogistics() {
                var def = $q.defer();

                $http.get('/api/logistics/' + $routeParams.id)
                    .success(function (data) {
                        def.resolve(data);
                    })
                    .error(function () {

                    });
                return def.promise;
            }

            // title
            if ($routeParams.id) {
                $scope.title = '修改物流';
                $q.all([getLogistics()]).then(function (ary) {
                    $scope.$emit('loading:hide');
                    $scope.logistics = ary[0];

                });
            } else {
                $scope.title = '创建物流';
                $scope.isCreate = true;

                $q.all().then(function (ary) {
                    $scope.$emit('loading:hide');

                });
            }

            $scope.closeAlert = function () {
                $scope.alerts.splice(0);
            };

            $scope.logisticsdelete = function () {

                if (confirm("您确定要删除当前数据吗？") === true) {
                    var postUrl = '/api/logistics/' + $routeParams.id;
                    $http({ url: postUrl, method: "DELETE" })
                        .success(function () {
                            var url = ("/admin/#/logistics" + '?' + Date.now());
                            window.location.href = url;
                        })
                        .error(function (msg) {
                            alert(msg);
                        });
                }
            }

            $scope.submit = function () {
                $scope.closeAlert();

                var alerts = $scope.alerts;
                //if ($scope.logistics.code == null) {
                //    alerts.push({
                //        msg: '请输入编码'
                //    });
                //}
                if ($scope.logistics.name == null) {
                    alerts.push({
                        msg: '请输入名字'
                    });
                }
                if (alerts.length > 0) {
                    return;
                }

                var user = $scope.user;
                var postData = $scope.logistics;

                var submitbtn = angular.element('#submitbtn');
                submitbtn.triggerHandler('start');

                var postUrl;
                if ($routeParams.id) {
                    postUrl = '/api/logistics/' + $routeParams.id;
                    $http({ url: postUrl, data: postData, method: "PATCH" })
                    .success(function () {
                        alert('保存成功');
                        submitbtn.triggerHandler('end');
                        if (!$routeParams.id) {

                        }
                    })
                    .error(function (ex) {
                        $scope.alerts.length = 0;
                        $scope.alerts.push({
                            msg: ex.message
                        });
                        submitbtn.triggerHandler('end');
                    });
                } else {
                    postUrl = '/api/logistics';
                    $http.post(postUrl, postData)
                    .success(function () {
                        alert('保存成功');
                        submitbtn.triggerHandler('end');
                        if (!$routeParams.id) {
                            $('input[type="text"]').val("");
                        }
                    })
                    .error(function (msg) {
                        $scope.alerts.length = 0;
                        $scope.alerts.push({
                            msg: msg
                        });
                        submitbtn.triggerHandler('end');
                    });
                }



                return false;
            };
        }
    ]);
    app.directive('loadingspinner', ['$rootScope',
        function($rootScope) {
            return {
                restrict: 'A',
                link: function(scope, elem, attrs) {
                    var parentEl = elem.parent();
                    parentEl.css('position', 'relative');

                    scope.$on('$routeChangeStart', function(evt, next, current) {
                        elem.css('display', 'block');
                    });
                    scope.$on('loading:hide', function() {
                        elem.css('display', 'none');
                    })
                }
            }
        }
    ]);

    function getLanguageTitle(language) {
        return language.substring(0, language.indexOf('('))
    }

    app.init();

    return app;
});