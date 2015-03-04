
namespace FoodSearch.Service.Client.Contracts
{
    public class StreetNumber
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public override string ToString()
        {
            return Number;
        }
    }
}
