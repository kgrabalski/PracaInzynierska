
using System.Configuration;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using FoodSearch.Data.Mapping.Interface;

using NHibernate;

namespace FoodSearch.Data.Mapping.Repository
{
    public class SessionSource : ISessionSource
    {
        private readonly ISessionFactory _factory;
        public const string ConnectionStringName = "FoodSearch.Data.Mapping.DALSetting.ConnectionString";
        public SessionSource()
        {
            var connectionStringConfig = ConfigurationManager.ConnectionStrings[ConnectionStringName];
            _factory = Fluently
                .Configure()
                .Database(MsSqlConfiguration.MsSql2012
                .ConnectionString(connectionStringConfig.ConnectionString)
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
