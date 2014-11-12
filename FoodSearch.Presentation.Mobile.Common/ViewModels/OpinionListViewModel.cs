using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodSearch.Service.Client.Interfaces;
using Acr.XamForms.UserDialogs;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class OpinionListViewModel : ViewModelBase
    {
        public OpinionListViewModel(IFoodSearchServiceClient client, IServiceLocator serviceLocator) : base(client, serviceLocator)
        {
        }
        
    }
}
