using System;

using FoodSearch.Presentation.Mobile.Common.Components;

using Xamarin.Forms;
using FoodSearch.Presentation.Mobile.Common.ViewModels;

namespace FoodSearch.Presentation.Mobile.Common.Views
{
	public class MainView : ViewBase<ContentPage>
	{
		protected override void ComposeView (ContentPage view)
		{
			var vm = new MainViewModel ();
            view.BindingContext = vm;

            //page header
		    var pageHeader = new Label()
		    {
		        Text = "FoodSearch.pl",
		        TextColor = new Color(66, 139, 202),
		        HorizontalOptions = LayoutOptions.CenterAndExpand,
		        MinimumHeightRequest = 100
		    };

            //label: Wybierz miasto
		    var labelSelectCity = new Label()
		    {
		        Text = "Wybierz miasto:"
		    };
            
            //picker wyboru miast
		    var citiesList = new ListPicker()
		    {
		        Title = "Wybierz miasto"
		    };
            citiesList.SetBinding<MainViewModel>(ListPicker.ItemsSourceProperty, x => x.Cities);
            citiesList.SetBinding<MainViewModel>(ListPicker.SelectedItemProperty, x => x.SelectedCity, BindingMode.TwoWay);

            //label: szukaj ulicę
		    var labelSelectStreet = new Label()
		    {
		        Text = "Wybierz ulicę:"
		    };

            //pasek wyszukiwania ulic
		    var searchStreet = new SearchBar()
		    {
		        Placeholder = "Nazwa ulicy"
		    };
            searchStreet.SetBinding<MainViewModel>(SearchBar.TextProperty, x => x.StreetQuery, BindingMode.TwoWay);
            searchStreet.SetBinding<MainViewModel>(SearchBar.SearchCommandProperty, x => x.SearchStreets);

            //wynik wyszukiwania ulic
		    var streetList = new ListPicker()
		    {
		        Title = "Wybierz ulicę"
		    };
            streetList.SetBinding<MainViewModel>(ListPicker.ItemsSourceProperty, x => x.Streets, BindingMode.TwoWay);
            streetList.SetBinding<MainViewModel>(ListPicker.SelectedItemProperty, x => x.SelectedStreet, BindingMode.TwoWay);

            //label: wybierz numer budynku
		    var selectStreetNumber = new Label()
		    {
		        Text = "Wybierz numer budynku:"
		    };

            //wybór numeru budynku
            var strNumbersList = new ListPicker()
            {
                Title = "Wybierz numer budynku"
            };
            strNumbersList.SetBinding<MainViewModel>(ListPicker.ItemsSourceProperty, x => x.StreetNumbers, BindingMode.TwoWay);
            strNumbersList.SetBinding<MainViewModel>(ListPicker.SelectedItemProperty, x => x.SelectedStreetNumber, BindingMode.TwoWay);

		    var btnSearch = new Button()
		    {
		        Text = "Szukaj",
		        VerticalOptions = LayoutOptions.EndAndExpand,
		    };
            //btnSearch.SetBinding<MainViewModel>(Button.CommandProperty, x => x.SearchRestaurants);
		    btnSearch.Clicked += async (sender, args) =>
		    {
		        await view.DisplayAlert("Wybrany adres", vm.SelectedStreetNumber.Id.ToString(), "OK", "Anuluj");
		    };

			view.Content = new StackLayout()
			{
                Children =
                {
                    pageHeader,
                    labelSelectCity,
                    citiesList,
                    labelSelectStreet,
                    searchStreet,
                    streetList,
                    selectStreetNumber,
                    strNumbersList,
                    btnSearch
                }
			};

            
            

            //przyciski 
            view.ToolbarItems.Clear();
            view.ToolbarItems.Add(new ToolbarItem()
            {
                Name = "Test",
                Command = new Command(() => view.Navigation.PushAsync(ViewLocator.GetView<AuthorizeView>()))
            });
		}
	}
}

