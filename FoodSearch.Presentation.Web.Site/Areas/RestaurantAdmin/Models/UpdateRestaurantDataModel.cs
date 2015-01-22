using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Models
{
    public class UpdateRestaurantDataModel
    {
        public string Name { get; set; }
        public bool IsOpen { get; set; }
        public decimal MinOrderAmount { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal FreeDeliveryFrom { get; set; }
    }
}
