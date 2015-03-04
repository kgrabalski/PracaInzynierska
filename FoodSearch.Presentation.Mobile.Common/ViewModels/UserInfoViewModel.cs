using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using FoodSearch.Service.Client.Interfaces;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class UserInfoViewModel : ViewModelBase
    {
        public UserInfoViewModel(IFoodSearchServiceClient client, IServiceLocator serviceLocator) : base(client, serviceLocator)
        {
            InitializeView();
        }

        private async void InitializeView()
        {
            var userDetails = await Client.User.GetUserDetails();
            FirstName = userDetails.FirstName;
            LastName = userDetails.LastName;
            Email = userDetails.Email;
            PhoneNumber = userDetails.PhoneNumber;
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

    }
}

