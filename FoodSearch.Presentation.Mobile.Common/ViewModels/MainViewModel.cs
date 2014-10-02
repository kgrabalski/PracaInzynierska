using System;

using FoodSearch.Presentation.Mobile.Common.Infrastucture;
using FoodSearch.Presentation.Mobile.Common.Services;
using FoodSearch.Service.Client.Interfaces;
using FoodSearch.Service.Client;
using System.Collections.Generic;
using FoodSearch.Service.Client.Contracts;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
        #region Cities

        private IEnumerable<City> _cities;
        public IEnumerable<City> Cities
        {
            get
            {
                return _cities;
            }
            set
            {
                if (_cities != value)
                {
                    _cities = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region SelectedCity

        private City _selectedCity;
        public City SelectedCity
        {
            get
            {
                return _selectedCity;
            }
            set
            {
                if (_selectedCity != value)
                {
                    _selectedCity = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region StreetQuery

        private string _streetQuery;
        public string StreetQuery
        {
            get
            {
                return _streetQuery;
            }
            set
            {
                if (_streetQuery != value)
                {
                    _streetQuery = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion
        
        #region Streets

        private IEnumerable<Street> _streets = new List<Street>();
        public IEnumerable<Street> Streets
        {
            get
            {
                return _streets;
            }
            set
            {
                if (!_streets.Equals(value))
                {
                    _streets = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region SelectedStreet

        private Street _selectedStreet;
        public Street SelectedStreet
        {
            get
            {
                return _selectedStreet;
            }
            set
            {
                if (_selectedStreet != value)
                {
                    _selectedStreet = value;
                    GetStreetNumbers();
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region StreetNumbers

        private IEnumerable<StreetNumber> _streetNumbers = new List<StreetNumber>();
        public IEnumerable<StreetNumber> StreetNumbers
        {
            get
            {
                return _streetNumbers;
            }
            set
            {
                if (!_streetNumbers.Equals(value))
                {
                    _streetNumbers = value;
                    SelectedStreetNumber = null;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region SelectedStreetNumber

        private StreetNumber _selectedStreetNumber;
        public StreetNumber SelectedStreetNumber
        {
            get
            {
                return _selectedStreetNumber;
            }
            set
            {
                if (_selectedStreetNumber != value)
                {
                    _selectedStreetNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region SearchStreets

        private Command _searchStreets;
        public Command SearchStreets
        {
            get
            {
                return _searchStreets ?? (_searchStreets = new Command(async () =>
                {
                    Streets = new List<Street>();
                    Streets = await Client.Core.GetStreets(SelectedCity.Id, StreetQuery);
                }));
            }
        }
        #endregion

        #region SearchRestaurants

        private Command _searchRestaurants;
        public Command SearchRestaurants
        {
            get
            {
                return _searchRestaurants ?? (_searchRestaurants = new Command(async () =>
                {
                    await NavigationService.Navigation.PushAsync(ViewLocator.RestaurantList);
                    //TODO: usunąć stałe AddressId
                    MessageService.Send(SelectedStreetNumber ?? new StreetNumber(){Id = 797});
                }));
            }
        }
        #endregion

		public MainViewModel ()
		{
			Cities = new List<City> ();
			InitializeView ();
		}

		private async void InitializeView()
		{
			Cities = await Client.Core.GetCities ();
		}

	    private async void GetStreetNumbers()
	    {
	        StreetNumbers = new List<StreetNumber>();
	        StreetNumbers = await Client.Core.GetStreetNumbers(_selectedStreet.Id);
	    }
	}
}

