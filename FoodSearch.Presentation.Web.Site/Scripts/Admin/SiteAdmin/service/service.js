var services = angular.module('FoodSearch.SiteAdmin.Services', ['ngResource']);

services.service('RestaurantService', [
    '$resource', '$http', function($resource, $http) {
        var service = $resource('/SiteAdmin/api/Restaurant/:restaurantId', { restaurantId: "@Id" });
        service.Items = service.query();
        service.createNew = function(toAdd) {
            return service.get({ Id: toAdd.Id });
        }
        return service;
    }
]);

services.service('CityService', [
    '$resource', function($resource) {
        var service = $resource('/SiteAdmin/api/City/:cityId', { cityId: "@Id" });
        return service;
    }
]);