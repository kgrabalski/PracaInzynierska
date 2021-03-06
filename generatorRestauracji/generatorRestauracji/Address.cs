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
    
    public partial class Address
    {
        public Address()
        {
            this.DeliveryAddresses = new HashSet<DeliveryAddress>();
            this.Restaurants = new HashSet<Restaurant>();
        }
    
        public int AddressId { get; set; }
        public int DistrictId { get; set; }
        public int StreetId { get; set; }
        public string Number { get; set; }
        public Nullable<double> Lat { get; set; }
        public Nullable<double> Lon { get; set; }
    
        public virtual District District { get; set; }
        public virtual ICollection<DeliveryAddress> DeliveryAddresses { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}
