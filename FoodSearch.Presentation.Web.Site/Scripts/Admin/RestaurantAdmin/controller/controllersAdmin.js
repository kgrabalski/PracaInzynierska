(function () {
    'use strict';
    var app = angular.module('FoodSearch.RestaurantAdmin');

    app.controller('Reports', [
        '$scope',
        function($scope) {
            $scope.napis = "Reports";
        }
    ]);
})();