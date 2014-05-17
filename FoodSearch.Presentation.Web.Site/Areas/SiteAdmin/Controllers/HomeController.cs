using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Helpers;

namespace FoodSearch.Presentation.Web.Site.Areas.SiteAdmin.Controllers
{
    [AreaAuthorize(Roles = "SiteAdmin")]
    public class HomeController : Controller
    {
        private readonly IFoodSearchDomain _domain;

        public HomeController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        public ActionResult Index()
        {
            return View();
        }
       
    }
}