
using FluentNHibernate.Mapping;

namespace FoodSearch.Data.Mapping.StoredProcedure.Results.Mapping
{
    internal class SystemDailyFinancialReportMap : ClassMap<SystemDailyFinancialReport>
    {
        public SystemDailyFinancialReportMap()
        {
            Table("GetSystemDailyFinancialReport");
            ReadOnly();
            Id(x => x.Id).Column("Id").Not.Nullable();
            Map(x => x.Year).Column("Year").Not.Nullable();
            Map(x => x.Month).Column("Month").Not.Nullable();
            Map(x => x.Day).Column("Day").Not.Nullable();
            Map(x => x.Amount).Column("Amount").Not.Nullable();
        }
    }
}
