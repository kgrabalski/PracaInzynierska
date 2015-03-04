using System.Xml.Serialization;

namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models
{
    /// <remarks/>
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "Orders")]
    public class RestaurantOrders
    {
        /// <remarks/>
        [XmlElement("Order")]
        public RestaurantOrder[] Orders { get; set; }
    }
}
