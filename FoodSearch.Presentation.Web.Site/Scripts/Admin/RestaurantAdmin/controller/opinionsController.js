(function() {
    var app = angular.module('FoodSearch.RestaurantAdmin');

    app.controller('OpinionsController', [
        '$scope', 'OpinionsService', 'RatingService',
        function($scope, opinion, ratingService) {

            function Star(s) {
                this.StarStyle = s;
            }

            $scope.opinions = opinion;

            $scope.rating = {};
            $scope.restaurantStars = new Array();

            ratingService.get({}, function(r) {
                $scope.rating = r;

                for (var i = 0; i < r.StarsCount; i++) {
                    $scope.restaurantStars.push({ style: "glyphicon glyphicon-star" });
                }
                for (var i = 0; i < (5 - r.StarsCount); i++) {
                    $scope.restaurantStars.push({ style: "glyphicon glyphicon-star-empty" });
                }

                $scope.$apply();
            });

            $scope.getMore = function() {
                $scope.opinions.Items = opinion.getOpinions({
                    page: $scope.opinions.Page++,
                    rating: $scope.opinions.Rating
                });
            }

            $scope.setRating = function(r) {
                $scope.opinions.Rating = r;
                $scope.opinions.Page = 0;
                $scope.getMore();
            }
        }
    ]);
})();