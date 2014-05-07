using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FoodSearch.Data.Mapping.Helpers;
using FoodSearch.Data.Mapping.Interface;

using NHibernate;

namespace FoodSearch.Data.Mapping.Repository
{
    public class FoodSearchRepository<T> : IRepository<T> where T : class
    {
        private readonly ISession session;

        public FoodSearchRepository()
        {
            session = NHibernateHelper.Configure().BuildSessionFactory().OpenSession();
        }

        public T Get<TId>(TId id) where TId : struct
        {
            return session.Get<T>(id);
        }

        public TId Save<TId>(T value) where TId : struct
        {
            using (var transaction = session.BeginTransaction())
            {
                TId id = (TId)session.Save(value);
                transaction.Commit();
                return id;
            }
        }

        public void Update(T value)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Update(value);
                transaction.Commit();
            }
        }

        public void Delete(T value)
        {
            using (var transaction = session.BeginTransaction())
            {
                session.Delete(value);
                transaction.Commit();
            }
        }

        public IQueryOver<T, T> GetAll()
        {
            return session.QueryOver<T>();
        }

        public void Dispose()
        {
            session.Dispose();
        }
    }
}
