﻿
namespace FoodSearch.Data.Mapping.StoredProcedure.Results
{
    public class SystemMonthlyFinancialReport
    {
        public virtual int Id { get; set; }
        public virtual int Year { get; set; }
        public virtual string Month { get; set; }
        public virtual decimal Amount { get; set; }
    }
}
