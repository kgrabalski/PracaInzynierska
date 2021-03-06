﻿using System.ComponentModel.DataAnnotations;

namespace FoodSearch.Presentation.Web.Site.Models
{
    public class ContactModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Range(1, int.MaxValue)]
        public int Title { get; set; }
        [Required]
        public string Message { get; set; }
    }
}