using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models;
using FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Models;
using FoodSearch.Presentation.Web.Site.Helpers;
using FoodSearch.Presentation.Web.Site.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

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

        [HttpGet]
        public IEnumerable<OpeningHour> GetAll([ModelBinder] RestaurantUser ru)
        {
            return _domain.RestaurantAdmin.GetOpeningHours(ru.RestaurantId);
        }

        [HttpPost]
        [ValidateModel]
        public HttpResponseMessage Create([ModelBinder] RestaurantUser ru, OpeningHourModel oh)
        {
            var result = _domain.RestaurantAdmin.CreateOpeningHour(ru.RestaurantId, oh.Day, oh.TimeFrom, oh.TimeTo);
            return Request.CreateResponse(result != null ? HttpStatusCode.Created : HttpStatusCode.Conflict, result);
        }

        [HttpDelete]
        public HttpResponseMessage Delete([ModelBinder] RestaurantUser ru, [FromUri] int id)
        {
            bool result = _domain.RestaurantAdmin.DeleteOpeningHour(id);
            return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        }
    }
}