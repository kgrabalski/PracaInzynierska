
using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.StoredProcedure.Results.Mapping
{
    internal class RestaurantDeliveryRangeMap : ClassMap<RestaurantDeliveryRange>
    {
        public RestaurantDeliveryRangeMap()
        {
            Table("GetDeliveryRange");
            ReadOnly();
            Id(x => x.RestaurantId).Column("RestaurantId").Not.Nullable();
            Map(x => x.RestaurantName).Column("RestaurantName").Not.Nullable();
            Map(x => x.Lat).Column("Lat").Not.Nullable();
            Map(x => x.Lon).Column("Lon").Not.Nullable();
            Map(x => x.HasDeliveryRadius).Column("HasDeliveryRadius").Not.Nullable();
            Map(x => x.DeliveryRadius).Column("DeliveryRadius").Not.Nullable();
            Map(x => x.Polygon).Column("Polygon").Not.Nullable();
        }
    }
}
