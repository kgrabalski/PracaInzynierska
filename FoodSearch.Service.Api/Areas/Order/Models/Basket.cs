using FoodSearch.BusinessLogic.Domain.Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FoodSearch.Service.Api.Areas.Order.Models
{
    public class Basket
    {
        private List<BasketItem> _items = new List<BasketItem>();
        public List<BasketItem> Items { get { return _items; } }
        private Guid _currentRestaurant;

        public Guid CurrentRestaurant
        {
            get { return _currentRestaurant; }
            set
            {
                if (_currentRestaurant != value)
                {
                    _currentRestaurant = value;
                    Items.Clear();
                }
            }
        }

        public int AddressId { get; set; }

        public void AddItem(Dish dish)
        {
            var item = _items.SingleOrDefault(x => x.DishId == dish.Id);
            if (item != null)
            {
                item.Count++;
                item.Price = (decimal)dish.Price;
            }
            else
            {
                _items.Add(new BasketItem()
                {
                    Count = 1,
                    DishId = dish.Id,
                    Price = (decimal)dish.Price,
                    Name = dish.Name
                });
            }
        }

        public bool RemoveItem(int dishId)
        {
            var item = _items.SingleOrDefault(x => x.DishId == dishId);
            if (item != null)
            {
                item.Count--;
                if (item.Count == 0) _items.Remove(item);
                return true;
            }
            return false;
        }

        public void ClearBasket()
        {
            _items = new List<BasketItem>();
        }

        public decimal Total
        {
            get { return _items.Sum(x => x.Price*x.Count); }
        }

        public decimal DeliveryPrice { get; set; }

        public decimal TotalWithDelivery
        {
            get { return Total + DeliveryPrice; }
        }
    }
}