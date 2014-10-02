using System;

using FoodSearch.Presentation.Mobile.Common.Components;
using FoodSearch.Presentation.Mobile.Common.Services;

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
		    view.Title = "FoodSearch";

            NavigationPage.SetHasNavigationBar(view, true);

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
            btnSearch.SetBinding<MainViewModel>(Button.CommandProperty, x => x.SearchRestaurants);

			view.Content = new StackLayout()
			{
                Children =
                {
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
                Command = new Command(() => NavigationService.Navigation.PushAsync(ViewLocator.Authorize))
            });
		}
	}
}

