(function () {
    'use strict';
    var app = angular.module('FoodSearch.SiteAdmin');

    app.controller('DailySFinancialReportController', [
       '$scope', 'DailySFinancialReportService', 'RangeService',
       function ($scope, dfReport, range) {
           $scope.report = [];
           $scope.range = range;

           $scope.showReport = function () {
               $scope.report = dfReport.query({
                   DateFrom: range.convertDate($scope.dateFrom),
                   DateTo: range.convertDate($scope.dateTo)
               });
           }
       }
    ]);

    app.controller('MonthlySFinancialReportController', [
        '$scope', 'MonthlySFinancialReportService', 'RangeService',
        function ($scope, mfReport, range) {
            $scope.report = [];
            $scope.range = range;

            $scope.showReport = function () {
                $scope.report = mfReport.query({
                    DateFrom: range.convertDate($scope.dateFrom),
                    DateTo: range.convertDate($scope.dateTo)
                });
            }
        }
    ]);
})();