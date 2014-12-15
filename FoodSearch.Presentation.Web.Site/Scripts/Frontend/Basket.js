$(document).ready(function() {
    ko.applyBindings(new BasketViewModel());
});

function BasketViewModel() {
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
    self.TotalValue = ko.observable("0.00 zł");

    self.incrementItemCount = function(item) {
        self.addItemToBasket(item.DishId());
    }

    self.decrementItemCount = function(item) {
        $.ajax({
            url: "/Basket/Remove",
            type: "POST",
            data: {
                'dishId': item.DishId()
            },
            success: self.getBasketItems
        });
    }

    self.addToBasket = function(data, e) {
        self.addItemToBasket(e.target.id);
    }

    self.addItemToBasket = function(dishId) {
        $.ajax({
            url: "/Basket/Add",
            type: "POST",
            data: {
                'dishId': dishId
            },
            success: self.getBasketItems
        });
    }

    self.getBasketItems = function() {
        $.ajax({
            url: "/Basket/GetBasketItems",
            type: "POST",
            success: function(basket) {
                self.TotalValue(basket.Total);
                self.BasketItems.removeAll();
                for (var x = 0; x < basket.Items.length; x++) {
                    var i = basket.Items[x];
                    self.BasketItems.push(new BasketItem(i.DishId, i.Name, i.Count, i.Price, i.Total));
                }
            }
        });
    }

    self.getBasketItems();

}