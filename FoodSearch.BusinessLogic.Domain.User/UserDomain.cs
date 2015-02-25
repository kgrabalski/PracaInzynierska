using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

using FoodSearch.BusinessLogic.Domain.User.Interface;
using FoodSearch.BusinessLogic.Domain.User.Mapping;
using FoodSearch.BusinessLogic.Domain.User.Models;
using FoodSearch.BusinessLogic.Helpers;
using FoodSearch.BusinessLogic.Helpers.Email;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Data.Mapping.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

using DeliveryAddressDto = FoodSearch.BusinessLogic.Domain.User.Models.DeliveryAddress;
using DeliveryAddress = FoodSearch.Data.Mapping.Entities.DeliveryAddress;


namespace FoodSearch.BusinessLogic.Domain.User
{
    public class UserDomain : IUserDomain
    {
        private readonly IRepositoryProvider _provider;

        public UserDomain(IRepositoryProvider provider)
        {
            _provider = provider;
        }

        public bool IsEmailDuplicated(string email)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.User>())
            {
                return rep.GetAll()
                    .Where(x => x.Email == email)
                    .RowCount() > 0;
            }
        }

        public Guid CreateUser(string firstName, string lastName, string email, string phoneNumber, string password)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.User>())
            {
                var passHash = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
                return rep.Create<Guid>(new Data.Mapping.Entities.User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    Password = passHash,
                    CreateDate = DateTime.Now,
                    UserStateId = (int)UserStates.Unconfirmed,
                    UserTypeId = (int)UserTypes.User,
                });
            }
        }

        public void CreateConfirmationEntry(Guid userId, string email)
        {
            using (var rep = _provider.GetRepository<RegistrationConfirm>())
            using (var repU = _provider.GetRepository<Data.Mapping.Entities.User>())
            {
                Guid code = Guid.NewGuid();
                rep.Create<int>(new RegistrationConfirm()
                {
                    Code = code,
                    Confirmed = false,
                    UserId = userId
                });
                var user = repU.Get(userId);

                EmailHelper.Send(MailHelperSendFrom.NoReply,
                    new List<string>() {email},
                    "FoodSearch: potwierdzenie rejestracji",
                    EmailBodyHelper.Registration(
                        code.ToString()));
            }
        }

        public RegisterConfirmationResult ConfirmRegistration(Guid code)
        {
            var result = RegisterConfirmationResult.NotConfirmed;
            using (var rep = _provider.GetRepository<RegistrationConfirm>())
            {
                RegistrationConfirm confirm = rep.GetAll()
                    .Where(x => x.Code == code)
                    .List()
                    .FirstOrDefault();
                if (confirm == null) return result;
                if (confirm.Confirmed) result = RegisterConfirmationResult.AlreadyConfirmed;
                else
                {
                    using (var repU = _provider.GetRepository<Data.Mapping.Entities.User>())
                    {
                        var user = repU.Get(confirm.User.UserId);
                        user.UserStateId = (int)UserStates.Active;
                        repU.Update(user);
                        result = RegisterConfirmationResult.Confirmed;
                    }
                }
            }
            return result;
        }

        public bool ValidateUser(string email, byte[] password)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.User>())
            {
                var user = rep.GetAll()
                    .Where(x => x.Email == email && x.UserStateId == (int)UserStates.Active)
                    .SingleOrDefault();
                return user != null && user.Password.SequenceEqual(password);
            }
        }

        public string GetUserRole(string email)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.User>())
            {
                var user = rep.GetAll()
                    .Where(x => x.Email == email)
                    .SingleOrDefault();
                return user != null ? user.UserType.Name : "";
            }
        }

        public bool ValidateUserRole(string email, string role)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.User>())
            {
                var user = rep.GetAll()
                    .Where(x => x.Email == email)
                    .SingleOrDefault();
                return user != null && user.UserType.Name == role;
            }
        }

        public int CreateDeliveryAddress(Guid userId, int addressId, string flatNumber)
        {
            using (var rep = _provider.GetRepository<DeliveryAddress>())
            {
                if (string.IsNullOrEmpty(flatNumber) || string.IsNullOrWhiteSpace(flatNumber)) flatNumber = string.Empty;

                var address = rep.GetAll()
                    .Where(x => x.UserId == userId && x.AddressId == addressId && x.FlatNumber == flatNumber)
                    .SingleOrDefault();

                if (address != null) return address.DeliveryAddressId;

                return rep.Create<int>(new DeliveryAddress()
                {
                    UserId = userId,
                    AddressId = addressId,
                    FlatNumber = flatNumber
                });
            }
        }

        public IEnumerable<DeliveryAddressDto> GetUserDeliveryAddresses(Guid userId)
        {
            using (var rep = _provider.GetRepository<DeliveryAddress>())
            {
                return rep.GetAll()
                    .Where(x => x.UserId == userId)
                    .List()
                    .Select(x => new DeliveryAddressDto()
                    {
                        Id = x.DeliveryAddressId,
                        AddressId = x.AddressId,
                        FirstName = x.User.FirstName,
                        LastName = x.User.LastName,
                        CityId = x.Address.District.CityId,
                        City = x.Address.District.City.Name,
                        StreetId = x.Address.StreetId,
                        Street = x.Address.Street.Name,
                        StreetNumber = x.Address.Number,
                        FlatNumber = x.FlatNumber
                    })
                    .ToList();
            }
        }

        public DeliveryAddressDto GetUserDeliveryAddress(Guid userId, int deliveryAddressId)
        {
            using (var rep = _provider.GetRepository<DeliveryAddress>())
            {
                var da = rep.Get(deliveryAddressId);
                if (da.UserId != userId) return default(DeliveryAddressDto);
                return new DeliveryAddressDto()
                {
                    Id = da.DeliveryAddressId,
                    AddressId = da.AddressId,
                    FirstName = da.User.FirstName,
                    LastName = da.User.LastName,
                    CityId = da.Address.District.CityId,
                    City = da.Address.District.City.Name,
                    StreetId = da.Address.StreetId,
                    Street = da.Address.Street.Name,
                    StreetNumber = da.Address.Number,
                    FlatNumber = da.FlatNumber
                };
            }
        }

        public DeliveryAddressDto GetDeliveryAddress(Guid userId, int addressId)
        {
            using (var rep = _provider.GetRepository<Address>())
            {
                var address = rep.Get(addressId);
                var userData = GetUserDetails(userId);
                return new DeliveryAddressDto()
                {
                    FirstName = userData.FirstName,
                    LastName = userData.LastName,
                    AddressId = address.AddressId,
                    Id = -1,
                    CityId = address.District.CityId,
                    City = address.District.City.Name,
                    StreetId = address.StreetId,
                    Street = address.Street.Name,
                    StreetNumber = address.Number,
                    FlatNumber = string.Empty
                };
            }
        }

        public void DeleteDeliveryAddress(int deliveryAddressId)
        {
            using (var rep = _provider.GetRepository<DeliveryAddress>())
            {
                rep.Delete(deliveryAddressId);
            }
        }

        public UserDetails GetUserDetails(Guid userId)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.User>())
            {
                var user = rep.Get(userId);
                return new UserDetails()
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    CreateDate = user.CreateDate.ToString("dd.MM.yyyy")
                };
            }
        }

        public Guid GetUserId(string user)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.User>())
            {
                return rep.GetAll()
                    .Where(x => x.Email == user)
                    .SingleOrDefault()
                    .UserId;
            }
        }

        public Guid CreatePasswordResetRequest(string email)
        {
            Data.Mapping.Entities.User user;
            using (var repU = _provider.GetRepository<Data.Mapping.Entities.User>())
            {
                user = repU.GetAll().Where(x => x.Email == email).SingleOrDefault();
                if (user == null) return Guid.Empty;
            }

            using (var rep = _provider.GetRepository<PasswordResetRequest>())
            {
                var oldRequests = rep.GetAll().Where(x => x.UserId == user.UserId).List();
                foreach (var r in oldRequests)
                {
                    r.IsActive = false;
                    rep.Update(r);
                }

                var request = new PasswordResetRequest()
                {
                    UserId = user.UserId,
                    CreateDate = DateTime.Now,
                    PasswordChanged = false,
                    IsActive = true
                };
                return rep.Create<Guid>(request);
            }
        }

        public IEnumerable<UserOrderItem> GetUserOrderItems(Guid orderId)
        {
            using (var rep = _provider.GetRepository<OrderDish>())
            {
                return rep.GetAll()
                    .Where(x => x.OrderId == orderId)
                    .List()
                    .Select(x => new UserOrderItem()
                    {
                        DishName = x.DishName,
                        Quantity = x.Quantity,
                        Price = x.Price.ToPln(),
                        Total = (x.Quantity * x.Price).ToPln()
                    });
            }
        }

        public IEnumerable<UserOrder> GetUserOrders(Guid userId, int page = 0, int pageSize = 10)
        {
            using (var rep = _provider.StoredProcedure)
            {
                return rep.GetUserOrders(userId, page, pageSize).Map<IEnumerable<UserOrder>>();
            }
        }

        public bool ChangeUserPassword(Guid userId, string oldPassword, string newPassword)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.User>())
            {
                var user = rep.Get(userId);
                var sha = SHA256.Create();
                var oldHash = sha.ComputeHash(Encoding.UTF8.GetBytes(oldPassword));
                var newHash = sha.ComputeHash(Encoding.UTF8.GetBytes(newPassword));
                
                if (!user.Password.SequenceEqual(oldHash)) return false;
                user.Password = newHash;
                rep.Update(user);
                return true;
            }
        }
    }
}
