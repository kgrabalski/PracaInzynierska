using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using FoodSearch.BusinessLogic.Domain.Core.Models;

namespace FoodSearch.BusinessLogic.Domain.Core.Mapping
{
    public static class MappingConfig
    {
        static MappingConfig()
        {
            Mapper.CreateMap<Data.Mapping.Entities.District, District>();
            Mapper.CreateMap<Data.Mapping.Entities.Image, Image>();
            Mapper.CreateMap<Data.Mapping.Entities.Street, Street>();
            Mapper.CreateMap<Data.Mapping.StoredProcedure.Results.RestaurantInfo, RestaurantInfo>();

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
