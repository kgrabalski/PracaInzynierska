(function() {
    function BasketSummaryViewModel() {
        var self = this;

        function BasketItem(id, n, c, p, t) {
            var b = this;
            b.DishId = ko.observable(id);
            b.Name = ko.observable(n);
            b.Count = ko.observable(c);
            b.Price = ko.observable(p);
            b.Total = ko.observable(t);
        }

        self.BasketItems = ko.observableArray();
        self.Total = ko.observable("0.00 zł");
        self.DeliveryPrice = ko.observable("0.00 zł");
        self.TotalWithDelivery = ko.observable("0.00 zł");

        self.decrementItemCount = function(item) {
            $.ajax({
                url: "/Basket/Remove",
                type: "POST",
                data: {
                    'dishId': item.DishId()
                },
                success: self.updateBasket
            });
        }

        self.updateBasket = function() {
            $.ajax({
                url: "/Basket/GetBasketItems",
                type: "POST",
                success: function(basket) {
                    self.BasketItems.removeAll();
                    for (var x = 0; x < basket.Items.length; x++) {
                        var i = basket.Items[x];
                        self.BasketItems.push(new BasketItem(i.DishId, i.Name, i.Count, i.Price, i.Total));
                    }
                    self.Total(basket.Total);
                    self.DeliveryPrice(basket.DeliveryPrice);
                    self.TotalWithDelivery(basket.TotalWithDelivery);
                }
            });
        }

        self.updateBasket();
    }

    $(document).ready(function() {
        ko.applyBindings(new BasketSummaryViewModel());
    });
})();

