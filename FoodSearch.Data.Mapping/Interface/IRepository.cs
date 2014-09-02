using NHibernate;
using System;

namespace FoodSearch.Data.Mapping.Interface
{
    public interface IRepository<T> : IDisposable where T : class
    {
        T Get<TId>(TId id) where TId : struct;
        bool TryGet<TId>(TId id, out T entity) where TId : struct;
        TId Create<TId>(T value) where TId : struct;
        void Update(T value);
        void Delete(T value);
        bool Delete<TId>(TId id) where TId : struct;
        bool TryDelete(T value);
        bool TryDelete<TId>(TId id) where TId : struct;
        IQueryOver<T, T> GetAll();
    }
}
