using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models
{
    public class RestaurantOrder
    {
        [XmlAttribute("OrderId")]
        public Guid OrderId { get; set; }
        [XmlElement("User")]
        public OrderUser OrderUser { get; set; }
        public string DeliveryAddress { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
