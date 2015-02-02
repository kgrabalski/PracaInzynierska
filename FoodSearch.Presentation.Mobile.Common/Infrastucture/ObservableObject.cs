using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FoodSearch.Presentation.Mobile.Common.Infrastucture
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T property, T newValue, [CallerMemberName] string propertyName = "")
        {
            if (property != null && property.Equals(newValue)) return false;

            property = newValue;
            var handler = PropertyChanged;
            if (handler != null) 
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            return true;
        }
    }
}

