using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models
{
    [XmlTypeAttribute(AnonymousType = true)]
    public class RestaurantOrder
    {
        public OrderUser User { get; set; }
        public string DeliveryAddress { get; set; }
        [XmlArrayItem("Item", IsNullable = false)]
        public OrderItem[] Items { get; set; }
        [XmlAttribute()]
        public string OrderId { get; set; }
        [XmlAttribute()]
        public string CreateDate { get; set; }
    }
}
