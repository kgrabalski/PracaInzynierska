﻿using System;

namespace FoodSearch.BusinessLogic.Domain.Restaurant.Models
{
    public class PartnerRestaurant
    {
        public Guid RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public int LogoId { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public float RestaurantRating { get; set; }
        public int StarsCount { get; set; }
        public int UsersVoted { get; set; }
    }
}
