using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models;
using FoodSearch.Presentation.Web.Site.Helpers;
using FoodSearch.Presentation.Web.Site.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Controllers
{
    [AreaAuthorize(Roles = "RestaurantAdmin, RestaurantEmployee")]
    public class RestaurantCuisineController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public RestaurantCuisineController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        //get list of restaurant cuisines
        [HttpGet]
        public IEnumerable<Cuisine> GetAll([ModelBinder] RestaurantUser ru)
        {
            return _domain.RestaurantAdmin.GetRestaurantCuisines(ru.RestaurantId);
        }

        //add new restaurant cuisine
        [HttpPost]
        public HttpResponseMessage Add([ModelBinder] RestaurantUser ru, int id)
        {
            var result = _domain.RestaurantAdmin.AddRestaurantCuisine(ru.RestaurantId, id);
            return Request.CreateResponse(result != null ? HttpStatusCode.Created : HttpStatusCode.Conflict, result);
        }

        //delete existing restaurant cuisine
        [HttpDelete]
        public HttpResponseMessage Delete([ModelBinder] RestaurantUser ru, int id)
        {
            bool result = _domain.RestaurantAdmin.RemoveRestaurantCuisine(ru.RestaurantId, id);
            return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        }
    }
}
