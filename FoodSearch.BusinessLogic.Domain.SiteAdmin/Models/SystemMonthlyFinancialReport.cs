﻿
namespace FoodSearch.BusinessLogic.Domain.SiteAdmin.Models
{
    public class SystemMonthlyFinancialReport
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Month { get; set; }
        public decimal Amount { get; set; }
    }
}
