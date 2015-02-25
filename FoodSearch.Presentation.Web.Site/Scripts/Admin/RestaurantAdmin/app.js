(function () {
    'use strict';

    var app = angular.module('FoodSearch.RestaurantAdmin', ['ngRoute',
                                                            'FoodSearch.Admin.Common',
                                                            'FoodSearch.RestaurantAdmin.Services',
                                                            'ui.bootstrap',
                                                            'ui.bootstrap.tpls',
                                                            'a8m.group-by']);

    app.config(function($routeProvider) {
        $routeProvider
            .when("/newOrders", {
                controller: 'NewOrdersController',
                templateUrl: 'newOrdersTemplate'
            })
            .when("/pendingOrders", {
                controller: 'PendingOrdersController',
                templateUrl: 'pendingOrdersTemplate'
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
            .when("/restaurant", {
                controller: 'RestaurantController',
                templateUrl: 'restaurantTemplate'
            })
            .when("/openingHours", {
                controller: 'OpeningHoursController',
                templateUrl: 'openingHoursTemplate'
            })
            .when("/deliveryRange", {
                controller: 'DeliveryRangeController',
                templateUrl: 'deliveryRangeTemplate'
            })
            .when("/opinions", {
                controller: 'OpinionsController',
                templateUrl: 'opinionsTemplate'
            })
            .when("/reports", {
                controller: 'Reports',
                templateUrl: 'reportsTemplate'
            })
            .when("/employees", {
                controller: 'EmployeesController',
                templateUrl: 'employeesTemplate'
            })
            .when("/repFinancialDaily", {
                controller: 'DailyFinancialReportController',
                templateUrl: 'dailyFinancialReportTemplate'
            })
            .when("/repFinancialMonthly", {
                controller: 'MonthlyFinancialReportController',
                templateUrl: 'monthlyFinancialReportTemplate'
            })
            .otherwise({
                redirectTo: "/newOrders"
            });
    });

})();