using System;
using FoodSearch.Presentation.Mobile.Common.Services.Interfaces;

namespace FoodSearch.Presentation.Mobile.Common.Services
{
    public class RestaurantService : IRestaurantService
    {
        public Guid CurrentRestaurantId { get; set; }
        public string CurrentRestaurantName { get; set; }
    }
}

