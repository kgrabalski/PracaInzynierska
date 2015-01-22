(function () {
    'use strict';

    var app = angular.module('FoodSearch.RestaurantAdmin');

    app.controller('DeliveryRangeController', [
        '$scope',
        function ($scope) {
            var options = {
                zoom: 3,
                center: new google.maps.LatLng(52.238, 21.012),
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                scrollwheel: true,
                streetViewControl: false,
                maxZoom: 18,
                minZoom: 12,
                mapTypeControlOptions: {
                    mapTypeIds: [google.maps.MapTypeId.ROADMAP]
                }
            };
            var map = new google.maps.Map(document.getElementById("googleMap"), options);
            var bounds = new google.maps.LatLngBounds();

            //var array = new google.maps.MVCArray();
            //var polyline = new google.maps.Polyline({
            //    path: array,
            //    strokeColor: "#FF0000",
            //    strokeOpacity: 0.6,
            //    strokeWeight: 5
            //});
            //polyline.setMap(map);

            //google.maps.event.addListener(map, 'click', function(e) {
            //    var path = polyline.getPath();
            //    path.push(e.latLng);
            //});

            var populationOptions = {
                strokeColor: '#FF0000',
                strokeOpacity: 0.8,
                strokeWeight: 2,
                fillColor: '#FF0000',
                fillOpacity: 0.35,
                map: map,
                center: new google.maps.LatLng(52.2492561340332, 20.9898357391357),
                radius: 2500
            };
            // Add the circle for this city to the map.
            var cityCircle = new google.maps.Circle(populationOptions);
            cityCircle.setMap(map);
        }
    ]);
})();