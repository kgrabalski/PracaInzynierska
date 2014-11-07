using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Service.Api.Areas.User.Models;

namespace FoodSearch.Service.Api.Areas.User.Controllers
{
    public class UserController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public UserController(IFoodSearchDomain domain)
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
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpPut]
        public HttpResponseMessage Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (!_domain.User.IsEmailDuplicated(model.Email))
                {
                    Guid userId = _domain.User.CreateUser(model.FirstName, model.LastName, model.Email, model.Password);
                    _domain.User.CreateConfirmationEntry(userId, model.Email);
                    _domain.User.CreateDeliveryAddress(userId, model.AddressId, model.FlatNumber);
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
                else Request.CreateResponse(HttpStatusCode.Conflict);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpDelete]
        public HttpResponseMessage Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Request.Cookies.Clear();
                HttpContext.Current.Response.Cookies.Clear();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateResponse(HttpStatusCode.Conflict);
        }
    }
}
