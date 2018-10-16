using System;
using System.ComponentModel.DataAnnotations;

namespace Organizer.Events.Dto
{
    public class RemoveUserFromListInput
    {
        [Required]
        public Guid EventListId { get; set; }

        [Required]
        public long UserId { get; set; }
    }
}
