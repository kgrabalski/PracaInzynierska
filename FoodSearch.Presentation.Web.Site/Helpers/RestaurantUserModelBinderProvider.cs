using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Models;

using Ninject;

namespace FoodSearch.Presentation.Web.Site.Helpers
{
    public class RestaurantUserModelBinderProvider : ModelBinderProvider
    {
        private readonly IModelBinder _modelBinder;

        public RestaurantUserModelBinderProvider()
        {
            _modelBinder = new RestaurantUserModelBinderWebApi(MvcApplication.DependencyResolver.Get<IFoodSearchDomain>());
        }

        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            if (modelType == typeof (RestaurantUser))
            {
                return _modelBinder;
            }
            return null;
        }
    }
}