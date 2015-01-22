(function () {
    'use strict';
    var app = angular.module('FoodSearch.RestaurantAdmin');

    app.controller('OpeningHoursController', [
        '$scope', 'OpeningHourService', '$modal',
        function($scope, openingHour, $modal) {
            $scope.openingHours = openingHour;

            $scope.remove = function(index) {
                var modalRemove = $modal.open({
                    templateUrl: 'removeOpeningHourModal',
                    controller: 'RemoveOpeningHoursController'
                });

                modalRemove.result.then(function(result) {
                    if (result) {
                        $scope.openingHours.Items[index].$delete(function() {
                            $scope.openingHours.Items.splice(index, 1);
                        });
                    }
                });
            };

            $scope.addNew = function() {
                var modalAdd = $modal.open({
                    templateUrl: 'addOpeningHourModal',
                    controller: 'AddOpeningHoursController',
                    resolve: {
                        toAdd: function() {
                            return new openingHour();
                        }
                    }
                });

                modalAdd.result.then(function(newOh) {
                    newOh.$save(function(oh) {
                        $scope.openingHours.Items.push(oh);
                    });
                });
            }
        }
    ]);

    app.controller('RemoveOpeningHoursController', [
        '$scope', '$modalInstance',
        function($scope, $modalInstance) {
            $scope.cancel = function() {
                $modalInstance.dismiss('cancel');
            };
            $scope.remove = function() {
                $modalInstance.close(true);
            }
        }
    ]);

    app.controller('AddOpeningHoursController', [
        '$scope', '$modalInstance', 'toAdd',
        function($scope, $modalInstance, toAdd) {
            $scope.toAdd = toAdd;
            $scope.cancel = function() {
                $modalInstance.dismiss('cancel');
            };
            $scope.add = function() {
                $modalInstance.close($scope.toAdd);
            };

            $scope.daysList = [
                { Id: 1, Name: "Poniedziałek" },
                { Id: 2, Name: "Wtorek" },
                { Id: 3, Name: "Środa" },
                { Id: 4, Name: "Czwartek" },
                { Id: 5, Name: "Piątek" },
                { Id: 6, Name: "Sobota" },
                { Id: 7, Name: "Niedziela" }
            ];

            $scope.options = {
                valueField: 'Id',
                labelField: 'Name',
                create: false
            };
        }
    ]);
})();