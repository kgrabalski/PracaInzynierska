using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using FoodSearch.BusinessLogic.Domain.User.Models;
using FoodSearch.BusinessLogic.Helpers;

namespace FoodSearch.BusinessLogic.Domain.User.Mapping
{
    public static class MappingConfig
    {
        static MappingConfig()
        {
            Mapper.CreateMap<Data.Mapping.StoredProcedure.Results.UserOrder, UserOrder>()
                .ForMember(x => x.OrderAmount, x => x.ResolveUsing(y => y.OrderAmount.ToPln()))
                .ForMember(x => x.CreateDate, x => x.ResolveUsing(y => y.CreateDate.ToString("dd.MM.yyyy hh:mm")));

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
