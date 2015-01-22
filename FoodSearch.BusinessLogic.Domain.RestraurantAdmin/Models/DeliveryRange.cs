using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models
{
    public class DeliveryRange
    {
        public bool HasDeliveryRadius { get; set; }
        public decimal DeliveryRadius { get; set; }
        public string Polygon { get; set; }
    }
}
