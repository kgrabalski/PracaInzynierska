using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.Entities
{
    public enum PaymentStates
    {
        NotCreated = 0,
        Created = 1,
        Cancelled = 2,
        InQueue = 50,
        Completed = 99
    }
}
