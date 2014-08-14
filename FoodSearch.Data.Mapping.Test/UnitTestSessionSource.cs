using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using FoodSearch.Data.Mapping.Interface;
using FoodSearch.Data.Mapping.Repository;

using NHibernate;

namespace FoodSearch.Data.Mapping.Test
{
    public class UnitTestSessionSource : ISessionSource
    {
        private readonly ISessionFactory _factory;

        public UnitTestSessionSource(string dbName, string dbPath)
        {
            _factory = Fluently
                .Configure()
                .Database(MsSqlConfiguration.MsSql2012.
                    ConnectionString(string.Format(@"Data Source=(LocalDb)\v11.0;Initial Catalog={0};Integrated Security=SSPI;AttachDBFilename={1}", dbName, dbPath))
                    .DefaultSchema("dbo"))
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
