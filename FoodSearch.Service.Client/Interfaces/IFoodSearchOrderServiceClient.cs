using FoodSearch.Service.Client.Contracts;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FoodSearch.Service.Client.Interfaces
{
    public interface IFoodSearchOrderServiceClient
    {
        Task<ObservableCollection<BasketItem>> GetBasket();
        Task<bool> AddToBasket(int dishId);
        Task<bool> RemoveFromBasket(int dishId);
        Task<bool> ClearBasket();
        Task<decimal> GetDeliveryPrice(Guid restaurantId, decimal totalPrice);
        Task<DeliveryAddress> GetDeliveryAddress();
        Task<CreateOrderResult> CreateOrder(PaymentTypes paymentType, DeliveryTypes deliveryType, int addressId, string flatNumber);
        Task SetCurrentRestaurant(Guid restaurantId);
        Task<DeliveryStatus> GetDeliveryStatus(Guid orderId);
    }
}

