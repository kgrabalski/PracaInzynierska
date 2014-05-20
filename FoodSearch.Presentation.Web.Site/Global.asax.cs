using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using FoodSearch.BusinessLogic.Domain.FoodSearch;
using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.Presentation.Web.Site.Helpers;
using FoodSearch.Presentation.Web.Site.Models;

using Ninject;
using Ninject.Web.Common;

namespace FoodSearch.Presentation.Web.Site
{
    public class MvcApplication : NinjectHttpApplication
    {
        public static readonly IKernel DependencyResolver;

        static MvcApplication()
        {
            DependencyResolver = new StandardKernel();
            DependencyResolver.Bind<IFoodSearchDomain>().To<FoodSearchDomain>().InSingletonScope();
        }

        protected override IKernel CreateKernel()
        {
            return DependencyResolver;
        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(RestaurantUser), DependencyResolver.Get<RestaurantUserModelBinder>());
        }
    }
}
