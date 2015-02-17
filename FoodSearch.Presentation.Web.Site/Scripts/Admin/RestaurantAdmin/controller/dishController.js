(function () {
    'use strict';
    var app = angular.module('FoodSearch.RestaurantAdmin');

    app.controller('DishesController', [
        '$scope', 'DishService', '$modal',
        function($scope, dish, $modal) {
            $scope.dishes = dish;

            $scope.remove = function(id) {
                var modalRemove = $modal.open({
                    templateUrl: 'removeDishModal',
                    controller: 'RemoveDishController',
                    resolve: {
                        toRemove: function() {
                            for (var i = 0; i < $scope.dishes.Items.length; i++) {
                                var d = $scope.dishes.Items[i];
                                if (d.Id == id) return d;
                            }
                        }
                    }
                });

                modalRemove.result.then(function(d) {
                    d.$delete(function() {
                        $scope.dishes.Items = dish.query();
                    });
                });
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

                modalAdd.result.then(function (newDish) {
                    var d = dish.createNew(newDish);
                    d.then(function(nd) {
                        if (nd.status == 201) {
                            $scope.dishes.Items = dish.query();
                        }
                    });
                });
            }

            $scope.editDish = function (id) {
                var modalEdit = $modal.open({
                    templateUrl: 'editDishModal',
                    controller: 'EditDishController',
                    resolve: {
                        toEdit: function() {
                            for (var i = 0; i < $scope.dishes.Items.length; i++) {
                                var d = $scope.dishes.Items[i];
                                if (d.Id == id) {
                                    var tmp = new dish();
                                    tmp.Id = d.Id;
                                    tmp.DishName = d.DishName;
                                    tmp.DishGroupId = d.DishGroupId;
                                    tmp.Price = parseFloat(d.Price);
                                    return tmp;
                                }
                            }
                        }
                    }
                });

                modalEdit.result.then(function(d) {
                    d.$update(function() {
                        $scope.dishes.Items = dish.query();
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

    app.controller('RemoveDishController', [
        '$scope', '$modalInstance', 'toRemove',
        function($scope, $modalInstance, toRemove) {
            $scope.toRemove = toRemove;

            $scope.cancel = function() {
                $modalInstance.dismiss('cancel');
            }

            $scope.remove = function() {
                $modalInstance.close($scope.toRemove);
            }
        }
    ]);

    app.controller('EditDishController', [
        '$scope', '$modalInstance', 'DishGroupService', 'toEdit',
        function($scope, $modalInstance, dishGroupService, toEdit) {
            $scope.allDishGroups = dishGroupService;
            $scope.toEdit = toEdit;

            $scope.edit = function() {
                $modalInstance.close($scope.toEdit);
            }

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