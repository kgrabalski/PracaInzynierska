(function() {
    'use strict';
    var services = angular.module('FoodSearch.SiteAdmin.Services', ['ngResource']);

    services.service('RestaurantService', [
        '$resource', '$http', function($resource, $http) {
            var service = $resource('/SiteAdmin/api/Restaurant/:restaurantId', { restaurantId: "@Id" });
            service.Items = service.query();
            service.createNew = function(toAdd) {
                var fd = new FormData();
                angular.forEach(toAdd, function(v, k) {
                    fd.append(k, v);
                });
                return $http.post("/SiteAdmin/api/Restaurant", fd, {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                });
            }
            return service;
        }
    ]);

    services.service('CityService', [
        '$resource', function($resource) {
            var service = $resource('/SiteAdmin/api/City/:cityId', { cityId: "@Id" });
            service.Items = service.query();
            return service;
        }
    ]);

    services.service('StreetService', [
        '$resource', function($resource) {
            var service = $resource('/SiteAdmin/api/Street/:streetId', { streetId: "@Id" });
            return service;
        }
    ]);

    services.service('AddressService', [
        '$resource', function($resource) {
            var service = $resource('/SiteAdmin/api/Address/:addressId', { addressId: "@Id" });
            return service;
        }
    ]);
})();