using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FoodSearch.Presentation.Mobile.Common.Views
{
    public class RestaurantMenuView : ViewBase<TabbedPage>
    {
        protected override void ComposeView(TabbedPage view)
        {
            view.Children.Add(ViewLocator.DishList);
            view.Children.Add(ViewLocator.OpinionList);
        }
    }
}
