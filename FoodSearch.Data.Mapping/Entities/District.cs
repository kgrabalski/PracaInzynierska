
namespace FoodSearch.Data.Mapping.Entities
{
    public class District
    {
        public virtual int DistrictId { get; set; }
        public virtual string Name { get; set; }
        public virtual int CityId { get; set; }
        public virtual City City { get; set; }
    }
}
