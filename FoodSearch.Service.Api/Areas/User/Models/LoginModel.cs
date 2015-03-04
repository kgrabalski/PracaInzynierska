using System.ComponentModel.DataAnnotations;

namespace FoodSearch.Service.Api.Areas.User.Models
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
