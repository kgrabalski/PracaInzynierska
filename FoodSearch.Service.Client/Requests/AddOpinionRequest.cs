using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Service.Client.Requests
{
    public class AddOpinionRequest
    {
        public Guid RestaurantId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
