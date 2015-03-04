using System.Xml.Serialization;

namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models
{
    [XmlType(AnonymousType = true)]
    public class OrderUser
    {
        [XmlAttribute()]
        public string UserId { get; set; }
        [XmlAttribute()]
        public string FirstName { get; set; }
        [XmlAttribute()]
        public string LastName { get; set; }
        [XmlAttribute()]
        public string Email { get; set; }
        [XmlAttribute()]
        public string PhoneNumber { get; set; }
    }
}
