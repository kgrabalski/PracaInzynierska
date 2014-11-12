using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;

namespace FoodSearch.Presentation.Mobile.Common.Services
{
    public class NavigationService : INavigationService
    {
        public INavigation Navigate
        {
			get { return ViewLocator.StartScreen.Navigation; }
        }
    }
}
