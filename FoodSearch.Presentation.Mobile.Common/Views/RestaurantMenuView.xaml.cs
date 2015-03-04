using FoodSearch.Presentation.Mobile.Common.Services;
using FoodSearch.Presentation.Mobile.Common.ViewModels;
using Ninject;
using Xamarin.Forms;

namespace FoodSearch.Presentation.Mobile.Common.Views
{	
    public partial class RestaurantMenuView : TabbedPage
	{	
		public RestaurantMenuView ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, true);
            BindingContext = new {
                Menu = DependencyResolver.Current.Get<DishListViewModel>(),
                Opinion = DependencyResolver.Current.Get<OpinionListViewModel>()
            };
            
		}
	}
}

