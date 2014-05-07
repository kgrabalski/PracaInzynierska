using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using FoodSearch.Data.Mapping.Interface;

using NHibernate;

namespace FoodSearch.Data.Mapping.Repository
{
    public enum Databases
    {
        FoodSearch,
        FoodSearchTest
    }

    public class SessionSource : ISessionSource
    {
        private readonly ISessionFactory _factory;
        public SessionSource(Databases db)
        {
            _factory = Fluently
                .Configure()
                .Database(MsSqlConfiguration
                    .MsSql2012
                    .ConnectionString(string.Format(@"Data Source=.\SQLEXPRESS14;Initial Catalog={0};Integrated Security=True;", db.ToString())))
                .Mappings(m =>
                {
                    m.FluentMappings.AddFromAssemblyOf<SessionSource>();
                    m.HbmMappings.AddFromAssemblyOf<SessionSource>();
                })
                .BuildConfiguration()
                .BuildSessionFactory();
        }

        public ISession Session { get { return _factory.OpenSession(); } }
    }
}
