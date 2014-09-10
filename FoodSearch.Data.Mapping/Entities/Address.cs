
namespace FoodSearch.Data.Mapping.Entities
{
    public class Address
    {
        public virtual int AddressId { get; set; }
        public virtual int DistrictId { get; set; }
        public virtual District District { get; set; }
        public virtual int StreetId { get; set; }
        public virtual Street Street { get; set; }
        public virtual string Number { get; set; }
        public virtual float Lat { get; set; }
        public virtual float Lon { get; set; }
    }
}
