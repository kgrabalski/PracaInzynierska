using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Models;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace FoodSearch.Presentation.Web.Site.Helpers
{
    public class RestaurantUserModelBinderWebApi : IModelBinder
    {
        private readonly IFoodSearchDomain _domain;
        private readonly string _sessionKey = "RestaurantUser";

        public RestaurantUserModelBinderWebApi(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            RestaurantUser restaurantUser = HttpContext.Current.Session[_sessionKey] as RestaurantUser;
            if (restaurantUser == null)
            {
                restaurantUser = new RestaurantUser
                {
                    UserId = _domain.RestaurantAdmin.GetUserId(HttpContext.Current.User.Identity.Name)
                };
                restaurantUser.RestaurantId = _domain.RestaurantAdmin.GetUserRestaurant(restaurantUser.UserId);
                HttpContext.Current.Session[_sessionKey] = restaurantUser;
            }
            bindingContext.Model = restaurantUser;
            return true;
        }
    }
}