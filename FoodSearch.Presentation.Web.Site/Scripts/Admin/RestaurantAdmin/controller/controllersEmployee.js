﻿var app = angular.module('FoodSearch.RestaurantAdmin');

app.controller('Dashboard', ['$scope',
    function ($scope) {
        $scope.napis = "Dashboard";
    }
]);

app.controller('Orders', ['$scope',
    function ($scope) {
        $scope.napis = "Orders";
    }
]);

app.controller('Dishes', ['$scope',
    function ($scope) {
        $scope.napis = "Dishes";
    }
]);