using System;
using System.Collections.Generic;
using Xamarin.Forms;
using FoodSearch.Presentation.Mobile.Common.Services;
using Ninject;
using FoodSearch.Presentation.Mobile.Common.ViewModels;

namespace FoodSearch.Presentation.Mobile.Common
{	
	public partial class OrderView : ContentPage
	{	
		public OrderView ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, true);
            BindingContext = DependencyResolver.Current.Get<OrderViewModel>();
		}
	}
}

