﻿using System;

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
        public virtual string DeliveryData { get; set; }
        public virtual Guid RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual int OrderStateId { get; set; }
        public virtual OrderState OrderState { get; set; }
    }
}
