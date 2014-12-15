using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FoodSearch.Presentation.Web.Site.Helpers;

namespace FoodSearch.Presentation.Web.Site.Areas.User.Controllers
{
    [Authorize(Roles = "User")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}