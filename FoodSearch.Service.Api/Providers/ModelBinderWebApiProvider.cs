using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

using FoodSearch.Service.Api.Areas.Order.Models;

namespace FoodSearch.Service.Api.Providers
{
    public class ModelBinderWebApiProvider<TType, TBinder> : ModelBinderProvider 
        where TType : class, new() 
        where TBinder : ModelBinderWebApi<TType>, new()
    {
        public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
        {
            return modelType == typeof(TType) ? new TBinder() : null;
        }
    }
}
