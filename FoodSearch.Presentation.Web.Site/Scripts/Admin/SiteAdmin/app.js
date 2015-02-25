(function () {
    'use strict';

    var app = angular.module('FoodSearch.SiteAdmin', ['ngRoute',
                                                      'FoodSearch.Admin.Common',
                                                      'FoodSearch.SiteAdmin.Services',
                                                      'ui.bootstrap',
                                                      'ui.bootstrap.tpls']);
    
    app.config( function ($routeProvider) {
        $routeProvider
            .when("/restaurants", {
                controller: 'RestaurantsController',
                templateUrl: 'restaurantsTemplate'
            })
            .when("/users", {
                controller: 'UsersController',
                templateUrl: 'usersTemplate'
            })
            .when("/repSFinancialDaily", {
                controller: 'DailySFinancialReportController',
                templateUrl: 'dailySFinancialReportTemplate'
            })
            .when("/repSFinancialMonthly", {
                controller: 'MonthlySFinancialReportController',
                templateUrl: 'monthlySFinancialReportTemplate'
            })
            .otherwise({
                redirectTo: "/restaurants"
            });
    });
})();