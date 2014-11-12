using System;
using System.Collections.Generic;

using FoodSearch.Presentation.Mobile.Common.Components;

using Xamarin.Forms;
using FoodSearch.Presentation.Mobile.Common.Services;
using Ninject;
using FoodSearch.Presentation.Mobile.Common.ViewModels;

namespace FoodSearch.Presentation.Mobile.Common.Views
{	
	public partial class PaymentView : ContentPage
	{
	    private readonly PaymentViewModel _vm;

		public PaymentView ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, true);
            _vm = DependencyResolver.Current.Get<PaymentViewModel>();
		    BindingContext = _vm;
		    var webView = this.FindByName<CookieWebView>("webView");
		    webView.Cookies = _vm.ServiceClient.Cookies.GetCookies(new Uri("http://foodsearch.azurewebsites.net"));
            webView.Navigating += WebViewOnNavigating;
		}

	    private void WebViewOnNavigating(object sender, CookieNavigationEventArgs args)
	    {
	        _vm.OnUrlChanged(args.Url);
	    }
	}
}

