using Xamarin.Forms;
using FoodSearch.Presentation.Mobile.Common.Services;
using FoodSearch.Presentation.Mobile.Common.ViewModels;
using Ninject;


namespace FoodSearch.Presentation.Mobile.Common.Views
{	
	public partial class MainView : ContentPage
	{	
		public MainView ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, true);
            if (!NetworkAvailabilityService.IsConnected)
            {
                NetworkAvailabilityService.CloseApp();
            } else BindingContext = DependencyResolver.Current.Get<MainViewModel>();

		}
	}
}

