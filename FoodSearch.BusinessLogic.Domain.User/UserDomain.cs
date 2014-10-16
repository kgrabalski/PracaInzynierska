using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

using FoodSearch.BusinessLogic.Domain.User.Interface;
using FoodSearch.BusinessLogic.Domain.User.Models;
using FoodSearch.BusinessLogic.Helpers.Email;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Data.Mapping.Interface;
using System;
using System.Collections.Generic;
using System.Linq;




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

        public Guid CreateUser(string firstName, string lastName, string email, string password)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.User>())
            {
                var passHash = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
                return rep.Create<Guid>(new Data.Mapping.Entities.User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
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
                        code.ToString(),
                        string.Format("{0} {1}", user.FirstName, user.LastName)));
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
                        var user = confirm.User;
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
                    .Where(x => x.Email == email)
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
                return rep.Create<int>(new DeliveryAddress()
                {
                    UserId = userId,
                    AddressId = addressId,
                    FlatNumber = flatNumber
                });
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
                    Email = user.Email
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
    }
}
