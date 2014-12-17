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
using City = FoodSearch.Data.Mapping.Entities.City;
using CityDto = FoodSearch.BusinessLogic.Domain.SiteAdmin.Models.City;

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
            using (var rep = _provider.GetRepository<Restaurant>())
            using (var repU = _provider.GetRepository<RestaurantUser>())
            {
                bool canCreate = rep.GetAll()
                    .Where(x => x.Name == name && x.AddressId == addressId)
                    .RowCount() == 0;
                if (!canCreate) return null;

                var newRestaurant = new Restaurant()
                {
                    Name = name,
                    AddressId = addressId,
                    ImageId = logoId,
                    IsDeleted = false,
                    IsOpen = false,
                    MinOrderAmount = 0m
                };
                var restaurantId = rep.Create<Guid>(newRestaurant);
                var userId = CreateUser(userEmail, userPassword, userFirstName, userLastName, UserTypes.RestaurantAdmin, UserStates.Active);
                var restaurantUserId = repU.Create<int>(new RestaurantUser()
                {
                    RestaurantId = restaurantId,
                    UserId = userId
                });
                return restaurantId;
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

        public CityDto CreateCity(string name)
        {
            using (var rep = _provider.GetRepository<City>())
            {
                bool canCreate = rep.GetAll()
                    .Where(x => x.Name == name)
                    .RowCount() == 0;
                if (!canCreate) return null;

                int cityId = rep.Create<int>(new City()
                {
                    Name = name
                });

                return new CityDto()
                {
                    Id = cityId,
                    Name = name
                };
            }
        }

        public bool DeleteCity(int cityId)
        {
            using (var repC = _provider.GetRepository<City>())
            using (var repD = _provider.GetRepository<District>())
            {
                bool canDelete = repD.GetAll()
                    .Where(x => x.CityId == cityId)
                    .RowCount() == 0;
                if (!canDelete) return false;

                return repC.Delete(cityId);
            }
        }
    }
}
