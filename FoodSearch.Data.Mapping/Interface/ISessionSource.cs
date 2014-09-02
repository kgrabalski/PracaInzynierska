
using NHibernate;

namespace FoodSearch.Data.Mapping.Interface
{
    public interface ISessionSource
    {
        ISession Session { get; }
    }
}
