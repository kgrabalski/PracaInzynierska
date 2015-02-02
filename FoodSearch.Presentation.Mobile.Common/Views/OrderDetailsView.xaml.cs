using System;
using System.Collections.Generic;

using Xamarin.Forms;
using FoodSearch.Presentation.Mobile.Common.ViewModels;
using FoodSearch.Presentation.Mobile.Common.Services;
using Ninject;

namespace FoodSearch.Presentation.Mobile.Common.Views
{
    public partial class OrderDetailsView : ContentPage
    {
        public OrderDetailsView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
            BindingContext = DependencyResolver.Current.Get<OrderDetailsViewModel>();
        }
    }
}

