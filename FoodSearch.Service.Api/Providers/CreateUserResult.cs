using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace FoodSearch.Service.Api.Providers
{
    public class CreateUserResult
    {
        public MembershipUser MembershipUser { get; set; }
        public MembershipCreateStatus Status { get; set; }
    }
}
