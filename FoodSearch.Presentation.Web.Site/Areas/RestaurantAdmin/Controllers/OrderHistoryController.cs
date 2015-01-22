using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Helpers;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Controllers
{
    [AreaAuthorize(Roles = "RestaurantAdmin")]
    public class OrderHistoryController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public OrderHistoryController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }
    }
}
