using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Data.Mapping.Interface;

using NHibernate;

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

        public void Delete<TId>(TId id) where TId : struct
        {
            T item = Get<TId>(id);
            using (var transaction = _session.BeginTransaction())
            {
                _session.Delete(item);
                transaction.Commit();
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
