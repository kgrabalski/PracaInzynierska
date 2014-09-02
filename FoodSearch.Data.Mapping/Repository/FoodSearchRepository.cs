using FoodSearch.Data.Mapping.Interface;
using NHibernate;
using System;

namespace FoodSearch.Data.Mapping.Repository
{
    public class FoodSearchRepository<T> : IRepository<T> where T : class
    {
        private readonly ISession _session;

        public FoodSearchRepository(ISessionSource sessionSource)
        {
            _session = sessionSource.Session;
        }

        public T Get<TId>(TId id) where TId : struct
        {
            return _session.Get<T>(id);
        }

        public bool TryGet<TId>(TId id, out T entity) where TId : struct
        {
            entity = Get(id);
            return entity != null;
        }

        public TId Create<TId>(T value) where TId : struct
        {
            using (var transaction = _session.BeginTransaction())
            {
                TId id = (TId)_session.Save(value);
                transaction.Commit();
                return id;
            }
        }

        public void Update(T value)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Update(value);
                transaction.Commit();
            }
        }

        public void Delete(T value)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Delete(value);
                transaction.Commit();
            }
        }

        public bool Delete<TId>(TId id) where TId : struct
        {
            using (var transaction = _session.BeginTransaction())
            {
                T item;
                if (!TryGet(id, out item)) return false;

                _session.Delete(item);
                transaction.Commit();
                return true;
            }
        }

        public bool TryDelete(T value)
        {
            try
            {
                Delete(value);
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool TryDelete<TId>(TId id) where TId : struct
        {
            try
            {
                Delete(id);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public IQueryOver<T, T> GetAll()
        {
            return _session.QueryOver<T>();
        }

        public void Dispose()
        {
            _session.Dispose();
        }
    }
}
