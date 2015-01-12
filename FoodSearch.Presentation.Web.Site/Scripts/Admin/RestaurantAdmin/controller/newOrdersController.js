var app = angular.module('FoodSearch.RestaurantAdmin');

app.controller('NewOrdersController', [
    '$scope', 'NewOrderService', '$modal',
    function($scope, newOrder, $modal) {
        $scope.orders = newOrder.query();

        $scope.confirmOrder = function(index) {
            alert(index);
        }

        $scope.cancelOrder = function(index) {
            var modalCancel = $modal.open({
                templateUrl: 'cancelOrderModal',
                controller: 'CancelOrderController',
                resolve: {
                    order: function() {
                        return $scope.orders[index];
                    }
                }
            });

            modalCancel.result.then(function (reason) {
                if (reason != "") {
                    $scope.orders = newOrder.query();
                }
            });
        }
    }
]);

app.controller('CancelOrderController', [
    '$scope', '$modalInstance', 'OrderService', 'order',
    function ($scope, $modalInstance, orderService, order) {

        $scope.order = order;

        $scope.cancelOrder = function () {
            $modalInstance.close($scope.cancelReason);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };

        $scope.options = {
            valueField: 'Id',
            labelField: 'Name',
            create: false
        };
    }
]);