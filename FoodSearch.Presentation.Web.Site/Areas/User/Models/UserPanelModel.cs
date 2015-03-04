using FoodSearch.BusinessLogic.Domain.User.Models;
using System.Collections.Generic;

namespace FoodSearch.Presentation.Web.Site.Areas.User.Models
{
    public class UserPanelModel
    {
        public UserDetails UserDetails { get; set; }
        public IEnumerable<DeliveryAddress> DeliveryAddresses { get; set; }
    }
}
