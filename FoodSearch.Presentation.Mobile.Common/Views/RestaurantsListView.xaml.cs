using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Ninject;
using FoodSearch.Presentation.Mobile.Common.ViewModels;

namespace FoodSearch.Presentation.Mobile.Common
{	
	public partial class RestaurantsListView : ContentPage
	{	
		public RestaurantsListView ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, true);
            BindingContext = ViewLocator.DependencyResolver.Get<RestaurantListViewModel>();
		}
	}
}

