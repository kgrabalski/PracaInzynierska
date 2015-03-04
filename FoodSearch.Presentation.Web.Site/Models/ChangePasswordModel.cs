using System.ComponentModel.DataAnnotations;

namespace FoodSearch.Presentation.Web.Site.Models
{
    public class ChangePasswordModel
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        [Compare("RepeatNewPassword")]
        public string NewPassword { get; set; }
        [Required]
        public string RepeatNewPassword { get; set; }
    }
}
