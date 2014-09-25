using FoodSearch.BusinessLogic.Domain.FoodSearch.Interface;
using Ninject;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace FoodSearch.Presentation.Web.Site.Providers
{
    public class FoodSearchMembershipProvider : MembershipProvider
    {
        private readonly IFoodSearchDomain _domain;

        public FoodSearchMembershipProvider()
        {
            _domain = FoodSearch.Service.Api.MvcApplication.DependencyResolver.Get<IFoodSearchDomain>();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public CreateUserResult CreateUser(string firstName, string lastName, string email, string password)
        {
            CreateUserResult result = new CreateUserResult();

            if (string.IsNullOrEmpty(password))
            {
                result.Status = MembershipCreateStatus.InvalidPassword;
                return result;
            }

            if (string.IsNullOrEmpty(email))
            {
                result.Status = MembershipCreateStatus.InvalidEmail;
                return result;
            }

            if (_domain.User.IsEmailDuplicated(email))
            {
                result.Status = MembershipCreateStatus.DuplicateEmail;
                return result;
            }

            Guid userId = _domain.User.CreateUser(firstName, lastName, email, password);
            if (userId != Guid.Empty)
            {
                _domain.User.CreateConfirmationEntry(userId, email);
                result.Status = MembershipCreateStatus.Success;
                result.MembershipUser = new MembershipUser(Membership.Provider.Name, email, null, email, null, null, true, false, DateTime.Now, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
                return result;
            }

            result.Status = MembershipCreateStatus.UserRejected;
            return result;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            SHA256 sha = SHA256.Create();
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return _domain.User.ValidateUser(username, hash);
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName 
        { 
            get { return GetType().Assembly.GetName().Name; }
            set { }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 5; }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 6; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 1; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }
    }
}