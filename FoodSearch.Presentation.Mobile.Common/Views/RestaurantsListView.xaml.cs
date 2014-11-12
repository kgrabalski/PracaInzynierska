using Xamarin.Forms;
using FoodSearch.Presentation.Mobile.Common.Services;
using Ninject;
using FoodSearch.Presentation.Mobile.Common.ViewModels;

namespace FoodSearch.Presentation.Mobile.Common.Views
{	
	public partial class RestaurantsListView : ContentPage
	{	
		public RestaurantsListView ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, true);
            BindingContext = DependencyResolver.Current.Get<RestaurantListViewModel>();
		}
	}
}

