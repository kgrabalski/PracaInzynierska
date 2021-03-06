﻿
using FoodSearch.BusinessLogic.Domain.Order.Interface;
using FoodSearch.BusinessLogic.Domain.Order.Mapping;
using FoodSearch.BusinessLogic.Domain.Order.Models;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Data.Mapping.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using DeliveryType = FoodSearch.Data.Mapping.Entities.DeliveryType;
using DeliveryTypeDto = FoodSearch.BusinessLogic.Domain.Order.Models.DeliveryType;
using PaymentType = FoodSearch.Data.Mapping.Entities.PaymentType;
using PaymentTypeDto = FoodSearch.BusinessLogic.Domain.Order.Models.PaymentType;

namespace FoodSearch.BusinessLogic.Domain.Order
{
    public class OrderDomain : IOrderDomain
    {
        private readonly IRepositoryProvider _provider;

        public OrderDomain(IRepositoryProvider provider)
        {
            _provider = provider;
        }

        public CreateOrderResult CreateOrder(Guid userId, Guid restaurantId, List<OrderItem> orderItems, DeliveryTypes deliveryType, PaymentTypes paymentType, int? deliveryAddressId)
        {
            if (orderItems.Count == 0) return null;

            using (var transaction = _provider.BeginTransaction)
            {
                try
                {
                    using (var repD = _provider.GetRepository<Dish>())
                    using (var repO = _provider.GetRepository<Data.Mapping.Entities.Order>())
                    using (var repR = _provider.GetRepository<Restaurant>())
                    using (var repOd = _provider.GetRepository<OrderDish>())
                    using (var repDA = _provider.GetRepository<DeliveryAddress>())
                    {
                        var dishes = orderItems.Select(x => repD.Get(x.DishId)).ToList();
                        
                        decimal orderValue = orderItems.Sum(x => (decimal) dishes.Single(y => y.DishId == x.DishId).Price * x.Quantity);
                        orderValue += GetDeliveryPrice(restaurantId, orderValue);

                        var deliveryData = new XElement("xml", 
                            new XElement("DeliveryType", (int) deliveryType),
                            new XElement("PredictedDeliveryTime", DateTime.Now.ToString("O", CultureInfo.InvariantCulture)));
                        if (deliveryType == DeliveryTypes.Shipping)
                        {
                            var da = repDA.Get(deliveryAddressId.Value);
                            deliveryData.Add(new XElement("DeliveryData",
                                new XElement("City", da.Address.District.City.Name),
                                new XElement("Street", da.Address.Street.Name),
                                new XElement("StreetNumber", da.Address.Number),
                                new XElement("FlatNumber", da.FlatNumber)));
                        }

                        var order = new Data.Mapping.Entities.Order()
                        {
                            UserId = userId,
                            CreateDate = DateTime.Now,
                            DeliveryTypeId = (int) deliveryType,
                            DeliveryData = deliveryData.ToString(),
                            RestaurantId = restaurantId,
                            OrderStateId = (int) OrderStates.Created
                        };
                        var orderId = repO.Create<Guid>(order);
                        var paymentid = CreatePayment(orderId, paymentType, orderValue);

                        foreach (var item in orderItems)
                        {
                            var dish = dishes.Single(x => x.DishId == item.DishId);
                            repOd.Create<int>(new OrderDish()
                            {
                                DishName = dish.DishName,
                                Price = dish.Price,
                                Quantity = item.Quantity,
                                OrderId = orderId
                            });
                        }

                        transaction.Commit();

                        return new CreateOrderResult()
                        {
                            OrderId = orderId,
                            PaymentId = paymentid,
                            RestaurantName = repR.Get(restaurantId).Name,
                            Price = orderValue
                        };
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            return null;
        }

        public Guid CreatePayment(Guid orderId, PaymentTypes paymentType, decimal amount)
        {
            using (var transaction = _provider.BeginTransaction)
            {
                try
                {
                    using (var rep = _provider.GetRepository<Payment>())
                    {
                        var paymentId = rep.Create<Guid>(new Payment()
                        {
                            OrderId = orderId,
                            PaymentTypeId = (int) paymentType,
                            PaymentStateId = (int) PaymentStates.Created,
                            CreateDate = DateTime.Now,
                            Amount = amount
                        });
                        transaction.Commit();
                        return paymentId;
                    }
                   
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool UpdatePayment(Guid paymentId, PaymentStates paymentState)
        {
            using (var rep = _provider.GetRepository<Payment>())
            {
                var payment = rep.Get(paymentId);
                if (payment.PaymentStateId != (int) paymentState)
                {
                    payment.PaymentStateId = (int) paymentState;
                    rep.Update(payment);
                    return true;
                }
            }
            return false;
        }

        public decimal GetDeliveryPrice(Guid restaurantId, decimal totalPrice)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.Restaurant>())
            {
                var rest = rep.Get(restaurantId);
                decimal freeDelivery = (decimal)rest.FreeDeliveryFrom;
                if (totalPrice >= freeDelivery) return decimal.Zero;
                return (decimal)rest.DeliveryPrice;
            }
        }

        public IEnumerable<PaymentTypeDto> GetPaymentTypes()
        {
            using (var rep = _provider.GetRepository<PaymentType>())
            {
                return rep.GetAll().List().Map<IEnumerable<PaymentTypeDto>>();
            }
        }

        public IEnumerable<DeliveryTypeDto> GetDeliveryTypes()
        {
            using (var rep = _provider.GetRepository<DeliveryType>())
            {
                return rep.GetAll().List().Map<IEnumerable<DeliveryTypeDto>>();
            }
        }

        public PaymentsOrder GetOrderForPayment(Guid paymentId)
        {
            using (var rep = _provider.GetRepository<Payment>())
            {
                var payment = rep.Get(paymentId);
                return new PaymentsOrder()
                {
                    OrderId = payment.OrderId,
                    RestaurantId = payment.Order.RestaurantId
                };
            }
        }

        public bool ChangeOrderState(Guid orderId, OrderStates newOrderState)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.Order>())
            {
                Data.Mapping.Entities.Order order;
                rep.TryGet(orderId, out order);
                if (order == null) return false;

                order.OrderStateId = (int) newOrderState;
                rep.Update(order);
                return true;
            }
        }

        public bool ConfirmOrder(Guid orderId, TimeSpan deliveryTime)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.Order>())
            {
                try
                {
                    var order = rep.Get(orderId);
                    order.OrderStateId = (int) OrderStates.Confirmed;

                    XElement xml = XElement.Parse(order.DeliveryData);
                    XElement node = xml.Descendants("PredictedDeliveryTime").First();
                    DateTime delivery = DateTime
                        .ParseExact(node.Value, "O", CultureInfo.InvariantCulture)
                        .Add(deliveryTime);
                    node.Value = delivery.ToString("O", CultureInfo.InvariantCulture);

                    order.DeliveryData = xml.ToString();
                    rep.Update(order);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool CancelOrder(Guid orderId, string cancellationReason)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.Order>())
            {
                try
                {
                    var order = rep.Get(orderId);
                    order.OrderStateId = (int) OrderStates.Cancelled;
                    XElement xml = XElement.Parse(order.DeliveryData);
                    xml.Add(new XElement("CancellationReason", cancellationReason));

                    order.DeliveryData = xml.ToString();
                    rep.Update(order);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public DeliveryStatus GetDeliveryStatus(Guid orderId)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.Order>())
            {
                var order = rep.Get(orderId);
                XElement xml = XElement.Parse(order.DeliveryData);
                XElement node = xml.Descendants("PredictedDeliveryTime").First();
                DateTime delivery = DateTime.ParseExact(node.Value, "O", CultureInfo.InvariantCulture);
                node = xml.Descendants("CancellationReason").FirstOrDefault();

                return new DeliveryStatus()
                {
                    ConfirmationStatus = GetConfirmationStatus(order.OrderStateId),
                    DeliveryDate = delivery.ToString("dd.MM.yyyy HH:mm"),
                    CancellationReason = node != null ? node.Value : string.Empty
                };
            }
        }

        private ConfirmationStatus GetConfirmationStatus(int orderState)
        {
            switch ((OrderStates) orderState)
            {
                case OrderStates.Confirmed: return ConfirmationStatus.Confirmed;
                case OrderStates.Cancelled: return ConfirmationStatus.Cancelled;
                case OrderStates.Completed: return ConfirmationStatus.Completed;
                default: return ConfirmationStatus.NotConfirmed;
            }
        }
    }
}
