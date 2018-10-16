using System;
using System.ComponentModel.DataAnnotations;

namespace Organizer.ToDos.Dto
{
    public class UpdateToDoElemInput
    {
        [Required]
        public Guid Id { get; set; }

        [StringLength(ToDoElem.MaxTitleLength)]
        public string Title { get; set; } = null;

        public bool? IsCompleted { get; set; } = null;

        public Guid? ListId { get; set; } = null;
    }
}
