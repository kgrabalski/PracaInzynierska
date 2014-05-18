using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models
{
    public class OpeningHour
    {
        public int OpeningId { get; set; }
        public string Day { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
    }
}
