using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models;
using FoodSearch.Presentation.Web.Site.Helpers;
using FoodSearch.Presentation.Web.Site.Models;

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

        //api/RestaurantCuisine
        public IEnumerable<Cuisine> Get([ModelBinder] RestaurantUser ru)
        {
            return _domain.RestaurantAdmin.GetRestaurantCuisines(ru.RestaurantId);
        }

        //api/RestaurantCuisine
        public HttpResponseMessage Post([ModelBinder] RestaurantUser ru, [FromBody]int id)
        {
            bool result = _domain.RestaurantAdmin.AddRestaurantCuisine(ru.RestaurantId, id);
            return Request.CreateResponse(result ? HttpStatusCode.Created : HttpStatusCode.OK);
        }

        public HttpResponseMessage Delete([ModelBinder] RestaurantUser ru, [FromBody]int id)
        {
            bool result = _domain.RestaurantAdmin.RemoveRestaurantCuisine(ru.RestaurantId, id);
            return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        }
    }
}
