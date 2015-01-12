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
    public class DishController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public DishController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public IEnumerable<DishGroup> GetDishes([ModelBinder] Basket basket, [FromUri] Guid restaurantId)
        {
            basket.CurrentRestaurant = restaurantId;
            return _domain.Restaurant.GetDishes(restaurantId);
        } 
    }
}
