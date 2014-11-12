using System;
using System.Collections.Generic;
using Xamarin.Forms;
using FoodSearch.Presentation.Mobile.Common.Services;
using FoodSearch.Presentation.Mobile.Common.ViewModels;
using Ninject;

namespace FoodSearch.Presentation.Mobile.Common.Views
{	
    public partial class AuthorizeView : TabbedPage
	{	
		public AuthorizeView ()
		{
			InitializeComponent ();
            BindingContext = new {
                Login = DependencyResolver.Current.Get<LoginViewModel>(),
                Register = DependencyResolver.Current.Get<RegisterViewModel>()
            };
		}
	}
}

