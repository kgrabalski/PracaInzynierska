using System;

namespace FoodSearch.Service.Client.Requests
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public int AddressId { get; set; }
        public string FlatNumber { get; set; }
    }
}

