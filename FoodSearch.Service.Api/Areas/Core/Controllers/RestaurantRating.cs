using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodSearch.Service.Api.Areas.Core.Controllers
{
    public class RestaurantRating : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public RestaurantRating(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public HttpResponseMessage GetRestaurantRating([FromUri] Guid id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _domain.Restaurant.GetRestaurantRating(id));
        }
    }
}