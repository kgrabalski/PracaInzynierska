
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using FoodSearch.BusinessLogic.Domain.Order.Interface;
using FoodSearch.BusinessLogic.Domain.Order.Models;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Data.Mapping.Interface;

using DeliveryAddressDto = FoodSearch.BusinessLogic.Domain.Order.Models.DeliveryAddress;
using DeliveryAddress = FoodSearch.Data.Mapping.Entities.DeliveryAddress;


namespace FoodSearch.BusinessLogic.Domain.Order
{
    public class OrderDomain : IOrderDomain
    {
        private readonly IRepositoryProvider _provider;

        public OrderDomain(IRepositoryProvider provider)
        {
            _provider = provider;
        }

        public DeliveryAddressDto GetUserDeliveryAddress(Guid userId)
        {
            using (var rep = _provider.GetRepository<DeliveryAddress>())
            {
                var da = rep.GetAll()
                    .Where(x => x.UserId == userId)
                    .SingleOrDefault();
                return new DeliveryAddressDto()
                {
                    FirstName = da.User.FirstName,
                    LastName = da.User.LastName,
                    City = da.Address.District.City.Name,
                    Street = da.Address.Street.Name,
                    StreetNumber = da.Address.Number,
                    FlatNumber = da.FlatNumber
                };
            }
        }

        public CreateOrderResult CreateOrder(Guid userId, Guid restaurantId, List<OrderItem> orderItems, DeliveryTypes deliveryType, PaymentTypes paymentType)
        {
            using (var transaction = _provider.BeginTransaction)
            {
                try
                {
                    using (var repD = _provider.GetRepository<Dish>())
                    using (var repO = _provider.GetRepository<Data.Mapping.Entities.Order>())
                    using (var repR = _provider.GetRepository<Restaurant>())
                    using (var repOd = _provider.GetRepository<OrderDish>())
                    {
                        var dishes = orderItems.Select(x => repD.Get(x.DishId)).ToList();
                        
                        decimal orderValue = orderItems.Sum(x => (decimal) dishes.Single(y => y.DishId == x.DishId).Price * x.Quantity);
                        orderValue += GetDeliveryPrice(restaurantId, orderValue);

                        var deliveryData = new XElement("xml", new XElement("DeliveryType", (int) deliveryType));
                        if (deliveryType == DeliveryTypes.Shipping)
                        {
                            var da = GetUserDeliveryAddress(userId);
                            deliveryData.Add(new XElement("DeliveryData",
                                new XElement("FirstName", da.FirstName),
                                new XElement("LastName", da.LastName),
                                new XElement("City", da.City),
                                new XElement("Street", da.Street),
                                new XElement("StreetNumber", da.StreetNumber),
                                new XElement("FlatNumber", da.FlatNumber)));
                        }

                        var order = new Data.Mapping.Entities.Order()
                        {
                            UserId = userId,
                            CreateDate = DateTime.Now,
                            DeliveryTypeId = (int) deliveryType,
                            DeliveryData = deliveryData.ToString()
                        };
                        var orderId = repO.Create<Guid>(order);
                        var paymentid = CreatePayment(orderId, paymentType, orderValue);

                        foreach (var item in orderItems)
                        {
                            var dish = dishes.Single(x => x.DishId == item.DishId);
                            repOd.Create<int>(new OrderDish()
                            {
                                DishId = dish.DishId,
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
                            Amount = (float) amount
                        });
                        AddPaymentHistory(paymentId, PaymentStates.Created);
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
                    AddPaymentHistory(paymentId, paymentState);
                    return true;
                }
            }
            return false;
        }

        public void AddPaymentHistory(Guid paymentId, PaymentStates paymentState)
        {
            using (var rep = _provider.GetRepository<PaymentHistory>())
            {
                rep.Create<int>(new PaymentHistory()
                {
                    PaymentId = paymentId,
                    PaymentStateId = (int) paymentState,
                    ModificationDate = DateTime.Now
                });
            }
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

        public void LogPaypalResponse(Guid? paymentId, string status)
        {
            using (var rep = _provider.GetRepository<PayPalIpnResponse>())
            {
                rep.Create<int>(new PayPalIpnResponse()
                {
                    PaymentId = paymentId,
                    Status = status,
                    CreateDate = DateTime.Now
                });
            }
        }
    }
}
