function CuisinesViewModel() {
    var self = this;
    
    function Cuisine(id, n) {
        this.CuisineId = ko.observable(id);
        this.Name = ko.observable(n);
    }

    self.AvailableCuisines = ko.observableArray();
    self.RestaurantCuisines = ko.observableArray();

    self.GetAvailableCuisines = function() {
        $.ajax({
            url: "/RestaurantAdmin/Cuisines/GetCuisines",
            type: "POST",
            dataType: "json",
            success: function(cs) {
                if (cs != null) {
                    self.AvailableCuisines.removeAll();
                    for (var i = 0; i < cs.length; i++) {
                        self.AvailableCuisines.push(new Cuisine(cs[i].CuisineId, cs[i].Name));
                    }
                    $("#addRCName").selectize({
                        create: false
                    });
                }
            }
        });
    };
    
    self.GetRestaurantCuisines = function () {
        $.ajax({
            url: "/RestaurantAdmin/Cuisines/GetRestaurantCuisines",
            type: "POST",
            dataType: "json",
            success: function (cs) {
                if (cs != null) {
                    self.RestaurantCuisines.removeAll();
                    for (var i = 0; i < cs.length; i++) {
                        self.RestaurantCuisines.push(new Cuisine(cs[i].CuisineId, cs[i].Name));
                    }
                }
            }
        });
    };

    self.RemoveRestaurantCuisine = function(cs) {
        $.ajax({
            url: "/RestaurantAdmin/Cuisines/RemoveRestaurantCuisine",
            type: "POST",
            dataType: "json",
            data: {
                'cuisineId': cs.CuisineId()
            },
            success: function (response) {
                if (response == "ok") {
                    self.RestaurantCuisines.remove(cs);
                }
            }
        });
    };

    self.GetCuisineById = function(id) {
        for (var i = 0; i < self.AvailableCuisines().length; i++) {
            var c = self.AvailableCuisines()[i];
            if (c.CuisineId() == id) {
                return c;
            }
        }
        return null;
    };

    self.AddRestaurantCuisine = function() {
        $("#addRCForm").unbind("submit").submit(function(e) {
            e.preventDefault();
            var cid = $("#addRCName").val();
            $.ajax({
                url: "/RestaurantAdmin/Cuisines/AddRestaurantCuisine",
                type: "POST",
                dataType: "json",
                data: {
                    'cuisineId': cid
                },
                success: function (response) {
                    if (response == true) {
                        var cs = self.GetCuisineById(cid);
                        self.RestaurantCuisines.push(cs);
                        self.AvailableCuisines(cs);
                        $("#addRestaurantCuisineModal").modal("hide");
                    }
                }
            });
        });
        
        $("#addRestaurantCuisineModal .btn-primary").unbind("click").click(function () {
            $("#addRCForm").submit();
        });
        var selectize = $("#addRCName")[0].selectize;
        selectize.clear();
        $("#addRestaurantCuisineModal").modal();
    };

    $("#addRestaurantCuisine").click(self.AddRestaurantCuisine);
    self.GetAvailableCuisines();
    self.GetRestaurantCuisines();
}