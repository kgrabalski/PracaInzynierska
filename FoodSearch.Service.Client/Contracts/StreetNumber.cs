using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.Service.Client.Contracts
{
    public class StreetNumber
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public override string ToString()
        {
            return Number;
        }
    }
}
