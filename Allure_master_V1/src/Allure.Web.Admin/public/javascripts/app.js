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
    '/public/javascripts/components/add_db_column.js',
    '/public/javascripts/components/category_delete.js',
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

    var STATUS = [{
        value: 0,
        text: '有效'
    }, {
        value: 1,
        text: '无效'
    }];

    function getStatus(value) {
        var r = _.find(STATUS, function(status) {
            return status.value == value;
        });
        if (r) return r.text;
    }

    var GENDER = [{
        value: 1,
        text: '女士'
    }, {
        value: 0,
        text: '先生'
    }];

    function getGender(value) {
        var r = _.find(GENDER, function(gender) {
            return gender.value == value;
        });

        if (r) return r.text;
    }

    var app = angular.module('allure', [
        'd-enter', 'c-selectimage', 'c-description', 'c-userrole', 'c-vedioupload', 'c-addColumn', 'd-submitbtn',
        'd-imgholder', 'c-imageupload', 'c-fileupload', 'c-datepicker', 'c-collapsed', 'c-updateInputImage',
        'c-alerts', 'c-categoryDelete', 'c-orderDelete', 'allureRoleServices', 'allureBrandServices',
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
                    templateUrl: '/public/partials/home.html?' + Date.now(),
                    controller: 'HomeCtrl'
                })
                .when('/categories', {
                    templateUrl: '/public/partials/product/categories.html?' + Date.now(),
                    controller: 'CategoriesCtrl'
                })
                .when('/category/main/create', {
                    templateUrl: '/public/partials/product/category-main.html?' + Date.now(),
                    controller: 'CategoryMainCtrl'
                })
                .when('/category/main/edit/:id', {
                    templateUrl: '/public/partials/product/category-main.html?' + Date.now(),
                    controller: 'CategoryMainCtrl'
                })
                .when('/category/sub/create/:mainId', {
                    templateUrl: '/public/partials/product/category-sub.html?' + Date.now(),
                    controller: 'CategorySubCtrl'
                })
                .when('/category/sub/edit/:mainId/:id', {
                    templateUrl: '/public/partials/product/category-sub.html?' + Date.now(),
                    controller: 'CategorySubCtrl'
                })
                .when('/user', {
                    templateUrl: '/public/partials/user/company.html?v=' + Date.now(),
                    controller: 'UserCompanyCtrl'
                })
                .when('/user/create', {
                    templateUrl: '/public/partials/user/create.html?' + Date.now(),
                    controller: 'UserCreateCtrl'
                })
                .when('/user/:id', {
                    templateUrl: '/public/partials/user/create.html?' + Date.now(),
                    controller: 'UserCreateCtrl'
                })
                .when('/products', {
                    templateUrl: '/public/partials/product/list.html?' + Date.now(),
                    controller: 'ProductionListCtrl'
                })
                .when('/product/create', {
                    templateUrl: '/public/partials/product/detail.html?' + Date.now(),
                    controller: 'ProductionDetailCtrl'
                })
                .when('/product/:id', {
                    templateUrl: '/public/partials/product/detail.html?' + Date.now(),
                    controller: 'ProductionDetailCtrl'
                })
                .when('/warehouse/list', {
                    templateUrl: '/public/partials/warehouse/list.html?' + Date.now(),
                    controller: 'WarehouseListCtrl'
                })
                .when('/warehouse/stores', {
                    templateUrl: '/public/partials/warehouse/stores.html?' + Date.now(),
                    controller: 'WarehouseStoresCtrl'
                })
                .when('/warehouse/create', {
                    templateUrl: '/public/partials/warehouse/create.html?' + Date.now(),
                    controller: 'WarehouseCreateCtrl'
                })
                .when('/orders', {
                    templateUrl: '/public/partials/order/list.html?' + Date.now(),
                    controller: 'OrdersCtrl'
                })
                .when('/order/:id', {
                    templateUrl: '/public/partials/order/detail.html?' + Date.now(),
                    controller: 'OrderDetailCtrl'
                })
                .when('/setting', {
                    templateUrl: '/public/partials/setting.html?' + Date.now(),
                    controller: 'SettingCtrl'
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
            $scope.size = PageSize;

            $scope.roleMap = function(ary) {
                return allureRole.map(ary)
            };


            $scope.getStatus = getStatus;
            $scope.getGender = getGender;

            $scope.STATUS = STATUS;

            $scope.GENDER = GENDER;

            $scope.search = {
                LastName: '',
                Email: '',
                Gender: '',
                Mobile: '',
                Telephone: '',
                Company: '',
                Roles: '',
                Status: '',
                PageSize: $scope.size,
                PageNumber: $scope.currentPage
            }

            $scope.$watch('user.roles', function(value) {
                $scope.search.roles = value;
            });

            function getUsers() {
                var defer = $q.defer();

                $http.post('/user/Search', $scope.search)
                    .success(function(data) {
                        defer.resolve(data)
                    })
                    .error(function() {

                    });


                return defer.promise;
            }


            $scope.searchFn = function() {
                getUsers().then(function(data) {
                    $scope.$emit('loading:hide');
                    $scope.totalCount = data.Count;
                    $scope.users = data.Users;
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

                $http.get('/user/id/' + $routeParams.id)
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

                    var roles = allureRole.parse(user.Roles);

                    $scope.user = {
                        email: user.Email,
                        firstname: user.FirstName,
                        lastname: user.LastName,
                        gender: user.Gender,
                        company: user.Company,
                        mobilephone: user.Mobile,
                        phone: user.Telephone,
                        roles: user.Roles,
                        status: user.Status,
                        addresses: user.Deliveries
                    };

                    $scope.roleNames = allureRole.map(user.Roles);
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
                $scope.$emit('loading:hide');
            }



            $scope.EMAIL_REGEXP = /^[a-z0-9!#$%&'*+/=?^_`{|}~.-]+@[a-z0-9-]+(\.[a-z0-9-]+)*$/;

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
                    postUrl = '/user/update';
                    postData.UserId = $routeParams.id;
                } else {
                    postUrl = '/user/create';
                }

                var submitbtn = angular.element('#submitbtn');
                submitbtn.triggerHandler('start');


                $http.post(postUrl, postData)
                    .success(function() {
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
                    .error(function() {
                        $scope.alerts.length = 0;
                        $scope.alerts.push({
                            msg: '服务器出错'
                        });
                        submitbtn.triggerHandler('end');
                    });
                return false;
            };
        }
    ]);

    app.controller('ProductionListCtrl', ['$scope', '$http', '$q', 'Locale', 'Brand',
        function($scope, $http, $q, Locale, Brand) {

            $scope.totalCount = 0;
            $scope.currentPage = 1;
            $scope.size = PageSize;

            $scope.search = {
                BrandId: '',
                Number: '',
                Locale: '',
                Name: '',
                MaxPrice: '',
                MinPrice: '',
                OrderBy: 'Id DESC',
                PageSize: $scope.size,
                PageNumber: $scope.currentPage
            }

            function getList() {
                var defer = $q.defer();

                $http.post('/Product/Search', $scope.search)
                    .success(function(data) {
                        defer.resolve(data)
                    })
                    .error(function() {

                    });


                return defer.promise;
            }

            function searchFn() {
                var def = $q.defer();
                getList().then(function(data) {
                    $scope.$emit('loading:hide');
                    $scope.totalCount = data.Count;
                    $scope.products = data.Products;

                    def.resolve();
                });

                return def.promise;
            }

            $q.all([searchFn(), Locale.query(), Brand.query()]).then(function(ary) {
                $scope.$emit('loading:hide');
                var data = ary[0];
                var locales = ary[1];
                $scope.brands = ary[2];
                $scope.locales = [];

                _.each(locales, function(item) {
                    var language = unit.getChineseOrDefault(item.Localized);
                    if (language) {
                        language.Id = item.Id;
                        $scope.locales.push(language);
                    }
                });
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
    app.controller('ProductionDetailCtrl', ['$scope', '$document', '$timeout', '$routeParams', '$http', '$q',
        'Production', 'Catergory', 'Locale', 'Brand', 'formDataObject', 'allureLanguage',
        function($scope, $document, $timeout, $routeParams, $http, $q, Production, Catergory, Locale, Brand, formDataObject, allureLanguage) {

            function getInitData() {
                return {
                    State: 0,
                    DisplayOrder: 0,
                    Images: [],
                    Localized: [],
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

            if ($routeParams.id) {
                $scope.title = '修改产品信息';
            } else {
                $scope.title = '创建产品';
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
                        var language = unit.getChineseOrDefault(item.Localized);
                        if (language) {
                            language.Id = item.Id;
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
                    $http.get('/product/id/' + $routeParams.id)
                        .success(function(data) {
                            data.SubCategoryId = data.SubCategory.Id;
                            data.largeCatergory = data.SubCategory.ParentId;
                            data.LocaleId = data.Locale.Id;
                            data.BrandId = data.Brand.Id;
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

                allureLanguage.initCategoryLanguages($scope.product, languages);

                refreshImages();
            });

            function refreshImages() {
                $timeout(function() {
                    $('.featuredimagezoomerhidden').remove();
                    $('.zoomtracker').remove();
                    featuredimagezoomer.loadinggif = '/public/test/images/spinningred.gif';
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
                        return item.ParentId == $scope.product.largeCatergory;
                    });
                }
            });

            $scope.closeAlert = function() {
                $scope.alerts.splice(0);
            };

            $scope.$watch('product.End', function(v) {
                console.log(v)
            });

            $scope.save = function() {
                $scope.closeAlert();

                var product = $scope.product,
                    alerts = $scope.alerts;

                if (isNaN(product.SubCategoryId)) {
                    alerts.push({
                        msg: '请选择小分类'
                    });
                }
                if (isNaN(product.BrandId)) {
                    alerts.push({
                        msg: '请选择品牌'
                    });
                }
                if (!product.Number) {
                    alerts.push({
                        msg: '请填写货号'
                    });
                }
                if (!product.Name) {
                    alerts.push({
                        msg: '请填写品名'
                    });
                }
                if (isNaN(product.Price)) {
                    alerts.push({
                        msg: '请正确填写价格'
                    });
                }
                if (isNaN(product.DisplayOrder)) {
                    alerts.push({
                        msg: '请正确填写排序位'
                    });
                }
                if (!product.Start || !product.End) {
                    alerts.push({
                        msg: '请正确填写时间段'
                    });
                }
                if (isNaN(product.LocaleId)) {
                    alerts.push({
                        msg: '请选择产地'
                    });
                }

                var r = _.find(product.Localized, function(item) {
                    return !item.Descrption;
                });
                if (r) {
                    alerts.push({
                        msg: '请填写描述'
                    });
                }

                if (product.Images.length === 0 && $scope.files.length === 0) {
                    alerts.push({
                        msg: '请上传图片'
                    });
                }

                if (alerts.length > 0) {
                    return;
                }

                var postUrl;
                if ($routeParams.id) {
                    postUrl = '/Product/update';
                } else {
                    postUrl = '/Product/create';
                }

                var postData = {};
                postData.product = JSON.stringify($scope.product);

                _.each($scope.files, function(file, i) {
                    postData['file' + i] = file;
                });

                var submitbtn = angular.element('#submitbtn');
                submitbtn.triggerHandler('start');
                $http({
                        method: 'POST',
                        url: postUrl,
                        headers: {
                            'Content-Type': undefined
                        },
                        data: postData,
                        transformRequest: formDataObject
                    })
                    .success(function(data) {
                        alert('保存成功');
                        submitbtn.triggerHandler('end');
                        if (!$routeParams.id) {
                            $scope.product = getInitData();
                            $scope.files = [];
                            allureLanguage.initCategoryLanguages($scope.product, languages);
                        }
                    })
                    .error(function() {
                        $scope.alerts.push({
                            msg: '请求出错'
                        });
                        submitbtn.triggerHandler('end');
                    });
            };


            $scope.removeImage = function(index) {
                $scope.files.splice(index, 1);
            }

            $scope.removeExistImage = function(index) {
                $scope.product.Images.splice(index, 1);
            }

            $scope.$on('$routeChangeStart', function(evt, next, current) {
                $('.featuredimagezoomerhidden').remove();
                $('.zoomtracker').remove();
            });
        }
    ]);
    app.controller('WarehouseCreateCtrl', ['$scope', '$timeout', '$q', 'WarehouseCode', 'Logistics', 'WarehouseProduct', 'WarehouseOrder',
        function($scope, $timeout, $q, WarehouseCode, Logistics, WarehouseProduct, WarehouseOrder) {
            $scope.$emit('loading:hide');

            // TODO...
            var currentUser = {
                name: 'Miser'
            };
            $scope.baseInfo = {
                operateName: currentUser.name
            };

            $q.app([WarehouseCode.query()]).then(function(ary) {
                console.log(ary[0])
            });

            var timerHandle, queryTimerHandle;

            function getTime() {
                var n = new Date;
                return n.getFullYear() + '-' + (n.getMonth() + 1) + '-' + n.getDate() + ' ' + n.getHours() + ':' + n.getMinutes();
            }

            function updateTime() {
                timerHandle = $timeout(function() {
                    $scope.baseInfo.time = getTime();
                    updateTime();
                }, 1000 * 60)
            }

            function setCurrentTime() {
                $scope.baseInfo.time = getTime();
                updateTime();
            }

            $scope.warehouses = [];

            setCurrentTime();

            WarehouseCode.query(function(res) {
                $scope.warehouseCodes = res;
            });

            Logistics.query(function(res) {
                $scope.logisticses = res;
            });


            $scope.addNewItem = function() {
                var result = _.find($scope.warehouses, function(item) {
                    return item.isNew == true;
                });
                if (result) {
                    return;
                }
                $scope.newWarehouse = {
                    isNew: true
                }
                $scope.warehouses.push($scope.newWarehouse);
            }
            $scope.removeItem = function(index) {
                if (index) {
                    $scope.warehouses.splice(index, 1);
                } else {
                    $scope.warehouses.splice(0);
                }
            }

            $scope.queryOrderNumber = function() {
                if (queryTimerHandle) {
                    $timeout.cancel(queryTimerHandle);
                    queryTimerHandle = null;
                }
                queryTimerHandle = $timeout(function() {
                    if ($scope.baseInfo.operate == 0) {
                        var orderNumber = $scope.baseInfo.orderNumber;
                        WarehouseOrder.get({
                            id: orderNumber
                        }, function(res) {
                            delete res.operate;
                            _.each(res, function(item, key) {
                                $scope.baseInfo[key] = item;
                            })
                        });
                    }
                }, 100);
            }
            $scope.queryNumber = function() {
                var result = _.find($scope.warehouses, function(item) {
                    return item.isNew == true;
                });

                // TODO...
                if (result && result.number) {
                    WarehouseProduct.get({
                        id: result.number
                    }, function(res) {
                        _.each(res, function(item, key) {
                            result[key] = item;
                        });
                        result.useCount = 0;
                        result.isNew = false;
                    })
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

            $scope.$watch('newWarehouse.useCount', function(newValue, oldValue) {

                if (!$scope.newWarehouse) return;

                var result = mustNumber(newValue, oldValue);
                if (result) {
                    $scope.newWarehouse.useCount = result;
                } else {
                    $scope.newWarehouse.useCount = 0;
                }

                if ($scope.newWarehouse.count - $scope.newWarehouse.useCount < 0) {
                    $scope.newWarehouse.useCount = $scope.newWarehouse.count;
                }
            });

            $scope.$watch('baseInfo.orderNumber', function(newValue, oldValue) {
                $scope.baseInfo.orderNumber = mustNumber(newValue, oldValue);
            });

            $scope.$watch('baseInfo.operate', function(newValue, oldValue) {
                if ($scope.baseInfo.operate == undefined) return;

                _.each(['orderNumber', 'systemNumber', 'number', 'warehouseCode', 'operateName', 'logistics', 'logisticsNumber', 'logisticsCost', 'remark', 'time'], function(name) {
                    delete $scope.baseInfo[name];
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
        }
    ]);
    app.controller('WarehouseListCtrl', ['$scope', 'WarehouseCode', 'Logistics',
        function($scope, WarehouseCode, Logistics) {
            $scope.$emit('loading:hide');

            WarehouseCode.query(function(res) {
                $scope.warehouseCodes = res;
            });

            Logistics.query(function(res) {
                $scope.logisticses = res;
            });
        }
    ]);
    app.controller('WarehouseStoresCtrl', ['$scope', 'WarehouseCode', 'Logistics',
        function($scope, WarehouseCode, Logistics) {
            $scope.$emit('loading:hide');

            // WarehouseCode.query(function(res) {
            //     $scope.warehouseCodes = res;
            // });

            // Logistics.query(function(res) {
            //     $scope.logisticses = res;
            // });
        }
    ]);
    app.controller('OrdersCtrl', ['$scope', '$q', '$http', 'Order',
        function($scope, $q, $http, Order) {
            $scope.open = function() {}
            $scope.$emit('loading:hide');
            $scope.totalCount = 0;
            $scope.currentPage = 1;
            $scope.size = PageSize;

            $scope.search = {
                Id: null,
                OrderStatus: null,
                MinTotalPrice: null,
                MaxTotalPrice: null,
                MinCreateTime: null,
                MaxCreateTime: null,
                MinUpdateTime: null,
                MaxUpdateTime: null,
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
                $http.post('/order/search', $scope.search).then(function(data){
                    if(data.data.Orders.length){
                        $scope.list = data.data.Orders;
                        $scope.totalCount = data.data.Count;
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
            $scope.pageChanged = function() {
                $scope.search.PageNumber = $scope.currentPage;
                $scope.searchFn();
            };
        }
    ]);
    app.controller('OrderDetailCtrl', ['$scope', '$http', '$location', 'OrderStatus',
        function($scope, $http, $location, OrderStatus) {
            $scope.$emit('loading:hide');

            $scope.isCollapsed1 = false;
            $scope.isCollapsed2 = false;
            $scope.isCollapsed3 = false;

            /*OrderStatus.get(function(res) {
                $scope.OrderStatus = res;
            });*/
            $scope.OrderStatus = [0,1];
            var hash = $location.$$path.split('/');
            var orderId = hash[hash.length - 1];
            $scope.UpdateOrder = {
                Id: null,
                OrderStatus: null,
                WillCheck: null,
                CheckAddress: null,
                CheckTime: null,
                CheckContact: null,
                LogisticCode: null,
                LogisticOrderNumber: null,
                Discount: null,
                Deposit: null,
                DepositReceipt: null,
                Remaining: null,
                RemaingReceipt: null,
                Details: []
            };
            var updateOrderValue = function(){
                $scope.UpdateOrder.Id = $scope.OrderInfo.Id;
                $scope.UpdateOrder.WillCheck = $scope.OrderInfo.WillCheck;
                $scope.UpdateOrder.CheckAddress = $scope.OrderInfo.CheckAddress;
                $scope.UpdateOrder.CheckTime = $scope.OrderInfo.CheckTime;
                $scope.UpdateOrder.CheckContact = $scope.OrderInfo.CheckContact;
                $scope.UpdateOrder.ReceiverName = $scope.OrderInfo.ReceiverName;
                $scope.UpdateOrder.ReceiverContact = $scope.OrderInfo.ReceiverContact;
                $scope.UpdateOrder.ReceiverAddress = $scope.OrderInfo.ReceiverAddress;
                $scope.UpdateOrder.ReceiverPostCode = $scope.OrderInfo.ReceiverPostCode;
                $scope.UpdateOrder.LogisticCode = $scope.OrderInfo.LogisticCode;
                $scope.UpdateOrder.LogisticOrderNumber = $scope.OrderInfo.LogisticOrderNumber;
                $scope.UpdateOrder.Discount = $scope.OrderInfo.Discount;
                $scope.UpdateOrder.Deposit = $scope.OrderInfo.Deposit;
                $scope.UpdateOrder.DepositReceipt = $scope.OrderInfo.DepositReceipt;
                $scope.UpdateOrder.Remaining = $scope.OrderInfo.Remaining;
                $scope.UpdateOrder.RemaingReceipt = $scope.OrderInfo.RemaingReceipt;
                angular.forEach($scope.OrderInfo.Details, function(item, key){
                    $scope.UpdateOrder.Details = $scope.UpdateOrder.Details || [];
                    $scope.UpdateOrder.Details.Id = item.Id;
                    $scope.UpdateOrder.Details.ProductIdId = item.ProductId;
                    $scope.UpdateOrder.Details.Price = item.Product.Price;
                    $scope.UpdateOrder.Details.Count = item.Count;
                });

            };
            $scope.amount = function(count, price){
                return count * price;
            };
            $scope.totalAmountMoney = function(){
                var details = $scope.OrderInfo.Details;
                var totalMoney = 0;
                angular.forEach(details, function(detail, index){
                    totalMoney = totalMoney + detail.Product.Price * detail.Count;
                });
                return totalMoney;
            };
            $scope.totalDiscount = function(){
                var details = $scope.OrderInfo.Details;
                var totalDiscount = 0;
                angular.forEach(details, function(detail, index){
                    totalDiscount = totalDiscount + detail.Discount * detail.Count;
                });
                return totalDiscount;
            };
            $scope.totalPay = function(){
                return $scope.totalAmountMoney() - $scope.totalDiscount();
            };
            $scope.orderUpdate = function(){
                $http.post('order/update', $scope.UpdateOrder).then(function(data){
                    if(data.data){
                        alert('更新订单成功');
                    }
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

            $http.get('/order/id/' + orderId).then(function(data){
                if(data.data){
                    $scope.OrderInfo = data.data;
                    updateOrderValue();
                }
            });
        }
    ]);

    app.controller('CategoriesCtrl', ['$scope', '$http', '$q', 'allureLanguage',
        function($scope, $http, $q, allureLanguage) {
            function getCategory() {
                var deferred = $q.defer();
                $http.get('/Category/list')
                    .success(function(data) {
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

    app.controller('CategoryMainCtrl', ['$scope', '$http', '$q', '$routeParams', 'allureLanguage',
        function($scope, $http, $q, $routeParams, allureLanguage) {
            $scope.alerts = [];

            function getCategory() {
                var def = $q.defer();
                if (!$routeParams.id) {
                    def.resolve();
                    return;
                }
                $http.get('/Category/Id/' + $routeParams.id)
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
                    Localized: []
                };
                $scope.languages = ary[1];

                allureLanguage.initCategoryLanguages($scope.category, $scope.languages);
            });
            $scope.submitCategory = function() {
                if (!$scope.category) return;

                $scope.alerts.length = 0;
                var errorTips;
                _.each($scope.category.Localized, function(item) {
                    if (!item.Description) {
                        errorTips = '请不要遗漏填写语言描述';
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
                if ($scope.category.Id) {
                    postUrl = '/Category/update';
                } else {
                    postUrl = '/Category/create';
                }

                $http.post(postUrl, $scope.category)
                    .success(function(data) {
                        alert('保存成功');
                        submitbtn.triggerHandler('end');
                    })
                    .error(function(msg) {
                        $scope.alerts.push({
                            msg: '请求出错'
                        });
                        submitbtn.triggerHandler('end');
                    });
            }
        }
    ]);

    app.controller('CategorySubCtrl', ['$scope', '$http', '$q', '$routeParams', 'allureLanguage',
        'formDataObject',
        function($scope, $http, $q, $routeParams, allureLanguage, formDataObject) {
            $scope.alerts = [];
            $scope.files = [];
            $scope.currentFile;

            function getCategory() {
                var def = $q.defer();

                if ($routeParams.id) {
                    $http.get('/Category/SubId/' + $routeParams.id)
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
                    Localized: []
                };

                allureLanguage.initCategoryLanguages($scope.category, $scope.languages);
            });

            $scope.removeImage = function() {
                $scope.files.length = 0;
            }
            $scope.submitCategory = function() {
                $scope.alerts.length = 0;
                var errorTips;
                _.each($scope.category.Localized, function(item) {
                    if (!item.Description) {
                        errorTips = '请不要遗漏填写语言描述';
                    }
                });
                if (!$routeParams.id && $scope.files.length === 0) {
                    errorTips = '请上传图片';
                }

                if (errorTips) {
                    $scope.alerts.push({
                        msg: errorTips
                    });
                    return;
                }
                $scope.category.CategoryId = $scope.category.Id;

                var postUrl;
                if ($routeParams.id) {
                    $scope.category.SubCategoryId = $routeParams.id;
                    $scope.category.ParentId = $routeParams.mainId;
                    postUrl = '/Category/UpdateSub';
                } else {
                    $scope.category.ParentId = $routeParams.mainId;
                    postUrl = '/Category/CreateSub';
                }

                var submitbtn = angular.element('#submitbtn');
                submitbtn.triggerHandler('start');

                $http({
                        method: 'POST',
                        url: postUrl,
                        headers: {
                            'Content-Type': undefined
                        },
                        data: {
                            file: $scope.files[0],
                            subcategory: JSON.stringify($scope.category)
                        },
                        transformRequest: formDataObject
                    })
                    .success(function(data) {
                        $scope.files.length = 0;
                        $scope.category = data;
                        allureLanguage.initCategoryLanguages($scope.category, $scope.languages);
                        alert('保存成功');
                        submitbtn.triggerHandler('end');
                    })
                    .error(function() {
                        $scope.alerts.push({
                            msg: '请求出错'
                        });
                        submitbtn.triggerHandler('end');
                    })

                return false;
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
                console.log(data)
                var currentTable = $scope.currentTable;

                if ($scope.currentTable.language) {
                    data = _.map($scope.currentData, function(data) {
                        var obj = _.find(data.Localized, function(Localized) {
                            return Localized.LanguageCode === $scope.languageCode;
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