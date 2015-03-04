using System.Web.Security;

namespace FoodSearch.Service.Api.Providers
{
    public class CreateUserResult
    {
        public MembershipUser MembershipUser { get; set; }
        public MembershipCreateStatus Status { get; set; }
    }
}
