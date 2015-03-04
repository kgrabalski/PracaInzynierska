using System.Collections.Generic;

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
