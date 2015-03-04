
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using Xamarin.Forms;

namespace FoodSearch.Presentation.Mobile.Common.Services
{
    public class NavigationService : INavigationService
    {
        public INavigation Navigate
        {
			get { return ViewLocator.StartScreen.Navigation; }
        }
    }
}
