using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

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

