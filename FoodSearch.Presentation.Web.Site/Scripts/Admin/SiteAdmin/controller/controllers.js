var app = angular.module('FoodSearch.SiteAdmin');

app.controller('Dashboard', ['$scope',
    function ($scope) {
        $scope.napis = "Dashboard";
    }
]);

app.controller('Restaurants', ['$scope',
    function($scope) {
        $scope.napis = "Restaurants";
    }
]);

app.controller('Users', ['$scope',
    function($scope) {
        $scope.napis = "Users";
    }
])