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
    [AreaAuthorize(Roles = "RestaurantAdmin, RestaurantEmployee")]
    public class DishGroupController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public DishGroupController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        //get list of restaurant dish groups
        [HttpGet]
        public IEnumerable<DishGroup> GetAll([ModelBinder] RestaurantUser ru)
        {
            return _domain.RestaurantAdmin.GetDishGroups(ru.RestaurantId);
        }

        //create new dish group
        [HttpPost]
        public HttpResponseMessage Create([ModelBinder] RestaurantUser ru, DishGroupModel dg)
        {
            if (ModelState.IsValid)
            {
                var result = _domain.RestaurantAdmin.CreateDishGroup(ru.RestaurantId, dg.Name);
                return Request.CreateResponse(result != null ? HttpStatusCode.Created : HttpStatusCode.Conflict, result);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        //edit dish group name
        [HttpPut]
        public HttpResponseMessage Edit([ModelBinder] RestaurantUser ru, [FromUri] int id, DishGroupModel dg)
        {
            if (ModelState.IsValid)
            {
                bool result = _domain.RestaurantAdmin.EditDishGroup(ru.RestaurantId, id, dg.Name);
                return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        //delete dish group
        [HttpDelete]
        public HttpResponseMessage Delete([ModelBinder] RestaurantUser ru, [FromUri] int id)
        {
            bool result = _domain.RestaurantAdmin.DeleteDishGroup(ru.RestaurantId, id);
            return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        }
    }
}