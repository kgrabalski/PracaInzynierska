using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Presentation.Web.Site.Models
{
    public class CreateOrderModel
    {
        [Range(1,2)]
        public int DeliveryType { get; set; }
        [Range(1,2)]
        public int PaymentType { get; set; }
        [Range(1, int.MaxValue)]
        public int AddressId { get; set; }
        public string FlatNumber { get; set; }
    }
}
