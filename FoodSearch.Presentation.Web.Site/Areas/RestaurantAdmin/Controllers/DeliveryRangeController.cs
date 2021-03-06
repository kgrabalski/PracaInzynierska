﻿using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models;
using FoodSearch.Presentation.Web.Site.Helpers;
using FoodSearch.Presentation.Web.Site.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

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

        [HttpPut]
        public HttpResponseMessage UpdateDeliveryRange([ModelBinder] RestaurantUser ru, DeliveryRange model)
        {
            bool result = _domain.RestaurantAdmin.UpdateDeliveryRange(ru.RestaurantId, model.HasDeliveryRadius, model.DeliveryRadius, model.Polygon);
            return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        }
    }
}
