function OpeningHoursViewModel() {
    var self = this;
    
    function OpeningHour(id, d, tf, tt) {
        this.OpeningHourId = ko.observable(id);
        this.Day = ko.observable(d);
        this.TimeFrom = ko.observable(tf);
        this.TimeTo = ko.observable(tt);
    }

    self.OpeningHours = ko.observableArray();

    self.GetOpeningHours = function() {
        $.ajax({
            url: "/RestaurantAdmin/OpeningHours/GetOpeningHours",
            type: "POST",
            dataType: "json",
            success: function (openings) {
                self.OpeningHours.removeAll();
                for (var i = 0; i < openings.length; i++) {
                    var o = openings[i];
                    self.OpeningHours.push(new OpeningHour(o.OpeningId, o.Day, o.TimeFrom, o.TimeTo));
                }
            }
        });
    };

    $("#addOpeningHour").click(function() {
        $("#addOHDay").selectize({
            create: false
        });

        $("#addOHForm").unbind("submit").submit(function(e) {
            e.preventDefault();
            var day = $("#addOHDay").val();
            var tf = $("#addOHTimeFrom").val();
            var tt = $("#addOHTimeTo").val();
            $.ajax({
                url: "/RestaurantAdmin/OpeningHours/Create",
                type: "POST",
                data: {
                    'day': day,
                    'timeFrom': tf,
                    'timeTo': tt
                },
                dataType: "json",
                success: function(oh) {
                    if (oh != null && oh.OpeningId != -1) {
                        self.OpeningHours.push(new OpeningHour(oh.OpeningId, oh.Day, oh.TimeFrom, oh.TimeTo));
                        $("#addOpeningHourModal").modal("hide");
                    }
                }
            });
        });

        

        $("#addOpeningHourModal .btn-primary").unbind("click").click(function() {
            $("#addOHForm").submit();
        });

        $("#addOpeningHourModal").modal();
        $("#addOHTimeFrom, #addOHTimeTo").timepicker({
            showInputs: false,
            showMeridian: false,
            showSeconds: false,
            template: 'dropdown',
            minuteStep: 30
        }).focus(function() {
            $(this).timepicker('showWidget');
        });
    });

    self.GetOpeningHours();

    self.DeleteOpeningHour = function (oh) {
        $("#deleteOpeningHourModal .btn-primary").unbind("click").click(function() {
            $.ajax({
                url: "/RestaurantAdmin/OpeningHours/Delete",
                type: "POST",
                dataType: "json",
                data: {
                    'openingHourId': oh.OpeningHourId()
                },
                success: function(response) {
                    if (response == "ok") {
                        self.OpeningHours.remove(oh);
                    }
                    $("#deleteOpeningHourModal").modal("hide");
                }
            });
        });
        $("#deleteOpeningHourModal").modal();
    };
}