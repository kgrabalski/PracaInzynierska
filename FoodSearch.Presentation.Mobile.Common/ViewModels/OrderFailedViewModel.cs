using System;
using FoodSearch.Presentation.Mobile.Common.ViewModels;
using FoodSearch.Service.Client.Interfaces;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using Acr.XamForms.UserDialogs;

namespace FoodSearch.Presentation.Mobile.Common
{
    public class OrderFailedViewModel : ViewModelBase
    {
        public OrderFailedViewModel(IFoodSearchServiceClient client, IAuthorizationService authorizationService, IUserDialogService dialogService) : base(client, authorizationService, dialogService)
        {
        }
        
    }
}

