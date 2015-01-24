(function () {
    'use strict';
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
            { 'update': { method: "PUT" } });
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

    services.service('OrderService', [
        '$resource', function($resource) {
            return $resource('/RestaurantAdmin/api/Order/:orderId',
            { orderId: "@Id" });
        }
    ]);

    services.service('NewOrderService', [
        '$resource', function($resource) {
            return $resource('/RestaurantAdmin/api/NewOrder/:orderId',
            { orderId: "@OrderId" },
            {
                cancelOrder: {
                    method: "DELETE",
                    params: { cancellationReason: "@CancellationReason" }
                }
            });
        }
    ]);

    services.service('PendingOrderService', [
        '$resource', function($resource) {
            return $resource('/RestaurantAdmin/api/PendingOrder/:orderId',
                { orderId: "@OrderId"},
            {
                completeOrder: {
                    method: "PUT"
                }
            });
        }
    ]);

    services.service('OpeningHourService', [
        '$resource', function ($resource) {
            var service = $resource('/RestaurantAdmin/api/OpeningHour/:openingId',
            { openingId: "@Id" });
            service.Items = service.query();
            return service;
        }
    ]);

    services.service('OpinionsService', [
        '$resource', function ($resource) {
            var service = $resource('/RestaurantAdmin/api/Opinion?page=:page&rating=:rating',
            { page: 0, rating: 0 },
            {
                getOpinions: {
                    method: 'GET',
                    params: { page: "@Page", rating: "@Rating" },
                    isArray: true
                }
            });
            service.Page = 0;
            service.Rating = 0;

            service.Items = service.getOpinions({ page: service.Page++, rating: service.Rating });
            return service;
        }
    ]);

    services.service('RatingService', [
        '$resource', function ($resource) {
            return $resource('/RestaurantAdmin/api/Rating');
        }
    ]);

    services.service('RestaurantDataService', [
        '$resource', function ($resource) {
            return $resource('/RestaurantAdmin/api/RestaurantData', {}, {
                getRestaurantData: {
                    method: 'GET',
                    isArray: false
                }
            });
        }
    ]);

    services.service('EmployeeService', [
        '$resource', function($resource) {
            return $resource('RestaurantAdmin/api/RestaurantEmployee/:userId',
            { userId: "@UserId" },
            {
                resetPassword: {
                    method: 'PUT'
                }
            });
        }
    ]);

    services.service('DeliveryRangeService', [
        '$resource', function($resource) {
            return $resource('RestaurantAdmin/api/DeliveryRange', {},
            {
                getDeliveryRange: {
                    method: 'GET',
                    isArray: false
                }
            });
        }
    ]);
})();