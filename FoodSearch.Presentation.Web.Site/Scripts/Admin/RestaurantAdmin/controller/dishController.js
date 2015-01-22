(function () {
    'use strict';
    var app = angular.module('FoodSearch.RestaurantAdmin');

    app.controller('DishesController', [
        '$scope', 'DishService', '$modal',
        function($scope, dish, $modal) {
            $scope.dishes = dish;

            $scope.remove = function(index) {
                alert("usun " + index);
            }

            $scope.addNew = function() {
                var modalAdd = $modal.open({
                    templateUrl: 'addDishModal',
                    controller: 'AddDishController',
                    resolve: {
                        toAdd: function() {
                            return new dish();
                        }
                    }
                });

                modalAdd.result.then(function(newDish) {
                    newDish.$save(function(d) {
                        $scope.dishes.Items.push(d);
                    });
                });
            }
        }
    ]);

    app.controller('AddDishController', [
        '$scope', '$modalInstance', 'DishGroupService', 'toAdd',
        function($scope, $modalInstance, dishGroupService, toAdd) {

            $scope.allDishGroups = dishGroupService;
            $scope.toAdd = toAdd;

            $scope.add = function() {
                $modalInstance.close($scope.toAdd);
            };

            $scope.cancel = function() {
                $modalInstance.dismiss('cancel');
            };

            $scope.options = {
                valueField: 'Id',
                labelField: 'Name',
                create: false
            };
        }
    ]);
})();