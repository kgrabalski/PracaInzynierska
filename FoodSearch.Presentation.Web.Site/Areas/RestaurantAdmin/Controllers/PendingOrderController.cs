using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Presentation.Web.Site.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using RestaurantUser = FoodSearch.Presentation.Web.Site.Models.RestaurantUser;

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

        [HttpPut]
        public HttpResponseMessage CompleteOrder([FromUri] Guid id)
        {
            bool result = _domain.Order.ChangeOrderState(id, OrderStates.Completed);
            return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        }
    }
}
