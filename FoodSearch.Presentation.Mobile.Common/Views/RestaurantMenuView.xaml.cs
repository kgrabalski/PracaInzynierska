using System;
using System.Collections.Generic;
using Xamarin.Forms;

using Ninject;
using FoodSearch.Presentation.Mobile.Common.ViewModels;
using FoodSearch.Presentation.Mobile.Common.Services;

namespace FoodSearch.Presentation.Mobile.Common
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

