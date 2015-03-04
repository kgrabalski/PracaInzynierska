using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Service.Api.Models;
using Ninject;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace FoodSearch.Service.Api.Providers
{
    public class UserInfoModelBinderWebApi : ModelBinderWebApi<UserInfo>
    {
        public override bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            UserInfo model = HttpContext.Current.Session[SessionKey] as UserInfo;
            if (model == null)
            {
                var domain = MvcApplication.DependencyResolver.Get<IFoodSearchDomain>();
                model = new UserInfo()
                {
                    UserId = domain.User.GetUserId(HttpContext.Current.User.Identity.Name)
                };
                HttpContext.Current.Session[SessionKey] = model;
            }
            bindingContext.Model = model;
            return true;
        }
    }
}
