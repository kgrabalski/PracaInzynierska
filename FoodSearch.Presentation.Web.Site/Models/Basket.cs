using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using FoodSearch.Data.Mapping.Entities;

using Dish = FoodSearch.BusinessLogic.Domain.Restaurant.Models.Dish;

namespace FoodSearch.Presentation.Web.Site.Models
{
    public class Basket
    {
        private readonly List<BasketItem> _items = new List<BasketItem>();
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

        public int? DeliveryAddressId { get; set; }
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

        public void RemoveItem(int dishId)
        {
            var item = _items.SingleOrDefault(x => x.DishId == dishId);
            if (item != null)
            {
                item.Count--;
                if (item.Count == 0) _items.Remove(item);
            }
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