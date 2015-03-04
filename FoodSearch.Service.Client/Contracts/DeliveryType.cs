
namespace FoodSearch.Service.Client.Contracts
{
    public class DeliveryType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
