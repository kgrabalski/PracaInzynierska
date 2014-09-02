using System;

namespace FoodSearch.Data.Mapping.Entities
{
    public class PaymentHistory
    {
        public virtual int PaymentHistoryId { get; set; }
        public virtual Guid PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual int PaymentStateId { get; set; }
        public virtual PaymentState PaymentState { get; set; }
        public virtual DateTime ModificationDate { get; set; }
    }
}
