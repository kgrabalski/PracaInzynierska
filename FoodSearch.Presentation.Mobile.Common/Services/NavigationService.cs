using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace FoodSearch.Presentation.Mobile.Common.Services
{
    public static class NavigationService
    {
        public static INavigation Navigation
        {
			get { return ViewLocator.StartScreen.Navigation; }
        }
    }
}
