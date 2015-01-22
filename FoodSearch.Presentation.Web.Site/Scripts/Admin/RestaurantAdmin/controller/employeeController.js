(function() {
    'use strict';

    var app = angular.module('FoodSearch.RestaurantAdmin');

    app.controller('EmployeesController', [
        '$scope', 'EmployeeService', '$modal',
        function($scope, employee, $modal) {
            $scope.employees = employee.query();

            $scope.addNew = function() {
                var modalAdd = $modal.open({
                    templateUrl: 'addEmployeeModal',
                    controller: 'AddEmployeeController',
                    resolve: {
                        toAdd: function() {
                            return new employee();
                        }
                    }
                });

                modalAdd.result.then(function(newEmployee) {
                    newEmployee.$save(function(e) {
                        $scope.employees.push(e);
                    });
                });
            }

            $scope.remove = function(index) {
                var modalRemove = $modal.open({
                    templateUrl: 'removeEmployeeModal',
                    controller: 'RemoveEmployeeController',
                    resolve: {
                        toRemove: function() {
                            return $scope.employees[index];
                        }
                    }
                });

                modalRemove.result.then(function(emp) {
                    emp.$delete(function() {
                        $scope.employees.splice(index, 1);
                    });
                });
            }

            $scope.resetPassword = function(index) {
                var modalReset = $modal.open({
                    templateUrl: 'resetPasswordModal',
                    controller: 'ResetPasswordController',
                    resolve: {
                        employee: function() {
                            return $scope.employees[index];
                        }
                    }
                });

                modalReset.result.then(function(emp) {
                    employee.resetPassword(emp);
                });
            }
        }
    ]);

    app.controller('AddEmployeeController', [
        '$scope', '$modalInstance', 'toAdd',
        function($scope, $modalInstance, toAdd) {
            $scope.toAdd = toAdd;

            $scope.cancel = function() {
                $modalInstance.dismiss('cancel');
            }

            $scope.add = function() {
                $modalInstance.close($scope.toAdd);
            }
        }
    ]);

    app.controller('RemoveEmployeeController', [
        '$scope', '$modalInstance', 'toRemove',
        function($scope, $modalInstance, toRemove) {
            $scope.toRemove = toRemove;

            $scope.cancel = function () {
                $modalInstance.dismiss('cancel');
            }

            $scope.remove = function () {
                $modalInstance.close($scope.toRemove);
            }
        }
    ]);

    app.controller('ResetPasswordController', [
        '$scope', '$modalInstance', 'employee',
        function($scope, $modalInstance, employee) {
            $scope.emp = employee;

            $scope.cancel = function () {
                $modalInstance.dismiss('cancel');
            }

            $scope.reset = function () {
                $modalInstance.close($scope.emp);
            }
        }
    ]);
})();