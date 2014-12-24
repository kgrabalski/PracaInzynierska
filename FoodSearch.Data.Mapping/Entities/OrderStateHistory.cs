using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.Entities
{
    public class OrderStateHistory
    {
        public virtual int OrderStateHistoryId { get; set; }
        public virtual Guid OrderId { get; set; }
        public virtual Order Order { get; set; }
        public virtual int OrderStateId { get; set; }
        public virtual OrderState OrderState { get; set; }
        public virtual DateTime ModificationDate { get; set; }
    }
}
