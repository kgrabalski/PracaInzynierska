using System;
using System.Collections.Generic;
using Xamarin.Forms;
using FoodSearch.Presentation.Mobile.Common.ViewModels;
using Ninject;

namespace FoodSearch.Presentation.Mobile.Common.Views
{	
	public partial class MainView : ContentPage
	{	
		public MainView ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, true);
            BindingContext = ViewLocator.DependencyResolver.Get<MainViewModel>();
		}
	}
}

