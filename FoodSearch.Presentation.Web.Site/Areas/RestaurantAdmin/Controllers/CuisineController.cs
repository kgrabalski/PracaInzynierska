using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models;
using FoodSearch.Presentation.Web.Site.Helpers;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Controllers
{
    [AreaAuthorize(Roles = "RestaurantAdmin, RestaurantEmployee")]
    public class CuisineController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public CuisineController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        // GET api/<controller>
        public IEnumerable<Cuisine> Get()
        {
            return _domain.RestaurantAdmin.GetCuisines();
        }
    }
}