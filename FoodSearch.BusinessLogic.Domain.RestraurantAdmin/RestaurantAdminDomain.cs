using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Interface;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Mapping;
using FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Data.Mapping.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Cuisine = FoodSearch.Data.Mapping.Entities.Cuisine;
using CuisineDto = FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models.Cuisine;
using Dish = FoodSearch.Data.Mapping.Entities.Dish;
using DishDto = FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models.Dish;
using DishGroup = FoodSearch.Data.Mapping.Entities.DishGroup;
using DishGroupDto = FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models.DishGroup;
using OpeningHour = FoodSearch.Data.Mapping.Entities.OpeningHour;
using OpeningHourDto = FoodSearch.BusinessLogic.Domain.RestraurantAdmin.Models.OpeningHour;

namespace FoodSearch.BusinessLogic.Domain.RestraurantAdmin
{
    public class RestaurantAdminDomain : IRestaurantAdminDomain
    {
        private readonly IRepositoryProvider _provider;

        public RestaurantAdminDomain(IRepositoryProvider provider)
        {
            _provider = provider;
        }

        public Guid CreateUser(Guid restaurantId, string userName, string firstName, string lastName, string email, string password, UserTypes userType)
        {
            using (var repU = _provider.GetRepository<User>())
            using (var repRU = _provider.GetRepository<RestaurantUser>())
            {
                SHA256 sha = new SHA256Cng();
                var pass = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                Guid userId = repU.Create<Guid>(new User()
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
                repRU.Create<int>(new RestaurantUser()
                {
                    UserId = userId,
                    RestaurantId = restaurantId
                });
                return userId;
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

        public IEnumerable<CuisineDto> GetCuisines()
        {
            using (var rep = _provider.GetRepository<Cuisine>())
            {
                return rep.GetAll().List().Map<IEnumerable<CuisineDto>>();
            }
        }

        public IEnumerable<CuisineDto> GetRestaurantCuisines(Guid restaurantId)
        {
            using (var rep = _provider.GetRepository<RestaurantCuisine>())
            {
                return rep.GetAll()
                    .Where(x => x.RestaurantId == restaurantId)
                    .Select(x => x.Cuisine)
                    .List<Cuisine>()
                    .Map<IEnumerable<CuisineDto>>();
            }
        }

        public CuisineDto AddRestaurantCuisine(Guid restaurantId, int cuisineId)
        {
            using (var repRC = _provider.GetRepository<RestaurantCuisine>())
            using (var repC = _provider.GetRepository<Cuisine>())
            {
                bool canCreate = repRC.GetAll()
                    .Where(x => x.RestaurantId == restaurantId && x.CuisineId == cuisineId)
                    .RowCount() == 0;
                if (!canCreate) return null;

                repRC.Create<int>(new RestaurantCuisine()
                {
                    RestaurantId = restaurantId,
                    CuisineId = cuisineId
                });

                return repC.Get(cuisineId).Map<CuisineDto>();
            }
        }

        public bool RemoveRestaurantCuisine(Guid restaurantId, int cuisineId)
        {
            using (var rep = _provider.GetRepository<RestaurantCuisine>())
            {
                var rc = rep.GetAll()
                    .Where(x => x.RestaurantId == restaurantId && x.CuisineId == cuisineId)
                    .SingleOrDefault();

                if (rc == null) return false;

                rep.Delete(rc);
                return true;
            }
        }

        public IEnumerable<DishGroupDto> GetDishGroups(Guid restaurantId)
        {
            using (var rep = _provider.GetRepository<DishGroup>())
            {
                return rep.GetAll()
                    .Where(x => x.RestaurantId == restaurantId)
                    .List()
                    .Map<IEnumerable<DishGroupDto>>();
            }
        }

        public DishGroupDto CreateDishGroup(Guid restaurantId, string groupName)
        {
            using (var rep = _provider.GetRepository<DishGroup>())
            {
                bool canCreate = rep.GetAll()
                    .Where(x => x.RestaurantId == restaurantId && x.Name == groupName)
                    .RowCount() == 0;
                if (!canCreate) return null;

                DishGroup newDishGroup = new DishGroup()
                {
                    RestaurantId = restaurantId,
                    Name = groupName
                };
                int dishGroupId = rep.Create<int>(newDishGroup);
                newDishGroup.DishGroupId = dishGroupId;
                
                return newDishGroup.Map<DishGroupDto>();
            }
        }

        public bool EditDishGroup(Guid restaurantId, int dishGroupId, string newGroupName)
        {
            using (var rep = _provider.GetRepository<DishGroup>())
            {
                DishGroup dishGroup;
                if (rep.TryGet(dishGroupId, out dishGroup) && dishGroup.RestaurantId == restaurantId)
                {
                    dishGroup.Name = newGroupName;
                    rep.Update(dishGroup);
                    return true;
                }
                return false;
            }
        }

        public bool DeleteDishGroup(Guid restaurantId, int dishGroupId)
        {
            using (var rep = _provider.GetRepository<DishGroup>())
            {
                DishGroup dishGroup;
                if (rep.TryGet(dishGroupId, out dishGroup) && dishGroup.RestaurantId == restaurantId)
                {
                    rep.Delete(dishGroup);
                    return true;
                }
                return false;
            }
        }

        public IEnumerable<DishDto> GetDishes(Guid restaurantId, int? dishId = null)
        {
            using (var rep = _provider.GetRepository<Dish>())
            {
                if (dishId.HasValue)
                {
                    return new[] { rep.Get(dishId.Value) }.Map<IEnumerable<DishDto>>();
                }
                return rep.GetAll()
                    .Where(x => x.RestaurantId == restaurantId)
                    .OrderBy(x => x.DishGroup).Asc
                    .ThenBy(x => x.DishName).Asc
                    .List()
                    .ToList()
                    .Map<IEnumerable<DishDto>>();
            }
        }

        public DishDto CreateDish(Guid restaurantId, string dishName, int dishGroupId, float price)
        {
            using (var rep = _provider.GetRepository<Dish>())
            {
                bool canCreate = rep.GetAll()
                    .Where(x => x.RestaurantId == restaurantId && x.DishName == dishName && x.DishGroupId == dishGroupId)
                    .RowCount() == 0;
                if (!canCreate) return null;

                var newDish = new Dish()
                {
                    RestaurantId = restaurantId,
                    DishName = dishName,
                    DishGroupId = dishGroupId,
                    Price = price
                };
                int dishId = rep.Create<int>(newDish);

                rep.Evict(newDish);
                return rep.Get(dishId).Map<DishDto>();
            }
        }

        public Guid GetUserId(string userName)
        {
            using (var rep = _provider.GetRepository<User>())
            {
                var user = rep.GetAll()
                    .Where(x => x.UserName == userName)
                    .List().FirstOrDefault();
                return user != null ? user.UserId : Guid.Empty;
            }
        }

        public Guid GetUserRestaurant(Guid userId)
        {
            using (var rep = _provider.GetRepository<RestaurantUser>())
            {
                var rest = rep.GetAll()
                    .Where(x => x.UserId == userId)
                    .List().FirstOrDefault();
                return rest != null ? rest.RestaurantId : Guid.Empty;
            }
        }

        public IEnumerable<OpeningHourDto> GetOpeningHours(Guid restaurantId, int? openingHourId = null)
        {
            using (var rep = _provider.GetRepository<OpeningHour>())
            {
                if (!openingHourId.HasValue)
                    return rep.GetAll()
                        .Where(x => x.RestaurantId == restaurantId)
                        .OrderBy(x => x.Day).Asc
                        .ThenBy(x => x.TimeFrom).Asc
                        .List()
                        .Map<IEnumerable<OpeningHourDto>>();

                var oh = rep.Get(openingHourId.Value);
                return new[] {oh}.Map<IEnumerable<OpeningHourDto>>();
            }
        }


        public int CreateOpeningHour(Guid restaurantId, int day, TimeSpan timeFrom, TimeSpan timeTo)
        {
            using (var rep = _provider.StoredProcedure)
            {
                return rep.CreateOpeningHour(restaurantId, day, timeFrom, timeTo);
            }
        }

        public void DeleteOpeningHour(int openingHourId)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.OpeningHour>())
            {
                rep.Delete(openingHourId);
            }
        }

        public EmployeeData GetEmployeeData(Guid restaurantId, Guid userId)
        {
            using (var repU = _provider.GetRepository<User>())
            using (var repR = _provider.GetRepository<Restaurant>())
            {
                var user = repU.Get(userId);
                var restaurant = repR.Get(restaurantId);

                return new EmployeeData()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    RestaurantLogoId = restaurant.ImageId,
                    RestaurantName = restaurant.Name
                };
            }
        }
    }
}
