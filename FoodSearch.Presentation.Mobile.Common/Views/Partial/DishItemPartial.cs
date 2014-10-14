using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FoodSearch.Presentation.Mobile.Common.Views.Partial
{
    public class DishItemPartial : ViewCell
    {
        public DishItemPartial()
        {
            var lblName = new Label()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Scale = 1.2f,
                AnchorX = 0,
                AnchorY = 0
            };
            var lblPrice = new Label() { HorizontalOptions = LayoutOptions.FillAndExpand };

            lblName.SetBinding(Label.TextProperty, "Name");
            lblPrice.SetBinding(Label.TextProperty, "PricePln");
			

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
                            lblPrice
                        }
                    },

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
