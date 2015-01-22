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
using FoodSearch.Presentation.Web.Site.Models;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Controllers
{
    public class RestaurantEmployeeController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public RestaurantEmployeeController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public IEnumerable<RestaurantEmployee> GetEmployees([ModelBinder] RestaurantUser ru)
        {
            return _domain.RestaurantAdmin.GetRestaurantEmployees(ru.RestaurantId);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteEmployee([ModelBinder] RestaurantUser ru, [FromUri] Guid id)
        {
            bool success = _domain.RestaurantAdmin.DeleteRestaurantEmployee(ru.RestaurantId, id);
            return Request.CreateResponse(success ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        }

        [HttpPost]
        public HttpResponseMessage AddEmployee([ModelBinder] RestaurantUser ru, AddEmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _domain.RestaurantAdmin.AddRestaurantEmployee(ru.RestaurantId, model.FirstName, model.LastName, model.Password);
                return Request.CreateResponse(HttpStatusCode.Created, employee);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        public HttpResponseMessage ResetPassword([FromUri] Guid id, ResetEmployeePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                _domain.RestaurantAdmin.ResetEmployeePassword(id, model.Password);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
    }
}
