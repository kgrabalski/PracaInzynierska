using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Presentation.Mobile.Common.Services
{
    public static class DialogService
    {
        public static Task<bool> Question(string title, string message, string okButtonText = "OK", string cancelButtonText = "Anuluj")
        {
            return ViewLocator.CurrentView.DisplayAlert(title, message, okButtonText, cancelButtonText);
        }

        public static Task Alert(string title, string message, string dismissButtonText = "OK")
        {
            return ViewLocator.CurrentView.DisplayAlert(title, message, dismissButtonText);
        }
    }
}
