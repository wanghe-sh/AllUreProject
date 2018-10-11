function mockDev(app) {
    app.run(function($httpBackend) {

        $httpBackend.when('GET', /\/product\/\d*$/)
            .respond({
                images: [{
                    small: '/public/test/images/millasmall.jpg',
                    large: '/public/test/images/milla.jpg',
                    tmb: '/public/test/images/milla_tmb.jpg'
                }, {
                    small: '/public/test/images/saleensmall.jpg',
                    large: '/public/test/images/saleen.jpg',
                    tmb: '/public/test/images/saleen_tmb.jpg'
                }, {
                    small: '/public/test/images/haydensmall.jpg',
                    large: '/public/test/images/hayden.jpg',
                    tmb: '/public/test/images/hayden_tmb.jpg'
                }, {
                    small: '/public/test/images/jaguarsmall.jpg',
                    large: '/public/test/images/jaguar.jpg',
                    tmb: '/public/test/images/jaguar_tmb.jpg'
                }]
            });

    });
}