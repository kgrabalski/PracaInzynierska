﻿using System;

namespace FoodSearch.BusinessLogic.Domain.SiteAdmin.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool UserActive { get; set; }
    }
}
