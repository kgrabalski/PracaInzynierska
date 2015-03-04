using Acr.XamForms.Mobile;
using Acr.XamForms.UserDialogs;

namespace FoodSearch.Presentation.Mobile.Common.Services.Interfaces
{
    public interface IServiceLocator
    {
        IMessagingService Messaging { get; }
        INavigationService Navigation { get; }
        IRestaurantService Restaurant { get; }
        IAuthorizationService Authorization { get; }
        IUserDialogService Dialog { get; }
        ISettings Settings { get; }
        INetworkAvailabilityService NetworkAvailability { get; }
    }
}

