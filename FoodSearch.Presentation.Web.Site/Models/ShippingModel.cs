using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.Order.Models;
using FoodSearch.BusinessLogic.Domain.User.Models;

namespace FoodSearch.Presentation.Web.Site.Models
{
    public class ShippingModel
    {
        public DeliveryAddress DeliveryAddress { get; set; }
        public IEnumerable<DeliveryType> DeliveryTypes { get; set; }
        public IEnumerable<PaymentType> PaymentTypes { get; set; }
        public Guid RestaurantId { get; set; }
    }
}
