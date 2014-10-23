using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodSearch.Service.Client.Interfaces;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel(IFoodSearchServiceClient client) : base(client)
        {
            
        }
    }
}
