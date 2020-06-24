'use strict';

// Application Modules and Routing
angular
    .module('newApp', ['ngRoute'])
    .config(function($routeProvider) {
        $routeProvider
            .when('/', {
                templateUrl: 'views/main.html',
                controller: 'Main'
            })
            // .when('/', {
            //     templateUrl: '/',
            //     controller: 'Auth'
            // });
    });