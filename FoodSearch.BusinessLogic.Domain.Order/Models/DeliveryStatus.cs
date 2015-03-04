
namespace FoodSearch.BusinessLogic.Domain.Order.Models
{
    public class DeliveryStatus
    {
        public ConfirmationStatus ConfirmationStatus { get; set; }
        public string DeliveryDate { get; set; }
        public string CancellationReason { get; set; }
    }
}
