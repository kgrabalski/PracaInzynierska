
using FoodSearch.Presentation.Mobile.Common.Components;
using FoodSearch.Presentation.Mobile.WindowsPhone.Renderers;

using Microsoft.Phone.Controls;

using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(CookieWebView), typeof(CookieWebViewRenderer))]
namespace FoodSearch.Presentation.Mobile.WindowsPhone.Renderers
{
    public class CookieWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.IsScriptEnabled = true;
                Control.Navigating += ControlOnNavigating;
                Control.Navigated += ControlOnNavigated;
            }
        }
        protected void ControlOnNavigated(object sender, System.Windows.Navigation.NavigationEventArgs navigationEventArgs)
        {
            CookieWebView.Cookies = Control.GetCookies();
            CookieWebView.OnNavigated(new CookieNavigatedEventArgs()
            {
                Cookies = CookieWebView.Cookies,
                Url = navigationEventArgs.Uri.ToString()
            });
        }
        protected void ControlOnNavigating(object sender, NavigatingEventArgs navigatingEventArgs)
        {
            CookieWebView.OnNavigating(new CookieNavigationEventArgs()
            {
                Url = navigatingEventArgs.Uri.ToString()
            });
        }
        public CookieWebView CookieWebView
        {
            get { return Element as CookieWebView; }
        }
    }
}
