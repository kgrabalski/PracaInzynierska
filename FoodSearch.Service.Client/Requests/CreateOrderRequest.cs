
namespace FoodSearch.Service.Client.Requests
{
    public class CreateOrderRequest
    {
        public int PaymentTypeId { get; set; }
        public int DeliveryTypeId { get; set; }
        public int AddressId { get; set; }
        public string FlatNumber { get; set; }
    }
}

