using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models
{
    public class OrderUser
    {
        [XmlAttribute("UserId")]
        public Guid UserId { get; set; }
        [XmlAttribute("FirstName")]
        public string FirstName { get; set; }
        [XmlAttribute("LastName")]
        public string LastName { get; set; }
        [XmlAttribute("Email")]
        public string Email { get; set; }
        [XmlAttribute("PhoneNumber")]
        public string PhoneNumber { get; set; }
    }
}
