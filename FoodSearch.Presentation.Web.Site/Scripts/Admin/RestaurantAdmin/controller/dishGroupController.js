var app = angular.module('FoodSearch.RestaurantAdmin');

app.controller('DishGroupsController', ['$scope', 'DishGroupService', '$modal',
    function ($scope, dishGroups, $modal) {
        $scope.napis = "DishGroups";
    }
]);