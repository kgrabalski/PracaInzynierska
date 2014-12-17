using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.BusinessLogic.Domain.Order.Models;
using FoodSearch.BusinessLogic.Domain.User.Models;

namespace FoodSearch.Presentation.Web.Site.Areas.User.Models
{
    public class UserPanelModel
    {
        public UserDetails UserDetails { get; set; }
        public DeliveryAddress DeliveryAddress { get; set; }
    }
}
