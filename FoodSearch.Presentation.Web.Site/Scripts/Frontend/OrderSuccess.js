(function () {
    'use strict';
    function OrderSuccessViewModel() {
        var self = this;

        self.showLoader = ko.observable(true);
        self.greeting = ko.observable("Oczekiwanie na potwierdzenie z restauracji.");
        self.confirmed = ko.observable(false);
        self.cancelled = ko.observable(false);
        self.cancellationReason = ko.observable("");
        self.deliveryTime = ko.observable("");

        self.getDeliveryStatus = function() {
            $.ajax({
                url: "/Order/GetDeliveryStatus",
                type: "POST",
                data: {
                    'orderId': $("#orderId").data("order")
                },
                success: function(status) {
                    switch (status.ConfirmationStatus) {
                    case 1:
                        {
                            window.clearInterval(self.timer);
                            self.showLoader(false);
                            self.greeting("Smacznego!");
                            self.confirmed(true);
                            self.deliveryTime(status.DeliveryDate);
                            break;
                        }
                    case 2:
                        {

                            window.clearInterval(self.timer);
                            self.showLoader(false);
                            self.greeting("Zamówienie anulowane");
                            self.cancelled(true);
                            self.cancellationReason(status.CancellationReason);
                            
                            break;
                        }
                    }
                }
            });
        }

        self.timer = setInterval(self.getDeliveryStatus, 5000);
        

    }

    $(document).ready(function() {
        ko.applyBindings(new OrderSuccessViewModel());
    });
})();