var app = angular.module('FoodSearch.RestaurantAdmin');

app.controller('CuisinesController', ['$scope', 'RestaurantCuisineService', '$modal',
    function ($scope, restaurantCuisine, $modal) {

        $scope.cuisines = restaurantCuisine;

        $scope.remove = function (index) {
            var modalRemove = $modal.open({
                templateUrl: 'removeRestaurantCuisineModal',
                controller: 'RemoveCuisineController',
                resolve: {
                    toRemove: function () {
                        return $scope.cuisines.Items[index];
                    }
                }
            });

            modalRemove.result.then(function (result) {
                if (result) {
                    $scope.cuisines.Items[index].$delete(function () {
                        $scope.cuisines.Items.splice(index, 1);
                    });
                }
            });
        };

        $scope.addNew = function () {
            var modalAdd = $modal.open({
                templateUrl: 'addRestaurantCuisineModal',
                controller: 'AddCuisineController',
                resolve: {
                    toAdd: function () {
                        return new restaurantCuisine();
                    }
                }
            });

            modalAdd.result.then(function (cuisine) {
                cuisine.$save(function (c) {
                    $scope.cuisines.Items.push(c);
                });
            });
        }
    }
]);

app.controller('AddCuisineController', ['$scope', '$modalInstance', 'CuisineService', 'toAdd',
    function ($scope, $modalInstance, cuisineService, toAdd) {

        $scope.allCuisines = cuisineService.query();
        $scope.toAdd = toAdd;

        $scope.add = function () {
            $modalInstance.close($scope.toAdd);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        $scope.options = {
            valueField: 'Id',
            labelField: 'Name',
            create: false
        };
    }
]);

app.controller('RemoveCuisineController', ['$scope', '$modalInstance', 'toRemove',
    function ($scope, $modalInstance, toRemove) {
        $scope.cuisine = toRemove;
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
        $scope.remove = function () {
            $modalInstance.close(true);
        }
    }]);
