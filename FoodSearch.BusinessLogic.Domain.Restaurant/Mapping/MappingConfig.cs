using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using FoodSearch.BusinessLogic.Domain.Restaurant.Models;

namespace FoodSearch.BusinessLogic.Domain.Restaurant.Mapping
{
    public static class MappingConfig
    {
        static MappingConfig()
        {
            Mapper.CreateMap<Data.Mapping.StoredProcedure.Results.RestaurantInfo, RestaurantInfo>()
                .ForMember(x => x.StarsCount, x => x.ResolveUsing(y => (int)Math.Round(y.RestaurantRating, 1)));

            Mapper.CreateMap<Data.Mapping.Entities.Dish, Dish>()
                .ForMember(x => x.Id, x => x.ResolveUsing(y => y.DishId))
                .ForMember(x => x.Name, x => x.ResolveUsing(y => y.DishName));

            Mapper.CreateMap<Data.Mapping.StoredProcedure.Results.PartnerRestaurant, PartnerRestaurant>()
                .ForMember(x => x.StarsCount, x => x.ResolveUsing(y => (int)Math.Round(y.RestaurantRating, 1)));

            Mapper.CreateMap<Data.Mapping.Entities.Opinion, Opinion>()
                .ForMember(x => x.Id, x => x.ResolveUsing(y => y.OpinionId))
                .ForMember(x => x.UserName, x => x.ResolveUsing(y => string.Format("{0} {1}", y.User.FirstName, y.User.LastName)));

            Mapper.AssertConfigurationIsValid();
        }

        #region Extensions
        public static TDestination Map<TSource, TDestination>(this TSource @this)
        {
            return Mapper.Map<TSource, TDestination>(@this);
        }

        public static TDestination Map<TDestination>(this object @this)
        {
            if (@this == null)
            {
                return default(TDestination);
            }

            return (TDestination)@this.Map(typeof(TDestination));
        }

        public static object Map(this object @this, Type destinationType)
        {
            return Mapper.Map(@this, @this.GetType(), destinationType);
        }
        #endregion
    }
}
