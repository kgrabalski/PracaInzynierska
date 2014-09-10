var app = angular.module('FoodSearch.SiteAdmin');

app.controller('RestaurantsController', ['$scope', 'RestaurantService', '$modal',
    function ($scope, restaurant, $modal) {
        $scope.restaurants = restaurant;

        $scope.remove = function(index) {
            alert(index);
        }

        $scope.addNew = function() {
            var modalAdd = $modal.open({
                templateUrl: 'addRestaurantModal',
                controller: 'AddRestaurantController'
            });
            modalAdd.result.then(function (newRestaurant) {
                var rest = restaurant.createNew(newRestaurant);
                if (rest != null) $scope.restaurants.Items.push(rest);
            });
        }

    }
]);

app.controller('AddRestaurantController', [
    '$scope', '$modalInstance', 'CityService', function($scope, $modalInstance, city) {
        $scope.allCities = city.query();
        $scope.cityOptions = {
            valueField: 'Id',
            labelField: 'Name',
            create: false
        };

        $scope.toAdd = {
            Name: "",
            CityId: -1,
            District: 'nieznana'
        }
        $scope.add = function () {
            $modalInstance.close($scope.toAdd);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }
]);