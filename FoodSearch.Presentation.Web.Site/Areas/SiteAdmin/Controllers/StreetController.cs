using FoodSearch.BusinessLogic.Domain.Core.Models;
using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Helpers;
using System.Collections.Generic;
using System.Web.Http;

namespace FoodSearch.Presentation.Web.Site.Areas.SiteAdmin.Controllers
{
    [AreaAuthorize(Roles = "SiteAdmin")]
    public class StreetController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public StreetController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public IEnumerable<Street> GetStreets(int cityId, string query)
        {
            return _domain.Core.GetStreets(cityId, query);
        }

        [HttpGet]
        public IEnumerable<StreetNumber> GetStreetNumbers([FromUri] int id)
        {
            return _domain.Core.GetStreetNumbers(id);
        }
    }
}