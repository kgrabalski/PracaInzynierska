using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.BusinessLogic.Domain.Core.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        public byte[] ImageData { get; set; }
        public string ContentType { get; set; }
    }
}
