using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Models;

namespace FoodSearch.Presentation.Web.Site.Controllers
{
    public class RestaurantsController : Controller
    {
        private readonly IFoodSearchDomain _domain;

        public RestaurantsController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        public ActionResult Index()
        {
            var restaurants = _domain.Restaurant.GetPartnerRestaurants();
            return View(restaurants);
        }

        public ActionResult Page(Guid? restaurantId)
        {
            if (!restaurantId.HasValue) return HttpNotFound();
            var model = new RestaurantPageModel
            {
                RestaurantDetails = _domain.Restaurant.GetRestaurantDetails(restaurantId.Value),
                OpeningHours = _domain.RestaurantAdmin.GetOpeningHours(restaurantId.Value),
                DishGroups = _domain.Restaurant.GetDishes(restaurantId.Value),
                UserCommented = false
            };
            if (User.Identity.IsAuthenticated)
            {
                model.UserCommented = _domain.Restaurant.CheckUserCommentExists(_domain.User.GetUserId(User.Identity.Name), restaurantId.Value);
            }
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddOpinion(UserInfo ui, OpinionModel op)
        {
            bool result = false;
            if (ModelState.IsValid)
            {
                result = _domain.Restaurant.AddOpinion(op.RestaurantId, ui.UserId, op.Rating, op.Comment);
            }
            return new HttpStatusCodeResult(result ? HttpStatusCode.Created : HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public ActionResult GetOpinions(Guid restaurantId, int rating = 0, int page = 0)
        {
            var opinions = _domain.Restaurant.GetOpinions(restaurantId, rating, page);
            return Json(opinions);
        }

        [HttpPost]
        public ActionResult GetRestaurantRating(Guid restaurantId)
        {
            var rating = _domain.Restaurant.GetRestaurantRating(restaurantId);
            return Json(rating);
        }
    }
}