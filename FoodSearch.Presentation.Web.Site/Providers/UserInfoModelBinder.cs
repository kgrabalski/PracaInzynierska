using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Models;

namespace FoodSearch.Presentation.Web.Site.Providers
{
    public class UserInfoModelBinder : IModelBinder
    {
        private readonly string _sessionKey = "UserInfo";
        private readonly IFoodSearchDomain _domain;

        public UserInfoModelBinder(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            UserInfo userInfo = controllerContext.HttpContext.Session[_sessionKey] as UserInfo;
            if (userInfo == null)
            {
                if (controllerContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    userInfo = new UserInfo()
                    {
                        UserId = _domain.User.GetUserId(controllerContext.HttpContext.User.Identity.Name)
                    };
                }
                controllerContext.HttpContext.Session[_sessionKey] = userInfo;
            }
            return userInfo;
        }
    }
}