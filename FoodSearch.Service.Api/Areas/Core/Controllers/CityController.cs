using FoodSearch.BusinessLogic.Domain.Core.Models;
using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using System.Collections.Generic;
using System.Web.Http;

namespace FoodSearch.Service.Api.Areas.Core.Controllers
{
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
