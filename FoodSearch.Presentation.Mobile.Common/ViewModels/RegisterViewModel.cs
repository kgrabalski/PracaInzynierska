using FoodSearch.Presentation.Mobile.Common.Infrastucture;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using FoodSearch.Service.Client.Contracts;
using FoodSearch.Service.Client.Interfaces;
using System.Collections.ObjectModel;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        public RegisterViewModel(IFoodSearchServiceClient client, IServiceLocator serviceLocator) : base(client, serviceLocator)
        {
            InitializeView();
        }

        private async void InitializeView()
        {
            Cities = await Client.Core.GetCities();
            CanSelectCity = true;
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

        private string _phoneNumber;

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { SetProperty(ref _phoneNumber, value); }
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
                CanSearch = changed && value != null;
            }
        }

        private bool _canSearch = false;

        public bool CanSearch
        {
            get { return _canSearch; }
            set { SetProperty(ref _canSearch, value); }
        }

        private string _queryStreet;

        public string QueryStreet
        {
            get { return _queryStreet; }
            set 
            { 
                if (SetProperty(ref _queryStreet, value))
                {
                    SelectedStreetNumber = null;
                    Streets.Clear();
                    StreetNumbers.Clear();
                    CanSelectStreetNumber = false;
                }
            }
        }

        private Command _searchStreet;

        public Command SearchStreet
        {
            get
            {
                return _searchStreet ?? (_searchStreet = new Command(async () =>
                    {
                        Services.Dialog.ShowLoading("Wyszukiwanie...");
                        Streets.Clear();
                        Streets = await Client.Core.GetStreets(SelectedCity.Id, QueryStreet);
                        Services.Dialog.HideLoading();
                    }));
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

        private ObservableCollection<StreetNumber> _streetNumber = new ObservableCollection<StreetNumber>();

        public ObservableCollection<StreetNumber> StreetNumbers
        {
            get { return _streetNumber; }
            set { SetProperty(ref _streetNumber, value); }
        }

        private bool _canSelectStreetNumber = false;

        public bool CanSelectStreetNumber
        {
            get { return _canSelectStreetNumber; }
            set { SetProperty(ref _canSelectStreetNumber, value); }
        }

        private StreetNumber _selectedStreetNumber = new StreetNumber();

        public StreetNumber SelectedStreetNumber
        {
            get { return _selectedStreetNumber; }
            set { SetProperty(ref _selectedStreetNumber, value); }
        }

        private string _flatNumber = string.Empty;

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
                return _registerCommand ?? (_registerCommand = new Command(async () =>
                    {
                        if (string.IsNullOrEmpty(FirstName))
                        {
                            Services.Dialog.Toast("Proszę podać imię");
                            return;
                        }
                        if (string.IsNullOrEmpty(LastName))
                        {
                            Services.Dialog.Toast("Proszę podać nazwisko");
                            return;
                        }
                        if (string.IsNullOrEmpty(Email))
                        {
                            Services.Dialog.Toast("Proszę podać poprawny adres email");
                            return;
                        }
                        if (string.IsNullOrEmpty(PhoneNumber))
                        {
                            Services.Dialog.Toast("Proszę podać numer telefonu");
                            return;
                        }
                        if (string.IsNullOrEmpty(Password))
                        {
                            Services.Dialog.Toast("Proszę podać hasło");
                            return;
                        }
                        if (Password != RepeatPassword)
                        {
                            Services.Dialog.Toast("Podane hasła rożnią się");
                            return;
                        }
                        if (SelectedStreetNumber == null || SelectedStreetNumber.Id == 0)
                        {
                            Services.Dialog.Toast("Proszę wybrać miasto, ulicę oraz numer budynku");
                        }

                        Services.Dialog.ShowLoading("Czekaj...");
                        var result = await Client.User.Register(FirstName, LastName, Email, PhoneNumber, Password, RepeatPassword, SelectedStreetNumber.Id, FlatNumber);
                        Services.Dialog.HideLoading();
                        switch (result)
                        {
                            case RegistrationResult.EmailDuplicated:
                                {
                                    Services.Dialog.Toast("Użytkownik o podanym adresie email już istnieje");
                                    break;
                                }
                            case RegistrationResult.NotCreated:
                                {
                                    Services.Dialog.Toast("Błąd podczas rejestracji - podano niepoprawne dane");
                                    break;
                                }
                            case RegistrationResult.Created:
                                {
                                    Services.Dialog.Toast("Pomyślnie utworzono konto użytkownika.");
                                    FirstName = string.Empty;
                                    LastName = string.Empty;
                                    Email = string.Empty;
                                    Password = string.Empty;
                                    RepeatPassword = string.Empty;
                                    SelectedCity = new City();
                                    QueryStreet = string.Empty;
                                    SelectedStreet = new Street();
                                    SelectedStreetNumber = new StreetNumber();
                                    FlatNumber = string.Empty;
                                    break;
                                }
                        }
                    }));
            }
        }
    }
}