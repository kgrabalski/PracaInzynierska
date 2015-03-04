
using FoodSearch.Presentation.Mobile.Common.Services;
using FoodSearch.Presentation.Mobile.Common.ViewModels;
using Ninject;
using Xamarin.Forms;

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

