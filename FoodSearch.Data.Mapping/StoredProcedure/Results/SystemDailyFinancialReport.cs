using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.StoredProcedure.Results
{
    public class SystemDailyFinancialReport
    {
        public virtual int Id { get; set; }
        public virtual int Year { get; set; }
        public virtual string Month { get; set; }
        public virtual int Day { get; set; }
        public virtual decimal Amount { get; set; }
    }
}
