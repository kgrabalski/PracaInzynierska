var app = angular.module('FoodSearch.SiteAdmin');

app.controller('CitiesController', ['$scope', '$modal', 'CityService',
    function ($scope, $modal, city) {
        $scope.cities = city;

        $scope.remove = function(index) {
            var modalRemove = $modal.open({
                templateUrl: 'removeCityModal',
                controller: 'RemoveCityController',
                resolve: {
                    toRemove: function() {
                        return $scope.cities.Items[index];
                    }
                }
            });

            modalRemove.result.then(function(result) {
                if (result) {
                    $scope.cities.Items[index].$delete(function() {
                        $scope.cities.Items.splice(index, 1);
                    });
                }
            });
        };

        $scope.addNew = function () {
            var modalAdd = $modal.open({
                templateUrl: 'addCityModal',
                controller: 'AddCityController',
                resolve: {
                    toAdd: function () {
                        return new city();
                    }
                }
            });

            modalAdd.result.then(function (city) {
                city.$save(function (c) {
                    $scope.cities.Items.push(c);
                });
            });
        }
    }
]);

app.controller('AddCityController', [
    '$scope', '$modalInstance', 'toAdd',
    function ($scope, $modalInstance, toAdd) {
        $scope.toAdd = toAdd;
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
        $scope.add = function () {
            $modalInstance.close($scope.toAdd);
        };
    }
]);

app.controller('RemoveCityController', [
    '$scope', '$modalInstance', 'toRemove',
    function ($scope, $modalInstance, toRemove) {
        $scope.city = toRemove;
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
        $scope.remove = function () {
            $modalInstance.close(true);
        }
    }
]);