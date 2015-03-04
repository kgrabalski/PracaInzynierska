using System.ComponentModel.DataAnnotations;

namespace FoodSearch.Service.Api.Areas.Order.Models
{
    public class OrderModel
    {
        [Range(1,2)]
        public int PaymentTypeId { get; set; }
        [Range(1,2)]
        public int DeliveryTypeId { get; set; }
        [Range(1, int.MaxValue)]
        public int AddressId { get; set; }
        public string FlatNumber { get; set; }
    }
}
