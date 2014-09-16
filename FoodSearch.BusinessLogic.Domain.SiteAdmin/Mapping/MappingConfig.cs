using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using FoodSearch.BusinessLogic.Domain.SiteAdmin.Models;

namespace FoodSearch.BusinessLogic.Domain.SiteAdmin.Mapping
{
    public static class MappingConfig
    {
        static MappingConfig()
        {
            Mapper.CreateMap<Data.Mapping.Entities.Restaurant, Restaurant>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.RestaurantId))
                .ForMember(x => x.LogoId, x => x.MapFrom(y => y.ImageId))
                .ForMember(x => x.City, x => x.MapFrom(y => y.Address.District.City.Name))
                .ForMember(x => x.District, x => x.MapFrom(y => y.Address.District.Name))
                .ForMember(x => x.Street, x => x.MapFrom(y => y.Address.Street.Name))
                .ForMember(x => x.Number, x => x.MapFrom(y => y.Address.Number));

            Mapper.CreateMap<Data.Mapping.Entities.Address, Address>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.AddressId))
                .ForMember(x => x.District, x => x.MapFrom(y => y.District.Name));

            Mapper.CreateMap<Data.Mapping.Entities.City, City>()
                .ForMember(x => x.Id, x => x.MapFrom(y => y.CityId));

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
