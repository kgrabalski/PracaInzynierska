using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Service.Api.Areas.User.Models;

namespace FoodSearch.Service.Api.Areas.User.Controllers
{
    public class LoginController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public LoginController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpPost]
        public HttpResponseMessage Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Email, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpDelete]
        [Authorize(Roles = "User")]
        public HttpResponseMessage Logout()
        {
            FormsAuthentication.SignOut();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
