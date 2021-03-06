﻿using System;

namespace FoodSearch.Data.Mapping.Entities
{
    public class User
    {
        public virtual Guid UserId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual byte[] Password { get; set; }
        public virtual int UserTypeId { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual int UserStateId { get; set; }
        public virtual UserState UserState { get; set; }
        public virtual DateTime CreateDate { get; set; }
    }
}
