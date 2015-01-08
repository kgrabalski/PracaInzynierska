var services = angular.module('FoodSearch.RestaurantAdmin.Services', ['ngResource']);

services.service('RestaurantCuisineService', [
    '$resource', function($resource) {
        var service = $resource('/RestaurantAdmin/api/RestaurantCuisine/:cuisineId',
                                { cuisineId: "@Id" });
        service.Items = service.query();
        return service;
    }
]);

services.service('CuisineService', [
    '$resource', function($resource) {
        return $resource('/RestaurantAdmin/api/Cuisine/:cuisineId', 
                        { cuisineId: "@Id" });
    }
]);

services.service('DishGroupService', [
    '$resource', function($resource) {
        var service = $resource('/RestaurantAdmin/api/DishGroup/:dishGroupId',
                                { dishGroupId: "@Id" },
                                {'update': {method: "PUT"}});
        service.Items = service.query();
        return service;
    }
]);

services.service('DishService', [
    '$resource', function($resource) {
        var service = $resource('/RestaurantAdmin/api/Dish/:dishId',
                                { dishId: "@Id" });
        service.Items = service.query();
        return service;
    }
]);

services.service('OpeningHourService', [
    '$resource', function($resource) {
        var service = $resource('/RestaurantAdmin/api/OpeningHour/:openingId',
                                { openingId: "@Id" });
        service.Items = service.query();
        return service;
    }
]);

services.service('OpinionsService', [
    '$resource', function($resource) {
        var service = $resource('/RestaurantAdmin/api/Opinion?page=:page&rating=:rating',
                                {page: 0, rating: 0},
        {
            getOpinions: {
                method: 'GET',
                params: { page: "@Page", rating: "@Rating" },
                isArray: true
            }
        });
        service.Page = 0;
        service.Rating = 0;

        service.Items = service.getOpinions({page: service.Page++, rating: service.Rating});
        return service;
    }
]);

services.service('RatingService', [
    '$resource', function($resource) {
        return $resource('/RestaurantAdmin/api/Rating');
    }
]);