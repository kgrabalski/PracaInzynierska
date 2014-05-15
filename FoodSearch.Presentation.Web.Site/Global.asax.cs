using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using FoodSearch.BusinessLogic.Domain.FoodSearch;
using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;

using Ninject;
using Ninject.Web.Common;

namespace FoodSearch.Presentation.Web.Site
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind<IFoodSearchDomain>().To<FoodSearchDomain>().InSingletonScope();
            return kernel;
        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
