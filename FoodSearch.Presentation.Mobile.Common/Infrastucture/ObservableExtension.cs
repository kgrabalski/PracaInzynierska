using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FoodSearch.Presentation.Mobile.Common.Infrastucture
{
    public static class ObservableExtension
    {
        public static ObservableCollection<T> AsObservable<T>(this IEnumerable<T> collection)
        {
            return new ObservableCollection<T>(collection);
        }
    }
}

