using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.StoredProcedure.Results.Mapping
{
    public class RestaurantRatingMap : ClassMap<RestaurantRating>
    {
        public RestaurantRatingMap()
        {
            Table("GetRestaurantRating");
            ReadOnly();
            Id(x => x.RestaurantId).Column("RestaurantId").Not.Nullable();
            Map(x => x.UsersVoted).Column("UsersVoted").Not.Nullable();
            Map(x => x.TotalRating).Column("TotalRating").Not.Nullable();
            Map(x => x.StarsCount).Column("StarsCount").Not.Nullable();
            Map(x => x.Percentage1Star).Column("Percentage1Star").Not.Nullable();
            Map(x => x.Percentage2Stars).Column("Percentage2Star").Not.Nullable();
            Map(x => x.Percentage3Stars).Column("Percentage3Star").Not.Nullable();
            Map(x => x.Percentage4Stars).Column("Percentage4Star").Not.Nullable();
            Map(x => x.Percentage5Stars).Column("Percentage5Star").Not.Nullable();
        }
    }
}
