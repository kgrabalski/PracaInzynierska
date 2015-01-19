(function() {
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

        self.changePassword = function() {
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
                error: function() {
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

        //delivery addresses


        function DeliveryAddress(id, aid, cid, c, sid, s, sn, fn) {
            this.Id = id;
            this.AddressId = aid;
            this.CityId = cid;
            this.City = c;
            this.StreetId = sid;
            this.Street = s;
            this.StreetNumber = sn;
            this.FlatNumber = fn;
            this.DisplayText = this.City + ", " + this.Street + " " + this.StreetNumber;
            if (this.FlatNumber != "") {
                this.DisplayText += "/" + this.FlatNumber;
            }
        }

        self.DeliveryAddresses = ko.observableArray();

        self.GetDeliveryAddresses = function() {
            $.ajax({
                url: "/Home/GetDeliveryAddresses",
                type: "POST",
                success: function(response) {
                    for (var i = 0; i < response.length; i++) {
                        var a = response[i];
                        self.DeliveryAddresses.push(
                            new DeliveryAddress(a.Id, a.AddressId, a.CityId, a.City,
                                a.StreetId, a.Street, a.StreetNumber, a.FlatNumber));
                    }
                }
            });
        }
        self.GetDeliveryAddresses();

        self.RemoveAddress = function(da) {
            $.ajax({
                url: "/User/Home/DeleteUserDeliveryAddress",
                type: "POST",
                data: {
                    'deliveryAddressId': da.Id
                },
                success: function(response) {
                    if (response == true) {
                        self.DeliveryAddresses.remove(da);
                    }
                }
            });
        };


        self.selectCity = $("#selectCity").selectize({
            valueField: 'Id',
            labelField: 'Name',
            sortField: 'Name',
            create: false,
            onChange: function(value) {
                self.selectStreet.clearOptions();
                self.selectNumber.clearOptions();
                if (value != "") {
                    self.selectStreet.enable();
                } else {
                    self.selectStreet.disable();
                    self.selectNumber.disable();
                }
            }
        })[0].selectize;

        self.selectCity.load(function(callback) {
            $.ajax({
                url: "/Address/Cities",
                type: "POST",
                dataType: "json",
                success: function(cities) {
                    callback(cities);
                }
            });
        });

        self.selectStreet = $("#selectStreet").selectize({
            valueField: 'Id',
            labelField: 'Name',
            sortField: 'Name',
            searchField: ['Name'],
            create: false,
            load: function(query, callback) {
                if (query.length < 3) return callback();
                $.ajax({
                    url: "/Address/Streets",
                    data: {
                        'cityId': $("#selectCity").val(),
                        'query': query
                    },
                    type: "POST",
                    dataType: 'json',
                    success: function(str) {
                        callback(str);
                    }
                });
            },
            onChange: function(value) {
                self.selectNumber.disable();
                self.selectNumber.clearOptions();
                self.selectNumber.load(function(callback) {
                    $.ajax({
                        url: "/Address/StreetNumbers",
                        data: { 'streetId': value },
                        type: "POST",
                        dataType: 'json',
                        success: function(numbers) {
                            self.selectNumber.enable();
                            callback(numbers);
                        }
                    });
                });
            }
        })[0].selectize;
        self.selectStreet.disable();

        self.selectNumber = $("#selectNumber").selectize({
            valueField: "Id",
            labelField: "Number",
            sortField: "Number",
            searchField: ["Number"],
            create: false
        })[0].selectize;

        self.selectNumber.disable();

        self.cleanAddAddressModal = function() {
            $("#addAddress").modal("hide");
            self.selectStreet.clearOptions();
            self.selectNumber.clearOptions();
            self.selectNumber.disable();
            $("#flatNumber").val("");
        }

        self.CreateAddressModal = function() {
            $("#addAddress").modal();
        }

        self.CreateAddress = function() {
            $.ajax({
                url: "/User/Home/CreateUserDeliveryAddress",
                type: "POST",
                data: {
                    'addressId': $("#selectNumber").val(),
                    'flatNumber': $("#flatNumber").val()
                },
                success: function(a) {
                    self.cleanAddAddressModal();

                    for (var i = 0; i < self.DeliveryAddresses().length; i++) {
                        var x = self.DeliveryAddresses()[i];
                        if (x.Id == a.Id) return;
                    }

                    self.DeliveryAddresses.push(new DeliveryAddress(a.Id, a.AddressId, a.CityId, a.City,
                        a.StreetId, a.Street, a.StreetNumber, a.FlatNumber));
                }
            });
        }
    }

    $(document).ready(function() {
        ko.applyBindings(new UserOdersViewModel(), document.getElementById("ordersTab"));
        ko.applyBindings(new UserInfoViewModel(), document.getElementById("infoTab"));
    });

})();