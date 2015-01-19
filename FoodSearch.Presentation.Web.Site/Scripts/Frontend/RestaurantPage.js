(function() {


    function getDateTime() {
        var date = new Date();
        var day = date.getDay();
        if (day < 10) day = "0" + day;
        var month = date.getMonth() + 1;
        if (month < 10) month = "0" + month;
        var year = date.getFullYear();
        return day + "." + month + "." + year;
    }

    function OpinionViewModel() {
        var self = this;

        function Opinion(u, c, r, d) {
            var o = this;
            o.UserName = ko.observable(u);
            o.Comment = ko.observable(c);
            o.Rating = ko.observable(r);
            o.Date = ko.observable(d);
            o.IconUrl = ko.observable();

            switch (r) {
            case 1:
            case 2:
                o.IconUrl("/Images/ic_action_bad.png");
                break;
            case 3:
                o.IconUrl("/Images/ic_action_avg.png");
                break;
            case 4:
            case 5:
                o.IconUrl("/Images/ic_action_good.png");
                break;
            }
        }

        function Star(s) {
            this.StarStyle = ko.observable(s);
        }

        //opinions
        self.Opinions = ko.observableArray();
        self.Rating = 0;
        self.Page = 0;
        self.RestaurantId = $("#opinionsTab").attr("data-rid");

        //restaurant rating
        self.UsersVoted = ko.observable();
        self.TotalRating = ko.observable();
        self.RestaurantStars = ko.observableArray();
        self.Percentage1Star = ko.observable(0);
        self.Percentage2Star = ko.observable(0);
        self.Percentage3Star = ko.observable(0);
        self.Percentage4Star = ko.observable(0);
        self.Percentage5Star = ko.observable(0);

        self.RestaurantStars = ko.observableArray();

        //add opinion
        self.NewRating = ko.observable();
        self.NewComment = ko.observable();
        self.NewVisible = ko.observable(true);

        //fetch opinions with ratings
        self.getOpinions1 = function() {
            self.getOpinions(1, false);
        }

        self.getOpinions2 = function() {
            self.getOpinions(2, false);
        }

        self.getOpinions3 = function() {
            self.getOpinions(3, false);
        }

        self.getOpinions4 = function() {
            self.getOpinions(4, false);
        }

        self.getOpinions5 = function() {
            self.getOpinions(5, false);
        }

        self.getOpinionsAll = function() {
            self.getOpinions(0, false);
        }

        self.getMoreOpinions = function() {
            self.getOpinions(-1, true);
        }

        //fetch opinion
        self.getOpinions = function(rating, append) {
            if (!append) {
                self.Rating = rating;
                self.Page = 0;
            }

            $.ajax({
                url: "/Restaurants/GetOpinions",
                type: "POST",
                data: {
                    "restaurantId": self.RestaurantId,
                    "rating": self.Rating,
                    "page": self.Page
                },
                success: function(opinions) {
                    self.Page++;
                    if (!append) self.Opinions.removeAll();
                    for (var i = 0; i < opinions.length; i++) {
                        var o = opinions[i];
                        self.Opinions.push(new Opinion(o.UserName, o.Comment, o.Rating, o.Date));
                    }
                }
            });
        }

        //add user opinion
        self.addOpinion = function() {
            $.ajax({
                url: "/Restaurants/AddOpinion",
                type: "POST",
                data: {
                    "RestaurantId": self.RestaurantId,
                    "Rating": self.NewRating(),
                    "Comment": self.NewComment()
                },
                success: function(data) {
                    self.NewVisible(false);
                    self.getRestaurantRating();
                    var opinion = new Opinion("Ja", self.NewComment(), self.NewRating(), getDateTime());
                    self.Opinions.unshift(opinion);
                },
                error: function() {
                    alert("blad");
                    self.NewRating("");
                    self.NewComment("");
                }
            });
        }

        self.getRestaurantRating = function() {
            $.ajax({
                url: "/Restaurants/GetRestaurantRating",
                type: "POST",
                data: {
                    "restaurantId": self.RestaurantId
                },
                success: function(r) {
                    self.UsersVoted(r.UsersVoted);
                    self.TotalRating(r.TotalRating);
                    self.Percentage1Star(r.Percentage1Star);
                    self.Percentage2Star(r.Percentage2Stars);
                    self.Percentage3Star(r.Percentage3Stars);
                    self.Percentage4Star(r.Percentage4Stars);
                    self.Percentage5Star(r.Percentage5Stars);

                    self.RestaurantStars.removeAll();
                    for (var i = 0; i < r.StarsCount; i++) {
                        self.RestaurantStars.push(new Star("glyphicon glyphicon-star"));
                    }
                    for (var i = 0; i < (5 - r.StarsCount); i++) {
                        self.RestaurantStars.push(new Star("glyphicon glyphicon-star-empty"));
                    }
                }
            });
        }

        self.getRestaurantRating();
        self.getOpinions(0);

        var stars = $(".starrr").starrr();
        $(".starrr").on("starrr:change", function(e, value) {
            if (typeof value !== 'undefined') {
                self.NewRating(value);
            } else {
                self.NewRating(1);
            }
        });
    }

    $(document).ready(function() {

        //mapa
        var options = {
            zoom: 3,
            center: new google.maps.LatLng(52.238, 21.012),
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            scrollwheel: false,
            streetViewControl: false,
            maxZoom: 18,
            minZoom: 12,
            mapTypeControlOptions: {
                mapTypeIds: [google.maps.MapTypeId.ROADMAP]
            },
            noClear: true
        };
        var map = new google.maps.Map(document.getElementById("map"), options);
        var bounds = new google.maps.LatLngBounds();

        var lat = $("#map").attr("data-lat");
        var lon = $("#map").attr("data-lon");
        var address = $("#map").attr("data-address");
        var name = $("#map").attr("data-title");

        var place = new google.maps.LatLng(lat, lon);
        var marker = new google.maps.Marker({
            position: place,
            map: map,
            title: name
        });
        var markers = new Array();
        markers.push(marker);

        var infoWindow = new google.maps.InfoWindow();
        google.maps.event.addListener(marker, 'click', function() {

            var content = " <div class=\"objectPageInfo\">" +
                "               <h2>" + name + "</h2>" +
                "               <p>" + address + "</p>" +
                "           </div>";
            infoWindow.setContent(content);
            infoWindow.open(map, marker);
            google.maps.event.removeEventListener(marker, "click");
        });

        bounds.extend(place);
        map.fitBounds(bounds);

        //opinie
        ko.applyBindings(new OpinionViewModel(), document.getElementById("opinionsTab"));
    });
})();