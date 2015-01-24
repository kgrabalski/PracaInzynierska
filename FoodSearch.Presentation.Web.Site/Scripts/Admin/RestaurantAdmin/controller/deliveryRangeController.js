(function () {
    var app = angular.module('FoodSearch.RestaurantAdmin');

    app.controller('DeliveryRangeController', [
        '$scope', 'DeliveryRangeService',
        function ($scope, deliveryRange) {

            $scope.minRadius = 0.25;

            $scope.dr = deliveryRange.getDeliveryRange({}, function() {
                $scope.deliveryRangeType = $scope.dr.HasDeliveryRadius ? "radius" : "range";
                $scope.restaurantPosition = new google.maps.LatLng($scope.dr.Lat, $scope.dr.Lon);
                //initialize map

                var options = {
                    zoom: 12,
                    center: $scope.restaurantPosition,
                    mapTypeId: google.maps.MapTypeId.ROADMAP,
                    scrollwheel: true,
                    streetViewControl: false,
                    mapTypeControlOptions: {
                        mapTypeIds: [google.maps.MapTypeId.ROADMAP]
                    }
                };
                $scope.map = new google.maps.Map(document.getElementById("googleMap"), options);
                
                var marker = new google.maps.Marker({
                    position: $scope.restaurantPosition,
                    map: $scope.map,
                    title: $scope.dr.RestaurantName
                });

                //show delivery range

                $scope.pointsArray = new google.maps.MVCArray();

                for (var i = 0; i < $scope.dr.Polygon.length; i++) {
                    var p = $scope.dr.Polygon[i];
                    $scope.pointsArray.push(new google.maps.LatLng(p.Lat, p.Lon));
                }

                $scope.polygon = new google.maps.Polygon({
                    path: $scope.pointsArray,
                    strokeColor: '#337ab7',
                    strokeOpacity: 0.8,
                    strokeWeight: 2,
                    fillColor: '#337ab7',
                    fillOpacity: 0.35,
                    map: $scope.map,
                    editable: true,
                    draggable: true,
                    geodesic: true
            });

                $scope.circle = new google.maps.Circle({
                    strokeColor: '#337ab7',
                    strokeOpacity: 0.8,
                    strokeWeight: 2,
                    fillColor: '#337ab7',
                    fillOpacity: 0.35,
                    center: $scope.restaurantPosition,
                    editable: true,
                    draggable: false
                });

                $scope.ignoreCenterChange = false;
                $scope.cancelCircleRedraw = false;
                $scope.preservePolygon = false;
                $scope.firstStart = true;

                $scope.$watch('dr.DeliveryRadius', function (newValue, oldValue) {
                    if ($scope.cancelCircleRedraw) {
                        $scope.cancelCircleRedraw = false;
                        return;
                    }
                    if (!$scope.firstStart) {
                        $scope.cancelCircleRedraw = true;
                    }
                    $scope.firstStart = false;
                    if (!$scope.preservePolygon) {
                        $scope.pointsArray.clear();
                    }
                    $scope.circle.setRadius(newValue * 1000);
                    $scope.circle.setMap($scope.map);
                });

                $scope.$watch('deliveryRangeType', function (newValue, oldValue) {
                    google.maps.event.clearInstanceListeners($scope.pointsArray);

                    if (newValue === "radius") {
                        $scope.polygon.setMap(null);
                        $scope.circle.setMap($scope.map);
                        google.maps.event.clearInstanceListeners($scope.circle);

                        google.maps.event.addListener($scope.circle, 'center_changed', function() {
                            if ($scope.ignoreCenterChange) {
                                $scope.ignoreCenterChange = false;
                                return;
                            }
                            $scope.circle.setEditable(false);
                            $scope.ignoreCenterChange = true;
                            $scope.circle.setCenter($scope.restaurantPosition);
                            $scope.circle.setEditable(true);
                            return;
                        });

                        google.maps.event.addListener($scope.circle, 'radius_changed', function() {
                            if ($scope.cancelCircleRedraw) {
                                $scope.cancelCircleRedraw = false;
                                return;
                            }
                            var radius = $scope.circle.getRadius() / 1000;
                            if (radius < $scope.minRadius) {
                                $scope.circle.setRadius($scope.minRadius * 1000);
                                return;
                            }
                            $scope.$apply(function() {
                                $scope.dr.DeliveryRadius = parseFloat(radius.toFixed(2));
                                $scope.cancelCircleRedraw = true;
                            });
                        });
                    } else {
                        google.maps.event.clearInstanceListeners($scope.polygon);
                        $scope.polygon.setMap($scope.map);
                        $scope.circle.setMap(null);

                        var tmp = new google.maps.Circle({
                            center: $scope.restaurantPosition,
                            radius: 2500
                        }).getBounds();

                        //$scope.pointsArray.clear();
                        if ($scope.pointsArray.getLength() == 0) {
                            var ne = tmp.getNorthEast();
                            var sw = tmp.getSouthWest();
                            var nw = new google.maps.LatLng(ne.lat(), sw.lng());
                            var se = new google.maps.LatLng(sw.lat(), ne.lng());
                            $scope.pointsArray.push(ne);
                            $scope.pointsArray.push(se);
                            $scope.pointsArray.push(sw);
                            $scope.pointsArray.push(nw);
                            $scope.preservePolygon = true;
                        }

                        google.maps.event.addListener($scope.polygon, 'rightclick', function(e) {
                            if (e.vertex == null) return;

                            if ($scope.pointsArray.length > 3) {
                                $scope.pointsArray.removeAt(e.vertex);
                            }
                        });

                        google.maps.event.addListener($scope.polygon, 'drag', function () {
                            var contains = $scope.polygon.containsLatLng($scope.restaurantPosition);
                            return contains;
                        });

                        google.maps.event.addListener($scope.pointsArray, 'remove_at', function(i, e) {
                            var contains = $scope.polygon.containsLatLng($scope.restaurantPosition);
                            if (!contains) {
                                alert("");
                                $scope.pointsArray.insertAt(i, e);
                            }
                        });

                        google.maps.event.addListener($scope.pointsArray, 'set_at', function (i, e) {
                            var contains = $scope.polygon.containsLatLng($scope.restaurantPosition);
                            if (!contains) {
                                alert("Wielokąt musi zawierać w sobie lokalizaję restauracji!");
                                $scope.pointsArray.setAt(i, e);
                            }
                        });
                    }
                });


            });

            $scope.saveDeliveryRange = function() {
                console.log(JSON.stringify($scope.pointsArray.getArray()));
            }
        }
    ]);
})();