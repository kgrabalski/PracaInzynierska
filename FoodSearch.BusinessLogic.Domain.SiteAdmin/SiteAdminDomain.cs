using System.Security.Cryptography;
using System.Text;

using FoodSearch.BusinessLogic.Domain.SiteAdmin.Interface;
using FoodSearch.BusinessLogic.Domain.SiteAdmin.Mapping;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Data.Mapping.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

using FoodSearch.BusinessLogic.Domain.SiteAdmin.Models;

using NHibernate.Criterion;

using Address = FoodSearch.Data.Mapping.Entities.Address;
using AddressDto = FoodSearch.BusinessLogic.Domain.SiteAdmin.Models.Address;
using Restaurant = FoodSearch.Data.Mapping.Entities.Restaurant;
using RestaurantDto = FoodSearch.BusinessLogic.Domain.SiteAdmin.Models.Restaurant;
using User = FoodSearch.Data.Mapping.Entities.User;
using UserDto = FoodSearch.BusinessLogic.Domain.SiteAdmin.Models.User;

namespace FoodSearch.BusinessLogic.Domain.SiteAdmin
{
    public class SiteAdminDomain : ISiteAdminDomain
    {
        private readonly IRepositoryProvider _provider;

        public SiteAdminDomain(IRepositoryProvider provider)
        {
            _provider = provider;
        }

        public IEnumerable<RestaurantDto> GetRestaurants(Guid? restaurantId = null)
        {
            using (var rep = _provider.GetRepository<Restaurant>())
            {
                if (restaurantId.HasValue)
                {
                    return new[] {rep.Get(restaurantId.Value)}.Map<IEnumerable<RestaurantDto>>();
                }

                return rep.GetAll()
                    .Where(x => x.IsDeleted == false)
                    .OrderBy(x => x.Name).Asc
                    .List()
                    .ToList()
                    .Map<IEnumerable<RestaurantDto>>();
            }
        }

        public Guid? CreateRestaurant(string name, int addressId, int logoId, string userPassword, string userEmail, string userFirstName, string userLastName)
        {
            using (var rep = _provider.StoredProcedure)
            {
                var passwordHash = (SHA256.Create()).ComputeHash(Encoding.UTF8.GetBytes(userPassword));
                return rep.CreateRestaurant(name, addressId, logoId, userFirstName, userLastName, userEmail, "123456789", passwordHash);
            }
        }

        public bool DeleteRestaurant(Guid restaurantId)
        {
            using (var rep = _provider.GetRepository<Restaurant>())
            {
                return rep.TryDelete(restaurantId);
            }
        }

        public AddressDto GetAddress(int addressId)
        {
            using (var rep = _provider.GetRepository<Address>())
            {
                return rep.Get(addressId).Map<AddressDto>();
            }
        }

        public IEnumerable<UserDto> GetUsers(string query, int? page, int? pageSize)
        {
            using (var rep = _provider.GetRepository<User>())
            {
                if (!page.HasValue) page = 0;
                if (!pageSize.HasValue) pageSize = 25;
                return rep.GetAll()
                    .Where(Restrictions.Disjunction()
                        .Add(Restrictions.On<User>(x => x.LastName).IsInsensitiveLike(query, MatchMode.Anywhere))
                        .Add(Restrictions.On<User>(x => x.Email).IsInsensitiveLike(query, MatchMode.Anywhere)))
                    .OrderBy(x => x.CreateDate).Desc
                    .Skip(page.Value * pageSize.Value)
                    .Take(pageSize.Value)
                    .List()
                    .ToList()
                    .Select(x => new UserDto()
                    {
                        Id = x.UserId,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                        UserActive = x.UserStateId == (int) UserStates.Active
                    });
            }
        }

        public bool ChangeUserState(Guid userId, UserStates userState)
        {
            using (var rep = _provider.GetRepository<User>())
            {
                User user = null;
                rep.TryGet(userId, out user);
                if (user == null) return false;

                user.UserStateId = (int) userState;
                rep.Update(user);

                return true;
            }
        }

        public IEnumerable<SystemDailyFinancialReport> GetSystemDailyFinancialReport(DateTime dateFrom, DateTime dateTo)
        {
            using (var rep = _provider.StoredProcedure)
            {
                return rep.GetSystemDailyFinancialReport(dateFrom, dateTo).Map<IEnumerable<SystemDailyFinancialReport>>();
            }
        }

        public IEnumerable<SystemMonthlyFinancialReport> GetSystemMonthlyFinancialReport(DateTime dateFrom, DateTime dateTo)
        {
            using (var rep = _provider.StoredProcedure)
            {
                return rep.GetSystemMonthlyFinancialReport(dateFrom, dateTo).Map<IEnumerable<SystemMonthlyFinancialReport>>();
            }
        }
    }
}
