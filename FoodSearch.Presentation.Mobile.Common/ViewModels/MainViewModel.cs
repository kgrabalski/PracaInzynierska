using System;

using FoodSearch.Presentation.Mobile.Common.Infrastucture;
using FoodSearch.Presentation.Mobile.Common.Services;
using FoodSearch.Service.Client.Interfaces;
using FoodSearch.Service.Client;
using System.Collections.Generic;
using FoodSearch.Service.Client.Contracts;
using System.Collections.ObjectModel;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using Acr.XamForms.UserDialogs;
using System.Windows.Input;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
        public MainViewModel (IFoodSearchServiceClient client, IServiceLocator serviceLocator) : base(client, serviceLocator)
        {
            InitializeView ();
        }

        private ObservableCollection<City> _cities = new ObservableCollection<City>();
        public ObservableCollection<City> Cities
        {
            get { return _cities; }
            set { SetProperty(ref _cities, value); }
        }

        private bool _canSelectCity = false;

        public bool CanSelectCity
        {
            get { return _canSelectCity; }
            set { SetProperty(ref _canSelectCity, value); }
        }


        private City _selectedCity = new City();
        public City SelectedCity
        {
            get { return _selectedCity; }
            set 
            { 
                bool changed = SetProperty(ref _selectedCity, value); 
                CanQueryStreets = changed && value != null;
            }
        }

        private bool _canQueryStreets = false;

        public bool CanQueryStreets
        {
            get { return _canQueryStreets; }
            set { SetProperty(ref _canQueryStreets, value); }
        }


        private string _streetQuery = string.Empty;
        public string StreetQuery
        {
            get { return _streetQuery; }
            set 
            { 
                if (SetProperty(ref _streetQuery, value))
                {
                    SelectedStreetNumber = null;
                    Streets.Clear();
                    StreetNumbers.Clear();
                    CanSelectStreetNumber = false;
                }
            }
        }

        private ObservableCollection<Street> _streets = new ObservableCollection<Street>();
        public ObservableCollection<Street> Streets
        {
            get { return _streets; }
            set { SetProperty(ref _streets, value); }
        }

        private Street _selectedStreet = new Street();
        public Street SelectedStreet
        {
            get { return _selectedStreet; }
            set 
            { 
                if (SetProperty(ref _selectedStreet, value)) GetStreetNumbers();
            }
        }

        private ObservableCollection<StreetNumber> _streetNumbers = new ObservableCollection<StreetNumber>();
        public ObservableCollection<StreetNumber> StreetNumbers
        {
            get { return _streetNumbers; }
            set { SetProperty(ref _streetNumbers, value); }
        }

        private bool _canSelectStreetNumber = false;

        public bool CanSelectStreetNumber
        {
            get { return _canSelectStreetNumber; }
            set { SetProperty(ref _canSelectStreetNumber, value); }
        }


        private StreetNumber _selectedStreetNumber;
        public StreetNumber SelectedStreetNumber
        {
            get { return _selectedStreetNumber; }
            set { SetProperty(ref _selectedStreetNumber, value); }
        }

        private Command _searchStreets;
        public Command SearchStreets
        {
            get
            {
                return _searchStreets ?? (_searchStreets = new Command(async () =>
                {
                    Services.Dialog.ShowLoading("Wyszukiwanie...");
                    Streets.Clear();
                    Streets = await Client.Core.GetStreets(SelectedCity.Id, StreetQuery);
                    Services.Dialog.HideLoading();
                }));
            }
        }

        private bool _canSearch = false;

        public bool CanSearch
        {
            get { return _canSearch; }
            set { SetProperty(ref _canSearch, value); }
        }
              
        private Command _searchRestaurants;
        public Command SearchRestaurants
        {
            get
            {
                return _searchRestaurants ?? (_searchRestaurants = new Command(async () =>
                {
                    if (SelectedStreetNumber == null || SelectedStreetNumber.Id == 0) {
                        //TODO: usunac na produkcji
                        SelectedStreetNumber = new StreetNumber{Id = 797};
                        return;
                    }
                    await Services.Navigation.Navigate.PushAsync(ViewLocator.RestaurantList);
                    Services.Messaging.Send(SelectedStreetNumber);
                }));
            }
        }

        public ICommand LoginCommand { get { return Services.Authorization.AuthorizationCommand; } }

        private Command _logOutCommand;

        public Command LogoutCommand
        {
            get
            {
                return _logOutCommand ?? (_logOutCommand = new Command(async () =>
                    {
                        await Services.Authorization.Logout();
                    }));
            }
        }

        private Command _userPanelCommand;

        public Command UserPanelCommand
        {
            get
            {
                return _userPanelCommand ?? (_userPanelCommand = new Command(async () =>
                    {
                        if (!Services.Authorization.IsAuthorized)
                        {
                            Services.Dialog.Confirm(new ConfirmConfig(){
                                Title = "Logowanie",
                                Message = "Aby przejść do panelu użytkownika musisz być zalogowany.\nCzy chcesz przejść teraz do ekranu logowania?",
                                OkText = "Tak",
                                CancelText = "Nie",
                                OnConfirm = (bool response) => { if (response) Services.Authorization.AuthorizationCommand.Execute(null); }
                            });
                            return;
                        }
                        await Services.Navigation.Navigate.PushAsync(ViewLocator.UserPanel);
                    }));
            }
        }
            
		private async void InitializeView()
		{
            Services.Dialog.ShowLoading("Ładowanie");
            Cities = await Client.Core.GetCities();
            CanSelectCity = true;
            Services.Dialog.HideLoading();
            //TODO: usunac na produkcji
            CanSearch = true;
		}

	    private async void GetStreetNumbers()
	    {
            Services.Dialog.ShowLoading("Wyszukiwanie...");
            CanSelectStreetNumber = false;
            StreetNumbers = new ObservableCollection<StreetNumber>();
	        StreetNumbers = await Client.Core.GetStreetNumbers(_selectedStreet.Id);
            CanSelectStreetNumber = true;
            Services.Dialog.HideLoading();
	    }
	}
}

