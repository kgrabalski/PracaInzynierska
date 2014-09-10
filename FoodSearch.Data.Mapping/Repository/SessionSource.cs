﻿
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
                .ConnectionString(string.Format(@"Server=tcp:dycfyhr4vj.database.windows.net,1433;Database={0};User ID=FoodSearch@dycfyhr4vj;Password=P@ssw0rd;", db.ToString()))
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
