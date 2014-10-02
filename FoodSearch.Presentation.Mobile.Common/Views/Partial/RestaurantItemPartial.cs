using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Service.Client.Contracts;

using Xamarin.Forms;

namespace FoodSearch.Presentation.Mobile.Common.Views.Partial
{
    public class RestaurantItemPartial : ViewCell
    {
        public RestaurantItemPartial()
        {
            var lblName = new Label()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Scale = 1.2f,
                AnchorX = 0,
                AnchorY = 0
            };
            var lblAddress = new Label() {HorizontalOptions = LayoutOptions.FillAndExpand};
            var lblOpenings = new Label() {HorizontalOptions = LayoutOptions.FillAndExpand};

            lblName.SetBinding(Label.TextProperty, "RestaurantName");
            lblAddress.SetBinding(Label.TextProperty, "Address");
            lblOpenings.SetBinding(Label.TextProperty, "Openings");

            var layout = new StackLayout()
            {
                Padding = new Thickness(5, 0),
                Orientation = StackOrientation.Horizontal,
                Children =
                {
					new BoxView() {BackgroundColor = Color.Blue, HeightRequest = 90, WidthRequest = 90},
                    new StackLayout()
                    {
                        Children =
                        {
                            lblName,
                            lblAddress,
                            lblOpenings
                        }
                    }
                }
            };
            View = layout;
        }

        protected override void OnBindingContextChanged()
        {
            View.BindingContext = BindingContext;
            base.OnBindingContextChanged();
        }
    }
}
