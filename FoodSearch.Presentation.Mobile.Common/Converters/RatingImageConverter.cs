using System;
using Xamarin.Forms;

namespace FoodSearch.Presentation.Mobile.Common.Converters
{
    public class RatingImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            short rating = (short)value;
            string imgPath = string.Empty;

            switch (rating)
            {
                case 1:
                case 2:
                {
                    imgPath = "ic_action_bad.png";
                    break;
                }
                case 3: 
                {
                    imgPath = "ic_action_avg.png";
                    break;
                }
                case 4:
                case 5:
                {
                    imgPath = "ic_action_good.png";
                    break;
                }
            }
            return ImageSource.FromFile(imgPath);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

