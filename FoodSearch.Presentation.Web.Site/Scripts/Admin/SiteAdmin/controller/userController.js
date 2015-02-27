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

            $scope.showDetails = function(index) {
                user.get({
                    Id: $scope.users.Items[index].Id
                }, function(u) {
                    var modal = $modal.open({
                        templateUrl: 'showUserDetailsModal',
                        controller: 'ShowUserDetailsController',
                        resolve: {
                            userData: function() {
                                return u;
                            }
                        }
                    });
                });
            }
        }
    ]);

    app.controller('ShowUserDetailsController', [
        '$scope', '$modalInstance', 'userData',
        function($scope, $modalInstance, userData) {
            $scope.user = userData;

            $scope.cancel = function () {
                $modalInstance.dismiss('cancel');
            }
        }
    ]);
})();