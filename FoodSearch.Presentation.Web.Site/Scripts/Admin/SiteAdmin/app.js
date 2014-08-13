(function () {
    'use strict';

    var app = angular.module('FoodSearch.SiteAdmin', ['ngRoute', 'FoodSearch.Admin.Common']);
    
    app.config( function ($routeProvider) {
        $routeProvider
            .when("/dashboard", {
                controller: 'Dashboard',
                templateUrl: 'dashboardTemplate'
            })
            .when("/restaurants", {
                controller: 'Restaurants',
                templateUrl: 'restaurantsTemplate'
            })
            .when("/users", {
                controller: 'Users',
                templateUrl: 'usersTemplate'
            })
            .otherwise({
                redirectTo: "/dashboard"
            });
    });
})();