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
    
    public partial class User
    {
        public User()
        {
            this.DeliveryAddresses = new HashSet<DeliveryAddress>();
            this.Opinions = new HashSet<Opinion>();
            this.Orders = new HashSet<Order>();
            this.RegistrationConfirmations = new HashSet<RegistrationConfirmation>();
            this.RestaurantUsers = new HashSet<RestaurantUser>();
        }
    
        public System.Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public int UserTypeId { get; set; }
        public int UserStateId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string PhoneNumber { get; set; }
    
        public virtual ICollection<DeliveryAddress> DeliveryAddresses { get; set; }
        public virtual ICollection<Opinion> Opinions { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<RegistrationConfirmation> RegistrationConfirmations { get; set; }
        public virtual ICollection<RestaurantUser> RestaurantUsers { get; set; }
        public virtual UserState UserState { get; set; }
        public virtual UserType UserType { get; set; }
    }
}
