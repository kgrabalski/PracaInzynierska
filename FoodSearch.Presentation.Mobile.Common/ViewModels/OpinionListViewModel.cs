using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodSearch.Service.Client.Interfaces;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class OpinionListViewModel : ViewModelBase
    {
        public OpinionListViewModel(IFoodSearchServiceClient client) : base(client)
        {
        }
        
    }
}
