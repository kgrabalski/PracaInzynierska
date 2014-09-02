
namespace FoodSearch.Data.Mapping.Entities
{
    public class Image
    {
        public virtual int ImageId { get; set; }
        public virtual byte[] ImageData { get; set; }
        public virtual string ContentType { get; set; }
    }
}
