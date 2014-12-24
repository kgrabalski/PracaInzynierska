$(document).ready(function() {
    ko.applyBindings(new UserOdersViewModel(), document.getElementById("ordersTab"));
    ko.applyBindings(new UserInfoViewModel(), document.getElementById("infoTab"));
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

function UserInfoViewModel() {
    var self = this;

    self.OldPassword = ko.observable();
    self.NewPassword = ko.observable();
    self.RepeatNewPassword = ko.observable();
    self.ShowAlert = ko.observable(false);

    self.inputChanged = function(oldValue, newValue) {
        if (self.NewPassword() != self.RepeatNewPassword()) {
            self.ShowAlert(true);
        } else self.ShowAlert(false);
    }
    self.NewPassword.subscribe(self.inputChanged);
    self.RepeatNewPassword.subscribe(self.inputChanged);

    self.changePasswordModal = function() {
        $("#changePassword").modal();
    }

    self.changePassword = function () {
        if (self.ShowAlert() == true) return;
        $.ajax({
            url: "/Account/ChangePassword",
            type: "POST",
            dataType: "json",
            data: {
                "OldPassword": self.OldPassword(),
                "NewPassword": self.NewPassword(),
                "RepeatNewPassword": self.RepeatNewPassword()
            },
            success: function(response) {
                if (response.Result == true) {
                    window.location = "/Account/Logout";
                } else {
                    self.cleanModal();
                    $("#changePasswordAlert").show();
                }
            },
            error: function () {
                self.cleanModal();
                $("#changePasswordAlert").show();
            }
        });
    }

    self.cleanModal = function() {
        $("#changePassword").modal("hide");
        self.OldPassword("");
        self.NewPassword("");
        self.RepeatNewPassword("");
        self.ShowAlert(false);
    }
}