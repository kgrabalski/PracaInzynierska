(function() {
    'use strict';
    var app = angular.module('FoodSearch.SiteAdmin');

    app.controller('UsersController', [
        '$scope', 'UserService', '$modal',
        function($scope, user, $modal) {
            $scope.users = user;
            $scope.page = 0;

            $scope.changeState = function(index) {
                var u = $scope.users.Items[index];
                user.changeState({
                    Id: u.Id,
                    IsActive: !u.UserActive
                }, function() {
                    u.UserActive = !u.UserActive;
                });
            }

            $scope.search = function () {
                $scope.page = 0;
                $scope.users.Items = user.query({
                    Query: $scope.query,
                    Page: $scope.page
                });
            }

            $scope.clearSearch = function() {
                $scope.query = "";
                $scope.search();
            }

            $scope.getMore = function() {
                user.query({
                    Query: $scope.query,
                    Page: ++$scope.page
                }, function (result) {
                    $scope.users.Items.push.apply($scope.users.Items, result);
                });
            }
        }
    ]);
})();