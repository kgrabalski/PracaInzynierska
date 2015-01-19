(function () {
    'use strict';
    $(document).ready(function () {
        var socket = $.connection.FoodSearchRestaurantAdmin;
        socket.client.newOrder = function() {
            $(".notifications").notify({
                type: 'info',
                closable: true,
                message: {
                    html: '<a href="/RestaurantAdmin#/newOrders">Złożono nowe zamówienie!</a>'
                },
                fadeOut: { enabled: true, delay: 10000 }
            }).show();
        }
        $.connection.hub.start().done(function () {
            $.ajax({
                url: '/RestaurantAdmin/Home/GetRestaurantId',
                type: 'POST',
                dataType: 'json',
                success: function (id) {
                    socket.server.registerRestaurant(id);
                }
            });
        });
    });
})();
