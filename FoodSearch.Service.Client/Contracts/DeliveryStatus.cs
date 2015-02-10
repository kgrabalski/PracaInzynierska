using System;

namespace FoodSearch.Service.Client.Contracts
{
    public class DeliveryStatus
    {
        public int ConfirmationStatus { get; set; }
        public string DeliveryDate { get; set; }
        public string CancellationReason { get; set; }
    }
}

