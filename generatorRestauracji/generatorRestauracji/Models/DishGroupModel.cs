using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace generatorRestauracji.Models
{
    class DishGroupModel
    {
        public string Name { get; set; }
        public Cuisines Cuisine { get; set; }
        public List<DishModel> Dishes { get; set; }
    }
}
