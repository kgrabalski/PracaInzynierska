var services = angular.module('FoodSearch.RestaurantAdmin.Services', ['ngResource']);

services.service('RestaurantCuisine', ['$resource', function($resource) {
    var service = $resource('/RestaurantAdmin/api/RestaurantCuisine/:cuisineId', { cuisineId: "@Id" });
    service.Items = service.query();
    return service;
}]);

services.service('Cuisine', ['$resource', function($resource) {
    return $resource('/RestaurantAdmin/api/Cuisine/:cuisineId', { cuisineId: "@Id" });
}])