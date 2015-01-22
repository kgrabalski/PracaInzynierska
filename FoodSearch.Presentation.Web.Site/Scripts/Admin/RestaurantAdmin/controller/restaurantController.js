(function() {
    'use strict';

    var app = angular.module('FoodSearch.RestaurantAdmin');

    app.controller('RestaurantController', [
        '$scope', 'RestaurantDataService', 
        function ($scope, restaurantData) {
            $scope.canSave = false;
            $scope.data = restaurantData.getRestaurantData(function() {
                $scope.canSave = true;
            });

            $scope.updateRestaurant = function() {
                $scope.data.$save();
            }
        }
    ]);
})();