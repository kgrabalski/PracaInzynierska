using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Models
{
    public class ConfirmOrderModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(5)]
        public string DeliveryTime { get; set; }
    }
}
