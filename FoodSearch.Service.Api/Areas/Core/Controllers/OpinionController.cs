using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.Restaurant.Models;
using FoodSearch.Service.Api.Areas.Core.Models;
using FoodSearch.Service.Api.Models;

namespace FoodSearch.Service.Api.Areas.Core.Controllers
{
    public class OpinionController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public OpinionController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public IEnumerable<Opinion> GetOpinions(Guid restaurantId, int rating = 0, int page = 0)
        {
            return _domain.Restaurant.GetOpinions(restaurantId, rating, page);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public HttpResponseMessage AddOpinion([ModelBinder] UserInfo ui, OpinionModel m)
        {
            if (ModelState.IsValid)
            {
                bool result = _domain.Restaurant.AddOpinion(m.RestaurantId, ui.UserId, m.Rating, m.Comment);
                return Request.CreateResponse(result ? HttpStatusCode.Created : HttpStatusCode.Conflict);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
    }
}