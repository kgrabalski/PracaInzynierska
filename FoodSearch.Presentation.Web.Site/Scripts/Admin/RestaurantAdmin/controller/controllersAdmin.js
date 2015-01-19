(function() {
    var app = angular.module('FoodSearch.RestaurantAdmin');

    app.controller('Restaurant', [
        '$scope',
        function($scope) {
            $scope.napis = "Restaurant";
        }
    ]);

    app.controller('Reports', [
        '$scope',
        function($scope) {
            $scope.napis = "Reports";
        }
    ]);

    app.controller('Employees', [
        '$scope',
        function($scope) {
            $scope.napis = "Employees";
        }
    ]);
})();