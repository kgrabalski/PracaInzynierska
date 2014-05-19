function DishGroupsViewModel() {
    var self = this;
    
    function DishGroup(id, n) {
        this.DishGroupId = ko.observable(id);
        this.Name = ko.observable(n);
    }

    self.DishGroups = ko.observableArray();

    self.GetDishGroups = function() {
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
                }
            }
        });
    };

    self.DeleteDishGroup = function(dg) {
        $.ajax({
            url: "/RestaurantAdmin/DishGroups/Delete",
            type: "POST",
            dataType: "json",
            data: {
                'dishGroupId': dg.DishGroupId()
            },
            success: function (response) {
                if (response == "ok") {
                    self.DishGroups.remove(dg);
                }
            }
        });
    };

    self.CreateDishGroup = function() {
        $("#addDGForm").unbind("submit").submit(function(e) {
            e.preventDefault();
            var name = $("#addDGName").val();
            $.ajax({
                url: "/RestaurantAdmin/DishGroups/Create",
                type: "POST",
                dataType: "json",
                data: {
                    'groupName': name
                },
                success: function (groupId) {
                    self.DishGroups.push(new DishGroup(groupId, name));
                    $("#addDishGropModal").modal("hide");
                }
            });
        });
        
        $("#addDishGropModal .btn-primary").unbind("click").click(function () {
            $("#addDGForm").submit();
        });
        
        $("#addDishGropModal").modal();
    };

    $("#addDishGroup").click(self.CreateDishGroup);
    self.GetDishGroups();
}