using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace Organizer.Users.Dto
{
    [AutoMapFrom(typeof(User))]
    public class UserDetailOutput : EntityDto<long>
    {

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string FullName { get; set; }

        public string EmailAddress { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? Birthday { get; set; }

        public string Adress { get; set; }
        
        public DateTime CreationTime { get; set; }

        public DateTime? LastLoginTime { get; set; }


    }
}
