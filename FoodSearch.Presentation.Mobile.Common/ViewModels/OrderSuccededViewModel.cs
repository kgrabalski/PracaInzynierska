﻿using System;
using FoodSearch.Service.Client.Interfaces;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using Acr.XamForms.UserDialogs;

namespace FoodSearch.Presentation.Mobile.Common.ViewModels
{
    public class OrderSuccededViewModel : ViewModelBase
    {
        public OrderSuccededViewModel(IFoodSearchServiceClient client, IAuthorizationService authorizationService, IUserDialogService dialogService) : base(client, authorizationService, dialogService)
        {
        }
    }
}
