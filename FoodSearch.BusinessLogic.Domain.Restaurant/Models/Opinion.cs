
namespace FoodSearch.BusinessLogic.Domain.Restaurant.Models
{
    public class Opinion
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public short Rating { get; set; }
        public string Date { get; set; }
    }
}
