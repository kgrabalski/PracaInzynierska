namespace FoodSearch.BusinessLogic.Domain.Order.Models
{
    public class DeliveryAddress
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public int StreetId { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string FlatNumber { get; set; }
    }
}
