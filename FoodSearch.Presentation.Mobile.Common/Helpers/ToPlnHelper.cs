using System.Globalization;

namespace FoodSearch.Presentation.Mobile.Common.Helpers
{
    public static class ToPlnHelper
    {
        public static string ToPln(this decimal value)
        {
            return value.ToString("C", new CultureInfo("pl-PL"));
        }
    }
}

