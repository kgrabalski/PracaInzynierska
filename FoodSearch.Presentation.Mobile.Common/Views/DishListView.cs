using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Presentation.Mobile.Common.ViewModels;
using FoodSearch.Presentation.Mobile.Common.Views.Partial;

using Xamarin.Forms;

namespace FoodSearch.Presentation.Mobile.Common.Views
{
    public class DishListView : ViewBase<ContentPage>
    {
        protected override void ComposeView(ContentPage view)
        {
            var vm = new DishListViewModel();
            view.BindingContext = vm;
            view.Title = "Menu";

            NavigationPage.SetHasNavigationBar(View, true);

            var dishList = new ListView();
            dishList.SetBinding<DishListViewModel>(ListView.ItemsSourceProperty, x => x.DishGroups);
            dishList.SetBinding<DishListViewModel>(ListView.SelectedItemProperty, x => x.SelectedDish);
            dishList.ItemTemplate = new DataTemplate(typeof(DishItemPartial));
            dishList.IsGroupingEnabled = true;
            dishList.GroupDisplayBinding = new Binding("GroupName");
            dishList.GroupShortNameBinding = new Binding("GroupName");


            view.Content = new StackLayout()
            {
                Children =
                {
                    dishList
                }
            };
        }
    }
}
