using System;
using Xamarin.Forms;
using FoodSearch.Service.Client.Contracts;

namespace FoodSearch.Presentation.Mobile.Common.Converters
{
    public class WebImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var restaurant = value as Restaurant;
            return new UriImageSource()
            {
                Uri = new Uri("http://foodsearch.azurewebsites.net/MobileApi/Core/Logo/" + restaurant.LogoId),
                CachingEnabled = true,
                CacheValidity = new TimeSpan(12, 0, 0)
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

