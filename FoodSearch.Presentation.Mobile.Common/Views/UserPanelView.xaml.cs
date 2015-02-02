using System;
using System.Collections.Generic;

using Xamarin.Forms;
using FoodSearch.Presentation.Mobile.Common.Services;
using Ninject;
using FoodSearch.Presentation.Mobile.Common.ViewModels;

namespace FoodSearch.Presentation.Mobile.Common
{
    public partial class UserPanelView : TabbedPage
    {
        public UserPanelView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
            BindingContext = new {
                UserInfo = DependencyResolver.Current.Get<UserInfoViewModel>(),
                OrdersHistory = DependencyResolver.Current.Get<OrdersHistoryViewModel>()
            };
        }
    }
}

