using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;


namespace FoodSearch.Service.Api.Providers
{
    public class ModelBinderWebApi<T> : IModelBinder where T : class, new()
    {
        protected readonly string SessionKey;

        public ModelBinderWebApi()
        {
            SessionKey = typeof (T).FullName;
        }

        public virtual bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            T model = HttpContext.Current.Session[SessionKey] as T;
            if (model == null)
            {
                model = new T();
                HttpContext.Current.Session[SessionKey] = model;
            }
            bindingContext.Model = model;
            return true;
        }
    }
}
