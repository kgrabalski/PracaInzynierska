using AutoMapper;
using FoodSearch.BusinessLogic.Domain.Order.Models;
using System;

namespace FoodSearch.BusinessLogic.Domain.Order.Mapping
{
    public static class MappingConfig
    {
        static MappingConfig()
        {
            Mapper.CreateMap<Data.Mapping.Entities.DeliveryType, DeliveryType>()
                .ForMember(x => x.Id, x => x.ResolveUsing(y => y.DeliveryTypeId));

            Mapper.CreateMap<Data.Mapping.Entities.PaymentType, PaymentType>()
                .ForMember(x => x.Id, x => x.ResolveUsing(y => y.PaymentTypeId));

            Mapper.AssertConfigurationIsValid();
        }

        #region MappingExtensions
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
