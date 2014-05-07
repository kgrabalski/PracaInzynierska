using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using NHibernate.Cfg;

namespace FoodSearch.Data.Mapping.Helpers
{
    public class NHibernateHelper
    {
        public static Configuration Configure()
        {
            return Fluently
                .Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(@"Data Source=.\SQLEXPRESS14;Initial Catalog=FoodSearch;Integrated Security=True;"))
                .Mappings(m =>
                {
                    m.FluentMappings.AddFromAssemblyOf<NHibernateHelper>();
                    m.HbmMappings.AddFromAssemblyOf<NHibernateHelper>();
                })
                .BuildConfiguration();
        }
    }
}
