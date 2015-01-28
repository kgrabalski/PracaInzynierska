using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Security;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.User.Models;
using FoodSearch.Service.Api.Areas.User.Models;
using FoodSearch.Service.Api.Models;

namespace FoodSearch.Service.Api.Areas.User.Controllers
{
    [Authorize(Roles = "User")]
    public class UserDataController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public UserDataController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public UserDetails GetUserDetails([ModelBinder] UserInfo ui)
        {
            return _domain.User.GetUserDetails(ui.UserId);
        }

        [HttpPut]
        public HttpResponseMessage ResetPassword([ModelBinder] UserInfo ui, ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                bool success = _domain.User.ChangeUserPassword(ui.UserId, model.OldPassword, model.NewPassword);
                if (!success) return Request.CreateResponse(HttpStatusCode.Forbidden);

                FormsAuthentication.SignOut();
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Request.Cookies.Clear();
                HttpContext.Current.Response.Cookies.Clear();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
    }
}
