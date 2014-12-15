$(document).ready(function() {
    var formBinded = false;
    var cuisineId = new Array();

    $("#searchForm").submit(function(e) {
        e.preventDefault();
        if (!formBinded) {
            formBinded = true;
            $("#searchForm").unbind("submit");
            ko.applyBindings(new searchRestaurantViewModel());
            $("#searchForm").submit();
        }
    });

    $("#allCuisines").change(function () {
        if ($(this).prop('checked')) {
            cuisineId = new Array();
            $(".cuisine").prop('checked', false);
            $("#searchForm").submit();
        }
    });

    $(".cuisine").change(function () {
        var id = $(this).val();
        if ($(this).prop('checked')) {
            $("#allCuisines").prop('checked', false);
            if (cuisineId.indexOf(id) == -1) cuisineId.push(id);
        } else {
            if (cuisineId.indexOf(id) != -1) {
                cuisineId.splice(cuisineId.indexOf(id), 1);
            }
            if (cuisineId.length == 0) {
                $("#allCuisines").prop('checked', true);
            }
        }
        $("#searchForm").submit();
    });

    function searchRestaurantViewModel() {
        var self = this;

        self.AddressId = $("#searchForm").attr("data-addressId");
        self.Restaurants = ko.observableArray();

        function Star(s) {
            this.StarStyle = ko.observable(s);
        }

        function Restaurant(id, n, l, tf, tt, s, no, mo, rr, sc, uv) {
            var r = this;
            r.RestaurantId = ko.observable(id);
            r.RestaurantName = ko.observable(n);
            r.TimeFrom = ko.observable(tf);
            r.TimeTo = ko.observable(tt);
            r.Street = ko.observable(s);
            r.Number = ko.observable(no);
            r.MinimumOrder = ko.observable(mo);
            r.RestaurantRating = ko.observable(rr);
            r.StarsCountUsersVoted = ko.observable(uv);
            r.UsersVoted = ko.observable(uv);

            r.Stars = ko.observableArray();
            for (var i = 0; i < sc; i++) {
                r.Stars.push(new Star("glyphicon glyphicon-star"));
            }
            for (var i = 0; i < (5 - sc); i++) {
                r.Stars.push(new Star("glyphicon glyphicon-star-empty"));
            }

            r.MenuUrl = ko.observable("/Home/RestaurantDishes?restaurantId=" + id);
            r.LogoUrl = ko.observable("/Home/GetImage?imageId=" + l);
        }

        self.DoSearch = function() {
            $.ajax({
                url: '/Home/GetRestaurants',
                type: 'POST',
                data: {
                    'addressId': self.AddressId,
                    'cuisineId': cuisineId
                },
                success: function(restaurants) {
                    self.Restaurants.removeAll();
                    for (var i = 0; i < restaurants.length; i++) {
                        var r = restaurants[i];
                        self.Restaurants.push(new Restaurant(
                            r.RestaurantId,
                            r.RestaurantName,
                            r.LogoId,
                            r.TimeFrom,
                            r.TimeTo,
                            r.Street,
                            r.Number,
                            r.MinimumOrder,
                            r.RestaurantRating,
                            r.StarsCount,
                            r.UsersVoted
                        ));
                    }
                }
            });
        };
    }
});