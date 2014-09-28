using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;

namespace FoodSearch.Presentation.Web.Site.Controllers
{
    [Authorize(Roles = "User")]
    public class OrderController : Controller
    {
        private readonly IFoodSearchDomain _domain;

        public OrderController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }
    }
}