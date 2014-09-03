var app = angular.module('FoodSearch.RestaurantAdmin');

app.controller('DishGroupsController', [
    '$scope', 'DishGroupService', '$modal',
    function($scope, dishGroup, $modal) {
        $scope.dishGroups = dishGroup;

        $scope.remove = function(index) {
            var modalRemove = $modal.open({
                templateUrl: 'removeDishGroupModal',
                controller: 'RemoveDishGroupController',
                resolve: {
                    toRemove: function() {
                        return $scope.dishGroups.Items[index];
                    }
                }
            });

            modalRemove.result.then(function(result) {
                if (result) {
                    $scope.dishGroups.Items[index].$delete(function () {
                        $scope.dishGroups.Items.splice(index, 1);
                    });
                }
            });
        };

        $scope.addNew = function () {
            var modalAdd = $modal.open({
                templateUrl: 'addDishGroupModal',
                controller: 'AddDishGroupController',
                resolve: {
                    toAdd: function () {
                        return new dishGroup();
                    },
                    edit: function () { return false; }
                }
            });

            modalAdd.result.then(function (newGroup) {
                newGroup.$save(function (g) {
                    $scope.dishGroups.Items.push(g);
                });
            });
        }

        $scope.edit = function (index) {
            var modalEdit = $modal.open({
                templateUrl: 'addDishGroupModal',
                controller: 'AddDishGroupController',
                resolve: {
                    toAdd: function () {
                        return $scope.dishGroups.Items[index];
                    },
                    edit: function () { return true; }
                }
            });

            modalEdit.result.then(function (group) {
                group.$update(function (g) {
                    $scope.dishGroups.Items[index] = g;
                });
            });
        }
    }
]);

app.controller('AddDishGroupController', [
    '$scope', '$modalInstance', 'DishGroupService', 'toAdd', 'edit',
    function ($scope, $modalInstance, dishGroupService, toAdd, edit) {
        $scope.originalValue = toAdd.Name;
        $scope.toAdd = toAdd;
        $scope.operation = edit == true ? "Edytuj" : "Dodaj";
        $scope.cancel = function () {
            $scope.toAdd.Name = $scope.originalValue;
            $modalInstance.dismiss('cancel');
        };
        $scope.add = function() {
            $modalInstance.close($scope.toAdd);
        };
    }
]);

app.controller('RemoveDishGroupController', [
    '$scope', '$modalInstance', 'toRemove',
    function($scope, $modalInstance, toRemove) {
        $scope.dishGroup = toRemove;
        $scope.cancel = function() {
            $modalInstance.dismiss('cancel');
        };
        $scope.remove = function() {
            $modalInstance.close(true);
        }
    }
]);
