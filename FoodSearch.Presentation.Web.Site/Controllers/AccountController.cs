using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Models;
using System;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;

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
            Session["UserInfo"] = null;
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
                    Guid userId = _domain.User.CreateUser(model.FirstName, model.LastName, model.Email, model.PhoneNumber, model.Password);
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

        public ActionResult ForgottenPassword()
        {
            return View(new ResetPasswordModel());
        }

        [HttpPost]
        public ActionResult ForgottenPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                Guid requestId = _domain.User.CreatePasswordResetRequest(model.Email);
                //todo: wysłanie maila
                model.RequestCreated = true;
            }
            return View(model);
        }

        public ActionResult ResetPassword(Guid? requestId)
        {
            return HttpNotFound();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model, UserInfo ui)
        {
            if (ModelState.IsValid)
            {
                return Json(new
                {
                    Result = _domain.User.ChangeUserPassword(ui.UserId, model.OldPassword, model.NewPassword)
                }, JsonRequestBehavior.DenyGet);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}