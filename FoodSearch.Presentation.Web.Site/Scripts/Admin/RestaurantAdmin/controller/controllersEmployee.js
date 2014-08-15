var app = angular.module('FoodSearch.RestaurantAdmin');

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

app.controller('DishGroups', ['$scope',
    function ($scope) {
        $scope.napis = "DishGroups";
    }
]);

app.controller('AddCuisine', ['$scope', '$modalInstance', 'Cuisine', 'toAdd',
    function ($scope, $modalInstance, cuisineService, toAdd) {

        $scope.allCuisines = cuisineService.query();
        $scope.toAdd = toAdd;

        $scope.add = function () {
            $modalInstance.close($scope.toAdd);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }
]);

app.controller('Cuisines', ['$scope', 'RestaurantCuisine', '$modal',
    function ($scope, restaurantCuisine, $modal) {

        $scope.cuisines = restaurantCuisine;

        $scope.remove = function(index) {
            $scope.cuisines.Items[index].$delete(function () {
                $scope.cuisines.Items.splice(index, 1);
                alert("ok " + restaurantCuisine.Items.length);
            });
        };

        $scope.addNew = function() {

            var modalInstance = $modal.open({
                templateUrl: 'addRestaurantCuisineModal',
                controller: 'AddCuisine',
                resolve: {
                    toAdd: function() {
                        return new restaurantCuisine();
                    }
                }
            });

            modalInstance.result.then(function (cuisine) {
                cuisine.$save(function(c) {
                    $scope.cuisines.Items.push(c);
                });
            });
        }
    }
]);