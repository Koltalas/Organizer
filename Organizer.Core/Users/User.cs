using System;
using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.Extensions;
using Microsoft.AspNet.Identity;

namespace Organizer.Users
{
    public class User : AbpUser<User>
    {
        public const int MaxAdressLength = 128;
        public const int MaxPhoneNumberLegth = 20;

        [DataType(DataType.Date)]
        public virtual DateTime? Birthday { get; protected set; }

        [StringLength(MaxAdressLength)]
        public virtual string Adress { get; protected set; }


        public const string DefaultPassword = "123qwe";

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress, string password)
        {
            return new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Password = new PasswordHasher().HashPassword(password)
            };
        }


        public void Update(string name=null,string surname=null,string emailAddress=null,string phoneNumber=null, DateTime? birthday=null, string address=null)
        {
            Name = name ?? Name;
            Surname = surname ?? Surname;
            EmailAddress = emailAddress ?? EmailAddress;
            PhoneNumber = phoneNumber ?? PhoneNumber;
            Birthday = birthday ?? Birthday;
            Adress = address ?? Adress;
        }
    }
}