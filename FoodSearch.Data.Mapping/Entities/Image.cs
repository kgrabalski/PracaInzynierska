using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Data.Mapping.Entities
{
    public class Image
    {
        public virtual int ImageId { get; set; }
        public virtual byte[] ImageData { get; set; }
        public virtual string ContentType { get; set; }
    }
}
