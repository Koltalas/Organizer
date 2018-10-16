using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using static Organizer.OrganizerConsts;

namespace Organizer.ToDos.Dto
{
    [AutoMapFrom(typeof(ToDoListUser))]
    public class ToDoUsersDetailOutput : CreationAuditedEntityDto
    {
        public SharingType SharingType { get; set; }

        public Guid ToDoListId { get; set; }

        public long UserId { get; set; }

        public string UserName { get;  set; }
    }
}
