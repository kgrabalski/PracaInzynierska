using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.StoredProcedure.Results
{
    public class StreetResult
    {
        public virtual int StreetId { get; set; }
        public virtual string Name { get; set; }
    }
}
