
using Acr.XamForms.Mobile.WindowsPhone.Net;
using Acr.XamForms.UserDialogs.WindowsPhone;

using FoodSearch.Presentation.Mobile.Common;

using Microsoft.Phone.Controls;

using Xamarin.Forms;

namespace FoodSearch.Presentation.Mobile.WindowsPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            //FIX:  create IUserDialogService and INetworkService instance
            //      due to Windows Phone deployment process issues
            var userDialog = new UserDialogService();
            var networkService = new NetworkService();
            Forms.Init();
            Content = ViewLocator.StartScreen.ConvertPageToUIElement(this);
        }
    }
}