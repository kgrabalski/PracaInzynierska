﻿(function () {
    'use strict';

    var app = angular.module('FoodSearch.RestaurantAdmin', ['ngRoute', 'FoodSearch.Admin.Common']);

    app.config(function ($routeProvider) {
        $routeProvider
            .when("/dashboard", {
                controller: 'Dashboard',
                templateUrl: 'dashboardTemplate'
            })
            .when("/orders", {
                controller: 'Orders',
                templateUrl: 'ordersTemplate'
            })
            .when("/dishes", {
                controller: 'Dishes',
                templateUrl: 'dishesTemplate'
            })
            .when("/dishGroups", {
                controller: 'Dishes',
                templateUrl: 'dishGroupsTemplate'
            })
            .when("/cuisines", {
                controller: 'Cuisines',
                templateUrl: 'cuisinesTemplate'
            })
        
            .when("/restaurant", {
                controller: 'Restaurant',
                templateUrl: 'restaurantTemplate'
            })
            .when("/openingHours", {
                controller: 'OpeningHours',
                templateUrl: 'openingHoursTemplate'
            })
            .when("/opinions", {
                controller: 'Opinions',
                templateUrl: 'opinionsTemplate'
            })
            .when("/reports", {
                controller: 'Reports',
                templateUrl: 'reportsTemplate'
            })
            .when("/employees", {
                controller: 'Employees',
                templateUrl: 'employeesTemplate'
            })
        
            .otherwise({
                redirectTo: "/dashboard"
            });
    });
})();