using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Presentation.Mobile.Common.Models
{
    public class Grouping<T> : List<T>
    {
        public string GroupName { get; set; }

        public Grouping(IEnumerable<T> items)
        {
            AddRange(items);
        }
    }
}
