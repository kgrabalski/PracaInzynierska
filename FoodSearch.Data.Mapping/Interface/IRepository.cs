using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NHibernate;

namespace FoodSearch.Data.Mapping.Interface
{
    public interface IRepository<T> : IDisposable where T : class
    {
        T Get<TId>(TId id) where TId : struct;
        TId Create<TId>(T value) where TId : struct;
        void Create(T value);
        void Update(T value);
        void Delete(T value);
        IQueryOver<T, T> GetAll();
    }
}
