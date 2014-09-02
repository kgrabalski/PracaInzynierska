using System;


namespace FoodSearch.Presentation.Web.Site.Models
{
    public class RestaurantUser
    {
        public Guid UserId { get; set; }
        public Guid RestaurantId { get; set; }
    }
}