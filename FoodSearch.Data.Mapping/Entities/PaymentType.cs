using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.Entities
{
    public class PaymentType
    {
        public virtual int PaymentTypeId { get; set; }
        public virtual string Name { get; set; }
    }
}
