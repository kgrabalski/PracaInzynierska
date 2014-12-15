using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Presentation.Web.Site.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public string Email { get; set; }

        public bool RequestCreated { get; set; }
    }
}
