using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models;
using FoodSearch.Presentation.Web.Site.Helpers;
using System.Collections.Generic;
using System.Web.Http;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Controllers
{
    [AreaAuthorize(Roles = "RestaurantAdmin")]
    public class CuisineController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public CuisineController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        //get list of cuisines
        public IEnumerable<Cuisine> Get()
        {
            return _domain.RestaurantAdmin.GetCuisines();
        }
    }
}