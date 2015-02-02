using Xamarin.Forms;
using FoodSearch.Presentation.Mobile.Common.Services;
using FoodSearch.Presentation.Mobile.Common.ViewModels;
using Ninject;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;


namespace FoodSearch.Presentation.Mobile.Common.Views
{	
	public partial class MainView : ContentPage
	{	
		public MainView ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, true);

            INetworkAvailabilityService service = DependencyResolver.Current.Get<INetworkAvailabilityService>();
            if (!service.IsConnected)
            {
                service.CloseApp();
            } else BindingContext = DependencyResolver.Current.Get<MainViewModel>();

		}
	}
}

