using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using FoodSearch.Service.Client.Interfaces;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
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

