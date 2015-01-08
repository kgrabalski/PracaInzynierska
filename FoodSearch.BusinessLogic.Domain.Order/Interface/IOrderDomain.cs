﻿

using System;
using System.Collections.Generic;

using FoodSearch.BusinessLogic.Domain.Order.Models;
using FoodSearch.Data.Mapping.Entities;
using DeliveryAddress = FoodSearch.BusinessLogic.Domain.Order.Models.DeliveryAddress;
using PaymentType = FoodSearch.BusinessLogic.Domain.Order.Models.PaymentType;
using DeliveryType = FoodSearch.BusinessLogic.Domain.Order.Models.DeliveryType;


namespace FoodSearch.BusinessLogic.Domain.Order.Interface
{
    public interface IOrderDomain
    {
        IEnumerable<DeliveryAddress> GetUserDeliveryAddresses(Guid userId);
        CreateOrderResult CreateOrder(Guid userId, Guid restaurantId, List<OrderItem> orderItems, 
            DeliveryTypes deliveryType, PaymentTypes paymentType, 
            int? addressId, string flatNumber);
        Guid CreatePayment(Guid orderId, PaymentTypes paymentType, decimal amount);
        bool UpdatePayment(Guid paymentId, PaymentStates paymentState);
        decimal GetDeliveryPrice(Guid restaurantId, decimal totalPrice);
        IEnumerable<PaymentType> GetPaymentTypes();
        IEnumerable<DeliveryType> GetDeliveryTypes();
        Guid GetOrderForPayment(Guid paymentId);
        void ChangeOrderState(Guid orderId, OrderStates newOrderState);
        bool ConfirmOrder(Guid orderId, TimeSpan deliveryTime);
        DeliveryStatus GetDeliveryStatus(Guid orderId);
    }
}
