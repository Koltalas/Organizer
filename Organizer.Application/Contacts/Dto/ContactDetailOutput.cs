using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace Organizer.Contacts.Dto
{
    [AutoMapFrom(typeof(Contact))]
    public class ContactDetailOutput : FullAuditedEntityDto<Guid>
    {

        public string FullName { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? Birthday { get; set; }

        public string Adress { get; set; }

        public long? ProfileId { get; set; }

        public string ProfileUserName { get; set; }

        public bool IsRealUser { get; set; }


        public long UserId { get; set; }

    }
}
