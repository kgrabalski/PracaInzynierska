using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models;
using FoodSearch.Presentation.Web.Site.Helpers;
using FoodSearch.Presentation.Web.Site.Models;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Controllers
{
    [AreaAuthorize(Roles = "RestaurantAdmin, RestaurantEmployee")]
    public class PendingOrderController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public PendingOrderController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public IEnumerable<RestaurantOrder> GetOrders([ModelBinder] RestaurantUser ru)
        {
            var orders = _domain.RestaurantAdmin.GetRestaurantOrders(ru.RestaurantId, false);
            return orders;
        }
    }
}
