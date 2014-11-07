using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Service.Api.Areas.Order.Models
{
    public class OrderModel
    {
        public int PaymentTypeId { get; set; }
        public int DeliveryTypeId { get; set; }
    }
}
