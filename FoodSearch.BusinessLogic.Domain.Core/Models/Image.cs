
namespace FoodSearch.BusinessLogic.Domain.Core.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }
    }
}
