using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.BusinessLogic.Domain.Order.Models
{
    public class DeliveryStatus
    {
        public ConfirmationStatus ConfirmationStatus { get; set; }
        public string DeliveryDate { get; set; }
        public int MinutesLeft { get; set; }
    }
}
