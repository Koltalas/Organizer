using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace Organizer.Notes.Dto
{
    [AutoMapFrom(typeof(Note))]
    public class NoteListDto : FullAuditedEntityDto<Guid>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string HashTag { get; set; }
        
    }
}
