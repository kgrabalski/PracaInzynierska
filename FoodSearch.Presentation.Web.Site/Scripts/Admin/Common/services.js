(function () {
    'use strict';
    var services = angular.module('FoodSearch.Admin.Common');

    services.service('ErrorService', [
        '$q', '$rootScope', function($q, $rootScope) {
            return {
                responseError: function(error) {
                    $rootScope.$broadcast('resourceErrorEvent', error.status);
                    return $q.reject(error);
                }
            }
        }
    ]);

    services.config([
        '$httpProvider', function($httpProvider) {
            $httpProvider.interceptors.push('ErrorService');
        }
    ]);

    services.service('RangeService', [
        function() {
            return {
                dfOpen: false,
                openDf: function($event) {
                    $event.preventDefault();
                    $event.stopPropagation();
                    this.dfOpen = true;
                },
                dtOpen: false,
                openDt: function($event) {
                    $event.preventDefault();
                    $event.stopPropagation();
                    this.dtOpen = true;
                },
                dateFormat: 'dd.MM.yyyy',
                dateOptions: {
                    'starting-day': 1,
                    'max-mode': 'month'
                },
                convertDate: function(date) {
                    var day = date.getDate();
                    if (day < 10) day = "0" + day;
                    var month = (date.getMonth() + 1);
                    if (month < 10) month = "0" + month;
                    var year = date.getFullYear();

                    return day + "." + month + "." + year;
                }
            }
        }
    ]);
})();

