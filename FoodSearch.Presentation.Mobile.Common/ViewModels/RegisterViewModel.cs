using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodSearch.Service.Client.Interfaces;
using Acr.XamForms.UserDialogs;
using FoodSearch.Presentation.Mobile.Common.Infrastucture;
using System.Collections.ObjectModel;
using FoodSearch.Service.Client.Contracts;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        public RegisterViewModel(IFoodSearchServiceClient client, IAuthorizationService authorizationService, IUserDialogService dialogService) : base(client, authorizationService, dialogService)
        {
        }

        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private string _repeatPassword;

        public string RepeatPassword
        {
            get { return _repeatPassword; }
            set { SetProperty(ref _repeatPassword, value); }
        }

        private ObservableCollection<City> _cities;

        public ObservableCollection<City> Cities
        {
            get { return _cities; }
            set { SetProperty(ref _cities, value); }
        }

        private City _selectedCity;
        public City SelectedCity
        {
            get { return _selectedCity; }
            set { SetProperty(ref _selectedCity, value); }
        }

        private string _queryStreet;

        public string QueryStreet
        {
            get { return _queryStreet; }
            set { SetProperty(ref _queryStreet, value); }
        }

        private Command _searchStreet;

        public Command SearchStreet
        {
            get
            {
                return _searchStreet ?? (_searchStreet = new Command(() =>
                    {

                    }));
            }
        }

        private ObservableCollection<Street> _streets;
        public ObservableCollection<Street> Streets
        {
            get { return _streets; }
            set { SetProperty(ref _streets, value); }
        }

        private Street _selectedStreet;

        public Street SelectedStreet
        {
            get { return _selectedStreet; }
            set { SetProperty(ref _selectedStreet, value); }
        }

        private ObservableCollection<StreetNumber> _streetNumber;

        public ObservableCollection<StreetNumber> StreetNumbers
        {
            get { return _streetNumber; }
            set { SetProperty(ref _streetNumber, value); }
        }

        private StreetNumber _selectedStreetNumber;

        public StreetNumber SelectedStreetNumber
        {
            get { return _selectedStreetNumber; }
            set { SetProperty(ref _selectedStreetNumber, value); }
        }

        private string _flatNumber;

        public string FlatNumber
        {
            get { return _flatNumber; }
            set { SetProperty(ref _flatNumber, value); }
        }

        private Command _registerCommand;

        public Command RegisterCommand
        {
            get
            {
                return _registerCommand ?? (_registerCommand = new Command(() =>
                    {
                        DialogService.Toast("Register");
                    }));
            }
        }
    }
}
