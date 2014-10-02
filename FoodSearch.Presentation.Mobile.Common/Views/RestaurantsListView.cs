using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Presentation.Mobile.Common.Services;
using FoodSearch.Presentation.Mobile.Common.ViewModels;
using FoodSearch.Presentation.Mobile.Common.Views.Partial;
using FoodSearch.Service.Client.Contracts;

using Xamarin.Forms;

namespace FoodSearch.Presentation.Mobile.Common.Views
{
    public class RestaurantsListView : ViewBase<ContentPage>
    {
        private object _itemSelected = null;
        protected override void ComposeView(ContentPage view)
        {
            
            var vm = new RestaurantListViewModel();
            view.BindingContext = vm;
			view.Title = "Znalezione restauracje";

			NavigationPage.SetHasNavigationBar(view, true);

            var restaurantList = new ListView();
            restaurantList.SetBinding<RestaurantListViewModel>(ListView.ItemsSourceProperty, x => x.Restaurants, BindingMode.TwoWay);
            restaurantList.SetBinding<RestaurantListViewModel>(ListView.SelectedItemProperty, x => x.SelectedRestaurant, BindingMode.TwoWay);
            restaurantList.ItemTemplate = new DataTemplate(typeof (RestaurantItemPartial));
            restaurantList.RowHeight = 100;
            restaurantList.VerticalOptions = LayoutOptions.FillAndExpand;
            restaurantList.ItemTapped += (sender, args) =>
            {
                if (args.Item != null && !_itemSelected.Equals(args.Item))
                {
                    restaurantList.SelectedItem = null;
                    restaurantList.SelectedItem = args.Item;
                }
            };
            restaurantList.ItemSelected += (sender, args) =>
            {
                _itemSelected = args.SelectedItem;
            };

            view.Appearing += (sender, args) =>
            {
                restaurantList.SelectedItem = null;
            };


            view.Title = "Lista restauracji";
            view.Content = new StackLayout()
            {
                Children =
                {
                    restaurantList
                }
            };
        }
    }
}
