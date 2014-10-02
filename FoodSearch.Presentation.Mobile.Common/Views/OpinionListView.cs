using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Presentation.Mobile.Common.ViewModels;

using Xamarin.Forms;

namespace FoodSearch.Presentation.Mobile.Common.Views
{
    public class OpinionListView : ViewBase<ContentPage>
    {
        protected override void ComposeView(ContentPage view)
        {
            var vm = new OpinionListViewModel();
            view.BindingContext = vm;

            view.Title = "Opinie";
            view.Content = new Label() {Text = "Opinie"};
        }
    }
}
