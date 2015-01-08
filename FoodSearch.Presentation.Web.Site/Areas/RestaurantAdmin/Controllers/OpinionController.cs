using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.Restaurant.Models;
using FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Models;
using FoodSearch.Presentation.Web.Site.Helpers;
using FoodSearch.Presentation.Web.Site.Models;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Controllers
{
    [AreaAuthorize(Roles = "RestaurantAdmin")]
    public class OpinionController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public OpinionController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public HttpResponseMessage GetOpinions([ModelBinder] RestaurantUser ru, [FromUri] OpinionFilterModel filter)
        {
            if (ModelState.IsValid)
            {
                var opinions = _domain.Restaurant.GetOpinions(ru.RestaurantId, filter.Rating, filter.Page);
                return Request.CreateResponse(HttpStatusCode.OK, opinions);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        } 
    }
}
