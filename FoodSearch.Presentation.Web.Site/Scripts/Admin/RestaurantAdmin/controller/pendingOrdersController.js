(function () {
    'use strict';
    var app = angular.module('FoodSearch.RestaurantAdmin');

    app.controller('PendingOrdersController', [
        '$scope', 'PendingOrderService', '$modal',
        function($scope, pendingOrder, $modal) {
            $scope.orders = pendingOrder.query();

            $scope.completeOrder = function(index) {
                var modalComplete = $modal.open({
                    templateUrl: 'completeOrderModal',
                    controller: 'CompleteOrderController'
                });

                modalComplete.result.then(function(result) {
                    if (result === true) {
                        pendingOrder.completeOrder({
                            OrderId: $scope.orders[index].OrderId
                        }, function() {
                            $scope.orders = pendingOrder.query();
                        });
                    }
                });
            }
        }
    ]);

    app.controller('CompleteOrderController', [
        '$scope', '$modalInstance',
        function($scope, $modalInstance) {
            $scope.completeOrder = function() {
                $modalInstance.close(true);
            }

            $scope.cancel = function() {
                $modalInstance.dismiss("cancel");
            }
        }
    ]);
})();