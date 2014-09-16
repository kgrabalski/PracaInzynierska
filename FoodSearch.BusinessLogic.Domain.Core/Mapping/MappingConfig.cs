using AutoMapper;
using FoodSearch.BusinessLogic.Domain.Core.Models;
using System;

namespace FoodSearch.BusinessLogic.Domain.Core.Mapping
{
    public static class MappingConfig
    {
        static MappingConfig()
        {
            Mapper.CreateMap<Data.Mapping.Entities.City, City>()
                .ForMember(x => x.Id, x => x.ResolveUsing(y => y.CityId));

            Mapper.CreateMap<Data.Mapping.Entities.District, District>()
                .ForMember(x => x.Id, x => x.ResolveUsing(y => y.DistrictId));

            Mapper.CreateMap<Data.Mapping.Entities.Image, Image>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.ImageId));

            Mapper.CreateMap<Data.Mapping.Entities.Street, Street>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.StreetId));

            Mapper.CreateMap<Data.Mapping.StoredProcedure.Results.StreetResult, Street>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.StreetId));

            

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
