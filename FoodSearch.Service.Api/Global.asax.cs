using FoodSearch.BusinessLogic.Domain.FoodSearch;
using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using Ninject;
using Ninject.Web.Common;
using System.Web.Http;
using System.Web.Mvc;

namespace FoodSearch.Service.Api
{
    public class MvcApplication : NinjectHttpApplication
    {
        public static readonly IKernel DependencyResolver;

        static MvcApplication()
        {
            DependencyResolver = new StandardKernel();
            DependencyResolver.Bind<IFoodSearchDomain>().To<FoodSearchDomain>();
        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected override IKernel CreateKernel()
        {
            return DependencyResolver;
        }
    }
}
