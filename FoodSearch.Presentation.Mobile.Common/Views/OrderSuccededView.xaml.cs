using System;
using System.Collections.Generic;
using Xamarin.Forms;
using FoodSearch.Presentation.Mobile.Common.Services;
using FoodSearch.Presentation.Mobile.Common.ViewModels;
using Ninject;

namespace FoodSearch.Presentation.Mobile.Common
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

