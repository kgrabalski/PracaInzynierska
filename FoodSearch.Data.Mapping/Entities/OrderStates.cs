using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.Entities
{
    public enum OrderStates
    {
        Created = 1,
        Cancelled = 2,
        Paid = 3,
        Confirmed = 4,
        Completed = 5
    }
}
