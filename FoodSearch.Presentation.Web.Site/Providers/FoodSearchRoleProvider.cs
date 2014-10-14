using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;

using Ninject;
using System;
using System.Web.Security;

namespace FoodSearch.Presentation.Web.Site.Providers
{
    public class FoodSearchRoleProvider : RoleProvider
    {
        private readonly IFoodSearchDomain _domain;

        public FoodSearchRoleProvider()
        {
            _domain = MvcApplication.DependencyResolver.Get<IFoodSearchDomain>(); ;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            return _domain.User.ValidateUserRole(username, roleName);
        }

        public override string[] GetRolesForUser(string username)
        {
            return new string[]
            {
                _domain.User.GetUserRole(username)
            };
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}