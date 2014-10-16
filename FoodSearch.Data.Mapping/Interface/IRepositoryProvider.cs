
using NHibernate;

namespace FoodSearch.Data.Mapping.Interface
{
    public interface IRepositoryProvider
    {
        IRepository<T> GetRepository<T>() where T : class;
        IStoredProcedureRepository StoredProcedure { get; }
        ITransaction BeginTransaction { get; }
    }
}
