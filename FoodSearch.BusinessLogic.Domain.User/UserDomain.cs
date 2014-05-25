using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FoodSearch.BusinessLogic.Domain.User.Interface;
using FoodSearch.BusinessLogic.Domain.User.Models;
using FoodSearch.BusinessLogic.Helpers.Email;
using FoodSearch.Data.Mapping.Entities;
using FoodSearch.Data.Mapping.Interface;

using NHibernate.Util;

namespace FoodSearch.BusinessLogic.Domain.User
{
    public class UserDomain : IUserDomain
    {
        private readonly IRepositoryProvider _provider;

        public UserDomain(IRepositoryProvider provider)
        {
            _provider = provider;
        }

        public bool IsUserNameDuplicated(string userName)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.User>())
            {
                return rep.GetAll()
                    .Where(x => x.UserName == userName)
                    .RowCount() > 0;
            }
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

        public Guid CreateUser(string userName, string firstName, string lastName, string email, byte[] password)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.User>())
            {
                return rep.Create<Guid>(new Data.Mapping.Entities.User()
                {
                    UserName = userName,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = password,
                    CreateDate = DateTime.Now,
                    UserStateId = (int)UserStates.Unconfirmed,
                    UserTypeId = (int)UserTypes.User,
                });
            }
        }

        public void CreateConfirmationEntry(Guid userId, string email, string userName)
        {
            using (var rep = _provider.GetRepository<RegistrationConfirm>())
            {
                Guid code = Guid.NewGuid();
                rep.Create<int>(new RegistrationConfirm()
                {
                    Code = code,
                    Confirmed = false,
                    UserId = userId
                });
                EmailHelper.Send(MailHelperSendFrom.NoReply,
                    new List<string>() { email },
                    "FoodSearch: potwierdzenie rejestracji",
                    EmailBodyHelper.Registration(code.ToString(), userName));
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

        public bool ValidateUser(string userName, byte[] password)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.User>())
            {
                var user = rep.GetAll()
                    .Where(x => x.UserName == userName)
                    .List()
                    .FirstOrDefault();
                return user != null && user.Password.SequenceEqual(password);
            }
        }

        public string GetUserRole(string userName)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.User>())
            {
                var user = rep.GetAll()
                    .Where(x => x.UserName == userName)
                    .List()
                    .FirstOrDefault();
                return user != null ? user.UserType.Name : "";
            }
        }

        public bool ValidateUserRole(string userName, string role)
        {
            using (var rep = _provider.GetRepository<Data.Mapping.Entities.User>())
            {
                var user = rep.GetAll()
                    .Where(x => x.UserName == userName)
                    .List()
                    .FirstOrDefault();
                return user != null && user.UserType.Name == role;
            }
        }
    }
}
