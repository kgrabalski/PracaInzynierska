using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.Restaurant.Models;
using FoodSearch.Service.Api.Areas.Order.Models;

namespace FoodSearch.Service.Api.Areas.Core.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public RestaurantController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public IEnumerable<RestaurantInfo> GetRestaurants([ModelBinder] Basket basket, [FromUri] int addressId)
        {
#if DEBUG
            DateTime date = DateTime.Now;
#else
            DateTime date = DateTime.Now.Subtract(new TimeSpan(2, 0, 0));
#endif
            basket.AddressId = addressId;
            return _domain.Restaurant.GetRestaurants(addressId, date);
        }
    }
}
