using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.StoredProcedure.Results.Mapping
{
    class SystemMonthlyFinancialReportMap : ClassMap<SystemMonthlyFinancialReport>
    {
        public SystemMonthlyFinancialReportMap()
        {
            Table("GetSystemMonthlyFinancialReport");
            ReadOnly();
            Id(x => x.Id).Column("Id").Not.Nullable();
            Map(x => x.Year).Column("Year").Not.Nullable();
            Map(x => x.Month).Column("Month").Not.Nullable();
            Map(x => x.Amount).Column("Amount").Not.Nullable();
        }
    }
}
