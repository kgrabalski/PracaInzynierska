using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Presentation.Web.Site.Providers;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password, bool? rememberMe)
        {
            FoodSearchRoleProvider roleProvider = new FoodSearchRoleProvider();
            if (roleProvider.IsUserInRole(userName, UserTypes.RestaurantAdmin.ToString()) || roleProvider.IsUserInRole(userName, UserTypes.RestaurantEmployee.ToString()))
            {
                if (Membership.ValidateUser(userName, password))
                {
                    FormsAuthentication.SetAuthCookie(userName, rememberMe.HasValue && rememberMe.Value);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}