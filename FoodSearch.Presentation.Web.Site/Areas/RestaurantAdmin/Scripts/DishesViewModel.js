function DishesViewModel() {
    var self = this;
    
    function Dish(id, n, dgid, dg, p) {
        this.DishId = ko.observable(id);
        this.DishName = ko.observable(n);
        this.DishGroupId = ko.observable(dgid);
        this.DishGroup = ko.observable(dg);
        this.Price = ko.observable(p);
    }
    
    function DishGroup(id, n) {
        this.DishGroupId = ko.observable(id);
        this.Name = ko.observable(n);
    }

    self.Dishes = ko.observableArray();

    self.DishGroups = ko.observableArray();

    self.GetDishes = function() {
        $.ajax({           
            url: "/RestaurantAdmin/Dishes/GetDishes",
            type: "POST",
            dataType: "json",
            success: function(dishes) {
                for (var i = 0; i < dishes.length; i++) {
                    var d = dishes[i];
                    self.Dishes.push(new Dish(d.DishId, d.DishName, d.DishGroupId, d.DishGroup, d.Price));
                }
            }
        });
    };

    self.CreateDish = function () {
        $.ajax({
            url: "/RestaurantAdmin/DishGroups/GetDishGroups",
            type: "POST",
            dataType: "json",
            success: function (dg) {
                if (dg != null) {
                    self.DishGroups.removeAll();
                    for (var i = 0; i < dg.length; i++) {
                        self.DishGroups.push(new DishGroup(dg[i].DishGroupId, dg[i].Name));
                    }
                    
                    $("#addDishDG").selectize({
                        create: false
                    });

                    $("#addDishForm").unbind("submit").submit(function (e) {
                        e.preventDefault();
                        var name = $("#addDishName").val();
                        var gid = $("#addDishDG").val();
                        var price = $("#addDishPrice").val();
                        $.ajax({
                            url: "/RestaurantAdmin/Dishes/Create",
                            type: "POST",
                            dataType: "json",
                            data: {
                                'dishName': name,
                                'dishGroupId': gid,
                                'price': price
                            },
                            success: function (d) {
                                if (d != null) {
                                    self.Dishes.push(new Dish(d.DishId, d.DishName, d.DishGroupId, d.DishGroup, d.Price));
                                    $("#addDishModal").modal("hide");
                                }
                            }
                        });
                    });

                    $("#addDishModal .btn-primary").unbind("click").click(function () {
                        $("#addDishForm").submit();
                    });
                    $("#addDishModal").modal();
                }
            }
        });
    };

    $("#addDish").click(self.CreateDish);
    self.GetDishes();
    
}