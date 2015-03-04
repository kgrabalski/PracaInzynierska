using System.Xml.Serialization;

namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models
{
    [XmlType(AnonymousType = true)]
    public class OrderItem
    {
        [XmlAttribute()]
        public string Name { get; set; }
        [XmlAttribute()]
        public int Quantity { get; set; }
        [XmlAttribute()]
        public decimal Price { get; set; }
    }
}
