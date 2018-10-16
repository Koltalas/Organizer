using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;

namespace Organizer.Events.Dto
{
    [AutoMapFrom(typeof(EventList))]
    public class EventListDetailOutput : FullAuditedEntityDto<Guid>
    {
        public string Title { get; set; }

        public string Descripion { get; set; }

        public string Color { get; set; }

        public bool IsShared { get; set; }

        public string SharingKey { get; set; }

        public bool Owner { get; set; }

        public ICollection<EventDetailOutput> Events { get; set; }

        public ICollection<EventUsersDetailOutput> ListUsers { get; set; }

    }
}
