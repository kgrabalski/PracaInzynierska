using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;

using Xamarin.Forms;
using FoodSearch.Presentation.Mobile.Common.Services;
using FoodSearch.Presentation.Mobile.Common.ViewModels;
using Ninject;


namespace FoodSearch.Presentation.Mobile.Common.Views
{	
	public partial class MainView : ContentPage
	{
	    private readonly IServiceLocator _serviceLocator;

	    public MainView ()
	    {
	        _serviceLocator = DependencyResolver.Current.Get<IServiceLocator>();
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, true);
            if (!_serviceLocator.NetworkAvailability.IsConnected)
            {
                _serviceLocator.NetworkAvailability.CloseApp();
            } else BindingContext = DependencyResolver.Current.Get<MainViewModel>();

		}
	}
}

