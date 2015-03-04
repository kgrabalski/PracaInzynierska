
using Acr.XamForms.Mobile.WindowsPhone.Net;
using Acr.XamForms.UserDialogs.WindowsPhone;

using FoodSearch.Presentation.Mobile.Common;

using Microsoft.Phone.Controls;

using Xamarin.Forms;

namespace FoodSearch.Presentation.Mobile.WindowsPhone
{
    public partial class MainPage : PhoneApplicationPage
    {
        //CookieWebViewRenderer _renderer = new CookieWebViewRenderer();

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            //FIX: create IUserDialogService instance
            var userDialog = new UserDialogService();
            var networkService = new NetworkService();
            Forms.Init();
            Content = ViewLocator.StartScreen.ConvertPageToUIElement(this);
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}