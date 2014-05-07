using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.Entities
{
    public class Payment
    {
        public virtual Guid PaymentId { get; set; }
        public virtual int PaymentTypeId { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual int PaymentStateId { get; set; }
        public virtual PaymentState PaymentState { get; set; }
        public virtual float Amount { get; set; }
        public virtual DateTime CreateDate { get; set; }
    }
}
