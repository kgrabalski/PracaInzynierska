using System;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using Ninject;
using Acr.XamForms.UserDialogs;
using Acr.XamForms.Mobile;

namespace FoodSearch.Presentation.Mobile.Common.Services
{
    public class ServiceLocator : IServiceLocator
    {
        private T ResolveService<T>() {
            return DependencyResolver.Current.Get<T>();
        }

        public IMessagingService Messaging { get { return ResolveService<IMessagingService>(); } }
        public INavigationService Navigation { get { return ResolveService<INavigationService>(); } }
        public IRestaurantService Restaurant { get { return ResolveService<IRestaurantService>(); } }
        public IAuthorizationService Authorization { get { return ResolveService<IAuthorizationService>(); } }
        public IUserDialogService Dialog { get { return ResolveService<IUserDialogService>(); } }
        public ISettings Settings { get { return ResolveService<ISettings>(); } }
        public INetworkAvailabilityService NetworkAvailability { get { return ResolveService<INetworkAvailabilityService>(); } }
    }
}

