using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;

namespace Organizer.ToDos.Dto
{
    [AutoMapFrom(typeof(ToDoList))]
    public class ToDoListDetailOutput : FullAuditedEntityDto<Guid>
    {
        public string Title { get; set; }

        public string Descripion { get; set; }

        public bool IsShared { get; set; }

        public string SharingKey { get; set; }

        public Guid? EventId { get; set; }

        public string EventTitle { get; set; }


        public ICollection<ToDoElemDetailOutput> Elems { get; set; }

        public ICollection<ToDoUsersDetailOutput> ListUsers { get; set; }
    }
}
