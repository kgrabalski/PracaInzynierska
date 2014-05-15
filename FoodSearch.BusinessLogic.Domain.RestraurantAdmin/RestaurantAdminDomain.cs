using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using CodeBits;

using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Interface;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Data.Mapping.Interface;

namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin
{
    public class RestaurantAdminDomain : IRestaurantAdminDomain
    {
        private readonly IRepositoryProvider _provider;

        public RestaurantAdminDomain(IRepositoryProvider provider)
        {
            _provider = provider;
        }

        public Guid CreateUser(string userName, string firstName, string lastName, string email, string password, UserTypes userType)
        {
            using (var rep = _provider.GetRepository<User>())
            {
                SHA256 sha = new SHA256Cng();
                var pass = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return rep.Create<Guid>(new User()
                {
                    UserName = userName,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = pass,
                    CreateDate = DateTime.Now,
                    UserStateId = (int)UserStates.Active,
                    UserTypeId = (int)userType
                });
            }
        }

        public bool ChangeRestaurantState(Guid restaurantId, bool isOpened)
        {
            using (var rep = _provider.GetRepository<Restaurant>())
            {
                var rest = rep.Get(restaurantId);
                rest.IsOpen = isOpened;
                rep.Update(rest);
                return isOpened;
            }
        }

        public int CreateCuisine(string name)
        {
            using (var rep = _provider.GetRepository<Cuisine>())
            {
                return rep.Create<int>(new Cuisine()
                {
                    Name = name
                });
            }
        }

        public void AddRestaurantCuisine(Guid restaurantId, int cuisineId)
        {
            using (var rep = _provider.GetRepository<RestaurantCuisine>())
            {
                rep.Create(new RestaurantCuisine()
                {
                    RestaurantId = restaurantId,
                    CuisineId = cuisineId
                });
            }
        }

        public bool RemoveRestaurantCuisine(Guid restaurantId, int cuisineId)
        {
            using (var rep = _provider.GetRepository<RestaurantCuisine>())
            {
                var rc = rep.GetAll().Where(x => x.RestaurantId == restaurantId && x.CuisineId == cuisineId)
                    .List().First();
                rep.Delete(rc);
                return true;
            }
        }

        public int CreateDishGroup(Guid restaurantId, string groupName)
        {
            using (var rep = _provider.GetRepository<DishGroup>())
            {
                return rep.Create<int>(new DishGroup()
                {
                    RestaurantId = restaurantId,
                    Name = groupName
                });
            }
        }

        public void EditDishGroup(int dishGroupId, string newGroupName)
        {
            using (var rep = _provider.GetRepository<DishGroup>())
            {
                var dishGroup = rep.Get(dishGroupId);
                dishGroup.Name = newGroupName;
                rep.Update(dishGroup);
            }
        }

        public int CreateDish(Guid restaurantId, string dishName, int dishGroupId, float price)
        {
            using (var rep = _provider.GetRepository<Dish>())
            {
                return rep.Create<int>(new Dish()
                {
                    RestauraintId = restaurantId,
                    DishName = dishName,
                    DishGroupId = dishGroupId,
                    Price = price
                });
            }
        }
    }
}
