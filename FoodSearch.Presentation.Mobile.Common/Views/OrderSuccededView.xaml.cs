using FoodSearch.Presentation.Mobile.Common.Services;
using FoodSearch.Presentation.Mobile.Common.ViewModels;
using Ninject;
using Xamarin.Forms;

namespace FoodSearch.Presentation.Mobile.Common.Views
{	
	public partial class OrderSuccededView : ContentPage
	{	
		public OrderSuccededView ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, true);
            BindingContext = DependencyResolver.Current.Get<OrderSuccededViewModel>();
		}
	}
}

