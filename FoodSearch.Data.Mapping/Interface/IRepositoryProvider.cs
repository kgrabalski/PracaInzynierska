using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.Interface
{
    public interface IRepositoryProvider
    {
        IRepository<T> GetRepository<T>() where T : class;
    }
}
