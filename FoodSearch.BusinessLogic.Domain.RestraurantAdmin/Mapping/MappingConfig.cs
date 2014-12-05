using AutoMapper;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models;
using System;

namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Mapping
{
    public static class MappingConfig
    {
        static MappingConfig()
        {
            Mapper.CreateMap<Data.Mapping.Entities.Cuisine, Cuisine>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.CuisineId));

            Mapper.CreateMap<Data.Mapping.Entities.DishGroup, DishGroup>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.DishGroupId))
                .ForSourceMember(x => x.RestaurantId, x => x.Ignore());

            Mapper.CreateMap<Data.Mapping.Entities.Dish, Dish>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.DishId))
                .ForSourceMember(x => x.RestaurantId, x => x.Ignore())
                .ForSourceMember(x => x.Restaurant, x => x.Ignore())
                .ForMember(x => x.Price, x => x.ResolveUsing(y => y.Price.ToString("0.00")))
                .ForMember(x => x.DishGroup, x => x.ResolveUsing(y => y.DishGroup.Name));

            string[] days = { "", "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela" };
            Mapper.CreateMap<Data.Mapping.Entities.OpeningHour, OpeningHour>()
                .ForSourceMember(x => x.RestaurantId, x => x.Ignore())
                .ForSourceMember(x => x.Restaurant, x => x.Ignore())
                .ForMember(x => x.Id, x => x.ResolveUsing(y => y.OpeningId))
                .ForMember(x => x.TimeFrom, x => x.ResolveUsing(y => y.TimeFrom.ToString(@"hh\:mm")))
                .ForMember(x => x.TimeTo, x => x.ResolveUsing(y => y.TimeTo.ToString(@"hh\:mm")))
                .ForMember(x => x.DayName, x => x.ResolveUsing(y => days[y.Day]));

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
