using System;
using System.Collections.Generic;
using Xamarin.Forms;
using FoodSearch.Presentation.Mobile.Common.Services;
using Ninject;

namespace FoodSearch.Presentation.Mobile.Common.Views
{	
	public partial class OrderFailedView : ContentPage
	{	
		public OrderFailedView ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, true);
            BindingContext = DependencyResolver.Current.Get<OrderFailedViewModel>();
		}
	}
}

