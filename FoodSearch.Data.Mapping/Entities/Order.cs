﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.Entities
{
    public class Order
    {
        public virtual Guid OrderId { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual int DeliveryTypeId { get; set; }
        public virtual DeliveryType DeliveryType { get; set; }
        public virtual Guid PaymentId { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual string DeliveryData { get; set; }
    }
}
