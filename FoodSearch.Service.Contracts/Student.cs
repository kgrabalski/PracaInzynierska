using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodSearch.Service.Contracts
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Group { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
