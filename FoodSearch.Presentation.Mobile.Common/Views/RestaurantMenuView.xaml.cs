using System;
using System.Collections.Generic;
using Xamarin.Forms;

using Ninject;
using FoodSearch.Presentation.Mobile.Common.ViewModels;

namespace FoodSearch.Presentation.Mobile.Common
{	
    public partial class RestaurantMenuView : TabbedPage
	{	
		public RestaurantMenuView ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, true);
            BindingContext = new {
                Menu = ViewLocator.DependencyResolver.Get<DishListViewModel>(),
                Opinion = ViewLocator.DependencyResolver.Get<OpinionListViewModel>()
            };
		}
	}
}

