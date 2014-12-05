$(document).ready(function () {

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
    google.maps.event.addListener(marker, 'click', function () {

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

function OpinionViewModel() {
    var self = this;

    function Opinion(u, c, r, d) {
        var o = this;
        o.UserName = ko.observable(u);
        o.Comment = ko.observable(c);
        o.Rating = ko.observable(r);
        o.Date = ko.observable(d);
    }

    self.Opinions = ko.observableArray();
    self.Rating = 0;
    self.Page = 0;
    self.RestaurantId = $("#opinionsTab").attr("data-rid");


}