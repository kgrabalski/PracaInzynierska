using FoodSearch.BusinessLogic.Domain.Core.Models;
using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Helpers;
using System.Collections.Generic;
using System.Web.Http;

namespace FoodSearch.Presentation.Web.Site.Areas.SiteAdmin.Controllers
{
    [AreaAuthorize(Roles = "SiteAdmin")]
    public class CityController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public CityController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public IEnumerable<City> GetAll()
        {
            return _domain.Core.GetCities();
        }
    }
}