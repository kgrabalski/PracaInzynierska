using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using FoodSearch.BusinessLogic.Domain.Core.Models;
using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Areas.SiteAdmin.Models;
using FoodSearch.Presentation.Web.Site.Helpers;

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

        [HttpPost]
        public HttpResponseMessage Create(CityModel city)
        {
            if (ModelState.IsValid)
            {
                var result = _domain.SiteAdmin.CreateCity(city.Name);
                return Request.CreateResponse(result != null ? HttpStatusCode.Created : HttpStatusCode.Conflict, result);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromUri] int id)
        {
            bool result = _domain.SiteAdmin.DeleteCity(id);
            return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        }
    }
}