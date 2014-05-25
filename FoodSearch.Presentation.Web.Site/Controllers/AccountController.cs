using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;

namespace FoodSearch.Presentation.Web.Site.Controllers
{
    public class AccountController : Controller
    {
        private readonly IFoodSearchDomain _domain;

        public AccountController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password, bool? rememberMe)
        {
            if (Membership.ValidateUser(userName, password))
            {
                FormsAuthentication.SetAuthCookie(userName, rememberMe.HasValue && rememberMe.Value);

            }
            return View();
        }

        [Authorize(Roles = "User")]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult IsUserNameDuplicated(string userName)
        {
            bool duplicated = _domain.User.IsUserNameDuplicated(userName);
            return Json(duplicated, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult IsEmailDuplicated(string email)
        {
            bool duplicated = _domain.User.IsEmailDuplicated(email);
            return Json(duplicated, JsonRequestBehavior.DenyGet);
        }

        public ActionResult ConfirmRegistration(Guid? code)
        {
            if (!code.HasValue) return new HttpNotFoundResult();
            var result = _domain.User.ConfirmRegistration(code.Value);
            return View(result);
        }
    }
}