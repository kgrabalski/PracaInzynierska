(function () {
    'use strict';
    var app = angular.module('FoodSearch.SiteAdmin');

    app.controller('DashboardController', [
        '$scope',
        function($scope) {
            $scope.napis = "Dashboard";
        }
    ]);

    app.controller('UsersController', [
        '$scope',
        function($scope) {
            $scope.napis = "Users";
        }
    ]);
})();
