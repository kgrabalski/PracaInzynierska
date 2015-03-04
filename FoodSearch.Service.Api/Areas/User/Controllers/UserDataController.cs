using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using FoodSearch.BusinessLogic.Domain.User.Models;
using FoodSearch.Service.Api.Models;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace FoodSearch.Service.Api.Areas.User.Controllers
{
    [Authorize(Roles = "User")]
    public class UserDataController : ApiController
    {
        private readonly IFoodSearchDomain _domain;

        public UserDataController(IFoodSearchDomain domain)
        {
            _domain = domain;
        }

        [HttpGet]
        public UserDetails GetUserDetails([ModelBinder] UserInfo ui)
        {
            return _domain.User.GetUserDetails(ui.UserId);
        }
    }
}
