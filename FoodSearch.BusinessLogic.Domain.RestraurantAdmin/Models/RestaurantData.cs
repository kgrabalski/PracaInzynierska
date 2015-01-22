using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models
{
    public class RestaurantData
    {
        public Guid RestaurantId { get; set; }
        public string Name { get; set; }
        public bool IsOpen { get; set; }
        public decimal MinOrderAmount { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal FreeDeliveryFrom { get; set; }
    }
}
