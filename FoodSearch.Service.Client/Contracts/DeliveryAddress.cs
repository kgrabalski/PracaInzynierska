using System;

namespace FoodSearch.Service.Client
{
    public class DeliveryAddress
    {
        public int AddressId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string FlatNumber { get; set; }
    }
}

