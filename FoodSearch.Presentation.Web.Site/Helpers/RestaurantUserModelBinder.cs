using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Models;
using System.Web.Mvc;


namespace FoodSearch.Presentation.Web.Site.Helpers
{
    public class RestaurantUserModelBinder : IModelBinder
    {
        private readonly IFoodSearchDomain _domain;
        private readonly string _sessionKey = "RestaurantUser";

        public RestaurantUserModelBinder(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        //public RestaurantUserModelBinder() : this(MvcApplication.DependencyResolver.Get<IFoodSearchDomain>())
        //{
            
        //}

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            RestaurantUser restaurantUser = controllerContext.HttpContext.Session[_sessionKey] as RestaurantUser;
            if (restaurantUser == null)
            {
                restaurantUser = new RestaurantUser
                {
                    UserId = _domain.RestaurantAdmin.GetUserId(controllerContext.HttpContext.User.Identity.Name)
                };
                restaurantUser.RestaurantId = _domain.RestaurantAdmin.GetUserRestaurant(restaurantUser.UserId);
                controllerContext.HttpContext.Session[_sessionKey] = restaurantUser;
            }
            return restaurantUser;
        }
    }
}