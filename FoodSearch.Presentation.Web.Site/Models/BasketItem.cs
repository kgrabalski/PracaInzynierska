
namespace FoodSearch.Presentation.Web.Site.Models
{
    public class BasketItem
    {
        public int DishId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get { return Price*Count; } }
    }
}