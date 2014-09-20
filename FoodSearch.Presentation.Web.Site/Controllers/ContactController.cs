using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Models;

namespace FoodSearch.Presentation.Web.Site.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class ContactController : Controller
    {
        private readonly IFoodSearchDomain _domain;

        public ContactController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        public ActionResult Index(UserInfo ui)
        {
            ContactModel model = new ContactModel();
            if (ui != null)
            {
                var userData = _domain.User.GetUserDetails(ui.UserId);
                model.Name = string.Format("{0} {1}", userData.FirstName, userData.LastName);
                model.Email = userData.Email;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: wysyłanie maila kontaktowego
            }
            return View(model);
        }
    }
}