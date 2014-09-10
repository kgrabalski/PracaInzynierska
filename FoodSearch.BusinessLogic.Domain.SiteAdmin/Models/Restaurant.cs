using System;

namespace FoodSearch.BusinessLogic.Domain.SiteAdmin.Models
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        public int LogoId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public int AddressId { get; set; }
    }
}
