using System;
using System.ComponentModel.DataAnnotations;

namespace Organizer.ToDos.Dto
{
    public class RemoveUserFromListInput
    {
        [Required]
        public Guid ToDoListId { get; set; }

        [Required]
        public long UserId { get; set; }
    }
}
