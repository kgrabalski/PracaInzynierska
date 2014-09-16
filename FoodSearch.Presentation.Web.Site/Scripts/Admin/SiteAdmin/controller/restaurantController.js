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
                rest.then(function(nr) {
                    if (nr.status == 201) {
                        $scope.restaurants.Items.push(restaurant.get({Id: nr.data.Id}));
                    }
                });
            });
        }

    }
]);

app.controller('AddRestaurantController', [
    '$scope', '$modalInstance', 'CityService', 'StreetService', 'AddressService',
    function ($scope, $modalInstance, city, street, address) {
        $scope.allCities = city.query();
        $scope.cityOptions = {
            valueField: 'Id',
            labelField: 'Name',
            create: false,
            onChange: function(value) {
                if (value == "") $scope.toAdd.StreetId = "";
            }
        };

        $scope.allStreets = [];
        $scope.streetOptions = {
            valueField: 'Id',
            labelField: 'Name',
            sortField: 'Name',
            searchField: ['Name'],
            create: false,
            load: function(query, callback) {
                if (query.length < 3) return callback();
                $scope.allStreets = street.query({ cityId: $scope.toAdd.CityId, query: query });
            },
            onChange: function (value) {
                $scope.toAdd.AddressId = "";
                $scope.allStreetNumbers = [];
                if (value != "") {
                    $scope.allStreetNumbers = street.query({ Id: value });
                }
            }
        };

        $scope.allStreetNumbers = [];
        $scope.streetNumbersOptions = {
            valueField: 'Id',
            labelField: 'Number',
            create: false,
            onChange: function(value) {
                if (value != "") {
                    address.get({ Id: value }, function(adr) {
                        $scope.toAdd.District = adr.District;
                    });
                } else $scope.toAdd.District = "nieznana";
            }
        }

        $scope.toAdd = {
            Name: "",
            CityId: -1,
            District: 'nieznana',
            StreetId: -1,
            AddressId: -1,
            UserName: "",
            UserEmail: "",
            UserPassword: "",
            LogoFile: null
        };

        $scope.add = function () {
            $modalInstance.close($scope.toAdd);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }
]);