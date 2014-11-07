using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Service.Api.Areas.User.Models
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string RepeatPassword { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int AddressId { get; set; }
        public string FlatNumber { get; set; }
    }
}
