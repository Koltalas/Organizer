using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace Organizer.Events.Dto
{
    [AutoMapFrom(typeof(Event))]
    public class EventDetailOutput : FullAuditedEntityDto<Guid>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public string Color { get; set; }

        public string BorderColor { get; set; }

        public DateTime End { get; set; }

        public bool AllDay { get; set; }

        public Guid EventListId { get; set; }

    }
}
