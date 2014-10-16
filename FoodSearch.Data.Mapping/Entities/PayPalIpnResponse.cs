using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.Entities
{
    public class PayPalIpnResponse
    {
        public virtual int ResponseId { get; set; }
        public virtual Guid? PaymentId { get; set; }
        public virtual string Status { get; set; }
        public virtual DateTime CreateDate { get; set; }
    }
}
