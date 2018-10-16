using System;
using System.ComponentModel.DataAnnotations;
using static Organizer.OrganizerConsts;

namespace Organizer.Events.Dto
{
    public class AddUserToListInput
    {
        [Required]
        public Guid EventListId { get; set; }

        [Required]
        public long UserId { get; set; }

        public SharingType SharingType { get; set; }
    }
}
