﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models
{
    public class RestaurantDailyFinancialReport
    {
        public int Id { get; set; }
        public string RestaurantName { get; set; }
        public int Year { get; set; }
        public string Month { get; set; }
        public int Day { get; set; }
        public decimal Amount { get; set; }
    }
}