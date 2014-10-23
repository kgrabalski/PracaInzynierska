using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Service.Api.Areas.Order.Models
{
    public class DeliveryPriceModel
    {
        public Guid RestaurantId { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
