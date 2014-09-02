var services = angular.module('FoodSearch.RestaurantAdmin.Services', ['ngResource']);

services.service('RestaurantCuisineService', [
    '$resource', function($resource) {
        var service = $resource('/RestaurantAdmin/api/RestaurantCuisine/:cuisineId', { cuisineId: "@Id" });
        service.Items = service.query();
        return service;
    }
]);

services.service('CuisineService', [
    '$resource', function($resource) {
        return $resource('/RestaurantAdmin/api/Cuisine/:cuisineId', { cuisineId: "@Id" });
    }
]);

services.service('DishGroupService', [
    '$resource', function($resource) {
        var service = $resource('/RestaurantAdmin/api/DishGroup/:dishGroupId', { dishGroupId: "@Id" });
        service.Items = service.query();
        return service;
    }
]);