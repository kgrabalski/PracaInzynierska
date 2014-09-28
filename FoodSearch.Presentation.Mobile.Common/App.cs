using System;
using Xamarin.Forms;

using FoodSearch.Service.Client;
using System.Linq;

namespace FoodSearch.Presentation.Mobile.Common
{
	public class App
	{
		public static FoodSearchServiceClient Client = new FoodSearchServiceClient();

		public static Page GetMainPage ()
		{	
			var btn = new Button {
				Text = "Pobierz miasta"
			};
			btn.Clicked += async (sender, e) => {
				var cities = await Client.GetCities();
				btn.Text = cities.First().Name;
			};


			return new ContentPage {
				Content = new StackLayout {
					Children = {
						new Label {
							Text = "Test WebAPI",
							VerticalOptions = LayoutOptions.CenterAndExpand,
							HorizontalOptions = LayoutOptions.CenterAndExpand,
						},
						btn
					}
				}
			};
		}
	}
}

