using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace generatorRestauracji.Models
{
    public class DishModel
    {
        public string Name { get; set; }
        public int PriceFrom { get; set; }
        public int PriceTo { get; set; }
        public int Image { get; set; }
    }
}
