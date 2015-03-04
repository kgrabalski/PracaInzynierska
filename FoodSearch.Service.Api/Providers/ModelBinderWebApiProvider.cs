using System;
using System.Web.Http;
using System.Web.Http.ModelBinding;


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
