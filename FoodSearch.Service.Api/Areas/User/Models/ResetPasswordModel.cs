using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Service.Api.Areas.User.Models
{
    public class ResetPasswordModel
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
