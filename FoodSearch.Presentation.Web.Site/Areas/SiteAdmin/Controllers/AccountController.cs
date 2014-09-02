using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Presentation.Web.Site.Providers;
using System.Web.Mvc;
using System.Web.Security;

namespace FoodSearch.Presentation.Web.Site.Areas.SiteAdmin.Controllers
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
            FoodSearchRoleProvider roleProvider = new FoodSearchRoleProvider();
            if (roleProvider.IsUserInRole(userName, UserTypes.SiteAdmin.ToString()))
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