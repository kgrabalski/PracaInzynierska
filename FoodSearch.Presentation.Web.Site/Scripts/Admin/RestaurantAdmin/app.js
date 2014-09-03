(function () {
    'use strict';

    var app = angular.module('FoodSearch.RestaurantAdmin', ['ngRoute',
                                                            'FoodSearch.Admin.Common',
                                                            'FoodSearch.RestaurantAdmin.Services',
                                                            'ui.bootstrap',
                                                            'ui.bootstrap.tpls']);

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
                controller: 'DishesController',
                templateUrl: 'dishesTemplate'
            })
            .when("/dishGroups", {
                controller: 'DishGroupsController',
                templateUrl: 'dishGroupsTemplate'
            })
            .when("/cuisines", {
                controller: 'CuisinesController',
                templateUrl: 'cuisinesTemplate'
            })
            .when("/cuisines/add", {

            })
        
            .when("/restaurant", {
                controller: 'Restaurant',
                templateUrl: 'restaurantTemplate'
            })
            .when("/openingHours", {
                controller: 'OpeningHoursController',
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