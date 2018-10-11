require.config({
	urlArgs: Date.now(),
	baseUrl: '/public/',
	waitSeconds: 0,
	paths: {
		text: '/public/lib/requirejs-plugins/lib/text',
		json: '/public/lib/requirejs-plugins/src/json',
		jquery: '/public/lib/jquery/dist/jquery.min',
		angular: '/public/lib/angular/angular',
		lodash: '/public/lib/lodash/dist/lodash.min',
		bootstrap: '/public/lib/bootstrap/dist/js/bootstrap.min',
		holder: '/public/lib/holderjs/holder',
		multizoom: '/public/lib/multizoom/multizoom',
		// 'angular-ui-bootstrap': 'http://angular-ui.github.io/bootstrap/ui-bootstrap-tpls-0.11.2.min',
		// 'angular-ui-bootstrap': '/public/lib/angular-ui-bootstrap/dist/ui-bootstrap-0.11.2',
		'angular-ui-bootstrap': '/public/lib/angular-ui-bootstrap/dist/ui-bootstrap-tpls-0.11.0.min',
		// 'ui.bootstrap': '/public/lib/angular-ui-bootstrap/dist/ui-bootstrap-tpls-0.11.0.min',
		// 'angular-ui-bootstrap': '/public/lib/angular-ui-bootstrap/dist/ui-bootstrap-tpls-0.11.2.min',
		'angular-route': '/public/lib/angular-route/angular-route.min',
		'angular-resource': '/public/lib/angular-resource/angular-resource.min',
		'webuploader': '/public/lib/webuploader_fex/dist/webuploader',
		'checklist-model': '/public/lib/checklist-model/checklist-model',
		// 'ngMockE2E': '/public/lib/angular-mocks/angular-mocks',
		when: '/public/lib/when/build/when',
		bxSlider: '/public/lib/bolster.bxSlider/jquery.bxslider.min',

		language: '/public/javascripts/admin/language',

		'unit': '/public/javascripts/admin/unit',
		'c-userrole': '/public/javascripts/components/user_role',
		'c-vedioupload': '/public/javascripts/components/vedio_upload',
		'c-imageupload': '/public/javascripts/components/image_upload',
		'c-fileupload': '/public/javascripts/components/file_upload',
		'c-description': '/public/javascripts/components/description',
		'c-selectimage': '/public/javascripts/components/home_select_image',
		'c-datepicker': '/public/javascripts/components/datepicker',
		'c-collapsed': '/public/javascripts/components/collapsed',
		'c-alerts': '/public/javascripts/components/alerts',
		'c-multiSelect': '/public/javascripts/components/multi_select',
		'd-enter': '/public/javascripts/directives/enter',
		'd-imgholder': '/public/javascripts/directives/img_holder'
	},
	shim: {
		angular: {
			deps: ['jquery'],
			exports: 'angular'
		},
		ngMockE2E: {
			deps: ['angular']
		},
		jquery: {
			exports: 'jQuery'
		},
		bootstrap: {
			deps: ['jquery']
		},
		multizoom: {
			deps: ['jquery']
		},
		webuploader: {
			exports: 'WebUploader'
		},
		when: {
			exports: 'when'
		},
		bxSlider: {
			deps: ['jquery']
				// exports: 'bxSlider'
		},
		'ui.bootstrap': {
			deps: ['angular']
		},
		'angular-ui-bootstrap': {
			deps: ['angular'
				// , 
				// 'angular-ui-bootstrap-tpls'
			]
		},
		'angular-ui-bootstrap-tpls': {
			deps: ['angular']
		},
		'angular-route': {
			deps: ['angular']
		},
		'angular-resource': {
			deps: ['angular']
		},
		'checklist-model': {
			deps: ['angular']
		}
	}
});