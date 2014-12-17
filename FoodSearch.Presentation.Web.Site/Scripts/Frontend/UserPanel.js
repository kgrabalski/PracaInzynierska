$(document).ready(function() {
    ko.applyBindings(new UserOdersViewModel(), document.getElementById("ordersTab"));
});

function UserOdersViewModel() {
    var self = this;

    function Order(id, cd, dt, da, n, rid, a) {
        var o = this;
        o.OrderId = ko.observable(id);
        o.CreateDate = ko.observable(cd);
        o.DeliveryType = ko.observable(dt);
        o.DeliveryAddress = ko.observable(da);
        o.RestaurantName = ko.observable(n);
        o.RestaurantId = ko.observable(rid);
        o.OrderAmount = ko.observable(a);
        o.OrderItems = ko.observableArray();
        o.ShowDetails = ko.observable(false);

        o.ToogleShowItems = function() {
            var state = o.ShowDetails();
            state = !state;
            if (state == true) {
                self.getOrderItems(o);
            }
            o.ShowDetails(state);
        }

        o.RestaurantUrl = ko.computed(function() {
            return "/Restaurants/Page?restaurantId=" + o.RestaurantId();
        });
    }

    function OrderItem(n, q, p, t) {
        var i = this;
        i.DishName = ko.observable(n);
        i.Quantity = ko.observable(q);
        i.Price = ko.observable(p);
        i.Total = ko.observable(t);
    }

    self.Orders = ko.observableArray();
    self.Page = 0;

    self.getOrders = function() {
        $.ajax({
            url: "/User/Home/GetUserOrders",
            type: "POST",
            data: {
                "page": self.Page
            },
            success: function(orders) {
                self.Page++;
                for (var i = 0; i < orders.length; i++) {
                    var o = orders[i];
                    self.Orders.push(new Order(o.OrderId, o.CreateDate, o.DeliveryType, o.DeliveryAddress,
                        o.RestaurantName, o.RestaurantId, o.OrderAmount));
                }
            }
        });
    }
    self.getOrders();

    self.getOrderItems = function(order) {
        $.ajax({
            url: "/User/Home/GetUserOrderItems",
            type: "POST",
            data: {
                "orderId": order.OrderId()
            },
            success: function(orderItems) {
                order.OrderItems.removeAll();
                for (var i = 0; i < orderItems.length; i++) {
                    var oi = orderItems[i];
                    order.OrderItems.push(new OrderItem(oi.DishName, oi.Quantity, oi.Price, oi.Total));
                }
            }
        });
    }
}