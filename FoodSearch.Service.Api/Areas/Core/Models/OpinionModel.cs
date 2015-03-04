using System;
using System.ComponentModel.DataAnnotations;

namespace FoodSearch.Service.Api.Areas.Core.Models
{
    public class OpinionModel
    {
        [Required]
        public Guid RestaurantId { get; set; }
        [Range(1,5)]
        public int Rating { get; set; }
        [Required]
        public string Comment { get; set; }
    }
}
