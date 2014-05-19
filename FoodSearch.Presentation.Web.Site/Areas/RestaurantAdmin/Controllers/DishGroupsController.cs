using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Models;

namespace FoodSearch.Presentation.Web.Site.Areas.RestaurantAdmin.Controllers
{
    public class DishGroupsController : Controller
    {
        private readonly IFoodSearchDomain _domain;

        public DishGroupsController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpPost]
        public ActionResult GetDishGroups(RestaurantUser restUser)
        {
            var groups = _domain.RestaurantAdmin.GetDishGroups(restUser.RestaurantId).Select(x => new
            {
                x.DishGroupId,
                x.Name
            });
            return Json(groups, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult Create(RestaurantUser restUser, string groupName)
        {
            int groupId = _domain.RestaurantAdmin.CreateDishGroup(restUser.RestaurantId, groupName);
            return Json(groupId, JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult Edit(int dishGroupId, string newGroupName)
        {
            _domain.RestaurantAdmin.EditDishGroup(dishGroupId, newGroupName);
            return Json("ok", JsonRequestBehavior.DenyGet);
        }

        [HttpPost]
        public ActionResult Delete(int dishGroupId)
        {
            _domain.RestaurantAdmin.DeleteDishGroup(dishGroupId);
            return Json("ok", JsonRequestBehavior.DenyGet);
        }
    }
}