(function () {
    'use strict';
    var app = angular.module('FoodSearch.RestaurantAdmin');

    app.controller('NewOrdersController', [
        '$scope', 'NewOrderService', '$modal',
        function($scope, newOrder, $modal) {
            $scope.orders = newOrder.query();

            $scope.confirmOrder = function(index) {
                var modalConfirm = $modal.open({
                    templateUrl: 'confirmOrderModal',
                    controller: 'ConfirmOrderController'
                });

                modalConfirm.result.then(function (dt) {
                    var order = new newOrder();
                    order.OrderId = $scope.orders[index].OrderId;
                    order.DeliveryTime = dt;
                    order.$save(function() {
                        $scope.orders = newOrder.query();
                    });
                });
            }

            $scope.cancelOrder = function(index) {
                var modalCancel = $modal.open({
                    templateUrl: 'cancelOrderModal',
                    controller: 'CancelOrderController'
                });

                modalCancel.result.then(function(reason) {
                    if (reason !== "") {
                        newOrder.cancelOrder({
                                OrderId: $scope.orders[index].OrderId,
                                CancellationReason: reason
                            },
                            function() {
                                $scope.orders = newOrder.query();
                            });
                    }
                });
            }
        }
    ]);

    app.controller('CancelOrderController', [
        '$scope', '$modalInstance',
        function($scope, $modalInstance) {

            $scope.cancelOrder = function() {
                $modalInstance.close($scope.cancelReason);
            };

            $scope.cancel = function() {
                $modalInstance.dismiss('cancel');
            };
        }
    ]);

    app.controller('ConfirmOrderController', [
        '$scope', '$modalInstance',
        function($scope, $modalInstance) {
            
            $scope.options = {
                create: false
            };
            $scope.cancel = function() {
                $modalInstance.dismiss('cancel');
            }
            $scope.confirmOrder = function() {
                $modalInstance.close($scope.deliveryTime);
            }
        }
    ]);
})();