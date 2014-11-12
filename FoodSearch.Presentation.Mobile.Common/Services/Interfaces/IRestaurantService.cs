using System;

namespace FoodSearch.Presentation.Mobile.Common.Services.Interfaces
{
    public interface IRestaurantService
    {
        Guid CurrentRestaurantId {get;set;}
        string CurrentRestaurantName {get;set;}
    }
}

