﻿(function () {
    'use strict';

    var app = angular.module('FoodSearch.SiteAdmin', ['ngRoute',
                                                      'FoodSearch.Admin.Common',
                                                      'FoodSearch.SiteAdmin.Services',
                                                      'ui.bootstrap',
                                                      'ui.bootstrap.tpls']);
    
    app.config( function ($routeProvider) {
        $routeProvider
            .when("/dashboard", {
                controller: 'DashboardController',
                templateUrl: 'dashboardTemplate'
            })
            .when("/restaurants", {
                controller: 'RestaurantsController',
                templateUrl: 'restaurantsTemplate'
            })
            .when("/users", {
                controller: 'UsersController',
                templateUrl: 'usersTemplate'
            })
            .otherwise({
                redirectTo: "/dashboard"
            });
    });
})();