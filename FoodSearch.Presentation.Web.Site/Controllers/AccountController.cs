using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using System;
using System.Web.Mvc;
using System.Web.Security;

using FoodSearch.Presentation.Web.Site.Models;

namespace FoodSearch.Presentation.Web.Site.Controllers
{
    public class AccountController : Controller
    {
        private readonly IFoodSearchDomain _domain;

        public AccountController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        public ActionResult Login(string returnUrl)
        {
            return View(new LoginModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public ActionResult Login(LoginModel m)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(m.Email, m.Password))
                {
                    FormsAuthentication.SetAuthCookie(m.Email, true);
                    if (!string.IsNullOrEmpty(m.ReturnUrl) && Url.IsLocalUrl(m.ReturnUrl))
                    {
                        return Redirect(m.ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(m);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult IsEmailDuplicated(string email)
        {
            bool duplicated = _domain.User.IsEmailDuplicated(email);
            return Json(duplicated, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Verify(Guid? code)
        {
            if (!code.HasValue) return new HttpNotFoundResult();
            var result = _domain.User.ConfirmRegistration(code.Value);
            return View(result);
        }

        public ActionResult AccountCreated()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
                if (!_domain.User.IsEmailDuplicated(model.Email))
                {
                    Guid userId = _domain.User.CreateUser(model.FirstName, model.LastName, model.Email, model.Password);
                    _domain.User.CreateConfirmationEntry(userId, model.Email);
                    _domain.User.CreateDeliveryAddress(userId, model.AddressId, model.FlatNumber);
                    return RedirectToAction("AccountCreated");
                }
                else
                {
                    ModelState.AddModelError("Email", "Podany adres email istnieje już w bazie");
                }
            return View(model);
        }
    }
}