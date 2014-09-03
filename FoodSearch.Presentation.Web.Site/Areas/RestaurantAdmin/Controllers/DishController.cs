using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models;
using FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Models;
using FoodSearch.Presentation.Web.Site.Helpers;
using FoodSearch.Presentation.Web.Site.Models;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Controllers
{
    [AreaAuthorize(Roles = "RestaurantAdmin, RestaurantEmployee")]
    public class DishController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public DishController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        //get list of restaurant dishes
        [HttpGet]
        public IEnumerable<Dish> GetAll([ModelBinder] RestaurantUser ru)
        {
            return _domain.RestaurantAdmin.GetDishes(ru.RestaurantId);
        } 

        //create new dish
        [HttpPost]
        public HttpResponseMessage Create([ModelBinder] RestaurantUser ru, DishModel dish)
        {
            if (ModelState.IsValid)
            {
                var result = _domain.RestaurantAdmin.CreateDish(ru.RestaurantId, dish.DishName, dish.DishGroupId, dish.Price);
                return Request.CreateResponse(result != null ? HttpStatusCode.Created : HttpStatusCode.Conflict, result);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
    }
}