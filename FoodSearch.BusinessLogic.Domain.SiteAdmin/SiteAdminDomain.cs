using System.Security.Cryptography;
using System.Text;

using FoodSearch.BusinessLogic.Domain.SiteAdmin.Interface;
using FoodSearch.BusinessLogic.Domain.SiteAdmin.Mapping;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Data.Mapping.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

using Address = FoodSearch.Data.Mapping.Entities.Address;
using AddressDto = FoodSearch.BusinessLogic.Domain.SiteAdmin.Models.Address;
using Restaurant = FoodSearch.Data.Mapping.Entities.Restaurant;
using RestaurantDto = FoodSearch.BusinessLogic.Domain.SiteAdmin.Models.Restaurant;

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

        public Guid CreateUser(string email, string userPassword, string firstName, string lastName, UserTypes userType, UserStates userState)
        {
            using (var rep = _provider.GetRepository<User>())
            {
                var password = (SHA256.Create()).ComputeHash(Encoding.UTF8.GetBytes(userPassword));
                return rep.Create<Guid>(new User()
                {
                    Password = password,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    CreateDate = DateTime.Now,
                    UserTypeId = (int) userType,
                    UserStateId = (int) userState
                });
            }
        }
    }
}
