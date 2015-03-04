using System.ComponentModel.DataAnnotations;

namespace FoodSearch.Presentation.Web.Site.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public string Email { get; set; }

        public bool RequestCreated { get; set; }
    }
}
