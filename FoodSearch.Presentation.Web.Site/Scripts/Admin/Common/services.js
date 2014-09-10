var services = angular.module('FoodSearch.Admin.Common');

services.service('ErrorService', ['$q', '$rootScope', function($q, $rootScope) {
    return {
        responseError: function (error) {
            $rootScope.$broadcast('resourceErrorEvent', error.status);
            return $q.reject(error);
        }
    }
}]);
services.config([
    '$httpProvider', function($httpProvider) {
        $httpProvider.interceptors.push('ErrorService');
    }
]);