using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models
{
    [XmlRoot("Item")]
    public class OrderItem
    {
        [XmlAttribute("Name")]
        public string DishName { get; set; }
        [XmlAttribute("Quantity")]
        public int Quantity { get; set; }
        [XmlAttribute("Price")]
        public decimal Price { get; set; }
    }
}
