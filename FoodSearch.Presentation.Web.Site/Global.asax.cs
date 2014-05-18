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
        private readonly IKernel _kernel;

        public MvcApplication()
        {
            _kernel = new StandardKernel();
            _kernel.Bind<IFoodSearchDomain>().To<FoodSearchDomain>().InSingletonScope();
        }

        protected override IKernel CreateKernel()
        {
            return _kernel;
        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(RestaurantUser), _kernel.Get<RestaurantUserModelBinder>());
        }
    }
}
