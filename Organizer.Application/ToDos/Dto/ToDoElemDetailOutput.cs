using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace Organizer.ToDos.Dto
{
    [AutoMapFrom(typeof(ToDoElem))]
    public class ToDoElemDetailOutput : FullAuditedEntityDto<Guid>
    {
        public string Title { get; set; }

        public bool IsCompleted { get; set; }

        public Guid ToDoListId { get; set; }
    }
}
