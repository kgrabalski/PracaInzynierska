using System.ComponentModel.DataAnnotations;

namespace FoodSearch.Presentation.Web.Site.Models
{
    public class ReportRangeModel
    {
        [Required]
        public string DateFrom { get; set; }
        [Required]
        public string DateTo { get; set; }
    }
}
