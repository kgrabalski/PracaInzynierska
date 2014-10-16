

using System;
using System.Collections.Generic;

using FoodSearch.BusinessLogic.Domain.Order.Models;
using FoodSearch.Data.Mapping.Entities;

using NHibernate.Mapping;

using DeliveryAddress = FoodSearch.BusinessLogic.Domain.Order.Models.DeliveryAddress;


namespace FoodSearch.BusinessLogic.Domain.Order.Interface
{
    public interface IOrderDomain
    {
        DeliveryAddress GetUserDeliveryAddress(Guid userId);
        CreateOrderResult CreateOrder(Guid userId, Guid restaurantId, List<OrderItem> orderItems, DeliveryTypes deliveryType, PaymentTypes paymentType);
        Guid CreatePayment(Guid orderId, PaymentTypes paymentType, decimal amount);
        bool UpdatePayment(Guid paymentId, PaymentStates paymentState);
        void AddPaymentHistory(Guid paymentId, PaymentStates paymentState);
        decimal GetDeliveryPrice(Guid restaurantId, decimal totalPrice);
        void LogPaypalResponse(Guid? paymentId, string status);
    }
}
