//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace generatorRestauracji
{
    using System;
    using System.Collections.Generic;
    
    public partial class Restaurant
    {
        public Restaurant()
        {
            this.Dishes = new HashSet<Dish>();
            this.OpeningHours = new HashSet<OpeningHour>();
            this.Opinions = new HashSet<Opinion>();
            this.Orders = new HashSet<Order>();
            this.RestaurantCuisines = new HashSet<RestaurantCuisine>();
            this.RestaurantUsers = new HashSet<RestaurantUser>();
        }
    
        public System.Guid RestaurantId { get; set; }
        public int AddressId { get; set; }
        public int ImageId { get; set; }
        public string Name { get; set; }
        public decimal MinOrderAmount { get; set; }
        public bool IsOpen { get; set; }
        public bool IsDeleted { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal FreeDeliveryFrom { get; set; }
        public decimal DeliveryRadius { get; set; }
        public System.Data.Entity.Spatial.DbGeography DeliveryRange { get; set; }
        public bool HasDeliveryRadius { get; set; }
        public string Description { get; set; }
    
        public virtual Address Address { get; set; }
        public virtual ICollection<Dish> Dishes { get; set; }
        public virtual Image Image { get; set; }
        public virtual ICollection<OpeningHour> OpeningHours { get; set; }
        public virtual ICollection<Opinion> Opinions { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<RestaurantCuisine> RestaurantCuisines { get; set; }
        public virtual ICollection<RestaurantUser> RestaurantUsers { get; set; }
    }
}