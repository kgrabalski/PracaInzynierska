using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Controllers
{
    public class OrderHistoryController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public OrderHistoryController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }
    }
}
