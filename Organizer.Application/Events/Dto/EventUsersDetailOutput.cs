using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using static Organizer.OrganizerConsts;

namespace Organizer.Events.Dto
{

    [AutoMapFrom(typeof(EventListUser))]
    public class EventUsersDetailOutput : CreationAuditedEntityDto
    {
        public SharingType SharingType { get; set; }

        public Guid EventListId { get; set; }

        public long UserId { get; set; }

        public string UserName { get; set; }
    }
}
