using System.ComponentModel.DataAnnotations;

namespace FoodSearch.Presentation.Web.Site.Helpers
{
    public class NullableIntRangeAttribute : ValidationAttribute
    {
        private readonly int _min, _max;
        public NullableIntRangeAttribute(int mininum, int maximum)
        {
            _min = mininum;
            _max = maximum;
        }

        public override bool IsValid(object value)
        {
            int? val = value as int?;
            if (val.HasValue)
            {
                return val.Value >= _min && val.Value <= _max;
            }
            return true;
        }
    }
}
