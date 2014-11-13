using System;
using FoodSearch.Presentation.Mobile.Common.ViewModels;
using FoodSearch.Service.Client.Interfaces;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using Acr.XamForms.UserDialogs;

namespace FoodSearch.Presentation.Mobile.Common
{
    public class OrderFailedViewModel : ViewModelBase
    {
        public OrderFailedViewModel(IFoodSearchServiceClient client, IServiceLocator serviceLocator) : base(client, serviceLocator)
        {
            InitializeView();
        }

        private async void InitializeView()
        {
            await Client.Order.ClearBasket();
        }
    }
}

