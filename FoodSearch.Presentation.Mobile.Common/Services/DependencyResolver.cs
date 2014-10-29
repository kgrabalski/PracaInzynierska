using System;
using Ninject;
using FoodSearch.Presentation.Mobile.Common.ViewModels;
using FoodSearch.Service.Client.Interfaces;
using FoodSearch.Service.Client;
using Acr.XamForms.UserDialogs;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;

namespace FoodSearch.Presentation.Mobile.Common.Services
{
    public static class DependencyResolver
    {
        private static IKernel _kernel;
        public static IKernel Current { get { return _kernel; } }

        static DependencyResolver()
        {
            _kernel = new StandardKernel();
            _kernel.Bind<MainViewModel>().ToSelf().InSingletonScope();
            _kernel.Bind<RestaurantListViewModel>().ToSelf();
            _kernel.Bind<DishListViewModel>().ToSelf();
            _kernel.Bind<OpinionListViewModel>().ToSelf();
            _kernel.Bind<BasketViewModel>().ToSelf();
            _kernel.Bind<LoginViewModel>().ToSelf();
            _kernel.Bind<RegisterViewModel>().ToSelf();
            _kernel.Bind<IFoodSearchServiceClient>().To<FoodSearchServiceClient>().InSingletonScope();
            _kernel.Bind<IAuthorizationService>().To<AuthorizationService>().InSingletonScope();
            _kernel.Bind<IUserDialogService>().ToMethod((a) => Xamarin.Forms.DependencyService.Get<IUserDialogService>());
        }
    }
}

