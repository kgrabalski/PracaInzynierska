using System.Web.Security;

namespace FoodSearch.Presentation.Web.Site.Providers
{
    public class CreateUserResult
    {
        public MembershipUser MembershipUser { get; set; }
        public MembershipCreateStatus Status { get; set; }
    }
}