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
    public class OrderController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public OrderController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public HttpResponseMessage GetOrder([ModelBinder] RestaurantUser ru, [FromUri] Guid id)
        {
            var order = _domain.RestaurantAdmin.GetRestaurantOrder(ru.RestaurantId, id);
            return Request.CreateResponse(order != null ? HttpStatusCode.OK : HttpStatusCode.NotFound, order);
        }

        [HttpGet]
        public IEnumerable<RestaurantOrder> GetOrders([ModelBinder] RestaurantUser ru, [FromUri] bool newOrders = true)
        {
            var orders = _domain.RestaurantAdmin.GetRestaurantOrders(ru.RestaurantId, newOrders);
            return orders;
        }
    }
}
