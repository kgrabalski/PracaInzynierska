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
    [AreaAuthorize(Roles = "RestaurantAdmin")]
    public class DeliveryRangeController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public DeliveryRangeController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public DeliveryRange GetDeliveryRange([ModelBinder] RestaurantUser ru)
        {
            return _domain.RestaurantAdmin.GetDeliveryRange(ru.RestaurantId);
        }
    }
}
