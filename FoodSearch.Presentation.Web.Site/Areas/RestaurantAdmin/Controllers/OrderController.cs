using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Helpers;
using FoodSearch.Presentation.Web.Site.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

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
    }
}
