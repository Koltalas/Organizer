using System;
using System.ComponentModel.DataAnnotations;
using static Organizer.OrganizerConsts;

namespace Organizer.ToDos.Dto
{
    public class AddUserToListInput
    {
        [Required]
        public Guid ToDoListId { get; set; }

        [Required]
        public long UserId { get; set; }

        public SharingType SharingType { get; set; }
    }
}
