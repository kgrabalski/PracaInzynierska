using FoodSearch.Presentation.Mobile.Common.Infrastucture;
using FoodSearch.Presentation.Mobile.Common.Models;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using FoodSearch.Service.Client.Contracts;
using FoodSearch.Service.Client.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class OpinionListViewModel : ViewModelBase
    {
        public OpinionListViewModel(IFoodSearchServiceClient client, IServiceLocator serviceLocator) : base(client, serviceLocator)
        {
            Services.Messaging.Register<RestaurantId>(GetOpinions);
        }

        private ObservableCollection<Opinion> _opinions = new ObservableCollection<Opinion>();

        public ObservableCollection<Opinion> Opinions
        {
            get { return _opinions; }
            set { SetProperty(ref _opinions, value); }
        }

        private int _opinionsPage = 0;
        private Guid _restaurantId = Guid.Empty;

        private Command _getMoreOpinions;
        public Command GetMoreOpinions
        {
            get
            {
                return _getMoreOpinions ?? (_getMoreOpinions = new Command(async () =>
                    {
                        if (_restaurantId == Guid.Empty)
                            return;

                        Services.Dialog.ShowLoading("Wczytywanie opinii...");
                        var opinions = await Client.Core.GetOpinions(_restaurantId, 0, ++_opinionsPage);
                        foreach (var o in opinions)
                        {
                            Opinions.Add(o);
                        }
                        Services.Dialog.HideLoading();
                    }));
            }
        }
            
        private async void GetOpinions(RestaurantId r)
        {
            _opinionsPage = 0;
            _restaurantId = r.Id;
            Opinions = await Client.Core.GetOpinions(_restaurantId, 0, _opinionsPage);
        }
    }
}
