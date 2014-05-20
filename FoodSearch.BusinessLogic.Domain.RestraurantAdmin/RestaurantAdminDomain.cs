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

        public IEnumerable<Cuisine> GetCuisines()
        {
            using (var rep = _provider.GetRepository<Cuisine>())
            {
                return rep.GetAll().List();
            }
        }

        public IEnumerable<Cuisine> GetRestaurantCuisines(Guid restaurantId)
        {
            using (var rep = _provider.GetRepository<RestaurantCuisine>())
            {
                return rep.GetAll()
                    .Where(x => x.RestaurantId == restaurantId)
                    .Select(x => x.Cuisine)
                    .List<Cuisine>()
                    .ToList();
            }
        }

        public void AddRestaurantCuisine(Guid restaurantId, int cuisineId)
        {
            using (var rep = _provider.GetRepository<RestaurantCuisine>())
            {
                rep.Create<int>(new RestaurantCuisine()
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
                var rc = rep.GetAll()
                    .Where(x => x.RestaurantId == restaurantId && x.CuisineId == cuisineId)
                    .List().First();
                rep.Delete(rc);
                return true;
            }
        }

        public IEnumerable<DishGroup> GetDishGroups(Guid restaurantId)
        {
            using (var rep = _provider.GetRepository<DishGroup>())
            {
                return rep.GetAll()
                    .Where(x => x.RestaurantId == restaurantId)
                    .List();
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

        public void DeleteDishGroup(int dishGroupId)
        {
            using (var rep = _provider.GetRepository<DishGroup>())
            {
                rep.Delete(dishGroupId);
            }
        }

        public IEnumerable<Models.Dish> GetDishes(Guid restaurantId, int? dishId = null)
        {
            using (var rep = _provider.GetRepository<Dish>())
            {
                if (dishId.HasValue)
                {
                    return new[]
                    {
                        TransformDish(rep.Get(dishId.Value))
                    };
                }
                return rep.GetAll()
                    .Where(x => x.RestauraintId == restaurantId)
                    .List()
                    .Select(TransformDish)
                    .OrderBy(x => x.DishGroup)
                    .ThenBy(x => x.DishName);
            }
        }

        private static Models.Dish TransformDish(Dish d)
        {
            return new Models.Dish()
            {
                DishId = d.DishId,
                DishName = d.DishName,
                DishGroupId = d.DishGroupId,
                DishGroup = d.DishGroup.Name,
                Price = d.Price.ToString("0.00")
            };
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

        public IEnumerable<Models.OpeningHour> GetOpeningHours(Guid restaurantId, int? openingHourId = null)
        {
            using (var rep = _provider.GetRepository<OpeningHour>())
            {
                if (openingHourId.HasValue)
                {
                    var oh = rep.Get(openingHourId.Value);
                    return new[]
                    {
                        TransformOpeningHour(oh)
                    };
                }
                return rep.GetAll()
                    .Where(x => x.RestaurantId == restaurantId)
                    .OrderBy(x => x.Day).Asc
                    .ThenBy(x => x.TimeFrom).Asc
                    .List()
                    .Select(TransformOpeningHour);
            }
        }

        private static Models.OpeningHour TransformOpeningHour(OpeningHour oh)
        {
            string[] days = { "", "Poniedziełek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela" };
            return new Models.OpeningHour()
            {
                OpeningId = oh.OpeningId,
                Day = days[oh.Day],
                TimeFrom = oh.TimeFrom.ToString(@"hh\:mm"),
                TimeTo = oh.TimeTo.ToString(@"hh\:mm")
            };
        }

        public int CreateOpeningHour(Guid restaurantId, int day, TimeSpan timeFrom, TimeSpan timeTo)
        {
            using (var rep = _provider.GetRepository<OpeningHour>())
            {
                return rep.Create<int>(new OpeningHour()
                {
                    RestaurantId = restaurantId,
                    Day = day,
                    TimeFrom = timeFrom,
                    TimeTo = timeTo
                });
            }
        }

        public void DeleteOpeningHour(int openingHourId)
        {
            using (var rep = _provider.GetRepository<OpeningHour>())
            {
                rep.Delete(openingHourId);
            }
        }
    }
}
