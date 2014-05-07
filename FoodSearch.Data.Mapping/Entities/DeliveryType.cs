using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.Entities
{
    public class DeliveryType
    {
        public virtual int DeliveryTypeId { get; set; }
        public virtual string Name { get; set; }
    }
}
