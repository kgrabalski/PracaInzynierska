using System;

namespace FoodSearch.Service.Client.Contracts
{
    public class UserOrderItem
    {
        public string DishName { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
        public string Total { get; set; }
    }
}

