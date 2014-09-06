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
    [AreaAuthorize(Roles = "RestaurantAdmin")]
    public class OpeningHourController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public OpeningHourController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        //get all opening hours
        [HttpGet]
        public IEnumerable<OpeningHour> GetAll([ModelBinder] RestaurantUser ru)
        {
            return _domain.RestaurantAdmin.GetOpeningHours(ru.RestaurantId);
        }

        //add new opening hour
        [HttpPost]
        public HttpResponseMessage Create([ModelBinder] RestaurantUser ru, OpeningHourModel oh)
        {
            if (ModelState.IsValid)
            {
                var result = _domain.RestaurantAdmin.CreateOpeningHour(ru.RestaurantId, oh.Day, oh.TimeFrom, oh.TimeTo);
                return Request.CreateResponse(result != null ? HttpStatusCode.Created : HttpStatusCode.Conflict, result);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        //delete opening hour
        [HttpDelete]
        public HttpResponseMessage Delete([ModelBinder] RestaurantUser ru, [FromUri] int id)
        {
            bool result = _domain.RestaurantAdmin.DeleteOpeningHour(id);
            return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        }
    }
}