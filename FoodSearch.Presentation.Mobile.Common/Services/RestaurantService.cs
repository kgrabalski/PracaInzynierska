using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;
using System;

namespace FoodSearch.Presentation.Mobile.Common.Services
{
    public class RestaurantService : IRestaurantService
    {
        public Guid CurrentRestaurantId { get; set; }
        public string CurrentRestaurantName { get; set; }
    }
}

