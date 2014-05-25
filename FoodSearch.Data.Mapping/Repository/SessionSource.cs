using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
                .Database(MsSqlConfiguration.MsSql2012
                //.ConnectionString(string.Format(@"Data Source=.\SQLEXPRESS14;Initial Catalog={0};Integrated Security=True;", db.ToString())))
                .ConnectionString(string.Format(@"Server=tcp:dycfyhr4vj.database.windows.net,1433;Database={0};User ID=kgrabalski@dycfyhr4vj;Password=GrabalskiP@ssw0rd;", db.ToString())))
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
