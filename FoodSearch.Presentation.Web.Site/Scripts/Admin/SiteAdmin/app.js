(function () {
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
            .when("/cities", {
                controller: 'CitiesController',
                templateUrl: 'citiesTemplate'
            })
            .when("/districts", {
                controller: 'DistrictsController',
                templateUrl: 'districtsTemplate'
            })
            .when("/streets", {
                controller: 'StreetsController',
                templateUrl: 'streetsTemplate'
            })
            .otherwise({
                redirectTo: "/dashboard"
            });
    });
})();