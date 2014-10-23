using System;

namespace FoodSearch.Service.Client
{
    public class BasketItem
    {
        public int DishId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}

