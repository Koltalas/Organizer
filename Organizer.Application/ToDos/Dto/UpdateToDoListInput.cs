using System;
using System.ComponentModel.DataAnnotations;

namespace Organizer.ToDos.Dto
{
    public class UpdateToDoListInput
    {
        [Required]
        public Guid Id { get; set; }

        [StringLength(ToDoList.MaxTitleLength)]
        public string Title { get; set; } = null;

        [StringLength(ToDoList.MaxDescriptionLength)]
        public string Description { get; set; } = null;

        public long? UserId { get; set; } = null;
    }
}
