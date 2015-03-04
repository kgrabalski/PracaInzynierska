using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.User.Models;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Presentation.Web.Site.Areas.SiteAdmin.Models;
using FoodSearch.Presentation.Web.Site.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace FoodSearch.Presentation.Web.Site.Areas.SiteAdmin.Controllers
{
    [AreaAuthorize(Roles = "SiteAdmin")]
    public class UserController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public UserController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        [ValidateModel]
        public IEnumerable<BusinessLogic.Domain.SiteAdmin.Models.User> GetUsers([ModelBinder] QueryModel model)
        {
            return _domain.SiteAdmin.GetUsers(model.Query, model.Page, model.PageSize);
        }

        [HttpGet]
        public UserDetails GetUserDetails([FromUri] Guid id)
        {
            return _domain.User.GetUserDetails(id);
        }

        [HttpPut]
        public HttpResponseMessage ChangeUserState([FromUri] Guid id, [FromUri] bool isActive)
        {
            bool result = _domain.SiteAdmin.ChangeUserState(id, isActive ? UserStates.Active : UserStates.Disabled);
            return Request.CreateResponse(result ? HttpStatusCode.OK : HttpStatusCode.NotFound);
        }
    }
}
