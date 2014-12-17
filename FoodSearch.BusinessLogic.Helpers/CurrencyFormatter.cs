using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.BusinessLogic.Helpers
{
    public static class CurrencyFormatter
    {
        public static string ToPln(this decimal price)
        {
            return price.ToString("C", CultureInfo.GetCultureInfo("pl-PL"));
        }

        public static string ToPln(this float price)
        {
            return price.ToString("C", CultureInfo.GetCultureInfo("pl-PL"));
        }

        public static string ToPln(this double price)
        {
            return price.ToString("C", CultureInfo.GetCultureInfo("pl-PL"));
        }

        public static string ToPln(this int price)
        {
            return price.ToString("C", CultureInfo.GetCultureInfo("pl-PL"));
        }
    }
}
