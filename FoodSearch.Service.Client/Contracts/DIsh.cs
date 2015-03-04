using System.Globalization;

namespace FoodSearch.Service.Client.Contracts
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string PricePln { get { return "Cena: " + Price.ToString("C", new CultureInfo("pl-PL")); } }
    }
}
