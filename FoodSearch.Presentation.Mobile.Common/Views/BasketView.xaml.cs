using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Ninject;
using FoodSearch.Presentation.Mobile.Common.Services;
using FoodSearch.Presentation.Mobile.Common.ViewModels;

namespace FoodSearch.Presentation.Mobile.Common.Views
{	
	public partial class BasketView : ContentPage
	{	
		public BasketView ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, true);
            BindingContext = DependencyResolver.Current.Get<BasketViewModel>();
		}
	}
}

